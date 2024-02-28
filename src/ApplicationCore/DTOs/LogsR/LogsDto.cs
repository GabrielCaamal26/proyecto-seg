﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs.Logs
{
    public class LogsDto
    {
        public int? Id { get; set; }
        public DateTime fecha { get; set; }
        public string nombreFuncion { get; set; }
        public string ipA { get; set; }
        public string datos { get; set; }
        public string response { get; set; }
    }
}
