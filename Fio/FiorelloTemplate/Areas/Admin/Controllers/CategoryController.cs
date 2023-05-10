using FiorelloTemplate.AppDbContext;
using FiorelloTemplate.Models;
using FiorelloTemplate.ViewModel.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly FiorellaDb _context;
        private readonly IWebHostEnvironment _environment;
        public CategoryController(FiorellaDb evaraDbContext, IWebHostEnvironment environment)
        {
            _context = evaraDbContext;
            _environment = environment;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.categories.ToList();
            List<GetCategoryVm> catagoryvm = new List<GetCategoryVm>();
            foreach (Category item in categories)
            {
                catagoryvm.Add(new GetCategoryVm()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                });
            }
            return View(catagoryvm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryVm categoryvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category category = new Category()
            {
                Name = categoryvm.Name,
                Description = categoryvm.Description,

            };
            _context.categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            Category category= _context.categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Category category = _context.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update()
        {
           

            return View();
        }
        [HttpPost]
        public IActionResult Update(Category category1)
        {
            Category? category = _context.categories.Find(category1.Id);
            if (category == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.categories.Any(c => c.Name.ToLower().Trim() == category1.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Same Name!");
                return View();

            }
            category.Name = category1.Name;
            _context.categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
