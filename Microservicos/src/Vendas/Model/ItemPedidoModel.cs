using System.ComponentModel.DataAnnotations;

namespace Vendas.Model
{
    public class ItemPedidoModel
    {
        [Required]
        public Guid ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal PrecoUnitario { get; set; }
    }
}
