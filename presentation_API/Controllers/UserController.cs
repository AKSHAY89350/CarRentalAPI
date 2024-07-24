using Business_Layer;
using Data_Assess_Layer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace presentation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
            private readonly IUserAuthentication _userAuthentication;

            public UserController(IUserAuthentication userAuthentication)
            {
                _userAuthentication = userAuthentication;
            }

            [HttpPost("LoginUser")]
            public async Task<IActionResult> Login([FromBody] NewLoginDto userLoginDto)
            {
                var token = await _userAuthentication.AuthenticateAsync(userLoginDto);

                if (token == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                return Ok(new { Token = token });
            }

            [HttpPost("RegisterUser")]
            public async Task<IActionResult> Register([FromBody] UserDto userRegisterDto)
            {
                var token = await _userAuthentication.RegisterAsync(userRegisterDto);

                if (token == null)
                {
                    return BadRequest("User registration failed.");
                }

                return Ok(new { Token = token });
            }


        }
    

}

