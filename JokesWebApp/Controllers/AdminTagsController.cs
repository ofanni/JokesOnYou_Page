
using JokesWebApp.Data;
using JokesWebApp.Models.Domain;
using JokesWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JokesWebApp.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public AdminTagsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddJokeRequest addJokeRequest)
        {
            var tag = new Tag
            {
                JokeQuestion = addJokeRequest.JokeQuestion,
                JokeAnswer = addJokeRequest.JokeAnswer
            };
            applicationDbContext.Tags.Add(tag);
            applicationDbContext.SaveChanges();
            return View("Add");
        }
    }
}

