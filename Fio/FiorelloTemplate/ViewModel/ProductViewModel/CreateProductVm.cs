namespace FiorelloTemplate.ViewModel.ProductViewModel
{
    public class CreateProductVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
