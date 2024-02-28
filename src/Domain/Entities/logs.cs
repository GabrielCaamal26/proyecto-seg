﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class logs
    {
        [Key]
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public string nombreFuncion { get; set; }
        public string ipA { get; set; }
        public string datos { get; set; }
        public string response { get; set; }   
    }
}
