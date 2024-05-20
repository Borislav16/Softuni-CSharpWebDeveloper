using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using System.Security.Claims;
using static SoftUniBazar.Data.Models.Ad;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext data;

        private readonly UserManager<IdentityUser> userManager;

        public AdController(BazarDbContext _data, UserManager<IdentityUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {

            List<AllViewModel> model = await data.Ads
                .Select(a => new AllViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn,
                    Category = a.Category.Name
                }).ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();


            var model = await data.Categories
                .Select(c => new AddViewModel()
                {
                    Categories = categories,
                })
                .FirstOrDefaultAsync();


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var category = await data.Categories
                    .FirstOrDefaultAsync(c => c.Id == model.CategoryId);

            if (id.Any() && category != null)
            {
                var owner = await userManager.FindByIdAsync(id);

                Ad generateAd = new Ad()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    OwnerId = id,
                    Owner = owner,
                    ImageUrl = model.ImageUrl,
                    CreatedOn = DateTime.Now,
                    CategoryId = model.CategoryId,
                    Category = category,
                };

                await data.Ads.AddAsync(generateAd);

                await data.SaveChangesAsync();

            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var AdBuyers = await data.AdBuyers
                .Where(ab => ab.BuyerId == id)
                .Select(ab => ab.AdId)
                .ToListAsync();

            var myAds = await data.Ads
                .Where(a => AdBuyers.Contains(a.Id))
                .Select(a => new AllViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn,
                    Category = a.Category.Name
                })
                .ToListAsync();

            return View(myAds);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            string buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isPossible = data.AdBuyers
                .Any(ab => ab.BuyerId == buyerId && ab.AdId == id);

            if (!isPossible)
            {
                AdBuyer newBuyer = new AdBuyer()
                {
                    BuyerId = buyerId,
                    AdId = id,
                };

                await data.AdBuyers.AddAsync(newBuyer);
                await data.SaveChangesAsync();
            }

            return RedirectToAction("Cart");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            string buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            AdBuyer? model = await data.AdBuyers
                .Where(ab => ab.BuyerId == buyerId && ab.AdId == id)
                .FirstOrDefaultAsync();

            if (model != null)
            {
                data.AdBuyers.Remove(model);
                await data.SaveChangesAsync();
            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Ad ad = await data.Ads
                .Where(ab => ab.Id == id)
                .FirstAsync();

            var categories = await data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();


            var model = await data.Categories
                .Select(c => new AddViewModel()
                {
                    Name = ad.Name,
                    Description = ad.Description,
                    Price = ad.Price,
                    ImageUrl = ad.ImageUrl,
                    CategoryId = ad.CategoryId,
                    Categories = categories,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddViewModel model)
        {
            var findAd = await data.Ads
                .Where(ab => ab.Id == id)
                .FirstOrDefaultAsync();

            if (findAd != null)
            {

                findAd.Name = model.Name;
                findAd.Description = model.Description;
                findAd.Price = model.Price;
                findAd.ImageUrl = model.ImageUrl;
                findAd.CreatedOn = DateTime.Now;
                findAd.CategoryId = model.CategoryId;

                await data.SaveChangesAsync();

            }

            return RedirectToAction(nameof(All));
        }
    }
}
