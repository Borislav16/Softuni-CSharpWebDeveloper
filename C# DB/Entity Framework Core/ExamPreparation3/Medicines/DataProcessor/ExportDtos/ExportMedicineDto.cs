using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportMedicineDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Price")]
        public string Price { get; set; }

        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlElement("Producer")]
        public string Producer { get; set; }

        [XmlElement("BestBefore")]
        public string ExpiryDate { get; set; }
    }
}