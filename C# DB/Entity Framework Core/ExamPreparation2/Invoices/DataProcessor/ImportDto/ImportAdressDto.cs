﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportAdressDto
    {
        [Required]
        [MaxLength(25), MinLength(10)]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(15), MinLength(5)]
        public string City { get; set; }

        [Required]
        [MaxLength(25), MinLength(5)]
        public string Country { get; set; }
    }
}
