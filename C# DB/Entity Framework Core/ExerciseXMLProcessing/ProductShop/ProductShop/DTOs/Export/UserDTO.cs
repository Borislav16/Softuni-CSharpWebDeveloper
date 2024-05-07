using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{

    [XmlType("User")]
    public class UserWithProductsExportDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; } = null!;

        [XmlElement("lastName")]
        public string LastName { get; set; } = null!;

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductsArrayDto SoldProducts { get; set; } = null!;
    }

    [XmlType("SoldProducts")]
    public class SoldProductsArrayDto
    {
        [XmlElement("count")]
        public int ProductsCount { get; set; }

        [XmlArray("products")]
        public ProductSoldExportDto[] Products { get; set; } = null!;
    }

    [XmlType("Product")]
    public class ProductSoldExportDto
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;

        [XmlElement("price")]
        public decimal Price { get; set; }
    }

    [XmlType("Users")]
    public class UserWithProductCountExportDto
    {
        [XmlElement("count")]
        public int CountOfUsers { get; set; }

        [XmlArray("users")]
        public UserWithProductsExportDto[] Users { get; set; }
    }
}
