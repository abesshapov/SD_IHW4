using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using SD_IHW4.Models;
using System.Text.RegularExpressions;

namespace SD_IHW4.Controllers
{
    /// <summary>
    /// Users controller.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        [HttpPost("register")]
        public IActionResult Post(string userName, string email, string password, string role) {
            Regex regex = new Regex("^\\S+@\\S+\\.\\S+$");
            String[] validRoles = { "manager", "customer", "chef" };
            if (userName is null || userName.Length == 0) {
                return new BadRequestObjectResult("Invalid username");
            }
            if (email is null || !regex.IsMatch(email)) {
                return new BadRequestObjectResult("Invalid email.");
            }
            if (password is null) {
                return new BadRequestObjectResult("Invalid password.");
            }
            if (role is null || !validRoles.Contains(role)) {
                return new BadRequestObjectResult("Invalid role.");
            }
            if (AuthManagement.CreateUser(userName, email, password, role)) {
                return new OkObjectResult("User created.");
            }
            return new BadRequestObjectResult("Error surfaced while creating a user.");
        }

        [HttpPost("login")]
        public IActionResult Post(String email, String password) {
            if (email is null || password is null) {
                return new BadRequestObjectResult("All fields must be filled.");
            }
            String hash;
            Int64 id;
            (hash, id) = AuthManagement.CheckUser(email);
            if (hash is null) {
                return new BadRequestObjectResult("User is non-existent.");
            }
            if (!HashingManagement.CheckPassword(password, hash)) {
                return new BadRequestObjectResult("Invalid password.");
            }
            var token = JWTGenerator.Generate();
            if (AuthManagement.CreateSession(id, token)) {
                return new OkObjectResult($"Entered the system, token: {token}");
            }
            return new BadRequestObjectResult("Error while entering.");
        }
        
        [HttpGet("user_info/{sessionToken}")]
        public IActionResult Get(String sessionToken) {
            Int64 userId = AuthManagement.CheckSession(sessionToken);
            if (userId < 0) {
                return new BadRequestObjectResult("Session is non-existent.");
            }
            return new OkObjectResult(AuthManagement.GetUserInfo(userId));
        }
    }
}