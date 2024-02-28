using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ap_Paterno { get; set; }
    }
}
