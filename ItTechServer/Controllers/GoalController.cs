using ItTechServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/{companyName}/{projectName}/{targetName}/goal")]
    [ApiController]
    public class GoalController : Controller
    {
        ApplicationContext db;
        public GoalController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals(string companyName, string projectName, string targetName)
        {
            return Json(db.Companies.Include(c => c.Projects).ThenInclude(p => p.Targets).ThenInclude(t => t.Goals).ThenInclude(g => g.Conditions).FirstOrDefault(c => c.Name == companyName).Projects.FirstOrDefault(p => p.Title == projectName).Targets.FirstOrDefault(t => t.Title == targetName).Goals);
        }

        [HttpPost]
        public async Task<IActionResult> AddGoal(string jsonGoalModel, string companyName, string projectName, string targetName)
        {
            var company = db.Companies.Include(c => c.Projects).ThenInclude(p => p.Targets).ThenInclude(t => t.Goals).FirstOrDefault(c => c.Name == companyName);
            var project = company.Projects.FirstOrDefault(p => p.Title == projectName);
            var target = project.Targets.FirstOrDefault(t => t.Title == targetName);
            foreach (var goal in target.Goals)
            {
                if (goal.Id == JsonSerializer.Deserialize<GoalModel>(jsonGoalModel).Id)
                {
                    return StatusCode(401);
                }
            }
            target.Goals.Add(JsonSerializer.Deserialize<GoalModel>(jsonGoalModel));
            await db.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}
