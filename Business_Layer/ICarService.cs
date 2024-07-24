using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface ICarService
    {
        List<CarDetails> GetAllCarDetails();
        CarDetails GetCarDetailsById(int vehicleId);

        void AddCarDetails(CarDetails carDetails);
        void UpdateCarDetails(int vehicleId, CarDetails updatedCarDetails);
        void DeleteCarDetails(int vehicleId);
        Task<User> GetUserByEmailAsync(string email); 
        Task<AdminLogin> GetAdminByEmailAsync(string email);
      
        Task AddUserAsync(User user);
     //   Task AddAdminAsync(AdminLogin admin);
    }

}
