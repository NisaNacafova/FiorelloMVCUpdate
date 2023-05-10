using FiorelloTemplate.AppDbContext;
using FiorelloTemplate.Models;
using FiorelloTemplate.ViewModel.ProductViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly FiorellaDb _db;
        private readonly IWebHostEnvironment _environment;
        public ProductController(FiorellaDb db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public IActionResult Index()
        {
            List<Product> products = _db.products.Include(c => c.Images).ToList();
            List<GetProductVm> productVms = new List<GetProductVm>();
            foreach (Product item in products)
            {
                productVms.Add(new GetProductVm()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    ImageName = item.Images.FirstOrDefault().ImageName
                });
            }
            return View(productVms);
        }
        public IActionResult Create()
        {
            List<Category> categories = _db.categories.ToList();
            ViewData["Categories"] = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductVm productvm)
        {
            List<Category> categories = _db.categories.ToList();
            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = categories;
                return View();
            }
            Product product = new Product()
            {
                Name = productvm.Name,
                Price = productvm.Price,
                Description = productvm.Description,
                CategoryId = productvm.CategoryId,
            };
            List<Image> images = new List<Image>();
            foreach (IFormFile item in productvm.Images)
            {
                string FileName = Guid.NewGuid().ToString() + item.FileName;
                string path = Path.Combine(_environment.WebRootPath, "img", FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
                {
                    item.CopyTo(fileStream);
                }
                images.Add(new Image()
                {
                    ImageName = FileName
                });
                product.Images = images;
            }
            _db.products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            Product product = _db.products.Include(i => i.Images).Include(c => c.Category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            DetailProductVm vm = new DetailProductVm()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                images = product.Images

            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product product = _db.products.FirstOrDefault(c=> c.Id==id);
            if (product == null) return NotFound();
            foreach(var item in _db.images.Where(v=> v.ProductId==id).ToList())
            {
                _db.images.Remove(item);
            }
            _db.products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            Product product = _db.products.Include(i => i.Images).Include(c => c.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            List<Category> categories = _db.categories.ToList();
            ViewData["Categories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id, EditproductVm productvm)
        {
            Product product = _db.products.Include(i=> i.Images).Include(c=>c.Category).FirstOrDefault(p=> p.Id==id);
            if (product == null) return NotFound();
            List<Category> categories=_db.categories.ToList();

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = categories;
                return View();
            }
           

            Product newproduct = new Product()
            {
                Id = productvm.Id,
                Name = productvm.Name,
                Description = productvm.Description,
                Price = productvm.Price,

            };
            List<Image> images = new List<Image>();
            foreach(var item in _db.images.Where(i=>i.ProductId==id).ToList())
            {
                _db.images.Remove(item);
            }
            foreach (IFormFile item in productvm.images)
            {
                string FileName = Guid.NewGuid().ToString() + item.FileName;
                string path = Path.Combine(_environment.WebRootPath, "img", FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
                {
                    item.CopyTo(fileStream);
                }
                images.Add(new Image()
                {
                    ImageName = FileName
                });
                product.Images = images;
            }
            _db.products.Add(product);
            newproduct.Images=images;
            _db.products.Update(newproduct);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
