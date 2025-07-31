using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Domain.Biblioteca.Entidades;
using Generos = Fgc.Application.Biblioteca.CasosDeUso.Generos;
using Moq;

namespace Fgc.Tests.Application
{
    public class GeneroCasosDeUsoTests
    {
       
        private readonly Mock<IGeneroRepository> _generoRepositoryMock;
        private readonly Generos.Listar.Handler _obterGenerosCasoDeUso;
        private readonly Generos.Buscar.Handler _obterGeneroCasoDeUso;
        private readonly Generos.Criar.Handler _criarGeneroCasoDeUso;
        private readonly Generos.Atualizar.Handler _atualizarGeneroCasoDeUso;
        private readonly Generos.Remover.Handler _excluirGeneroCasoDeUso;

        public GeneroCasosDeUsoTests()
        {
            _generoRepositoryMock = new Mock<IGeneroRepository>();
            _obterGenerosCasoDeUso = new Generos.Listar.Handler(_generoRepositoryMock.Object);
            _obterGeneroCasoDeUso = new Generos.Buscar.Handler(_generoRepositoryMock.Object);
            _criarGeneroCasoDeUso = new Generos.Criar.Handler(_generoRepositoryMock.Object);
            _atualizarGeneroCasoDeUso = new Generos.Atualizar.Handler(_generoRepositoryMock.Object);
            _excluirGeneroCasoDeUso = new Generos.Remover.Handler(_generoRepositoryMock.Object);
        }

        [Fact]
        public async void CriarGeneroCasoDeUso_DeveCriarNovoGenero()
        {
            // Arrange           
            // Configura o mock do repositório para retornar sucesso ao cadastrar o gênero
            _generoRepositoryMock.Setup(repo => repo.Cadastrar(It.IsAny<Genero>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            // Chama o caso de uso para criar o gênero
            var resultado = await _criarGeneroCasoDeUso.Handle(new Generos.Criar.Command("Ação"), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido e se o gênero criado é o mesmo que o esperado
            Assert.True(resultado.IsSuccess);
            Assert.Equal("Ação", resultado.Value.Genero);
        }

        [Fact]
        public async Task ObterGenerosCasoDeUso_DeveRetornarListaDeGeneros()
        {
            // Arrange
            // Cria uma lista de gêneros para simular o retorno do repositório
            var generos = new List<Genero>
            {
                Genero.Criar("Ação"),
                Genero.Criar("Aventura")
            };

            // Configura o mock do repositório para retornar a lista de gêneros
            _generoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(generos);

            // Act
            // Chama o caso de uso para obter a lista de gêneros
            var resultado = await _obterGenerosCasoDeUso.Handle(new Generos.Listar.Query(), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido e se a lista de gêneros contém a quantidade de generos esperada
            Assert.True(resultado.IsSuccess);
            Assert.Equal(2, resultado.Value.generos.Count);
        }

        [Fact]
        public async Task ObterGeneroCasoDeUso_DeveRetornarGeneroEspecifico()
        {
            // Arrange
            // Cria um gênero específico para simular o retorno do repositório
            var genero = Genero.Criar("Ação");
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(genero);

            // Act
            // Chama o caso de uso para obter o gênero específico
            var resultado = await _obterGeneroCasoDeUso.Handle(new Generos.Buscar.Query(genero.Id.ToString()), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido e se o gênero retornado é o mesmo que o esperado
            Assert.True(resultado.IsSuccess);
            Assert.Equal(genero.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task AtualizarGeneroCasoDeUso_DeveAtualizarGeneroExistente()
        {
            // Arrange
            // Cria um gênero existente para simular o retorno do repositório
            var generoExistente = Genero.Criar("Ação");

            // Configura o mock do repositório para verificar se o gênero existe
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Configura o mock do repositório para retornar o gênero existente ao buscar por ID
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(generoExistente);

            // Act
            // Chama o caso de uso para atualizar o gênero
            var resultado = await _atualizarGeneroCasoDeUso.Handle(new Generos.Atualizar.Command(generoExistente.Id.ToString(), "Ação Alterado"), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido e se o gênero atualizado é o mesmo que o esperado
            Assert.True(resultado.IsSuccess);
            Assert.Equal("Ação Alterado", resultado.Value.Genero);
        }

        [Fact]
        public async void ExcluirGeneroCasoDeUso_DeveExcluirGeneroExistente()
        {
            // Arrange
            // Cria um gênero existente para simular o retorno do repositório
            var generoExistente = Genero.Criar("Ação");

            // Configura o mock do repositório para verificar se o gênero existe
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Configura o mock do repositório para retornar o gênero existente ao buscar por ID
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(generoExistente);

            // Act
            // Chama o caso de uso para excluir o gênero
            var resultado = await _excluirGeneroCasoDeUso.Handle(new Generos.Remover.Command(generoExistente.Id.ToString()), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public async void ExcluirGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            // Configura o mock do repositório para verificar se o gênero existe e retornar falso
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            // Chama o caso de uso para excluir um gênero que não existe
            var resultado = await _excluirGeneroCasoDeUso.Handle(new Generos.Remover.Command(Guid.NewGuid().ToString()), CancellationToken.None);

            // Assert
            // Verifica se o resultado é um erro e se o código e a mensagem de erro são os esperados
            Assert.False(resultado.IsSuccess);
            Assert.Equal("404", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public async void AtualizarGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            // Configura o mock do repositório para verificar se o gênero existe e retornar falso
            _generoRepositoryMock.Setup(repo => repo.VerificaSeGeneroExisteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            // Chama o caso de uso para atualizar um gênero que não existe
            var resultado = await _atualizarGeneroCasoDeUso.Handle(new Generos.Atualizar.Command(Guid.NewGuid().ToString(), "Ação Alterado"), CancellationToken.None);

            // Assert
            // Verifica se o resultado é um erro e se o código e a mensagem de erro são os esperados
            Assert.False(resultado.IsSuccess);
            Assert.Equal("400", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public async void ObterGeneroCasoDeUso_DeveRetornarErroQuandoGeneroNaoExistir()
        {
            // Arrange
            // Configura o mock do repositório para retornar nulo quando buscar por um ID de gênero inexistente
            _generoRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Genero?)null);

            // Act
            // Chama o caso de uso para obter um gênero que não existe
            var resultado = await _obterGeneroCasoDeUso.Handle(new Generos.Buscar.Query(Guid.NewGuid().ToString()), CancellationToken.None);

            // Assert
            // Verifica se o resultado é um erro e se o código e a mensagem de erro são os esperados
            Assert.True(resultado.IsFailure);
            Assert.Equal("404", resultado.Error!.Code);
            Assert.Equal("Este gênero não existe.", resultado.Error.Message);
        }

        [Fact]
        public async void ObterGenerosCasoDeUso_DeveRetornarListaVaziaQuandoNaoHouverGeneros()
        {
            // Arrange
            // Configura o mock do repositório para retornar uma lista vazia de gêneros
            _generoRepositoryMock.Setup(repo => repo.ObterTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Genero>());

            // Act
            // Chama o caso de uso para obter a lista de gêneros
            var resultado = await _obterGenerosCasoDeUso.Handle(new Generos.Listar.Query(), CancellationToken.None);

            // Assert
            // Verifica se o resultado foi bem-sucedido e se a lista de gêneros está vazia
            Assert.True(resultado.IsSuccess);
            Assert.Empty(resultado.Value.generos);
        }        
    }
}
