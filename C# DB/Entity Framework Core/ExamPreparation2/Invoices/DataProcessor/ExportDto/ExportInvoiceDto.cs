using Invoices.Data.Models.Enums;
using Microsoft.VisualBasic;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Invoice")]
    public class ExportInvoiceDto
    {
        [XmlElement("InvoiceNumber")]
        public int InvoiceNumber { get; set; }

        [XmlElement("InvoiceAmount")]
        public decimal InvoiceAmount { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; } = null!;

        [XmlElement("Currency")]
        public CurrencyType CurrencyType { get; set; }
    }
}