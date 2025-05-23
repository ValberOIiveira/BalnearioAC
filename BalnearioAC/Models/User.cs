using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BalnearioAC.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("age")]
        public DateTime? Age { get; set; }

        [Column("passwd")]
        public string Passwd { get; set; }

        [ForeignKey("user_type")]
        [Column("id_user_type")]
        public int Id_user_type { get; set; }

    }
}