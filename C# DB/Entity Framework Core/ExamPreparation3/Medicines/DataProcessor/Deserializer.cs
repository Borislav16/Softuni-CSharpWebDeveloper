namespace Medicines.DataProcessor
{
    using Boardgames.Helper;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Microsoft.VisualBasic;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            var patientsDtos = JsonConvert.DeserializeObject<ImportPatientDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Patient> patients = new List<Patient>();

            foreach (var patientDto in patientsDtos)
            {
                if(!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient patient = new Patient()
                {
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender,
                };

                foreach (var id in patientDto.Medicines)
                {
                    if (patient.PatientsMedicines.Any(x => x.MedicineId == id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    patient.PatientsMedicines.Add(new PatientMedicine()
                    {
                        Patient = patient,
                        MedicineId = id
                    });
                }

                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patientDto.FullName, patientDto.Medicines.Count()));
            }

            context.Patients.AddRange(patients);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            var pharmaciesDtos = XmlSerializationHelper.Deserialize<ImportPharmacyDto[]>(xmlString, "Pharmacies");

            StringBuilder sb = new StringBuilder();

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            foreach (var pharmacyDto in pharmaciesDtos)
            {
                if (!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!(pharmacyDto.IsNonStop.ToString() == "true")
                    && !(pharmacyDto.IsNonStop.ToString() == "false"))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy pharmacy = new Pharmacy()
                {
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                };

                foreach (var item in pharmacyDto.Medicines)
                {
                    if (!IsValid(item))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(item.ProductionDate) || string.IsNullOrWhiteSpace(item.ExpiryDate))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!DateTime.TryParseExact(item.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime productionDate))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!DateTime.TryParseExact(item.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (productionDate >= expiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if(pharmacy.Medicines.Any(m => m.Name == item.Name && m.Producer == item.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    pharmacy.Medicines.Add(new Medicine()
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Category = (Category)item.Category,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Producer = item.Producer,
                    });
                }

                pharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count()));
            }

            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
