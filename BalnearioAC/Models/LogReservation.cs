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

        [ForeignKey("reservations")]
        [Column("id_reservation")]
        public int? ReservationId { get; set; }

        [Column("action")]
        public string Action { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        [ForeignKey("users")]
        [Column("performed_by")]
        public int? PerformedBy { get; set; }
    }
}
