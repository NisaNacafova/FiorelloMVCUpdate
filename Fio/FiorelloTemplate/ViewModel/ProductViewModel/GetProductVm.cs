namespace FiorelloTemplate.ViewModel.ProductViewModel
{
    public class GetProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
