namespace FiorelloTemplate.ViewModel.ProductViewModel
{
    public class EditproductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection images { get; set; }
    }
}
