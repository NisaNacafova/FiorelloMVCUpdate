using System.ComponentModel.DataAnnotations.Schema;

namespace FiorelloTemplate.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [NotMapped]
        public IFormFileCollection images { get; set; }
    }
}
