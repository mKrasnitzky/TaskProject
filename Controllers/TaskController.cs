using Microsoft.AspNetCore.Mvc;
using MyList.Services;
using System.Collections.Generic;

namespace MyList.Controllers
{
    using MyList.Models;


    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Task>> Get()
        {
            return TaskService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var task = TaskService.GetById(id);
            if (task == null)
                return NotFound();
            return task;
        }

        [HttpPost]
        public ActionResult Post(Task newTask)
        {
            var newId = TaskService.Add(newTask);

            return CreatedAtAction("Post", new {id = newId}, TaskService.GetById(newId));
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task newTask)
        {
            var result = TaskService.Updata(id, newTask);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }

}