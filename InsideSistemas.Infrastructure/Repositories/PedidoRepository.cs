using InsideSistemas.Domain.Models;
using InsideSistemas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsideSistemas.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ListarTodosAsync()
        {
            try
            {
                return await _context.Pedidos
                .Include(p => p.Produtos)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Pedido>> ListarPaginadosPorStatusAsync(bool estaFechado, int pageNumber, int pageSize)
        {
            return await _context.Pedidos
                .Where(p => p.EstaFechado == estaFechado)
                .Include(p => p.Produtos)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarTotalPorStatusAsync(bool estaFechado)
        {
            return await _context.Pedidos.CountAsync(p => p.EstaFechado == estaFechado);
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
