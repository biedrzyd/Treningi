using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Treningi.WebApp.Models;

namespace Treningi.Infrastructure.Commands
{
    public class CreateCompetitor
    {
        public CreateCompetitor(string n, string s, int h, int w, string c, DateTime d, string t, ImageModel i, string iid)
        {
            Forename = n;
            Surname = s;
            Height = h;
            Weight = w;
            Country = c;
            DateBirth = d;
            CoachId = t;
            Image = i;
            UserImageId = iid;
        }

        public CreateCompetitor() {}
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Country { get; set; }
        public string CoachId { get; set; }
        [JsonIgnore]
        public ImageModel Image { get; set; }
        public string UserImageId { get; set; }
    }
}
