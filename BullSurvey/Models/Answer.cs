using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BullSurvey.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int ResponseId { get; set; }
         [JsonIgnore]
        public Response Response { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int? OptionId { get; set; }
        public Option Option { get; set; }

        public string TextAnswer { get; set; }
    }
}