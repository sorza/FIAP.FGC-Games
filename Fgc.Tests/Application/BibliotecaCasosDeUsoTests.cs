using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Moq;
using Bibliotecas = Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas;

namespace Fgc.Tests.Application
{
    public class BibliotecaCasosDeUsoTests
    {
        private readonly Mock<IBibliotecaRepository> _bibliotecaRepositoryMock;
        private readonly Bibliotecas.Listar.Handler _obterBibliotecas;
        private readonly Bibliotecas.Buscar.Handler _obterBiblioteca;
        private readonly Bibliotecas.Criar.Handler _criarBiblioteca;
        private readonly Bibliotecas.Atualizar.Handler _atualizarJBiblioteca;
        private readonly Bibliotecas.Remover.Handler _excluirBiblioteca;

        public BibliotecaCasosDeUsoTests()
        {
            _bibliotecaRepositoryMock = new Mock<IBibliotecaRepository>();
            _obterBibliotecas = new Bibliotecas.Listar.Handler(_bibliotecaRepositoryMock.Object);
            _obterBiblioteca = new Bibliotecas.Buscar.Handler(_bibliotecaRepositoryMock.Object);
            _criarBiblioteca = new Bibliotecas.Criar.Handler(_bibliotecaRepositoryMock.Object);
            _atualizarJBiblioteca = new Bibliotecas.Atualizar.Handler(_bibliotecaRepositoryMock.Object);
            _excluirBiblioteca = new Bibliotecas.Remover.Handler(_bibliotecaRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterBibliotecas_DeveRetornarListaDeBibliotecas()
        {
            // Arrange
            var bibliotecas = new List<Fgc.Domain.Biblioteca.Entidades.Biblioteca>
            {
                Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca 1"),
                Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca 2")
            };

            _bibliotecaRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(bibliotecas);

            // Act
            var result = await _obterBibliotecas.Handle(new Bibliotecas.Listar.Query(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Bibliotecas.Count);
        }

        [Fact]
        public async Task CriarBiblioteca_DeveCriarNovaBiblioteca()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var titulo = "Nova Biblioteca";

            _bibliotecaRepositoryMock.Setup(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Biblioteca.Entidades.Biblioteca>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _criarBiblioteca.Handle(new Bibliotecas.Criar.Command(contaId, titulo), CancellationToken.None);

            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.True(resultado.Value.ContaId == contaId);
            Assert.Equal(titulo, resultado.Value.Titulo);
        }

        [Fact]
        public async Task AtualizarBiblioteca_DeveAtualizarBibliotecaExistente()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var novoTitulo = "Biblioteca Atualizada";
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca Original");

            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);

            _bibliotecaRepositoryMock.Setup(repo => repo.Alterar(It.IsAny<Fgc.Domain.Biblioteca.Entidades.Biblioteca>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _atualizarJBiblioteca.Handle(new Bibliotecas.Atualizar.Command(biblioteca.Id.ToString(), novoTitulo), CancellationToken.None);
           
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(novoTitulo, resultado.Value.Titulo);
        }

        [Fact]
        public async Task ExcluirBiblioteca_DeveRemoverBibliotecaExistente()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca para Excluir");

            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);

            _bibliotecaRepositoryMock.Setup(repo => repo.Deletar(biblioteca.Id, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _excluirBiblioteca.Handle(new Bibliotecas.Remover.Command(biblioteca.Id.ToString()), CancellationToken.None);
           
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(biblioteca.Id, resultado.Value.id);
        }

        [Fact]
        public async Task BuscarBiblioteca_DeveRetornarBibliotecaExistente()
        {
            // Arrange
            var contaId = Guid.NewGuid();
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(contaId, "Biblioteca de Teste");

            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);
            // Act
            var resultado = await _obterBiblioteca.Handle(new Bibliotecas.Buscar.Query(biblioteca.Id.ToString()), CancellationToken.None);
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(biblioteca.Titulo, resultado.Value.Titulo);
        }

        [Fact]
        public async Task BuscarBiblioteca_DeveRetornarErroQuandoBibliotecaNaoExistir()
        {
            // Arrange
            var bibliotecaId = Guid.NewGuid();
            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(bibliotecaId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Fgc.Domain.Biblioteca.Entidades.Biblioteca)null!);
            // Act
            var resultado = await _obterBiblioteca.Handle(new Bibliotecas.Buscar.Query(bibliotecaId.ToString()), CancellationToken.None);
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("404", resultado.Error.Code);
            Assert.Equal("Biblioteca não encontrada.", resultado.Error.Message);
        }
    }
}
