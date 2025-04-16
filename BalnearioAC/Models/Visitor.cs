using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BalnearioAC.Models
{
    [Table("visitors")]
    public class Visitor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Column("cpf")]
        public string? Cpf { get; set; }

        [Column("age")]
        public DateTime? Age { get; set; }

        [Column("id_user")]
        public int? IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User? User { get; set; }
    }
}