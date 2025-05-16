using Business_Layer.Interface;
using Data_Assess_Layer;
using Data_Assess_Layer.DTO;
using Data_Assess_Layer.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace presentation_API.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;
        public CarController(ICarService carDetailsService, ILogger<CarController> logger)
        {
            _carService = carDetailsService ?? throw new ArgumentNullException(nameof(carDetailsService));
            _logger = logger;
        }


        

        [HttpGet("log-example")]
        public IActionResult Get()
        {
            _logger.LogInformation("This is an informational log.");
            _logger.LogWarning("This is a warning log.");
            _logger.LogError("This is an error log.");

            return Ok("Check the console for log output.");
        }

        [HttpGet("GetAllCarDetails")]
        public ActionResult<IEnumerable<CarDetails>> GetAllCarDetails()
        {
            try
            {
                var carDetails = _carService.GetAllCarDetails();
                return Ok(carDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("GetCarDetailsById/{id}")]
        public ActionResult<CarDetails> GetCarDetailsById(int id)
        {
            try
            {
                var carDetails = _carService.GetCarDetailsById(id);

                if (carDetails == null)
                {
                    return NotFound();
                }

                return Ok(carDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("AddCar")]
        public IActionResult AddCarDetails([FromBody] CarDetails carDetails)
        {
            try
            {
                if (carDetails == null)
                {
                    return BadRequest();
                }

                _carService.AddCarDetails(carDetails);
                return CreatedAtAction(nameof(GetCarDetailsById), new { id = carDetails.Vehicle_Id }, carDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("UpdateCar/{id}")]
        public IActionResult UpdateCarDetails(int id, [FromBody] CarDetails updatedCarDetails)
        {
            try
            {
                if (updatedCarDetails == null || id != updatedCarDetails.Vehicle_Id)
                {
                    return BadRequest();
                }

                var existingCarDetails = _carService.GetCarDetailsById(id);

                if (existingCarDetails == null)
                {
                    return NotFound();
                }
                _carService.UpdateCarDetails(id, updatedCarDetails);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCarDetails(int id)
        {
            try
            {
                var existingCarDetails = _carService.GetCarDetailsById(id);

                if (existingCarDetails == null)
                {
                    return NotFound();
                }

                _carService.DeleteCarDetails(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
