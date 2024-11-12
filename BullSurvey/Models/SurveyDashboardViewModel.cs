namespace BullSurvey.Models
{
    // Main ViewModel to pass to the view
    public class SurveyDashboardViewModel
    {
        public Survey Survey { get; set; }
        public int TotalResponses { get; set; }
        public double ResponseRate { get; set; }
        public List<QuestionAnalyticViewModel> QuestionAnalytics { get; set; }
        public List<TimeSeriesDataPoint> TimeSeries { get; set; }
    }

    // ViewModel for each question's analytics
    public class QuestionAnalyticViewModel
    {
        public int QuestionId { get; set; }
        public int Count { get; set; }
        public List<AnswerViewModel> Answers { get; set; }  // Added this property to store answers and their counts
    }

    // ViewModel for the answers, including their text and count
    public class AnswerViewModel
    {
        public string TextAnswer { get; set; }  // The text of the answer
        public int Count { get; set; }          // The number of times this answer was selected
    }

    // ViewModel for question options (if needed for displaying options)
    public class QuestionOptionViewModel
    {
        public int? OptionId { get; set; }
        public string TextAnswer { get; set; }
        public int Count { get; set; }
    }

    // ViewModel for time series data (date and count of responses on each day)
    public class TimeSeriesDataPoint
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
