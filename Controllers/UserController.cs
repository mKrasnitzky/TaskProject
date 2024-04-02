using Microsoft.AspNetCore.Mvc;
using TaskProject.Models;
using TaskProject.Interfaces;
using TaskProject.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TaskProject.Controllers{

    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase {

        IUserService UserService;
        int userId;
        
        public UserController(IUserService UserService, IHttpContextAccessor httpcontextAccessor)
        {
            userId = int.Parse(httpcontextAccessor?.HttpContext.User.FindFirst("id")?.Value);
            this.UserService = UserService;
        }

        [HttpGet]
        [Route("GetMyUser")]
        [Authorize(Policy = "User")]
        public ActionResult<User> GetMyUser()
        {
            return UserService.Get(userId);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public ActionResult<List<User>> Get()
        {
            return UserService.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult<User> Get(int id)
        {
            var user = UserService.GetById(id);

            if (user == null)
                return NotFound();
                
            return user;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult Post(User newUser)
        {
            
            var newId = UserService.Add(newUser);

            return CreatedAtAction("Post", new {id = newId}, UserService.GetById(newId));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Put(int id, User newUser)
        {
            var result = UserService.Update(id, newUser);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete(int id)
        {
            var result = UserService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        
    }
}
