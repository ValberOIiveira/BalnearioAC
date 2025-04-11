using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalnearioAC.Models
{
    [Table("items_sale")]
    public class ItemSale
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("sales")]
        [Column("id_sale")]
        public int SaleId { get; set; }

        [ForeignKey("products")]
        [Column("id_product")]
        public int? ProductId { get; set; }

        [Column("qtd")]
        public int Quantity { get; set; }
    }
}
