using FiorelloTemplate.Models;

namespace FiorelloTemplate.ViewModel.ProductViewModel
{
    public class DetailProductVm
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Image> images { get; set; }
    }
}
