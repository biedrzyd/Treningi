using System;

namespace Treningi.Infrastructure.Commands
{
    public class CreateCoach
    {
        public CreateCoach(string n, string s, DateTime d)
        {
            Forename = n;
            Surname = s;
            DateBirth = d;
        }

        public CreateCoach() { }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
