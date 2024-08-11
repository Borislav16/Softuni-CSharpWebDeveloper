using Boardgames.Helper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var customersDtos = XmlSerializationHelper.Deserialize<ImportCustomersDto[]>(xmlString, "Customers");

            var existantCustomers = context.Customers
                .Select(c => new
                {
                    c.FullName,
                    c.Email,
                    c.PhoneNumber,
                })
                .ToList();

            List<Customer> customers = new List<Customer>();

            foreach (var customerDto in customersDtos)
            {
                if(!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if(existantCustomers.Any(ec =>
                    ec.FullName == customerDto.FullName
                    || ec.Email == customerDto.Email
                    || ec.PhoneNumber == customerDto.PhoneNumber))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                if(customers.Any(ec =>
                    ec.FullName == customerDto.FullName
                    || ec.Email == customerDto.Email
                    || ec.PhoneNumber == customerDto.PhoneNumber))
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                Customer customer = new Customer()
                {
                    FullName = customerDto.FullName,
                    Email = customerDto.Email,
                    PhoneNumber = customerDto.PhoneNumber,
                };

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, customerDto.FullName));
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var bookingsDtos = JsonConvert.DeserializeObject<ImportBookingDto[]>(jsonString);

            List<Booking> bookings = new List<Booking>();

            foreach (var bookingDto in bookingsDtos)
            {
                if(!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isTrue = DateTime
                    .TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime bookingDate);

                if(!isTrue)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = context.Customers
                    .First(c => c.FullName == bookingDto.CustomerName);

                var tourPackage = context.TourPackages
                    .First(c => c.PackageName == bookingDto.TourPackageName);

                Booking booking = new Booking()
                { 
                    BookingDate = bookingDate,
                    CustomerId = customer.Id,
                    TourPackageId = tourPackage.Id,
                };


                bookings.Add(booking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, bookingDto.TourPackageName, bookingDate.ToString(("yyyy-MM-dd"))));
            }

            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
