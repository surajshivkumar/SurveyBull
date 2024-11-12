// Models/AboutUsViewModel.cs
namespace BullSurvey.Models
{
    public class AboutUsViewModel
    {
        public string WebsiteName { get; set; }
        public string Description { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }

    public class TeamMember
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }
    }
}
