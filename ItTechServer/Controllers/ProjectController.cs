using ItTechServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/{companyName}/project")]
    [ApiController]
    public class ProjectController : Controller
    {
        ApplicationContext db;
        public ProjectController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(string companyName)
        {
            return Json(db.Companies.Include(c => c.Projects).ThenInclude(p => p.Targets).FirstOrDefault(c => c.Name == companyName).Projects);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(string jsonProjectModel, string companyName)
        {
            var company = db.Companies.Include(c => c.Projects).FirstOrDefault(c => c.Name == companyName);
            foreach (var proj in company.Projects)
            {
                if (JsonSerializer.Deserialize<ProjectModel>(jsonProjectModel).Title == proj.Title)
                {
                    return StatusCode(401);
                }
            }

            company.Projects.Add(JsonSerializer.Deserialize<ProjectModel>(jsonProjectModel));
            await db.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}
