using InsideSistemas.Application.DTOs;
using InsideSistemas.Application.Services;
using InsideSistemas.Domain.Entities;
using InsideSistemas.Domain.Repositories;
using Moq;

public class PedidoAppServiceTest
{
    [Fact]
    public async Task Deve_Iniciar_Novo_Pedido_Corretamente()
    {
        // Arrange
        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

        // Act
        var pedidoDTO = await pedidoAppService.IniciarNovoPedidoAsync();

        // Assert
        Assert.NotNull(pedidoDTO);
        Assert.False(pedidoDTO.EstaFechado);
        Assert.Equal(DateTime.UtcNow.Date, pedidoDTO.DataCriacao.Date);
        Assert.Empty(pedidoDTO.Produtos);
        pedidoRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Pedido>()), Times.Once);
        pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Adicionar_Produto_Ao_Pedido_Corretamente()
    {
        // Arrange
        var pedido = new Pedido();
        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pedido);

        var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

        var produtoDto = new ProdutoDTO
        {
            Id = 1,
            Nome = "Produto Teste",
            Preco = 10.0m,
            Quantidade = 2
        };

        // Act
        var pedidoDTO = await pedidoAppService.AdicionarProdutoAoPedidoAsync(pedido.Id, produtoDto);

        // Assert
        Assert.NotNull(pedidoDTO);
        Assert.False(pedidoDTO.EstaFechado);
        Assert.Single(pedidoDTO.Produtos);
        Assert.Equal(produtoDto.Id, pedidoDTO.Produtos[0].Id);
        Assert.Equal(produtoDto.Nome, pedidoDTO.Produtos[0].Nome);
        Assert.Equal(produtoDto.Preco, pedidoDTO.Produtos[0].Preco);
        Assert.Equal(produtoDto.Quantidade, pedidoDTO.Produtos[0].Quantidade);

        pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Remover_Produto_Do_Pedido_Corretamente()
    {
        // Arrange
        var produto = new Produto(1, "Produto Teste", 10.0m, 2);
        var pedido = new Pedido();
        pedido.AdicionarProduto(produto);

        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pedido);

        var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

        // Act
        var pedidoDTO = await pedidoAppService.RemoverProdutoDoPedidoAsync(pedido.Id, produto.Id);

        // Assert
        Assert.NotNull(pedidoDTO);
        Assert.False(pedidoDTO.EstaFechado);
        Assert.Empty(pedidoDTO.Produtos);

        pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Se_Produto_Nao_Existir_No_Pedido()
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
    public async Task Deve_Fechar_Pedido_Corretamente_Se_Houver_Produtos()
    {
        // Arrange
        var produto = new Produto(1, "Produto Teste", 10.0m, 2);
        var pedido = new Pedido();
        pedido.AdicionarProduto(produto);

        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pedido);

        var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

        // Act
        var pedidoDTO = await pedidoAppService.FecharPedidoAsync(pedido.Id);

        // Assert
        Assert.NotNull(pedidoDTO);
        Assert.True(pedidoDTO.EstaFechado);
        pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Ao_Tentar_Fechar_Pedido_Sem_Produtos()
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
    public async Task Deve_Lancar_Excecao_Ao_Tentar_Adicionar_Produto_Em_Pedido_Fechado()
    {
        // Arrange
        var produto = new Produto(1, "Produto Inicial", 10.0m, 1);
        var pedido = new Pedido();
        pedido.AdicionarProduto(produto);
        pedido.FecharPedido();

        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        pedidoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pedido);

        var pedidoAppService = new PedidoAppService(pedidoRepositoryMock.Object);

        var produtoDto = new ProdutoDTO
        {
            Id = 2,
            Nome = "Produto Teste",
            Preco = 10.0m,
            Quantidade = 2
        };

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            pedidoAppService.AdicionarProdutoAoPedidoAsync(pedido.Id, produtoDto));

        pedidoRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Never);
    }


    [Fact]
    public async Task Deve_Lancar_Excecao_Ao_Tentar_Remover_Produto_De_Pedido_Fechado()
    {
        // Arrange
        var produto = new Produto(1, "Produto Teste", 10.0m, 2);
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
    public async Task Deve_Retornar_Null_Se_Pedido_Nao_For_Encontrado()
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
