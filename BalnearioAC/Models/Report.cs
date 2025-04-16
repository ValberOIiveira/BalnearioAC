using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report")]
    public class Report
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("generation_date")]
        public DateTime GenerationDate { get; set; }
    }
}
