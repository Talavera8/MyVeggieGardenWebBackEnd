using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyVeggieGarden.Models.Account;
using MyVeggieGarden.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVeggieGarden.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        private readonly SignInManager<ApplicationUserIdentity> _signInManager;

        public AccountController(
            ITokenService tokenService,
            UserManager<ApplicationUserIdentity> userManager,
            SignInManager<ApplicationUserIdentity> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUserCreate applicationUserCreate)
        {
            Console.WriteLine("About to start setting values for applicationUserIdentity");

            var applicationUserIdentity = new ApplicationUserIdentity
            {
                Username = applicationUserCreate.Username,
                Email = applicationUserCreate.Email,
                Fullname = applicationUserCreate.Fullname
            };

            Console.WriteLine("applicationUserIdentity defined; About to call userManager.CreateAsync");

            var result = await _userManager.CreateAsync(applicationUserIdentity, applicationUserCreate.Password);

            Console.WriteLine("Just called userManager.CreateAsync; There's a result");

            Console.WriteLine("applicationUserIdentity: " + applicationUserIdentity.ApplicationUserId);

            if (result.Succeeded)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    ApplicationUserId = applicationUserIdentity.ApplicationUserId,
                    Username = applicationUserIdentity.Username,
                    Email = applicationUserIdentity.Email,Fullname = applicationUserIdentity.Fullname,
                    Token = _tokenService.CreateToken(applicationUserIdentity)
                };

                Console.WriteLine("applicationUser created");

                return (applicationUser);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(ApplicationUserLogin applicationUserLogin)
        {
            var applicationUserIdentity = await _userManager.FindByNameAsync(applicationUserLogin.Username);

            if (applicationUserIdentity != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(
                    applicationUserIdentity,
                    applicationUserLogin.Password, false);

                if (result.Succeeded)
                {
                    ApplicationUser applicationUser = new ApplicationUser
                    {
                        ApplicationUserId = applicationUserIdentity.ApplicationUserId,
                        Username = applicationUserIdentity.Username,
                        Email = applicationUserIdentity.Email,
                        Fullname = applicationUserIdentity.Fullname,
                        Token = _tokenService.CreateToken(applicationUserIdentity)
                    };

                    return Ok(applicationUser);
                }
            }

            return BadRequest("Invalid login attempt.");
        }
    }
}
