using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Treningi.WebApp.Models;

namespace Treningi.Infrastructure.DTO
{
    public class CompetitorDTO
    {
        public int ID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string CoachId { get; set; }
        [JsonIgnore]
        public ImageModel Image { get; set; }
        public string UserImageId { get; set; }
    }
}
