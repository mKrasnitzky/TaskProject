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
        int userId;

        public LoginController(ILoginService loginService)
        {
            this.LoginService = loginService;
        }

        [HttpPost]
        public ActionResult<String> Login([FromBody] User user)
        {
            System.Console.WriteLine(user.name);
            bool u=LoginService.CheckLogin(user);
            // System.Console.WriteLine(u.name);
            if (u==false)
              {  
                System.Console.WriteLine("in if");
                return Unauthorized();
}
            var claims = new List<Claim> { };

            if (LoginService.CheckAdmin(user))
                claims.Add(new Claim("type", "Admin"));

            claims.Add(new Claim("type", "User"));
            claims.Add(new Claim("id", user.id.ToString()));
            // var claims = new List<Claim>
            // {
            //     check(User.key);
            // };
            foreach( Claim c in claims)
            Console.WriteLine(c);

            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }
    }
}