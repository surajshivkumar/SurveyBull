using Microsoft.AspNetCore.Mvc;
using BullSurvey.Models;

namespace BullSurvey.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new AboutUsViewModel
            {
                WebsiteName = "SurveyBull",
                Description = "SurveyBull is a leading platform for creating and managing surveys.",
                TeamMembers = new List<TeamMember>
                {
                    new TeamMember
                    {
                        Name = "Suraj Shiva Kumar",
                        PhotoUrl = "/images/suraj.jpg",
                        Role = "Backend Developer",
                        Bio = "Suraj developed the backend architecture, including setting up the database and integrating APIs."
                    },
                    new TeamMember
                    {
                        Name = "Shivani Suryawanshi",
                        PhotoUrl = "/images/shivani.jpeg",
                        Role = "Frontend Developer",
                        Bio = "Shivani led frontend development, designing UI and layout for survey forms and dashboard."
                    },
                    new TeamMember
                    {
                        Name = "Sharvari More",
                        PhotoUrl = "/images/Sharvari.jpg",
                        Role = "Software Engineer",
                        Bio = "Sharvari handled the integration of survey logic, and worked on form validations and session management."
                    },
                     new TeamMember
                    {
                        Name = "Venkata Sai Satvik Reddy Tanuboddi",
                        PhotoUrl = "/images/satvik.jpg",
                        Role = "Data Analyst",
                        Bio = "Satvik contributed to data analysis by parsing survey data and deriving insights to enhance survey process."
                    }
                }
            };

            return View(viewModel);
        }
    }
}
