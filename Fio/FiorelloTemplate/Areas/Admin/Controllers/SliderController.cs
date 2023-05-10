using FiorelloTemplate.AppDbContext;
using FiorelloTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorelloTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly FiorellaDb _db;
        public SliderController(FiorellaDb fiorellaDb)
        {
            _db = fiorellaDb;
        }
        public IActionResult Index()
        {
          List<Slider> sliders=_db.sliders.ToList();
            return View(sliders);
        }
        public IActionResult Details(int id)
        {
            Slider? slider = _db.sliders.Find(id);
            if(slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Slider? slider = _db.sliders.Find(id);
            if(slider == null) return NotFound();
            _db.sliders.Remove(slider);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_db.sliders.Any(c => c.Title.ToLower().Trim() == slider.Title.ToLower().Trim()))
            {
                ModelState.AddModelError("Title", "Same Title!");
                return View();

            }
           
            _db.sliders.Add(slider);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Update(Slider s)
        {
            Slider? slider = _db.sliders.Find(s);
            if (slider == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_db.sliders.Any(c => c.Title.ToLower().Trim() == slider.Title.ToLower().Trim()))
            {
                ModelState.AddModelError("Title", "Same Title!");
                return View();

            }
           slider.Title = s.Title;
            _db.sliders.Update(slider);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
