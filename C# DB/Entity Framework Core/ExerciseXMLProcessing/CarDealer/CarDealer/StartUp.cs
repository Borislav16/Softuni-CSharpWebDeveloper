using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string path = File.ReadAllText("../../../Datasets/sales.xml");

            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }

        //09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSuppliersDTO[]),
                new XmlRootAttribute("Suppliers"));

            using StringReader reader = new StringReader(inputXml);

            ImportSuppliersDTO[] importSupplierDTOs = (ImportSuppliersDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSupplierDTOs);
                
            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }

        //10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportPartsDTO[]), new XmlRootAttribute("Parts"));

            using StringReader reader = new StringReader(inputXml);
            ImportPartsDTO[] importPartsDTOs = (ImportPartsDTO[])xmlSerializer.Deserialize(reader);

            var supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();

            Mapper mapper = GetMapper();

            Part[] parts = mapper.Map<Part[]>(importPartsDTOs.Where(p => supplierIds.Contains(p.SupplierId)));

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        //11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCarsDTO[]), new XmlRootAttribute("Cars"));

            using StringReader reader = new StringReader(inputXml);
            ImportCarsDTO[] importCarsDTOs = (ImportCarsDTO[])xmlSerializer.Deserialize(reader);


            Mapper mapper = GetMapper();
            List<Car> cars = new List<Car>();

            foreach (var carDTO in importCarsDTOs)
            {
                Car car = mapper.Map<Car>(carDTO);

                int[] carPartIds = carDTO.PartsIds
                    .Select(c => c.Id)
                    .Distinct()
                    .ToArray();

                var carParts = new List<PartCar>();
                ICollection<PartCar> parts = new HashSet<PartCar>();
                foreach (var partDto in carDTO.PartsIds.DistinctBy(p => p.Id))
                {
                    if (!context.Parts.Any(p => p.Id == partDto.Id))
                        continue;

                    PartCar carPart = new PartCar()
                    {
                        PartId = partDto.Id
                    };
                    parts.Add(carPart);
                    car.PartsCars.Add(carPart);
                }
                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}";
        }

        //12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCustomersDTO[]), new XmlRootAttribute("Customers"));

            using StringReader reader = new StringReader(inputXml);
            ImportCustomersDTO[] importCustomersDTOs = (ImportCustomersDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            Customer[] customers = mapper.Map<Customer[]>(importCustomersDTOs);

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}";

        }

        //13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportSalesDTO[]), new XmlRootAttribute("Sales"));

            using StringReader reader = new StringReader(inputXml);
            ImportSalesDTO[] importSalesDTOs = (ImportSalesDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            int[] cardIds = context.Cars
                .Select(car => car.Id).ToArray();

            Sale[] sales = mapper.Map<Sale[]>(importSalesDTOs)
                .Where(s => cardIds.Contains(s.CarId)).ToArray();

            context.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}";
        }

        //14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            Mapper mapper = GetMapper();

            var carsWithDistance = context.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<ExportCarWithDistanceDTO>(mapper.ConfigurationProvider)
                .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportCarWithDistanceDTO[]), new XmlRootAttribute("cars"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, carsWithDistance, xsn);

            return stringBuilder.ToString().Trim();
        }

        //15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            Mapper mapper = GetMapper();

            var carsFromBmw = context.Cars
                .Where(c => c.Make == "BMW")
                .Select(c => new ExportBmwCarsDTO
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportBmwCarsDTO[]), new XmlRootAttribute("cars"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, carsFromBmw, xsn);

            return stringBuilder.ToString().Trim();
        }

        //16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            Mapper mapper = GetMapper();

            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportSuppliersDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToArray();

            XmlSerializer xmlSerializer =
                 new XmlSerializer(typeof(ExportSuppliersDTO[]), new XmlRootAttribute("suppliers"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, suppliers, xsn);

            return stringBuilder.ToString().Trim();
        }

        //17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            Mapper mapper = GetMapper();
            var carsWithParts = context.Cars
                .Select(c => new ExportCarsWithPartsDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    Parts = c.PartsCars
                    .Select(p => new ExportPartDTO
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price,
                    })
                    .OrderByDescending(p => p.Price)
                    .ToArray(),
                    
                })
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportCarsWithPartsDTO[]), new XmlRootAttribute("cars"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, carsWithParts, xsn);

            return stringBuilder.ToString().Trim();
        }

        //18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            Mapper mapper = GetMapper();

            var totalSales = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new ExportTotalSalesDTO
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales
                    .Sum(s => s.Car.PartsCars
                        .Sum(pc =>
                            Math.Round(c.IsYoungDriver ? pc.Part.Price * 0.95m : pc.Part.Price, 2)
                            )
                        )


                })
                .OrderByDescending(c => c.SpentMoney)
                .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportTotalSalesDTO[]), new XmlRootAttribute("customers"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, totalSales, xsn);

            return stringBuilder.ToString().Trim();
        }

        //19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            Mapper mapper = GetMapper();

            var sales = context.Sales
                .Select(s => new ExportSalesWithDiscountDTO
                {
                    Car = new ExportCarDTO
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance,
                    },
                    Discount = (int)s.Discount,
                    CustomerName = s.Customer.Name,
                    Price = s.Car.PartsCars.Sum(p => p.Part.Price),
                    PriceWithDiscount = Math.Round((double)(s.Car.PartsCars.Sum(p => p.Part.Price) * (1 - (s.Discount / 100))), 4)
                })
                .ToArray();
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportSalesWithDiscountDTO[]), new XmlRootAttribute("sales"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, sales, xsn);

            return stringBuilder.ToString().Trim();
        }
    }
}

