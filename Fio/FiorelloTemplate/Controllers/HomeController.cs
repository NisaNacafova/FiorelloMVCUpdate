
using FiorelloTemplate.AppDbContext;
using FiorelloTemplate.Models;
using FiorelloTemplate.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FiorelloTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly FiorellaDb _db;
        public HomeController(FiorellaDb fiorellaDb)
        {
            _db = fiorellaDb;
        }
        public IActionResult Index()
        {
           // List<Product> products = _db.products.Include(c=> c.Categories).Include(v=> v.Tags).ToList();
           List<Product> products = _db.products.ToList();
            List<Category> categories = _db.categories.ToList();
            List<Slider> sliders=_db.sliders.ToList();
            ProductCategory Vm = new ProductCategory()
            {
                products = products,
                categories = categories,
                sliders = sliders
            };
            return View(Vm);
        }
      


    }
}