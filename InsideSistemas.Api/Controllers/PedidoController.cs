using InsideSistemas.Application.DTOs;
using InsideSistemas.Application.Services;
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

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarNovoPedido()
        {
            var pedido = await _pedidoAppService.IniciarNovoPedidoAsync();
            return Ok(pedido);
        }

        [HttpPost("{pedidoId}/adicionar-produto")]
        public async Task<IActionResult> AdicionarProdutoAoPedido(int pedidoId, [FromBody] ProdutoDTO produto)
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

        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> ObterPedidoPorId(int pedidoId)
        {
            var pedido = await _pedidoAppService.ObterPedidoPorIdAsync(pedidoId);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoAppService.ListarPedidosAsync();
            return Ok(pedidos);
        }
    }
}