using Data_Assess_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface IUserAuthentication
    {
        Task<string> AuthenticateAsync(NewLoginDto userLoginDto);
        Task<string> RegisterAsync(UserDto userRegisterDto);
    }
}
