using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("cpf")]
        public string cpf { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("phone")]
        public string? phone { get; set; }

        [Column("age")]
        public DateTime? age { get; set; }

        [Column("passwd")]
        public string passwd { get; set; }

        [Column("id_user_type")]
        public int id_user_type { get; set; }

        public UserType user_type { get; set; }
    }
}