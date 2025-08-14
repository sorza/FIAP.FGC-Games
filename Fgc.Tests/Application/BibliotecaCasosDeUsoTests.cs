using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;
using Moq;
using System.ComponentModel.DataAnnotations;
using Bibliotecas = Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas;

namespace Fgc.Tests.Application
{
    public class BibliotecaCasosDeUsoTests
    {
        private readonly Mock<IBibliotecaRepository> _bibliotecaRepositoryMock;
        private readonly Bibliotecas.Listar.Handler _obterBibliotecas;
        private readonly Bibliotecas.Buscar.Handler _obterBiblioteca;
        private readonly Bibliotecas.Criar.Handler _criarBiblioteca;
        private readonly Bibliotecas.Atualizar.Handler _atualizarBiblioteca;
        private readonly Bibliotecas.Remover.Handler _excluirBiblioteca;
        private readonly Bibliotecas.RemoverJogo.Handler _removerJogoBiblioteca;

        public BibliotecaCasosDeUsoTests()
        {
            _bibliotecaRepositoryMock = new Mock<IBibliotecaRepository>();
            _obterBibliotecas = new Bibliotecas.Listar.Handler(_bibliotecaRepositoryMock.Object);
            _obterBiblioteca = new Bibliotecas.Buscar.Handler(_bibliotecaRepositoryMock.Object);
            _criarBiblioteca = new Bibliotecas.Criar.Handler(_bibliotecaRepositoryMock.Object);
            _atualizarBiblioteca = new Bibliotecas.Atualizar.Handler(_bibliotecaRepositoryMock.Object);
            _excluirBiblioteca = new Bibliotecas.Remover.Handler(_bibliotecaRepositoryMock.Object);
            _removerJogoBiblioteca = new Bibliotecas.RemoverJogo.Handler(_bibliotecaRepositoryMock.Object);
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
            _bibliotecaRepositoryMock.Verify(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Biblioteca.Entidades.Biblioteca>(), It.IsAny<CancellationToken>()), Times.Once);
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
            var resultado = await _atualizarBiblioteca.Handle(new Bibliotecas.Atualizar.Command(biblioteca.Id.ToString(), novoTitulo), CancellationToken.None);

            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(novoTitulo, resultado.Value.Titulo);
            _bibliotecaRepositoryMock.Verify(repo => repo.Alterar(It.IsAny<Fgc.Domain.Biblioteca.Entidades.Biblioteca>(), It.IsAny<CancellationToken>()), Times.Once);
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
            _bibliotecaRepositoryMock.Verify(repo => repo.Deletar(biblioteca.Id, It.IsAny<CancellationToken>()), Times.Once);
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

        [Fact]
        public async Task AdicionarJogoBiblioteca_DeveAdicionarJogoNaBiblioteca()
        {
            // Arrange
            var generos = new List<Fgc.Domain.Biblioteca.Entidades.Genero>
            {
                Fgc.Domain.Biblioteca.Entidades.Genero.Criar(Guid.NewGuid(), "Ação"),
                Fgc.Domain.Biblioteca.Entidades.Genero.Criar(Guid.NewGuid(), "Aventura")
            };
            var jogo = Fgc.Domain.Biblioteca.Entidades.Jogo.Criar("Jogo de Teste", 59.99m, 2023, "Desenvolvedora Teste", generos);
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca de Teste");

            // Mockando o repositório de jogos
            var jogoRepositoryMock = new Mock<IJogoRepository>();

            // Mockando o método de obter jogo por ID
            jogoRepositoryMock.Setup(repo => repo.ObterPorId(jogo.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogo);

            // Criando handler para adicionar jogo na biblioteca
            var _adicionarJogoBiblioteca = new Bibliotecas.AdicionarJogo.Handler(_bibliotecaRepositoryMock.Object, jogoRepositoryMock.Object);

            // Mockando o repositório de bibliotecas
            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);

            // Mockando o método de salvar a biblioteca
            _bibliotecaRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            // Adicionando o jogo na biblioteca
            var resultado = await _adicionarJogoBiblioteca.Handle(new Bibliotecas.AdicionarJogo.Command(biblioteca.Id.ToString(), jogo.Id.ToString()), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.True(biblioteca.Jogos.Count == 1);
            Assert.Equal(biblioteca.Id, resultado.Value.BibliotecaId);
            Assert.Equal(jogo.Id, resultado.Value.JogoId);
            _bibliotecaRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoverJogoBiblioteca_DeveRemoverJogoDaBiblioteca()
        {
            // Arrange
            var generos = new List<Fgc.Domain.Biblioteca.Entidades.Genero>
            {
                Fgc.Domain.Biblioteca.Entidades.Genero.Criar(Guid.NewGuid(), "Ação"),
                Fgc.Domain.Biblioteca.Entidades.Genero.Criar(Guid.NewGuid(), "Aventura")
            };
            var jogo = Fgc.Domain.Biblioteca.Entidades.Jogo.Criar("Jogo de Teste", 59.99m, 2023, "Desenvolvedora Teste", generos);
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca de Teste");
            
            var jogoRepositoryMock = new Mock<IJogoRepository>();

            // Mockando o método de obter jogo por ID
            jogoRepositoryMock.Setup(repo => repo.ObterPorId(jogo.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogo);           
            
            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);

            _bibliotecaRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Criando handler para adicionar jogo na biblioteca
            var _adicionarJogoBiblioteca = new Bibliotecas.AdicionarJogo.Handler(_bibliotecaRepositoryMock.Object, jogoRepositoryMock.Object);

            // Adicionando o jogo na biblioteca antes de remover
            await _adicionarJogoBiblioteca.Handle(new Bibliotecas.AdicionarJogo.Command(biblioteca.Id.ToString(), jogo.Id.ToString()), CancellationToken.None);

            // Act
            var resultado = await _removerJogoBiblioteca.Handle(new Bibliotecas.RemoverJogo.Command(biblioteca.Id.ToString(), jogo.Id.ToString()), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.True(biblioteca.Jogos.Count == 0);
            _bibliotecaRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task RemoverJogoBiblioteca_DeveRetornarErroQuandoJogoNaoExistirNaBiblioteca()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Biblioteca de Teste");
            var jogoId = Guid.NewGuid().ToString(); 

            _bibliotecaRepositoryMock.Setup(repo => repo.ObterPorId(biblioteca.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(biblioteca);

            // Act
            var resultado = await _removerJogoBiblioteca.Handle(new Bibliotecas.RemoverJogo.Command(biblioteca.Id.ToString(), jogoId), CancellationToken.None);
            
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("404", resultado.Error.Code);
            Assert.Equal("Jogo não encontrado na biblioteca.", resultado.Error.Message);
        }

    }
}

