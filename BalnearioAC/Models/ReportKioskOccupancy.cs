using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("report_kiosk_occupancy")]
    public class ReportKioskOccupancy
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("report_date")]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Column("kiosk_id")]
        public int? KioskId { get; set; }

        [Column("total_reservations")]
        public int? TotalReservations { get; set; }

        [Column("occupied_days")]
        public int? OccupiedDays { get; set; }

        [ForeignKey("KioskId")]
        public Kiosk? Kiosk { get; set; }
    }
}