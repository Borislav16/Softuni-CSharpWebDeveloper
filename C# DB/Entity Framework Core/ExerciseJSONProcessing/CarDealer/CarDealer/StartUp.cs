using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string path = File.ReadAllText("../../../Datasets/sales.json");

            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }


        //09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);

            SupplierDTO[] supplierDTO = JsonConvert.DeserializeObject<SupplierDTO[]>(inputJson);

            Supplier[] suppliers = mapper.Map<Supplier[]>(supplierDTO);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }

        //10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);

            PartDTO[] partDTOs = JsonConvert.DeserializeObject<PartDTO[]>(inputJson);
            Part[] parts = mapper.Map<Part[]>(partDTOs);

            int[] supplierIds = context.Suppliers
                .Select(s => s.Id)
                .ToArray();

            Part[] partsWithValidIds = parts
                .Where(p => supplierIds.Contains(p.SupplierId)).ToArray();

            context.Parts.AddRange(partsWithValidIds);
            context.SaveChanges();

            return $"Successfully imported {partsWithValidIds.Count()}.";
        }

        //11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsAndPartsDTO = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);

            List<PartCar> parts = new List<PartCar>();
            List<Car> cars = new List<Car>();

            foreach (var dto in carsAndPartsDTO)
            {
                Car car = new Car()
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TraveledDistance = dto.TravelledDistance
                };
                cars.Add(car);

                foreach (var part in dto.PartsId.Distinct())
                {
                    PartCar partCar = new PartCar()
                    {
                        Car = car,
                        PartId = part,
                    };
                    parts.Add(partCar);
                }
            }

            context.Cars.AddRange(cars);
            context.PartsCars.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}.";
        }

        //12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}.";
        }

        //13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}.";
        }

        //14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .ToList();


            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return json;
        }

        //15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }

        //16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count(),
                });

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            return json;
        }

        //17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TraveledDistance = c.TraveledDistance
                    },
                    parts = c.PartsCars
                        .Select(pc => new
                        {
                            pc.Part.Name,
                            Price = pc.Part.Price.ToString("f2")
                        })
                        .ToList()
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }

        //18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any(s => s.CustomerId != null))
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count(),
                    spentMoney = c.Sales
                    .SelectMany(s => s.Car.PartsCars
                    .Select(pc => pc.Part.Price))
                })
                .ToList();

            var output = customers
                .Select(o => new
                {
                    o.fullName,
                    o.boughtCars,
                    spentMoney = o.spentMoney.Sum(),
                })
                .OrderByDescending(o => o.spentMoney)
                .ThenByDescending(o => o.boughtCars)
                .ToList();

            string json = JsonConvert.SerializeObject(output, Formatting.Indented);

            return json;
        }

        //19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TraveledDistance = s.Car.TraveledDistance,
                    },
                    customerName = s.Customer.Name,
                    discount = s.Discount.ToString("f2"),
                    price = s.Car.PartsCars.Sum(pc => pc.Part.Price).ToString("f2"),
                    priceWithDiscount = (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - s.Discount / 100)).ToString("f2"),
                })
                .ToList();

            string json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return json;
        }
    }
}