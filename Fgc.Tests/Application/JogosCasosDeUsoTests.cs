using Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar;
using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Moq;

namespace Fgc.Tests.Application
{
    public class JogosCasosDeUsoTests
    {
        private readonly Mock<IJogoRepository> _jogoRepositoryMock;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar.Handler _obterJogosCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar.Handler _obterJogoCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar.Handler _criarJogoCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar.Handler _atualizarJogoCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover.Handler _excluirJogoCasoDeUso;

        public JogosCasosDeUsoTests()
        {
            _jogoRepositoryMock = new Mock<IJogoRepository>();
            _obterJogosCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar.Handler(_jogoRepositoryMock.Object);
            _obterJogoCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar.Handler(_jogoRepositoryMock.Object);
            _criarJogoCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar.Handler(_jogoRepositoryMock.Object);
            _atualizarJogoCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar.Handler(_jogoRepositoryMock.Object);
            _excluirJogoCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover.Handler(_jogoRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterJogosCasoDeUso_DeveRetornarListaDeJogos()
        {
            // Arrange         
            var jogos = new List<Jogo>
            {
                Jogo.Criar("Jogo 1", 100, 2000, "Desenvolvedora 1", new List<Genero> { Genero.Criar("Ação") }),
                Jogo.Criar("Jogo 2", 125, 1900, "Desenvolvedora 2", new List<Genero> { Genero.Criar("Aventura"), Genero.Criar("Terror") })
            };

            _jogoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogos);

            // Act
            var result = await _obterJogosCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar.Query(), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Jogos.Count);
        }

        [Fact]
        public async void CriarJogoCasoDeUso_DeveCriarNovoJogo()
        {
            // Arrange
            List<Response> generos = new List<Response> { new Response(Guid.NewGuid(), "Ação"), new Response(Guid.NewGuid(), "Aventura") };

            //Act
            _jogoRepositoryMock.Setup(repo => repo.Cadastrar(It.IsAny<Jogo>(), It.IsAny<CancellationToken>()))
                  .Returns(Task.CompletedTask);

            var resultado = await _criarJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Criar.Command("Novo Jogo", 50, DateTime.Now.Year, "Fiap", generos), CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Novo Jogo", resultado.Value.titulo);
        }

        [Fact]
        public async Task ObterJogoCasoDeUso_DeveRetornarJogoPorId()
        {
            // Arrange

            var jogo = Jogo.Criar("Jogo Teste", 100, DateTime.Now.Year, "Desenvolvedora Teste", new List<Genero> { Genero.Criar("Ação") });

            _jogoRepositoryMock.Setup(repo => repo.ObterPorId(jogo.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogo);

            // Act
            var result = await _obterJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar.Query(jogo.Id.ToString()), CancellationToken.None);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Jogo Teste", result.Value.Titulo);
            Assert.Equal(100, result.Value.Preco);
            Assert.Equal("Desenvolvedora Teste", result.Value.Desenvolvedora);

        }

        [Fact]
        public async void AtualizarJogoCasoDeUso_DeveAtualizarJogoExistente()
        {
            // Arrange
            var jogoExistente = Jogo.Criar("Jogo Existente", 100, DateTime.Now.Year, "Desenvolvedora Existente", new List<Genero> { Genero.Criar("Ação") });

            _jogoRepositoryMock.Setup(repo => repo.ObterPorId(jogoExistente.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogoExistente);

            // Act
            var resultado = await _atualizarJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar.Command(jogoExistente.Id.ToString(), "Jogo Atualizado", 150, DateTime.Now.Year, "Nova Desenvolvedora", new List<Response>()), CancellationToken.None);
            
            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Jogo Atualizado", jogoExistente.Titulo);
            Assert.Equal(150, jogoExistente.Preco);
        }

        [Fact]
        public async Task ExcluirJogoCasoDeUso_DeveExcluirJogoExistente()
        {
            // Arrange
            var jogoExistente = Jogo.Criar("Jogo para Excluir", 100, DateTime.Now.Year, "Desenvolvedora para Excluir", new List<Genero> { Genero.Criar("Ação") });

            _jogoRepositoryMock.Setup(repo => repo.VerificaSeJogoExisteAsync(It.IsAny<Jogo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _jogoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(jogoExistente);

            // Act
            var resultado = await _excluirJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover.Command(jogoExistente.Id.ToString()), CancellationToken.None);

            // Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public async void ExcluirJogoCasoDeUso_JogoNaoExistente_DeveRetornarErro()
        {
            // Arrange
            _jogoRepositoryMock.Setup(repo => repo.VerificaSeJogoExisteAsync(It.IsAny<Jogo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var resultado = await _excluirJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover.Command(Guid.NewGuid().ToString()), CancellationToken.None);
            
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("Jogo não encontrado.", resultado.Error.Message);
            Assert.Equal("404", resultado.Error.Code);
        }

        [Fact]
        public async Task AtualizarJogoCasoDeUso_JogoNaoExistente_DeveRetornarErro()
        {
            // Arrange
            _jogoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Jogo)null!);

            // Act
            var resultado = await _atualizarJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar.Command(Guid.NewGuid().ToString(), "Jogo Atualizado", 150, DateTime.Now.Year, "Nova Desenvolvedora", new List<Response>()), CancellationToken.None);
            
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("Jogo não encontrado.", resultado.Error.Message);
            Assert.Equal("404", resultado.Error.Code);
        }

        [Fact]
        public async Task ObterJogoCasoDeUso_JogoNaoExistente_DeveRetornarErro()
        {
            // Arrange
            _jogoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Jogo)null!);

            // Act
            var resultado = await _obterJogoCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar.Query(Guid.NewGuid().ToString()), CancellationToken.None);

            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("Jogo não encontrado.", resultado.Error.Message);
            Assert.Equal("404", resultado.Error.Code);
        }

        [Fact]
        public async Task ObterJogosCasoDeUso_JogosNaoExistentes_DeveRetornarListaVazia()
        {
            // Arrange
            _jogoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Jogo>());
            // Act
            var result = await _obterJogosCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Jogos.Listar.Query(), CancellationToken.None);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value.Jogos);
        }
    }
}
