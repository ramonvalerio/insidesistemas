using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InsideSistemas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

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

        [HttpGet("listar")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoAppService.ListarPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("listar-filtrar")]
        public async Task<IActionResult> ListarPedidosComPaginacao([FromQuery] string status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var paginatedResult = await _pedidoAppService.ListarPedidosPorStatusAsync(status, pageNumber, pageSize);
            return Ok(paginatedResult);
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarNovoPedido()
        {
            var pedido = await _pedidoAppService.IniciarNovoPedidoAsync();
            return Ok(pedido);
        }

        [HttpPost("{pedidoId}/adicionar-produto")]
        public async Task<IActionResult> AdicionarProdutoAoPedido(int pedidoId, [FromBody] ProdutoCommand produto)
        {
            var pedidoAtualizado = await _pedidoAppService.AdicionarProdutoAoPedidoAsync(pedidoId, produto);
            return Ok(pedidoAtualizado);
        }

        [HttpPost("{pedidoId}/fechar")]
        public async Task<IActionResult> FecharPedido(int pedidoId)
        {
            var pedidoFechado = await _pedidoAppService.FecharPedidoAsync(pedidoId);
            return Ok(pedidoFechado);
        }

        [HttpDelete("{pedidoId}/remover-produto/{produtoId}")]
        public async Task<IActionResult> RemoverProdutoDoPedido(int pedidoId, int produtoId)
        {
            var pedidoAtualizado = await _pedidoAppService.RemoverProdutoDoPedidoAsync(pedidoId, produtoId);
            return Ok(pedidoAtualizado);
        }
    }
}