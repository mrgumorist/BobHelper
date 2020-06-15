using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using CasualHub.DAL.Entities;
using CasualHub.UI.Helpers;
using CasualHub.UI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasualHub.UI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    public class TasksController : Controller
    {
        ApplicationDbContext _context;
        public TasksController(ApplicationDbContext db)
        {
            _context = db;
        }
        [HttpGet]
        public IActionResult GetAllTasksByUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var list =_context.Tasks.Where(x => x.ApplicationUser == _context.Users.First(x => x.Id == userId)).ToList();
            var listdtos = new List<TaskDto>();
            foreach (var item in list)
            {
                listdtos.Add(new TaskDto() {ID=item.ID, ApplicationUser = item.ApplicationUser, Category = item.Category, date = item.date, Description = item.Description, Name = item.Name, IsComplited=item.IsComplited });
            }
            return Ok(listdtos);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDto task)
        {
            if (!ModelState.IsValid)
            {
                var errrors = AuthValidator.GetErrorsByModel(ModelState);
                return BadRequest(errrors);
            }
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DAL.Entities.Task newtask = new DAL.Entities.Task() { ApplicationUser = _context.Users.First(x => x.Id == userId), Category=task.Category, date=task.date, Description=task.Description, Name=task.Name, IsComplited=task.IsComplited};
            _context.Add(newtask);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> SetTaskAsComplited([FromBody] int ID)
        {
            try
            {
                _context.Tasks.First(x => x.ID == ID).IsComplited = true;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}