using InsideSistemas.Application.Pedidos;
using InsideSistemas.Application.Pedidos.Requests;
using InsideSistemas.Domain.Models;
using Moq;

namespace InsideSistemas.Test.AppServices
{
    public class PedidoAppServiceTest
    {
        private readonly string statusAberto;
        private readonly string statusFechado;
        public PedidoAppServiceTest()
        {
            statusAberto = "aberto";
            statusFechado = "fechado";
        }

        [Fact]
        public async Task Iniciar_Novo_Pedido_Corretamente()
        {
            // Arrange
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act
            var pedidoQuery = await pedidoAppService.IniciarNovoPedidoAsync();

            // Assert
            Assert.NotNull(pedidoQuery);
            Assert.Equal(statusAberto, pedidoQuery.Status);
            Assert.Equal(DateTime.UtcNow.Date, pedidoQuery.DataCriacao.Date);
            Assert.Null(pedidoQuery.Produtos);

            pedidoRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Pedido>()), Times.Once);
            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task Adicionar_Produto_Ao_Pedido_Corretamente()
        {
            // Arrange
            var pedido = new Pedido();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            var produtoCommand = new ProdutoRequest
            {
                Nome = "Produto Teste",
                Preco = 10.0m,
                Quantidade = 2
            };

            // Act
            var pedidoCommand = await pedidoAppService.AdicionarProdutoAoPedidoAsync(pedido.Id, produtoCommand);

            // Assert
            Assert.NotNull(pedidoCommand);
            Assert.Equal(statusAberto, pedidoCommand.Status);
            Assert.Single(pedidoCommand.Produtos);
            Assert.Equal(produtoCommand.Nome, pedidoCommand.Produtos[0].Nome);
            Assert.Equal(produtoCommand.Preco, pedidoCommand.Produtos[0].Preco);
            Assert.Equal(produtoCommand.Quantidade, pedidoCommand.Produtos[0].Quantidade);

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task Remover_Produto_Do_Pedido_Corretamente()
        {
            // Arrange
            var produto = new Produto("Produto Teste", 10.0m, 2);
            var pedido = new Pedido();
            pedido.AdicionarProduto(produto);

            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act
            var pedidoQuery = await pedidoAppService.RemoverProdutoDoPedidoAsync(pedido.Id, produto.Id);

            // Assert
            Assert.NotNull(pedidoQuery);
            Assert.Equal(statusAberto, pedidoQuery.Status);
            Assert.Null(pedidoQuery.Produtos);

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task Lancar_Excecao_Se_Produto_Nao_Existir_No_Pedido()
        {
            // Arrange
            var pedido = new Pedido();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                pedidoAppService.RemoverProdutoDoPedidoAsync(pedido.Id, 999));

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Never);
        }

        [Fact]
        public async Task Fechar_Pedido_Corretamente_Se_Houver_Produtos()
        {
            // Arrange
            var produto = new Produto("Produto Teste", 10.0m, 2);
            var pedido = new Pedido();
            pedido.AdicionarProduto(produto);

            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act
            var pedidoQuery = await pedidoAppService.FecharPedidoAsync(pedido.Id);

            // Assert
            Assert.NotNull(pedidoQuery);
            Assert.Equal(statusFechado, pedidoQuery.Status);
            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
        }

        [Fact]
        public async Task Lancar_Excecao_Ao_Tentar_Fechar_Pedido_Sem_Produtos()
        {
            // Arrange
            var pedido = new Pedido();
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                pedidoAppService.FecharPedidoAsync(pedido.Id));

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Never);
        }

        [Fact]
        public async Task Lancar_Excecao_Ao_Tentar_Adicionar_Produto_Em_Pedido_Fechado()
        {
            // Arrange
            var produto = new Produto("Produto Inicial", 10.0m, 1);
            var pedido = new Pedido();
            pedido.AdicionarProduto(produto);
            pedido.FecharPedido();

            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            var produtoCommand = new ProdutoRequest
            {
                Nome = "Produto Teste",
                Preco = 10.0m,
                Quantidade = 2
            };

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                pedidoAppService.AdicionarProdutoAoPedidoAsync(pedido.Id, produtoCommand));

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Never);
        }

        [Fact]
        public async Task Lancar_Excecao_Ao_Tentar_Remover_Produto_De_Pedido_Fechado()
        {
            // Arrange
            var produto = new Produto("Produto Teste", 10.0m, 2);
            var pedido = new Pedido();
            pedido.AdicionarProduto(produto);
            pedido.FecharPedido();

            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync(pedido);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                pedidoAppService.RemoverProdutoDoPedidoAsync(pedido.Id, produto.Id));

            pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Never);
        }

        [Fact]
        public async Task Retornar_Null_Se_Pedido_Nao_For_Encontrado()
        {
            // Arrange
            var pedidoRepositoryMock = new Mock<IPedidoRepository>();
            pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Pedido)null);

            var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

            // Act
            var pedidoDTO = await pedidoAppService.ObterPedidoPorIdAsync(1);

            // Assert
            Assert.Null(pedidoDTO);
        }
    }
}