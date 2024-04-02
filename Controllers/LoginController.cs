using Microsoft.AspNetCore.Mvc;
using TaskProject.Interfaces;
using TaskProject.Models;
using TaskProject.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace TaskProject.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        ILoginService LoginService ;

        public LoginController(ILoginService loginService)
        {
            this.LoginService = loginService;
        }

        [HttpPost]
        public ActionResult<String> Login([FromBody] User user)
        {
            if (!LoginService.CheckLogin(user))
                return Unauthorized();

            var claims = new List<Claim> { };

            if (LoginService.CheckAdmin(user))
                claims.Add(new Claim("type", "Admin"));

            claims.Add(new Claim("type", "User"));
            claims.Add(new Claim("id", user.id.ToString()));

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }
    }
}