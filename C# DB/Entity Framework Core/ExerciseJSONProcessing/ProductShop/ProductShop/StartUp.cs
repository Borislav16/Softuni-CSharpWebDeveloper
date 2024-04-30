using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //string userJson = File.ReadAllText("../../../Datasets/categories-products.json");

            Console.WriteLine(GetUsersWithProducts(context));
        }

        //01
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);

            context.SaveChanges();
            return $"Successfully imported {users.Count()}"; ;
        }

        //02
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var product = JsonConvert.DeserializeObject<Product[]>(inputJson);



            if (product is not null)
            {
                context.Products.AddRange(product);
                context.SaveChanges();

            }
            return $"Successfully imported {product.Count()}";
        }

        //03
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);

            var validCategories = categories?.Where(c => c.Name is not null).ToList();

            if (validCategories is not null)
            {
                context.Categories.AddRange(validCategories);
                context.SaveChanges();
                return $"Successfully imported {validCategories.Count()}";

            }


            return $"Successfully imported 0";
        }

        //04
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        //05
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName,
                })
                .OrderBy(p => p.price)
                .ToList();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }

        //06
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price.ToString("f2"),
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName

                    })
                    .ToList()

                })
                .ToList();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            return json;
        }

        //07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count,
                    averagePrice = c.CategoriesProducts
                    .Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = c.CategoriesProducts
                    .Sum(cp => cp.Product.Price).ToString("f2")
                })
                .OrderByDescending(c => c.productsCount)
                .ToList();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return json;
        }

        //08
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold
                    .Where(p => p.BuyerId != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price

                    })
                    .ToList(),

                })
                .OrderByDescending(u => u.soldProducts.Count)
                .ToList();


            var output = new
            {
                usersCount = users.Count(),
                users = users
                .Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count(),
                        products = u.soldProducts 
                    }
                })
            };
            
            string json = JsonConvert.SerializeObject (output, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });

            return json;
        }
    }
}