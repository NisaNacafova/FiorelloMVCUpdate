using FiorelloTemplate.Models;

namespace FiorelloTemplate.ViewModel
{
    public class ProductCategory
    {
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<Slider>? sliders { get; set; }
    }
}
