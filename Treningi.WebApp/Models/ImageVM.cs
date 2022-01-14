using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treningi.WebApp.Models;
using Microsoft.AspNetCore.Http;

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
