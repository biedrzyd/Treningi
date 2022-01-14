using Microsoft.EntityFrameworkCore;
using Treningi.WebApp.Models;

namespace Treningi.WebApp.Controllers
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        { }

        public DbSet<ImageModel> Images { get; set; }
    }
}
