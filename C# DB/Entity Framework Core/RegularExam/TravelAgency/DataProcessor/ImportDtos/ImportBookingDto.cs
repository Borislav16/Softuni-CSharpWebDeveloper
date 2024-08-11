using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos
{
    public class ImportBookingDto
    {
        [Required]
        public string BookingDate { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string TourPackageName { get; set; }
    }
}
