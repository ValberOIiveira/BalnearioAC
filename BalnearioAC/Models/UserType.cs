using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("user_type")]
    public class UserType
    {
        [Column("id")]
        public int id { get; set; }

        [Column("tipo")]
        public string tipo { get; set; }
    }
}