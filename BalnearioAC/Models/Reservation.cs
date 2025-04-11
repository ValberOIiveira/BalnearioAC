using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("reservations")]
    public class Reservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("id_visitor")]
        public int VisitorId { get; set; }

        [ForeignKey("visitors")]
        [Column("id_visitor")]
        public int Visitor { get; set; }
        
        [ForeignKey("kiosks")]
        [Column("id_kiosk")]
        public int? KioskId { get; set; }
    }
}
