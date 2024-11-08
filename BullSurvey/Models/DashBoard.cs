namespace BullSurvey.Models
{
public class DashboardViewModel
{
    public int TotalSubmissions { get; set; }
    public double ResponseRate { get; set; }
    public double AverageResponseTime { get; set; }
    public List<string> QuestionLabels { get; set; }
    public List<int> QuestionResponses { get; set; }
    public List<string> TimeLabels { get; set; }
    public List<int> TimeSeriesData { get; set; }
}
}