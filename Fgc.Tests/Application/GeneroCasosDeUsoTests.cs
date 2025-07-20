using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Moq;

namespace Fgc.Tests.Application
{
    public class GeneroCasosDeUsoTests
    {
       
        private readonly Mock<IGeneroRepository> _generoRepositoryMock;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar.Handler _obterGenerosCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar.Handler _obterGeneroCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Handler _criarGeneroCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar.Handler _atualizarGeneroCasoDeUso;
        private readonly Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover.Handler _excluirGeneroCasoDeUso;

        public GeneroCasosDeUsoTests()
        {
            _generoRepositoryMock = new Mock<IGeneroRepository>();
            _obterGenerosCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar.Handler(_generoRepositoryMock.Object);
            _obterGeneroCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar.Handler(_generoRepositoryMock.Object);
            _criarGeneroCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Handler(_generoRepositoryMock.Object);
            _atualizarGeneroCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar.Handler(_generoRepositoryMock.Object);
            _excluirGeneroCasoDeUso = new Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover.Handler(_generoRepositoryMock.Object);
        }

        [Fact]
        public void CriarGeneroCasoDeUso_DeveCriarNovoGenero()
        {
            // Arrange
            var novoGenero = Genero.Criar("Ação");

            _generoRepositoryMock.Setup(repo => repo.Cadastrar(It.IsAny<Genero>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = _criarGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Command(novoGenero.Nome), CancellationToken.None).Result;
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(novoGenero.Nome, resultado.Value.Genero);
        }

        [Fact]
        public async Task ObterGenerosCasoDeUso_DeveRetornarListaDeGeneros()
        {
            // Arrange
            var generos = new List<Genero>
            {
                Genero.Criar("Ação"),
                Genero.Criar("Aventura")
            };

            _generoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(generos);

            // Act
            var resultado = await _obterGenerosCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar.Query(), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(2, resultado.Value.generos.Count);
        }

        [Fact]
        public async Task ObterGeneroCasoDeUso_DeveRetornarGeneroEspecifico()
        {
            // Arrange
            var genero = Genero.Criar("Ação");
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genero);

            // Act
            var resultado = await _obterGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar.Query(genero.Id.ToString()), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(genero.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task AtualizarGeneroCasoDeUso_DeveAtualizarGeneroExistente()
        {
            // Arrange
            var generoExistente = Genero.Criar("Ação");

            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(generoExistente);

            // Act
            var resultado = await _atualizarGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar.Command(generoExistente.Id.ToString(), "Ação Alterado"), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal("Ação Alterado", resultado.Value.Genero);
        }

        [Fact]
        public void ExcluirGeneroCasoDeUso_DeveExcluirGeneroExistente()
        {
            // Arrange
            var generoExistente = Genero.Criar("Ação");
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(generoExistente);
            // Act
            var resultado = _excluirGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover.Command(generoExistente.Id.ToString()), CancellationToken.None).Result;
            
            // Assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public async void ExcluirGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            // Act
            var resultado = await _excluirGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover.Command(Guid.NewGuid().ToString()), CancellationToken.None);
            
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("404", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public async void AtualizarGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            // Act
            var resultado = await _atualizarGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar.Command(Guid.NewGuid().ToString(), "Ação Alterado"), CancellationToken.None);
            
            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("400", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public async void ObterGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Genero?)null);
            // Act
            var resultado = await _obterGeneroCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar.Query(Guid.NewGuid().ToString()), CancellationToken.None);
            
            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("404", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public void ObterGenerosCasoDeUso_DeveRetornarListaVaziaQuandoNaoHouverGeneros()
        {
            // Arrange
            _generoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Genero>());
            // Act
            var resultado = _obterGenerosCasoDeUso.Handle(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar.Query(), CancellationToken.None).Result;
            
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Empty(resultado.Value.generos);
        }        
    }
}
