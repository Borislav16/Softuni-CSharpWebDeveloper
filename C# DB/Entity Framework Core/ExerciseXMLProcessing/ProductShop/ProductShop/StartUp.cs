using AutoMapper;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();

            string path = File.ReadAllText("../../../Datasets/categories-products.xml");

            Console.WriteLine(GetUsersWithProducts(context));

        }
        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<ProductShopProfile>());
            return new Mapper(cfg);
        }

        //01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportUsersDTO[]), new XmlRootAttribute("Users"));

            using StringReader reader = new StringReader(inputXml);
            ImportUsersDTO[] importUsersDTOs = (ImportUsersDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            User[] users = mapper.Map<User[]>(importUsersDTOs);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        //02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportProductsDTO[]), new XmlRootAttribute("Products"));

            using StringReader reader = new StringReader(inputXml);
            ImportProductsDTO[] importProductsDTOs = (ImportProductsDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            Product[] products = mapper.Map<Product[]>(importProductsDTOs);

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        //03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCategoriesDTO[]), new XmlRootAttribute("Categories"));

            using StringReader reader = new StringReader(inputXml);
            ImportCategoriesDTO[] importCategoriesDTOs = (ImportCategoriesDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            Category[] categories = mapper.Map<Category[]>(importCategoriesDTOs.Where(c => c.Name != null));

            context.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }

        //04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCategoryProductsDTO[]), new XmlRootAttribute("CategoryProducts"));

            using StringReader reader = new StringReader(inputXml);
            ImportCategoryProductsDTO[] importCategoriesProductsDTOs = (ImportCategoryProductsDTO[])xmlSerializer.Deserialize(reader);

            Mapper mapper = GetMapper();

            CategoryProduct[] categoryProducts = mapper.Map<CategoryProduct[]>(importCategoriesProductsDTOs);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        //05
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProductsDTO
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToArray();


            XmlSerializer xmlSerializer =
                 new XmlSerializer(typeof(ExportProductsDTO[]), new XmlRootAttribute("Products"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, products, xsn);

            return stringBuilder.ToString().Trim();
        }

        //06
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(pb => pb.BuyerId != null))
                .Select(u => new ExportSoldProductsDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new SoldProductsDTO
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()

                })
                 .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportSoldProductsDTO[]), new XmlRootAttribute("Users"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, users, xsn);

            return stringBuilder.ToString().Trim();
        }

        //07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new ExportCategoryDTO()
                {
                    Name = c.Name,
                    CountOfProducts = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
            .OrderByDescending(c => c.CountOfProducts)
            .ThenBy(c => c.TotalRevenue)
            .ToArray();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ExportCategoryDTO[]), new XmlRootAttribute("Categories"));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);

            StringBuilder stringBuilder = new StringBuilder();

            using StringWriter stringWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(stringWriter, categories, xsn);

            return stringBuilder.ToString().Trim();
        }

        //08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersDtos = context
                .Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .Select(u => new UserWithProductsExportDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsArrayDto()
                    {
                        ProductsCount = u.ProductsSold.Count,
                        Products = u.ProductsSold
                                .Select(p => new ProductSoldExportDto
                                {
                                    Name = p.Name,
                                    Price = p.Price
                                })
                                .OrderByDescending(p => p.Price)
                                .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            UserWithProductCountExportDto result = new UserWithProductCountExportDto
            {
                CountOfUsers = context.Users.Count(x => x.ProductsSold.Any()),
                Users = usersDtos
            };

            XmlRootAttribute xmlRoot = new XmlRootAttribute("Users");
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);
            var sb = new StringBuilder();
            using StringWriter writer = new StringWriter(sb);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserWithProductCountExportDto), xmlRoot);
            xmlSerializer.Serialize(writer, result, xmlNamespaces);

            return sb.ToString().Trim();
        }
    }

}
