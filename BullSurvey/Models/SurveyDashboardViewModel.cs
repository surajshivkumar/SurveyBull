namespace BullSurvey.Models;
// Models/ViewModels/SurveyDashboardViewModel.cs
public class SurveyDashboardViewModel
{
    public Survey Survey { get; set; }
    public int TotalResponses { get; set; }
    public double ResponseRate { get; set; }
    public List<QuestionAnalyticViewModel> QuestionAnalytics { get; set; }
    public List<TimeSeriesDataPoint> TimeSeries { get; set; }
}

public class QuestionAnalyticViewModel
{
    public int QuestionId { get; set; }
    public int Count { get; set; }
    public List<QuestionOptionViewModel> Options { get; set; }
}

public class QuestionOptionViewModel
{
    public int? OptionId { get; set; }
    public string TextAnswer { get; set; }
    public int Count { get; set; }
}

public class TimeSeriesDataPoint
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
}