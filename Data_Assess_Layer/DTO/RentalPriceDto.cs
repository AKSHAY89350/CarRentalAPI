using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer.DTO
{
    public class RentalPriceDto
    {
        public int Id { get; set; }
        public int RentalAgreementId { get; set; }
        public double TotalPrice { get; set; }
        public int? Fine { get; set; }
    }
}
