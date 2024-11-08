using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BullSurvey.Models; // Adjust based on your namespace
using System.Threading.Tasks;
using System.Linq;

namespace BullSurvey.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveysController(ApplicationDbContext context)
        {
            _context = context;
        }
       [HttpGet("api/responses/json/{id}")]
public async Task<IActionResult> GetResponseJson(int id)
{
    Console.WriteLine($"Entering GetResponseJson with id: {id}"); // Debugging print statement

    var response = await _context.Responses
        .Include(r => r.Answers)
            .ThenInclude(a => a.Question)
        .FirstOrDefaultAsync(r => r.ResponseId == id);

    if (response == null)
    {
        Console.WriteLine("Response not found"); // Debugging print statement
        return NotFound(new { message = "Response not found" });
    }

    // Map to a DTO or anonymous object for better structure and serialization
    var responseData = new
    {
        response.ResponseId,
        response.Name,
        response.Email,
        Answers = response.Answers.Select(a => new
        {
            a.QuestionId,
            a.OptionId,
            a.TextAnswer
        })
    };

    Console.WriteLine("Response found, returning data"); // Debugging print statement
    return Ok(responseData); // Return the JSON data
}
        // GET: api/surveys/json/{id} - Returns survey data as JSON



        [HttpGet("api/surveys/json/{id}")]
        public async Task<IActionResult> GetSurveyJson(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            
            if (survey == null) return NotFound(new { message = "Survey not found" });

            // Map to a DTO or anonymous object for better structure and serialization
            var surveyData = new
            {
                survey.SurveyId,
                survey.Title,
                survey.Description,
                Questions = survey.Questions.Select(q => new
                {
                    q.QuestionId,
                    q.Text,
                    q.Type,
                    Options = q.Options.Select(o => new
                    {
                        o.OptionId,
                        o.Text
                    })
                })
            };

            return Ok(surveyData); // Return the JSON data
        }


        // POST: api/surveys/submit
        [HttpPost("api/surveys/submit")]
public async Task<IActionResult> SubmitResponse([FromBody] SurveyResponseDto responseDto)
{
    if (responseDto == null || !ModelState.IsValid)
    {
        // Log ModelState errors for debugging
        foreach (var modelState in ModelState.Values)
        {
            foreach (var error in modelState.Errors)
            {
                Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
            }
        }
        return BadRequest(ModelState); // Check for model state errors
    }

    // Create a new Response entity
    var response = new Response
    {
        SurveyId = responseDto.SurveyId,
        Name = responseDto.Name,
        Email = responseDto.Email,
        SubmittedAt = DateTime.UtcNow,
        Answers = new List<Answer>()
    };

    foreach (var answerDto in responseDto.Answers)
    {
        var answer = new Answer
        {
            QuestionId = answerDto.QuestionId,
            OptionId = answerDto.OptionId,
            TextAnswer = answerDto.TextAnswer,
            Response = response
        };
        response.Answers.Add(answer);
    }

    _context.Responses.Add(response);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetResponseJson), new { id = response.ResponseId }, response);
}



        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Surveys.ToListAsync());
        }

        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
{
    if (id == null) return NotFound();

    var survey = await _context.Surveys
        .Include(s => s.Questions)                 // Include Questions
            .ThenInclude(q => q.Options)           // Include Options for each Question
        .FirstOrDefaultAsync(m => m.SurveyId == id);

    if (survey == null) return NotFound();

    return View(survey);
}

        // GET: Surveys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
 
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Survey survey)
{
    if (ModelState.IsValid)
    {
        // Set the Survey reference on each Question and Option before saving
        foreach (var question in survey.Questions)
        {
            question.SurveyId = survey.SurveyId;
            foreach (var option in question.Options)
            {
                option.QuestionId = question.QuestionId;
            }
        }

        _context.Add(survey);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Log ModelState errors for debugging
    foreach (var modelState in ModelState.Values)
    {
        foreach (var error in modelState.Errors)
        {
            Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
        }
    }

    return View(survey);
}


[HttpPost]
public async Task<IActionResult> PublishSurvey(int surveyId)
{
    var survey = await _context.Surveys.FindAsync(surveyId);
    if (survey == null) return NotFound();

    survey.IsPublished = true;
    _context.Update(survey);
    await _context.SaveChangesAsync();

    return RedirectToAction("Details", new { id = surveyId });
}

public async Task<IActionResult> Publish(int id)
{
    var survey = await _context.Surveys
        .Include(s => s.Questions)
        .ThenInclude(q => q.Options)
        .FirstOrDefaultAsync(s => s.SurveyId == id);

    if (survey == null)
    {
        return NotFound();
    }

    return View(survey); // Pass a single Survey, not a List<Survey>
}
      
        // GET: Surveys/Edit/5
public async Task<IActionResult> Edit(int? id)
{
    if (id == null) return NotFound();

    var survey = await _context.Surveys
        .Include(s => s.Questions)        // Include questions
        .ThenInclude(q => q.Options)      // Include options for each question
        .FirstOrDefaultAsync(m => m.SurveyId == id);

    if (survey == null) return NotFound();

    return View(survey);
}

        // POST: Surveys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,Title,Description")] Survey survey)
        {
            if (id != survey.SurveyId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.SurveyId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var survey = await _context.Surveys
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null) return NotFound();

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public class SurveyResponseDto
        {
            public int SurveyId { get; set; } 
            public string Name { get; set; } 
            public string Email { get; set; } 
            public List<AnswerDto> Answers { get; set; } 
        }

        public class AnswerDto
        {
            public int QuestionId { get; set; } 
            public int? OptionId { get; set; } 
            public string TextAnswer { get; set; } 
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.SurveyId == id);
        }
    }
}
