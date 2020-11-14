using Mysql_blazor2.Server;
using Mysql_blazor2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly AppDbContext db;

    public UserController(AppDbContext db)
    {
        this.db = db;
    }


    [HttpPost]
    [Route("LoginUser")]
      public async Task<ActionResult> Post([FromBody] User user)
      {
          User loggedUser = await db.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();

          if(loggedUser != null)
          {
              var claim = new Claim(ClaimTypes.Name, loggedUser.Email);
              var claimIdentity = new ClaimsIdentity(new[] {claim}, "serverAuth");
              var claimPrincipal = new ClaimsPrincipal(claimIdentity);
              await HttpContext.SignInAsync(claimPrincipal);
          }

          return Ok(loggedUser);
      }

    
    [HttpGet]
    [Route("GetUser")]

    public async Task<ActionResult<User>> GetCurrentUser()
    {
        User currentUser = new User();

        if(User.Identity.IsAuthenticated)
        {
            currentUser.Email = User.FindFirstValue(ClaimTypes.Name);
        }


        return await Task.FromResult(currentUser);
    }



    [HttpGet]
    [Route("LogOut")]
    public async Task<ActionResult<String>> LogOut()
    {
        await HttpContext.SignOutAsync();
        return "Success";
    }


}