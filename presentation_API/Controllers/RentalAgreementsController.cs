using Business_Layer;
using Data_Assess_Layer;
using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace presentation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalAgreementsController : ControllerBase
    {
        private readonly IRentalAgreementService _service;
        private readonly CarRentalDbContext _context;

        public RentalAgreementsController(IRentalAgreementService service, CarRentalDbContext context)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<RentalAgreementDto>>> GetRentalAgreements()
        {
            try
            {
                var rentalAgreements = await _service.GetAllRentalAgreementsAsync();
                return Ok(rentalAgreements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<List<RentalAgreementDto>>> GetRentalAgreementsByUserId(int id)
        {
            try
            {
                var rentalAgreements = await _service.GetRentalAgreementsByUserIdAsync(id);

                if (rentalAgreements == null || rentalAgreements.Count == 0)
                {
                    return NotFound();
                }

                return Ok(rentalAgreements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{rentalAgreementId}")]
        public async Task<ActionResult<RentalAgreementDto>> GetRentalAgreementById(int rentalAgreementId)
        {
            try
            {
                var rentalAgreement = await _service.GetRentalAgreementById(rentalAgreementId);

                if (rentalAgreement == null)
                {
                    return NotFound();
                }

                return Ok(rentalAgreement);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpGet("byVehicleId/{vehicleId}")]
        public async Task<ActionResult<List<RentalAgreementDto>>> GetRentalAgreementsByVehicle_IdAsync(int vehicleId)
        {
            try
            {
                var rentalAgreements = await _service.GetRentalAgreementsByVehicle_IdAsync(vehicleId);
                return Ok(rentalAgreements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCarRentalAgreement(int id)
        {
            try
            {
                var deleted = await _service.DeleteRentalAgreementAsync(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCarRentalAgreement(int id, RentalAgreementDto updatedRentalAgreementDTO)
        {
            try
            {
                var edited = await _service.EditRentalAgreementAsync(id, updatedRentalAgreementDTO);

                if (!edited)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<RentalAgreementDto>> CreateRentalAgreementAsync(RentalAgreementDto rentalAgreementDTO)
        {
            try
            {
                var createdRentalAgreement = await _service.CreateRentalAgreementAsync(rentalAgreementDTO);

                if (createdRentalAgreement == null)
                {
                    return BadRequest();
                }

                return Ok(createdRentalAgreement);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

       }
}
