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
                        Role = "BAIS Student",
                        Bio = "Suraj is a passionate leader with over 3 years of experience in backend development."
                    },
                    new TeamMember
                    {
                        Name = "Shivani Suryawanshi",
                        PhotoUrl = "/images/shivani.jpeg",
                        Role = "BAIS Student",
                        Bio = "Shivani is an expert in technology solutions, with a background in software engineering."
                    },
                    new TeamMember
                    {
                        Name = "Sharvari More",
                        PhotoUrl = "/images/Sharvari.jpg",
                        Role = "BAIS Student",
                        Bio = "Sharvari has 3 years of experience in frontend development and customer engagement."
                    },
                     new TeamMember
                    {
                        Name = "Venkata Sai Satvik Reddy Tanuboddi",
                        PhotoUrl = "/images/satvik.jpg",
                        Role = "BAIS Student",
                        Bio = "Satvik has a decade of experience in business analytics and data insights."
                    }
                }
            };

            return View(viewModel);
        }
    }
}
