using System.ComponentModel.DataAnnotations;

namespace Vendas.Model
{
    public class PedidoModel
    {
        [Required]
        public ICollection<ItemPedidoModel> Itens { get; set; } = new List<ItemPedidoModel>();
    }
}
