using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BalnearioAC.Models
{
    [Table("log_reservations")]
    public class LogReservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_reservation")]
        public int? ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [Column("action")]
        public string Action { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        [Column("performed_by")]
        public int? PerformedBy { get; set; }

        [ForeignKey("PerformedBy")]
        public User User { get; set; }

        [Column("old_data")]
        public string OldData { get; set; }

        [Column("new_data")]
        public string NewData { get; set; }
    }
}
