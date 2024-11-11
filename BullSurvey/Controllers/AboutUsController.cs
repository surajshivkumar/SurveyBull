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
                        Bio = "Suraj is a passionate leader with over three years of experience in backend development."
                    },
                    new TeamMember
                    {
                        Name = "Shivani Suryawanshi",
                        PhotoUrl = "/images/shivani.jpeg",
                        Role = "BAIS Student",
                        Bio = "Shivani Suryawanshi has over two years of experience in technology solutions as a Software Engineer."
                    },
                    new TeamMember
                    {
                        Name = "Sharvari More",
                        PhotoUrl = "/images/Sharvari.jpg",
                        Role = "BAIS Student",
                        Bio = "Sharvari has three years of experience in frontend development and customer engagement."
                    },
                     new TeamMember
                    {
                        Name = "Venkata Sai Satvik Reddy Tanuboddi",
                        PhotoUrl = "/images/satvik.jpg",
                        Role = "BAIS Student",
                        Bio = "Satvik is a data-driven professional with two years of experience as a Data Analyst."
                    }
                }
            };

            return View(viewModel);
        }
    }
}
