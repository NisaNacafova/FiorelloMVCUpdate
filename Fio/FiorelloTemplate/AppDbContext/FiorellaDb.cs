using FiorelloTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorelloTemplate.AppDbContext
{
    public class FiorellaDb : DbContext
    {
        public FiorellaDb( DbContextOptions<FiorellaDb> opt ) :base(opt)
        {
            
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Image> images { get; set; }
    }
}
