using Newtonsoft.Json;

namespace Vendas.DTO
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        [JsonIgnore]
        public Pedido Pedido { get; set; }
    }
}
