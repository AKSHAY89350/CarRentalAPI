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
    public class RentalAgreementService : IRentalAgreementService
    {
        private readonly CarRentalDbContext _context;

        public RentalAgreementService(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<List<RentalAgreementDto>> GetAllRentalAgreementsAsync()
        {
            // Implement logic to retrieve rental agreements from the database and map them to DTOs.
            var rentalAgreements = await _context.RentalAgreement
            .Select(ra => new RentalAgreementDto
            {
                RentalAgreementId = ra.RentalAgreementId,
                UserId = ra.UserId,
                Vehicle_Id = ra.Vehicle_Id,
                BookingDate = ra.BookingDate,
                ReturnDate = ra.ReturnDate,
                IsReturned = ra.IsReturned,
                RequestForReturn = ra.RequestForReturn
            })
            .ToListAsync();

            return rentalAgreements;
        }


        public async Task<List<RentalAgreementDto>> GetRentalAgreementsByUserIdAsync(int userId)
        {
            var rentalAgreements = await _context.RentalAgreement
                .Where(ra => ra.UserId == userId)
                .Select(ra => new RentalAgreementDto
                {
                    RentalAgreementId = ra.RentalAgreementId,
                    UserId = ra.UserId,
                    Vehicle_Id = ra.Vehicle_Id,
                    BookingDate = ra.BookingDate,
                    ReturnDate = ra.ReturnDate,
                    IsReturned = ra.IsReturned,
                    RequestForReturn = ra.RequestForReturn
                })
                .ToListAsync(); 

            return rentalAgreements;
        }
        public async Task<List<RentalAgreementDto>> GetRentalAgreementsByVehicle_IdAsync(int vehicleId)
        {
            var rentalAgreements = await _context.RentalAgreement
                .Where(ra => ra.Vehicle_Id == vehicleId)
                .Select(ra => new RentalAgreementDto
                {
                    RentalAgreementId = ra.RentalAgreementId,
                    UserId = ra.UserId,
                    Vehicle_Id = ra.Vehicle_Id,
                    BookingDate = ra.BookingDate,
                    ReturnDate = ra.ReturnDate,
                    IsReturned = ra.IsReturned,
                    RequestForReturn = ra.RequestForReturn
                })
                .ToListAsync();

            return rentalAgreements;
        }

        public async Task<RentalAgreementDto> GetRentalAgreementById(int rentalAgreementId)
        {
            var rentalAgreement = await _context.RentalAgreement
                .Where(ra => ra.RentalAgreementId == rentalAgreementId)
                .Select(ra => new RentalAgreementDto
                {
                    RentalAgreementId = ra.RentalAgreementId,
                    UserId = ra.UserId,
                    Vehicle_Id = ra.Vehicle_Id,
                    BookingDate = ra.BookingDate,
                    ReturnDate = ra.ReturnDate,
                    IsReturned = ra.IsReturned,
                    RequestForReturn = ra.RequestForReturn
                })
                .FirstOrDefaultAsync();

            return rentalAgreement;
        }


        public async Task<bool> DeleteRentalAgreementAsync(int id)
        {
            var rentalAgreement = await _context.RentalAgreement.FindAsync(id);

            if (rentalAgreement == null)
            {
                return false;
            }

            _context.RentalAgreement.Remove(rentalAgreement);
            await _context.SaveChangesAsync();

            return true; 
        }
        public async Task<bool> EditRentalAgreementAsync(int id, RentalAgreementDto updatedRentalAgreementDTO)
        {
            var rentalAgreement = await _context.RentalAgreement.FindAsync(id);

            if (rentalAgreement == null)
            {
                return false; 
            }

            // Update the rental agreement properties with values from the DTO.
            rentalAgreement.UserId = updatedRentalAgreementDTO.UserId;
            rentalAgreement.Vehicle_Id = updatedRentalAgreementDTO.Vehicle_Id;
            rentalAgreement.BookingDate = updatedRentalAgreementDTO.BookingDate;
            rentalAgreement.ReturnDate = updatedRentalAgreementDTO.ReturnDate;
            rentalAgreement.IsReturned = updatedRentalAgreementDTO.IsReturned;
            rentalAgreement.RequestForReturn = updatedRentalAgreementDTO.RequestForReturn;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<RentalAgreementDto> CreateRentalAgreementAsync(RentalAgreementDto rentalAgreementDTO)
        {
            var newRentalAgreement = new RentalAgreement
            {
                UserId = rentalAgreementDTO.UserId,
                Vehicle_Id = rentalAgreementDTO.Vehicle_Id,
                BookingDate = rentalAgreementDTO.BookingDate,
                ReturnDate = rentalAgreementDTO.ReturnDate,
                IsReturned = rentalAgreementDTO.IsReturned,
                RequestForReturn = rentalAgreementDTO.RequestForReturn
            };

            _context.RentalAgreement.Add(newRentalAgreement);
            await _context.SaveChangesAsync();

            rentalAgreementDTO.RentalAgreementId = newRentalAgreement.RentalAgreementId;
            return rentalAgreementDTO;
        }


        

    }

}

