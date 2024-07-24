using Data_Assess_Layer;
using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using Microsoft.EntityFrameworkCore;


namespace Business_Layer
{
    public class CarService : ICarService
    {

        private readonly CarRentalDbContext _dbContext;

        public CarService(CarRentalDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<CarDetails> GetAllCarDetails()
        {
            return _dbContext.CarDetails.ToList();
        }

        public CarDetails GetCarDetailsById(int vehicleId)
        {
            return _dbContext.CarDetails.FirstOrDefault(car => car.Vehicle_Id == vehicleId);
        }
        public void AddCarDetails(CarDetails carDetails)
        {
            if (carDetails == null)
            {
                throw new ArgumentNullException(nameof(carDetails));
            }

            _dbContext.CarDetails.Add(carDetails);
            _dbContext.SaveChanges();
        }
        public void UpdateCarDetails(int vehicleId, CarDetails updatedCarDetails)
        {
            if (updatedCarDetails == null)
            {
                throw new ArgumentNullException(nameof(updatedCarDetails));
            }

            var existingCarDetails = _dbContext.CarDetails.FirstOrDefault(car => car.Vehicle_Id == vehicleId);

            if (existingCarDetails != null)
            {
                existingCarDetails.Maker = updatedCarDetails.Maker;
                existingCarDetails.Model = updatedCarDetails.Model;
                existingCarDetails.Rental_Price = updatedCarDetails.Rental_Price;
                existingCarDetails.Availability_status = updatedCarDetails.Availability_status;
                existingCarDetails.Image_Link = updatedCarDetails.Image_Link;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteCarDetails(int vehicleId)
        {
            var carToDelete = _dbContext.CarDetails.FirstOrDefault(car => car.Vehicle_Id == vehicleId);

            if (carToDelete != null)
            {
                _dbContext.CarDetails.Remove(carToDelete);
                _dbContext.SaveChanges();
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<AdminLogin> GetAdminByEmailAsync(string email)
        {
            return await _dbContext.AdminLogins.FirstOrDefaultAsync(a => a.Email == email);
        }

       

        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAdminAsync(AdminLogin admin)
        {
           
            _dbContext.AdminLogins.Add(admin);
            await _dbContext.SaveChangesAsync();
        }

    }



}
