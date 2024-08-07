﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.DataProcessor.ImportDto
{
    public class ImportSellersDTO
    {
        [Required]
        [MaxLength(20), MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30), MinLength(2)]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"www\.[A-Za-z0-9-]+\.com")]
        public string Website { get; set; }

        [JsonProperty("Boardgames")]
        public int[] BoardgamesId { get; set; }

    }
}
