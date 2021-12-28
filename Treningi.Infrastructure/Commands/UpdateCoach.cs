using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Treningi.Infrastructure.Commands
{
    public class UpdateCoach
    {
        [JsonConstructor]
        public UpdateCoach(int sid, string n, string s, DateTime d)
        {
            ID = sid;
            Forename = n;
            Surname = s;
            DateBirth = d;
        }
        public UpdateCoach()
        {

        }
        public int ID { get; set; }
        public string Forename { get; set; }    
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
