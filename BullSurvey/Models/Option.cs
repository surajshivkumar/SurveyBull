using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BullSurvey.Models
{
public class Option
{
    public int OptionId { get; set; }
    public string Text { get; set; }
    public int QuestionId { get; set; }
    public Question? Question { get; set; }  // Nullable to avoid ModelState error
}

}
