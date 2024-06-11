using MVCIntroDemo.Models.Product;

namespace MVCIntroDemo.Seeding
{
    public static class ProductsData
    {
        public static IEnumerable<ProductViewModel> Products =
            new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50
                },
                new ProductViewModel() 
                {
                    Id=3,
                    Name = "Bread",
                    Price = 1.50
                }
            };

    }
}
