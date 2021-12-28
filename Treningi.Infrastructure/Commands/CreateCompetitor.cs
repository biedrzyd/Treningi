using System;
using System.Collections.Generic;
using System.Text;

namespace Treningi.Infrastructure.Commands
{
    public class CreateCompetitor
    {
        public CreateCompetitor(string n, string s, int h, int w, string c, DateTime d)
        {
            Forename = n;
            Surname = s;
            Height = h;
            Weight = w;
            Country = c;
            DateBirth = d;
        }

        public CreateCompetitor() {}
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Country { get; set; }
    }
}
