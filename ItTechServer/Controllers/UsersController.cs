﻿using ItTechServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        ApplicationContext db;
        public UsersController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string jsonModel)
        {
            foreach (var user in db.Users.ToList())
            {
                if (JsonSerializer.Deserialize<UserModel>(jsonModel).Id == user.Id)
                {
                    return StatusCode(401);
                }
            }
            db.Users.Add(JsonSerializer.Deserialize<UserModel>(jsonModel));
            await db.SaveChangesAsync();
            return StatusCode(201);
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Json(db.Users.ToList());
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id != null)
            {
                UserModel? user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(string jsonModel)
        {
            db.Users.Update(JsonSerializer.Deserialize<UserModel>(jsonModel));
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
