using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BalnearioAC.DTOs
{
    /* ----------------------------- READ ----------------------------- */

    /// <summary>
    /// Retorno básico de produto (GET /products).
    /// </summary>
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string ProductName  { get; set; } = string.Empty;
        public string Description  { get; set; } = string.Empty;
        public decimal Cost        { get; set; }
        public decimal Price       { get; set; }
        public int Stock           { get; set; }
        public DateTime CreatedAt  { get; set; }
        public DateTime UpdatedAt  { get; set; }
    }

    /// <summary>
    /// Retorno de produto incluindo os itens vendidos.
    /// Use quando precisar exibir as vendas do produto.
    /// </summary>
    public class ProductWithSalesReadDto : ProductReadDto
    {
        public List<ItemSaleReadDto> ItemSales { get; set; } = new();
    }

    /// <summary>
    /// Informações de um item de venda na listagem do produto.
    /// </summary>
    public class ItemSaleReadDto
    {
        public int   SaleId   { get; set; }
        public int   Qtd      { get; set; }
        public DateTime SaleDate { get; set; }
    }

    /* --------------------------- CREATE ----------------------------- */

    /// <summary>
    /// Dados mínimos para criar um produto.
    /// </summary>
    public class ProductCreateDto
    {
        [Required] public string ProductName { get; set; } = string.Empty;
        public string Description            { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Cost  { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }

    /* --------------------------- UPDATE ----------------------------- */

    /// <summary>
    /// Todos os campos são opcionais; envie apenas o que for alterar.
    /// </summary>
    public class ProductUpdateDto
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Cost  { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }
    }
}
