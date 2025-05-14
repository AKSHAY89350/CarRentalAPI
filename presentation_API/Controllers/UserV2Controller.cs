using Data_Assess_Layer;
using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace presentation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserV2Controller : ControllerBase
    {
        private readonly CarRentalDbContext _context;
        public UserV2Controller(CarRentalDbContext context)
        {
            _context = context;
        }
        [Route("Register")]
        [HttpPost]

        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            if (registerDTO == null)
            {
                return BadRequest("User data is null");
            }
            var existingUser = _context.Users.FirstOrDefault(user=> user.Email == registerDTO.Email);
            if (existingUser != null)
            {
                return Conflict("User with this email already exists");
            }
            try {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
                var newUser = new Users
                {
                    Firstname = registerDTO.Firstname,
                    Lastname = registerDTO.Lastname,
                    Email = registerDTO.Email,
                    Password = hashedPassword
                };
                
                _context.Users.Add(newUser);
                _context.SaveChanges();
               
                var userRole = _context.Roles.FirstOrDefault(role => role.Name == "User");
                if(userRole != null)
                {
                    var newUserRole = new UserRole
                    {
                        UserId = newUser.Id,
                        RoleId = userRole.Id

                    };
                    _context.UserRoles.Add(newUserRole);
                    _context.SaveChanges();
                }
                return CreatedAtAction(nameof(Register), new { email = registerDTO.Email }, registerDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving user data: {ex.Message}");
            }


        }
        [Route("GetProfileDetails")]
        
        [HttpGet]
        public async Task<IActionResult> getProfileByHeader([FromHeader] string token){
            //var email = token;
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest("Token is missing.");
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;
            try
            {
                jwtToken = handler.ReadJwtToken(token);
            } 
            catch
            {
                return BadRequest("Invalid token format.");
            }
            var emailClaim = jwtToken.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Email || c.Type == "email");

            if (emailClaim == null)
                return NotFound("Email claim not found in token.");

            string email = emailClaim.Value;
            var userDetails =await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (userDetails == null)
                return NotFound("User is not found.");

            return Ok(userDetails);

        }
        [Route("GetProfile")]
        
        [HttpGet]
        public async Task<IActionResult> getProfile()
        {

            var emailClaims = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email);
            return Ok(System.Security.Claims.ClaimTypes.Email);
            if(emailClaims == null)
            {
                return Unauthorized("User not found");
            }
            string email = emailClaims.Value;
            var user = await _context.Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                return NotFound("User not found");
            }
            var Profile = new ProfileDTO
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
            return Ok(Profile);
        }
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpadateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request data is incorrect");
            }
            var ClaimsEmails = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email);
            if(ClaimsEmails == null)
            {
                return Unauthorized(new { message = "Invalid token: Email claim missing." });
            }
            string userEmail = ClaimsEmails.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.Firstname))
            {
                user.Firstname = updateProfileDTO.Firstname;
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.Lastname))
            {
                user.Lastname = updateProfileDTO.Lastname;
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.Email))
            {
                var existingEmails = _context.Users.AnyAsync(u => u.Email.ToLower() == updateProfileDTO.Email.ToLower());
                if (existingEmails != null)
                {
                    return Conflict(new { message = "Email is already in use by another account." });
                }
                else
                {
                    user.Email = updateProfileDTO.Email;
                }
            }
            if (!string.IsNullOrEmpty(updateProfileDTO.Password))
            {
                
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(updateProfileDTO.Password);
                user.Password = hashedPassword;
            }
            var updateProfile =  await _context.AddAsync(updateProfileDTO);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
