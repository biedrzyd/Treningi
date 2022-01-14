using Microsoft.AspNetCore.Http;
using Treningi.WebApp.Models;

namespace Treningi.WebApp
{
    public class ImageVM : IImageModel
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
