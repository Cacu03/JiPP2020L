﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konwerter.Desktop
{
    public class GRADES
    {
        [Key]
        public int id { get; set; }
        public int grade { get; set; }
    }
}
