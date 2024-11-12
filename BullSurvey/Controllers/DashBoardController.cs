using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BullSurvey.Models;
using System.Linq;
using System.Threading.Tasks;

[Route("Dashboard")]
public class DashBoardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashBoardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Dashboard (Lists all surveys)
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var surveys = await _context.Surveys.ToListAsync();
        return View(surveys);
    }

    // GET: /Dashboard/Dashboard/{id} (Displays the specific dashboard for a survey)
    [HttpGet("Dashboard/{id}")]
    public async Task<IActionResult> Dashboard(int id)
    {
        var survey = await _context.Surveys
            .Include(s => s.Questions) // Include related questions
            .ThenInclude(q => q.Options) // Include options for each question
            .FirstOrDefaultAsync(s => s.SurveyId == id);

        if (survey == null)
        {
            return NotFound(); // Return 404 if survey not found
        }

        // Prepare the time series data for responses over time
        var timeSeries = await _context.Responses
            .Where(r => r.SurveyId == id)
            .GroupBy(r => r.SubmittedAt.Date) // Group responses by the date part of SubmittedAt
            .Select(g => new
            {
                Date = g.Key,   // Get the date
                Count = g.Count() // Count how many responses for each date
            })
            .OrderBy(ts => ts.Date)  // Order by Date ascending
            .ToListAsync();

        // Prepare question analytics (answers distribution for each question)
        var questionAnalytics = await _context.Answers
            .Where(a => a.Question.SurveyId == id)
            .GroupBy(a => new { a.QuestionId, a.TextAnswer })
            .Select(g => new
            {
                QuestionId = g.Key.QuestionId,
                TextAnswer = g.Key.TextAnswer,
                Count = g.Count()
            })
            .ToListAsync();

        // Create the ViewModel for passing data to the view
        var viewModel = new SurveyDashboardViewModel
        {
            Survey = survey,
            TotalResponses = await _context.Responses.CountAsync(r => r.SurveyId == id),
            ResponseRate = 100, // Modify based on your logic to calculate response rate
            TimeSeries = timeSeries.Select(ts => new TimeSeriesDataPoint
            {
                Date = ts.Date,
                Count = ts.Count
            }).ToList(),
            QuestionAnalytics = questionAnalytics.GroupBy(q => q.QuestionId).Select(group => new QuestionAnalyticViewModel
            {
                QuestionId = group.Key,
                Answers = group.Select(g => new AnswerViewModel
                {
                    TextAnswer = g.TextAnswer,
                    Count = g.Count
                }).ToList()
            }).ToList()
        };

        return View(viewModel);
    }
}
