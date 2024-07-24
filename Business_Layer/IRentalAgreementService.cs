using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface IRentalAgreementService
    {
        Task<List<RentalAgreementDto>> GetAllRentalAgreementsAsync();
        // Task<RentalAgreementDto> CreateRentalAgreementAsync(RentalAgreementDto rentalAgreementDTO);
        Task<RentalAgreementDto> CreateRentalAgreementAsync(RentalAgreementDto rentalAgreementDTO);
        Task<List<RentalAgreementDto>> GetRentalAgreementsByUserIdAsync(int userId);
        Task<RentalAgreementDto> GetRentalAgreementById(int rentalAgreementId);


        Task<bool> DeleteRentalAgreementAsync(int id);
        Task<bool> EditRentalAgreementAsync(int id, RentalAgreementDto updatedRentalAgreementDTO);


        Task<List<RentalAgreementDto>> GetRentalAgreementsByVehicle_IdAsync(int vehicleId);

    }

}
