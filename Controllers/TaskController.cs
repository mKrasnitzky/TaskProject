using Microsoft.AspNetCore.Mvc;
using TaskProject.Services;
using System.Collections.Generic;
using TaskProject.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TaskProject.Controllers
{

    using TaskProject.Models;
    [ApiController]
    [Route("[controller]")]
    
    public class TaskController : ControllerBase
    {
        ITaskService TaskService;
        public int userId;

        public TaskController(ITaskService TaskService,IHttpContextAccessor httpcontextAccessor)
        {
            System.Console.WriteLine("after");
            userId = int.Parse(httpcontextAccessor.HttpContext?.User.FindFirst("id")?.Value);
            System.Console.WriteLine(userId);

            this.TaskService = TaskService;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<List<Task>> Get()
        {
            return TaskService.GetAll(userId);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Task> Get(int id)
        {
            var task = TaskService.GetById(id, userId);
            if (task == null)
                return NotFound();
            return task;
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public ActionResult Post(Task newTask)
        {
            System.Console.WriteLine("fgfg");
            var newId = TaskService.Add(newTask, userId);

            return CreatedAtAction("Post", new {id = newId}, TaskService.GetById(newId, userId));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Put(int id, Task newTask)
        {
            var result = TaskService.Update(id, newTask, userId);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Delete(int id)
        {
            var result = TaskService.Delete(id, userId);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }

}