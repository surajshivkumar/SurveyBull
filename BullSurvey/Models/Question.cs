using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BullSurvey.Models
{
public class Question
{
    public int QuestionId { get; set; }
    [Required]
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int SurveyId { get; set; }
    public Survey? Survey { get; set; }  // Nullable to avoid ModelState error
    public ICollection<Option> Options { get; set; } = new List<Option>();
}

}
