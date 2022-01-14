using Newtonsoft.Json;
using System;

namespace Treningi.WebApp
{
    public class CompetitorVM
    {

        public int ID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string CoachId { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public ImageVM Image { get; set; }
        public string UserImageId { get; set; }
    }
}
