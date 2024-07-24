using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Assess_Layer.Models
{
    public class RentalAgreement
    {
        [Key]
        public int RentalAgreementId { get; set; } // Unique identifier for the rental agreement

        public int UserId { get; set; }
        [JsonIgnore]
        public CarDetails? CarDetails { get; set; } // Reference to the rented car

        public int Vehicle_Id { get; set; }
        [JsonIgnore]
        public User? User { get; set; }  // Reference to the user renting the car

        [Required(ErrorMessage = "Booking Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; } // Date when the rental was booked

        [Required(ErrorMessage = "Return Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; } // Date when the car is expected to be returned

        public string? IsReturned { get; set; }

        public string? RequestForReturn { get; set; }
    }
}
