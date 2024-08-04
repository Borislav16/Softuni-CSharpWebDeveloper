using Boardgames.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class BoardgameDTO
    {
        [Required]
        [MaxLength(20), MinLength(10)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10.00)]
        public double Rating { get; set; }

        [Required]
        [Range(2018, 2023)]
        public int YearPublished { get; set; }

        [Required]
        public int CategoryType { get; set; }

        [Required]
        public string Mechanics { get; set; }
    }
}