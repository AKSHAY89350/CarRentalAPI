using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer.DTO
{
    public class AgreementDto
    {
        public int RentalAgreementId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? IsReturned { get; set; }
        public string? RequestForReturn { get; set; }
    }
}
