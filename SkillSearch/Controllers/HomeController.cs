using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkillSearch.Models;
using SkillSearch.Repository;
using Index = SkillSearch.Repository.Index;

namespace SkillSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private          ElsRepo                 _repo;

        public HomeController(ILogger<HomeController> logger, ElsRepo repo)
        {
            _repo   = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSkills(string input)
        {
            var smart = _repo.GetSkills(input, Repository.Index.Smart);
            var dedup = _repo.GetSkills(input, Repository.Index.Dedup);
            var wiki = _repo.GetSkills(input, Repository.Index.Wiki);
            return Json(new
            {
                Smart = smart,
                Dedup = dedup,
                Wiki  = wiki,
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}