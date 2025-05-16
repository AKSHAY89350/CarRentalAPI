using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Assess_Layer.DTO
{
    public class CarDto
    {
        public int Vehicle_Id { get; set; }

        public string Maker { get; set; }

        public string Model { get; set; }

        public double Rental_Price { get; set; }

        public string Availability_status { get; set; }

        public string Image_Link { get; set; }

    }
}
