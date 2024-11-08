using System.Collections.Generic;
using BullSurvey.Models;
using System.Text.Json.Serialization;

namespace BullSurvey
{
    public class Response
    {
        public int ResponseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime SubmittedAt { get; set; }

        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
        public ICollection<Answer> Answers { get; set; }

    }
}