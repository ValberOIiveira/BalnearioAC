using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("kiosk")]
    public class Kiosk
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
