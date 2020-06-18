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
using Microsoft.AspNetCore.Identity;
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
        public IActionResult GetAllCategories()
        {
            var list = _context.Categories.ToList();
            return Ok(list);
        }
           [HttpPost]
        public async Task<IActionResult> GetAllTasksByUser([FromBody] DateDto date)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var list =_context.Tasks.Where(x => x.ApplicationUserId == userId).ToList();
            var listdtos = new List<TaskDto>();
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    if (item.date.ToShortDateString() == date.date.ToShortDateString())
                    {
                        if (item.IsComplited == false)
                        {
                            listdtos.Add(new TaskDto() { ID = item.ID, ApplicationUserId = item.ApplicationUserId, Category = _context.Categories.First(x => x.ID == item.CategoryID).CategoryName, date = item.date, Description = item.Description, Name = item.Name, IsComplited = item.IsComplited });
                        }
                    }
                }

                return Ok(listdtos);
            }
            else
            {
                return (BadRequest());
            }
        }
   
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDto task)
        {
            if (!ModelState.IsValid)
            {
                var errrors = AuthValidator.GetErrorsByModel(ModelState);
                return BadRequest(errrors);
            }
            try
            {

                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                DAL.Entities.Task newtask = new DAL.Entities.Task() { ApplicationUserId= userId, Category = _context.Categories.FirstOrDefault(x => x.CategoryName == task.Category), date = task.date, Description = task.Description, Name = task.Name, IsComplited = task.IsComplited };
                _context.Tasks.Add(newtask);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SetTaskAsComplited([FromBody] IdDto ID)
        {
            try
            {
                _context.Tasks.First(x => x.ID == ID.ID).IsComplited = true;
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