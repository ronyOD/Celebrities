﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celebrities.Models
{
    /* Plain celebrity model */
    public class CelebrityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}