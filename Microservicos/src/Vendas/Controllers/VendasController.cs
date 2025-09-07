using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Vendas.Data;
using Vendas.DTO;
using Vendas.Model;

namespace Vendas.Controllers
{
    [Authorize]
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendasDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public VendasController(VendasDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("criar-pedido")]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoModel pedidoModel)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // 1. Itera sobre cada item do pedido para verificar o estoque.
            foreach (var item in pedidoModel.Itens)
            {
                // 2. Faz uma requisição GET para o serviço de estoque.
                // O serviço de estoque precisa ter um endpoint para esta verificação.
                // Exemplo de endpoint no serviço de estoque: GET api/estoque/verificar/{id}/{quantidade}
                var response = await httpClient.GetAsync($"http://localhost:5062/api/estoque/verificar/{item.ProdutoId}/{item.Quantidade}");

                if (!response.IsSuccessStatusCode)
                {
                    // Se a validação falhar para qualquer item, retorna um erro.
                    return BadRequest("Produto fora de estoque ou ID do produto inválido.");
                }
            }

            var novoPedido = new Pedido
            {
                Itens = pedidoModel.Itens.Select(itemDto => new ItemPedido
                {
                    ProdutoId = itemDto.ProdutoId,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = itemDto.PrecoUnitario
                }).ToList()
            };

            // Lógica para salvar o pedido no banco de dados.
            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();

            //// Notificação assíncrona para o serviço de estoque (via RabbitMQ)
            //var factory = new ConnectionFactory { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    // Declara a fila 'vendas'
            //    channel.QueueDeclare(queue: "vendas",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    // Serializa o pedido para JSON
            //    var message = JsonSerializer.Serialize(novoPedido);
            //    var body = Encoding.UTF8.GetBytes(message);

            //    // Publica a mensagem na fila
            //    channel.BasicPublish(exchange: "",
            //                         routingKey: "vendas",
            //                         basicProperties: null,
            //                         body: body);
            //}

            return Ok("Pedido criado com sucesso! O estoque será atualizado.");
        }

        [HttpGet("consultar-pedidos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.Include(p => p.Itens).ToListAsync();
        }
    }
}
