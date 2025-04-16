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
        public string Name { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("id_user")]
        [ForeignKey("User")]
        public int IdUser { get; set; }
        public virtual User User { get; set; }
    }
}