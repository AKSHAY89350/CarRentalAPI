using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Assess_Layer.Models
{
    [Table("CarDetails")]
    public class CarDetails
    {
        [Key]
        public int Vehicle_Id { get; set; }

        [Required(ErrorMessage = "Maker is required.")]
        [MaxLength(100)]
        public string? Maker { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [MaxLength(100)]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Rental Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rental Price must be greater than 0")]
        public double? Rental_Price { get; set; }

        [Required]
        public string? Availability_status { get; set; }

        public string? Image_Link { get; set; }
        [JsonIgnore]
        public List<RentalAgreement>? RentalAgreements { get; set; }
    }
}
