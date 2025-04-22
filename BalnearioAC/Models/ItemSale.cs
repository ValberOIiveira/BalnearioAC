using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BalnearioAC.Models
{
    [Table("items_sale")]
    public class ItemSale
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_sale")]
        public int SaleId { get; set; }

        [Column("id_product")]
        public int? ProductId { get; set; }

        [Column("qtd")]
        public int Qtd { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }

}
