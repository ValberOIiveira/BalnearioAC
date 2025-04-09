using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("log_user_activity")]
    public class LogUserActivity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_user")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column("activity_type")]
        public string ActivityType { get; set; }

        [Column("activity_date")]
        public DateTime ActivityDate { get; set; } = DateTime.Now;

        [Column("description")]
        public string Description { get; set; }
    }
}
