using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BullSurvey.Models
{
   public class Survey
{
    public int SurveyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPublished { get; set; }
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}

}
