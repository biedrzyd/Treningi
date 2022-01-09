using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Treningi.Infrastructure.Commands
{
    public class UpdateCompetitor
    {
        [JsonConstructor]
        public UpdateCompetitor(int sid, string n, string s, int h, int w, string c, DateTime d, string t)
        {
            ID = sid;
            Forename = n;
            Surname = s;
            Height = h;
            Weight = w;
            Country = c;
            DateBirth = d;
            CoachId = t;
        }
        public UpdateCompetitor()
        {

        }
        public int ID { get; set; }
        public string Forename { get; set; }    
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Country { get; set; }
        public string CoachId { get; set; }
    }
}
