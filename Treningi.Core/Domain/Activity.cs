﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Treningi.Core
{
    public class Activity
    {
        public int ID { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public string exercise { get; set; }
        public string CompetitorID { get; set; }
    }
}