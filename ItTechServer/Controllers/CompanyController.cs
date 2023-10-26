using ItTechServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/company")]
    [ApiController]
    public class CompanyController : Controller
    {
        ApplicationContext db;
        public CompanyController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            return Json(db.Companies.Include(c => c.Projects).ThenInclude(p => p.Targets).ThenInclude(t => t.Goals).ToList());
        }
        //{"Name":"Tom","Password":"tom","Email":"tom@ex.com","Company":{"Name":"MyCompany"}}
        [HttpPost]
        public async Task<IActionResult> AddCompany(string jsonCompanyModel)
        {
            foreach (var company in db.Companies.ToList())
            {
                if (JsonSerializer.Deserialize<CompanyModel>(jsonCompanyModel).Id == company.Id)
                {
                    return StatusCode(401);
                }
            }
            
            db.Companies.Add(JsonSerializer.Deserialize<CompanyModel>(jsonCompanyModel));
            await db.SaveChangesAsync();
            return StatusCode(201);
        }

    }
}
