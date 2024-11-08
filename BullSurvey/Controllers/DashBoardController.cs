using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BullSurvey.Models; // Adjust based on your namespace
using System.Linq;
using System.Threading.Tasks;

public class DashBoardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashBoardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Dashboard/{id}
    public async Task<IActionResult> Index()
    {
        var surveys = await _context.Surveys.ToListAsync();
        return View(surveys); // Ensure you're returning the correct model
    }
// GET: Dashboard/{id}
    public async Task<IActionResult> Dashboard(int id)
    {
        var survey = await _context.Surveys
            .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.SurveyId == id);

        if (survey == null)
        {
            return NotFound();
        }

        // Prepare analytics data
        var totalResponses = await _context.Responses.CountAsync(r => r.SurveyId == id);
        var questionAnalytics = await _context.Responses
            .Where(r => r.SurveyId == id)
            .SelectMany(r => r.Answers)
            .GroupBy(a => a.QuestionId)
            .Select(g => new
            {
                QuestionId = g.Key,
                Count = g.Count(),
                Options = g.Select(a => new { a.OptionId, a.TextAnswer }).ToList()
            })
            .ToListAsync();

        // Prepare a time series for responses over time
        var timeSeries = await _context.Responses
            .Where(r => r.SurveyId == id)
            .GroupBy(r => r.SubmittedAt.Date)
            .Select(g => new
            {
                Date = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        // Pass the data to the view
        var analyticsData = new
        {
            Survey = survey,
            TotalResponses = totalResponses,
            QuestionAnalytics = questionAnalytics,
            TimeSeries = timeSeries
        };
        var viewModel = new SurveyDashboardViewModel
    {
        Survey = survey, // Your survey object
        TotalResponses = totalResponses,
        ResponseRate = 100, // Implement this method based on your logic
        QuestionAnalytics = questionAnalytics.Select(qa => new QuestionAnalyticViewModel
        {
            QuestionId = qa.QuestionId,
            Count = qa.Count,
            Options = qa.Options.Select(o => new QuestionOptionViewModel
            {
                OptionId = o.OptionId,
                TextAnswer = o.TextAnswer,
                Count = _context.Answers.Count(a => a.QuestionId == qa.QuestionId && a.OptionId == o.OptionId)  // Make sure to include the count for each option
            }).ToList()
        }).ToList(),
        TimeSeries = timeSeries.Select(ts => new TimeSeriesDataPoint
        {
            Date = ts.Date,
            Count = ts.Count
        }).ToList()
    };


        return View(viewModel);
    }
}
