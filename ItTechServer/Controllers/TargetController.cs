using ItTechServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/{companyName}/{projectName}/target")]
    [ApiController]
    public class TargetController : Controller
    {
        ApplicationContext db;
        public TargetController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTargets(string companyName,string projectName)
        {
            return Json(db.Companies.Include(c => c.Projects).ThenInclude(p => p.Targets).ThenInclude(t => t.Goals).FirstOrDefault(c => c.Name == companyName).Projects.FirstOrDefault(p => p.Title == projectName).Targets);
        }

        [HttpPost]
        public async Task<IActionResult> AddTarget(string jsonTargetModel, string companyName, string projectName)
        {
            var company = db.Companies.Include(c => c.Projects).FirstOrDefault(c => c.Name == companyName);
            var project = company.Projects.FirstOrDefault(p => p.Title == projectName);
            foreach (var target in project.Targets)
            {
                if (target.Id == JsonSerializer.Deserialize<TargetModel>(jsonTargetModel).Id)
                {
                    return StatusCode(401);
                }
            }
            project.Targets.Add(JsonSerializer.Deserialize<TargetModel>(jsonTargetModel));
            await db.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}
