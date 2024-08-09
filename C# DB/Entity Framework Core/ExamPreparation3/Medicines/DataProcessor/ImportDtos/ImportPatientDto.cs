using Medicines.Data.Models.Enums;
using Medicines.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [Required]
        [MaxLength(100), MinLength(5)]
        public string FullName { get; set; }

        [Required]
        [Range(0, 4)]
        public int AgeGroup { get; set; }

        [Required]
        [Range(0, 1)]
        public Gender Gender { get; set; }

        public int[] Medicines { get; set; }
    }
}
