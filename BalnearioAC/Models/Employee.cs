using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_user")]
        public int? IdUser { get; set; }

        [Column("role")]
        public string? Role { get; set; }

        [Column("salary")]
        public decimal? Salary { get; set; }

        [Column("admission_date")]
        public DateTime? AdmissionDate { get; set; }

        // Relacionamento com a tabela User
        [ForeignKey("IdUser")]
        public User? User { get; set; }
    }
}
