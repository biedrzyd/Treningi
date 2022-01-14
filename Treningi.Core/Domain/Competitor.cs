using System;
using Treningi.WebApp.Models;

namespace Treningi.Core
{
    public class Competitor
    {
        public Competitor() {}
        public int ID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public DateTime Date_of_birth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string CoachId { get; set; }
        public ImageModel Image { get; set; }
    }
}
