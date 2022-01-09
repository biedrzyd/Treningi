using System;
using System.Collections.Generic;
using System.Text;

namespace Treningi.Infrastructure.Commands
{
    public class CreateActivity
    {
        public CreateActivity(string n, string s, string h, string w)
        {
            CompetitorID = n;
            day = s;
            hour = h;
            exercise = w;
        }

        public CreateActivity() {}
        public string CompetitorID { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public string exercise { get; set; }
    }
}
