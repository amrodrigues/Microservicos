namespace Vendas.DTO
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public decimal ValorTotal { get; set; }
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
