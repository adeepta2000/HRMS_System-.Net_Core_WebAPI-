using HRMSDevelopment.Common;
using HRMSDevelopment.Model;
using HRMSDevelopment.Model.DBEntity;
using HRMSDevelopment.Model.DBEntity.ViewModel;
using HRMSDevelopment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMSDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBaseService<Employee> _employeeService;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IBaseService<Employee> employeeService, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _employeeService = employeeService;
            _configuration = configuration;

        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(CreatedResult), 201)]
        [Route("registration")]
        public async Task<IActionResult> EmployeeRegistration([FromBody] EmployeeViewModel model, string role)
        {
            var response = new PayloadResponse<Employee>
            {
                success = false,
                message = new List<string>() { "" },
                payload = null,
                operation_type = PayloadType.Save
            };

            IdentityUser user = new()
            {
                Email = model.email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.username
            };

            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, model.password);

                if (!result.Succeeded)
                {
                    return BadRequest("Failed to register user.");
                }

                await _userManager.AddToRoleAsync(user, role);

                var newUser = new Employee()
                {
                    Name = model.name,
                    Email = model.email,
                    MobileNumber = model.mobile_number,
                    Position = model.position,
                    Department = model.department,
                    AccountNumber = model.account_number,
                    EmploymentStatus = model.employment_status,
                    Username = model.username,
                    Password = model.password
                };

                OperationResult createResult = await _employeeService.Add(newUser);

                if (createResult.Success)
                {
                    response.success = true;
                    response.message = new List<string>() { "User Registered Successfully" };
                    response.payload = createResult.Result;
                }

                return Ok(response);
            }

            return StatusCode(500, "This role isn't exist");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> EmployeeLogin([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if(user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>

                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRole = await _userManager.GetRolesAsync(user);

                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authicateToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(authicateToken)
                });
            }

            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT: ValidIssuer"],
                audience: _configuration["JWT: ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

    }
}
