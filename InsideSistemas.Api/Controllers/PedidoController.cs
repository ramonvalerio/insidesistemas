using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InsideSistemas.Api.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        // GET /api/pedidos/{pedidoId}
        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> ObterPedidoPorId(int pedidoId)
        {
            var pedido = await _pedidoAppService.ObterPedidoPorIdAsync(pedidoId);
            if (pedido == null)
            {
                return NotFound(new { Message = "Pedido não encontrado." });
            }
            return Ok(pedido);
        }

        // GET /api/pedidos
        [HttpGet]
        public async Task<IActionResult> ListarPedidos([FromQuery] string status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var paginatedResult = await _pedidoAppService.ListarPedidosPorStatusAsync(status, pageNumber, pageSize);
            return Ok(paginatedResult);
        }

        // POST /api/pedidos
        [HttpPost]
        public async Task<IActionResult> IniciarNovoPedido()
        {
            var pedido = await _pedidoAppService.IniciarNovoPedidoAsync();
            return CreatedAtAction(nameof(ObterPedidoPorId), new { pedidoId = pedido.Id }, pedido);
        }

        // POST /api/pedidos/{pedidoId}/produtos
        [HttpPost("{pedidoId}/produtos")]
        public async Task<IActionResult> AdicionarProdutoAoPedido(int pedidoId, [FromBody] ProdutoCommand produto)
        {
            var pedidoAtualizado = await _pedidoAppService.AdicionarProdutoAoPedidoAsync(pedidoId, produto);
            return Ok(pedidoAtualizado);
        }

        // PATCH /api/pedidos/{pedidoId}/fechar
        [HttpPatch("{pedidoId}/fechar")]
        public async Task<IActionResult> FecharPedido(int pedidoId)
        {
            var pedidoFechado = await _pedidoAppService.FecharPedidoAsync(pedidoId);
            return Ok(pedidoFechado);
        }

        // DELETE /api/pedidos/{pedidoId}/produtos/{produtoId}
        [HttpDelete("{pedidoId}/produtos/{produtoId}")]
        public async Task<IActionResult> RemoverProdutoDoPedido(int pedidoId, int produtoId)
        {
            var pedidoAtualizado = await _pedidoAppService.RemoverProdutoDoPedidoAsync(pedidoId, produtoId);
            return Ok(pedidoAtualizado);
        }
    }
}
