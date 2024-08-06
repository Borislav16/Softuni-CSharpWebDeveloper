namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Boardgames.Helper;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            List<Client> clients = new List<Client>();

            var clientsDto = XmlSerializationHelper
                .Deserialize<ImportClientDto[]>(xmlString, "Clients");

            foreach (var clientDto in clientsDto)
            {
                if (!IsValid(clientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = clientDto.Name,
                    NumberVat = clientDto.NumberVat,
                };

                foreach (var addressDto in clientDto.Adress)
                {
                    if (!IsValid(addressDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    client.Addresses.Add(new Address()
                    {
                        StreetName = addressDto.StreetName,
                        StreetNumber = addressDto.StreetNumber,
                        PostCode = addressDto.PostCode,
                        City = addressDto.City,
                        Country = addressDto.Country,
                    });
                }

                clients.Add(client);
                sb.AppendLine(string.Format(SuccessfullyImportedClients, client.Name));
            }

            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().Trim();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<Invoice> invoicesToImport = new List<Invoice>();

            ImportInvoicesDto[] deserializedInvoices =
                JsonConvert.DeserializeObject<ImportInvoicesDto[]>(jsonString)!;
            foreach (ImportInvoicesDto invoiceDto in deserializedInvoices)
            {
                if (!IsValid(invoiceDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isIssueDateValid = DateTime
                    .TryParse(invoiceDto.IssueDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime issueDate);
                bool isDueDateValid = DateTime
                    .TryParse(invoiceDto.DueDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate);
                if (isIssueDateValid == false || isDueDateValid == false ||
                    DateTime.Compare(dueDate, issueDate) < 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!context.Clients.Any(cl => cl.Id == invoiceDto.ClientId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice newInvoice = new Invoice()
                {
                    Number = invoiceDto.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    Amount = invoiceDto.Amount,
                    CurrencyType = (CurrencyType)invoiceDto.CurrencyType,
                    ClientId = invoiceDto.ClientId
                };

                invoicesToImport.Add(newInvoice);
                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, invoiceDto.Number));
            }

            context.Invoices.AddRange(invoicesToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportProductsDto[] productDtos = JsonConvert.DeserializeObject<ImportProductsDto[]>(jsonString);

            List<Product> products = new List<Product>();

            foreach (var productDto in productDtos)
            {
                if (!IsValid(productDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Product p = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    CategoryType = productDto.CategoryType,
                };

                foreach (int clientId in productDto.ClientId.Distinct())
                {
                    Client c = context.Clients.Find(clientId);
                    if (c == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    p.ProductsClients.Add(new ProductClient()
                    {
                        Client = c
                    });
                }
                products.Add(p);
                sb.AppendLine(String.Format(SuccessfullyImportedProducts, p.Name, p.ProductsClients.Count));
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
