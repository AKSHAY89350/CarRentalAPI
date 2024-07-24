using Data_Assess_Layer;
using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class LoginService : ILoginService
    {
        private readonly ICarService _userRepository;
        private readonly CarRentalDbContext _context; // Replace 'YourDbContext' with your actual DbContext class name.

        public LoginService(ICarService userRepository, CarRentalDbContext context)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _context = context;
        }
        public async Task<User> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return null;
            }
            if (!VerifyPassword(loginDto.Password, user.Password))
            {
                return null;
            }

            if (!string.Equals(loginDto.Role, user.Role, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return user;
        }
        private bool VerifyPassword(string providedPassword, string storedPassword)
        {
            return string.Equals(providedPassword, storedPassword);
        }
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return null;
            }
            var userDto = new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return userDto;
        }
    }
}

