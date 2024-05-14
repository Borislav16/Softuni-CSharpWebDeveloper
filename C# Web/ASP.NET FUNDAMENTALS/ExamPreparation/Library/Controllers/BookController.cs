using Library.Data;
using Library.Data.Models;
using Library.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly LibraryDbContext data;
        private readonly UserManager<IdentityUser> userManager;

        public BookController(LibraryDbContext _data, UserManager<IdentityUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await data
                .Books
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    Category = b.Category.Name,
                })
                .ToListAsync();
                

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Mine()
        {

            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await data
                .Books
                .Where(b => b.UsersBooks.Any(ub => ub.CollectorId == id))
                .Select(b => new MineBookViewModel()
                {
                    Id = b.Id,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title,
                    Author = b.Author,
                    Category = b.Category,
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel bookModel = new AddBookViewModel()
            {
                Categories = await data.Categories
                    .Select(c => new Models.Caategory.CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                    })
                    .ToListAsync(),
            };
                

            return View(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if(!data.Categories.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.Url,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await data.Books.AddAsync(book);
            await data.SaveChangesAsync();

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            AddToCollectionViewModel? book = await data.Books
                .Where(b => b.Id == id)
                .Select(b => new AddToCollectionViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    CategoryId = b.CategoryId,
                }).FirstOrDefaultAsync();


            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool alreadyAdded = await data.IdentityUserBooks
                .AnyAsync(ub => ub.BookId == id && ub.CollectorId == userId);

            if (!alreadyAdded)
            {
                var userBook = new IdentityUserBook()
                {
                    BookId = book.Id,
                    CollectorId = userId,
                };

                await data.IdentityUserBooks.AddAsync(userBook);
                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            AddToCollectionViewModel? book = await data.Books
                .Where(b => b.Id == id)
                .Select(b => new AddToCollectionViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    CategoryId = b.CategoryId,
                }).FirstOrDefaultAsync();


            if (book == null)
            {
                return RedirectToAction(nameof(Mine));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userBook = await data.IdentityUserBooks
                    .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (userBook != null)
            {
                data.IdentityUserBooks.Remove(userBook);
                await data.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Mine));
            
        }

    }
}
