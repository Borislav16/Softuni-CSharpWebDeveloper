using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TravelAgency.DataProcessor.ImportDtos
{
    [XmlType("Customer")]
    public class ImportCustomersDto
    {
        [Required]
        [MaxLength(60), MinLength(4)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50), MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MaxLength(13), MinLength(13)]
        [RegularExpression("\\+\\d{12}")]
        [XmlAttribute("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
