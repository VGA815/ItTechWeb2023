using ItTechServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/roles")]
    [ApiController]
    public class RolesController : Controller
    {
        ApplicationContext db;
        public RolesController(ApplicationContext context)
        {
            db = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetRoles()
        //{
        //    return Json(db.Roles.ToList());
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddRole(string jsonRoleModel)
        //{
        //    foreach (var role in db.Roles.ToList())
        //    {
        //        if (JsonSerializer.Deserialize<RoleModel>(jsonRoleModel).Id == role.Id)
        //        {
        //            return StatusCode(401);
        //        }
        //    }
        //    db.Roles.Add(JsonSerializer.Deserialize<RoleModel>(jsonRoleModel));
        //    await db.SaveChangesAsync();
        //    return StatusCode(201);
        //}

    }
}
