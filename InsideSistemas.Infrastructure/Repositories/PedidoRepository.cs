using InsideSistemas.Domain.Entities;
using InsideSistemas.Domain.Repositories;
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
            return await _context.Pedidos
                .Include(p => p.Produtos)
                .ToListAsync();
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
