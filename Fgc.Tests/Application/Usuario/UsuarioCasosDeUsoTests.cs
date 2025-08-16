using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Compartilhado.Services;
using Fgc.Domain.Usuario.ObjetosDeValor;
using Moq;
using Conta = Fgc.Application.Usuario.CasosDeUso.Conta;

namespace Fgc.Tests.Application.Usuario
{
    public class UsuarioCasosDeUsoTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new();
        private readonly Mock<IJwtTokenService> _jwtTokenService = new();
        private readonly Conta.Criar.Handler _criarContaHandler;
        private readonly Conta.Autenticar.Handler _autenticarContaHandler;
        public UsuarioCasosDeUsoTests()
        {
            _criarContaHandler = new Conta.Criar.Handler(_usuarioRepositoryMock.Object);
            _autenticarContaHandler = new Conta.Autenticar.Handler(_usuarioRepositoryMock.Object, _jwtTokenService.Object);
        }

        [Fact]
        public async Task CriarConta_DeveCriarNovaContaComSucesso()
        {
            // Arrange
            Conta.Criar.Command cmd = new("Teste Usuario", "Senha@123", "teste@teste.com.br");

            _usuarioRepositoryMock
                .Setup(repo => repo.VerificaSeUsuarioExiste(cmd.email, It.IsAny<CancellationToken>()))
                .Returns(false);

            _usuarioRepositoryMock
                .Setup(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Usuario.Entidades.Conta>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _criarContaHandler.Handle(cmd, CancellationToken.None);

            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.NotNull(resultado.Value);
            Assert.Equal("Teste Usuario", resultado.Value.Nome);
            _usuarioRepositoryMock
                .Verify(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Usuario.Entidades.Conta>(), It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async Task CriarConta_DeveRetornarErroSeUsuarioJaExistir()
        {

            // Arrange
            Conta.Criar.Command cmd = new("Teste Usuario", "Senha@123", "teste@teste.com.br");

            // Simula que o usuário já existe
            _usuarioRepositoryMock
                .Setup(repo => repo.VerificaSeUsuarioExiste(cmd.email, It.IsAny<CancellationToken>()))
                .Returns(true);

            // Deve retornar erro se o usuário já existir
            _usuarioRepositoryMock
                .Setup(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Usuario.Entidades.Conta>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Este usuário já está cadastrado."));

            // Act
            var resultado = await _criarContaHandler.Handle(cmd, CancellationToken.None);

            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.NotNull(resultado.Error);
            Assert.Equal("409", resultado.Error.Code);
            _usuarioRepositoryMock.Verify(repo => repo.Cadastrar(It.IsAny<Fgc.Domain.Usuario.Entidades.Conta>(), It.IsAny<CancellationToken>()), Times.Never);

        }

        [Fact]
        public async Task AutenticarConta_DeveRetornarTokenEValidadeComSucesso()
        {
            // Arrange
            var email = "teste@teste.com";
            var senha = "Senha@123";
            var conta = Fgc.Domain.Usuario.Entidades.Conta.Criar("Teste Usuario", senha, email, Fgc.Domain.Usuario.Enums.ETipoPerfil.Comum);

            _usuarioRepositoryMock
                .Setup(repo => repo.Autenticar(Email.Criar(email), It.IsAny<CancellationToken>()))
                .ReturnsAsync(conta);

            var exp = DateTime.UtcNow.AddMinutes(30);
            _jwtTokenService
                .Setup(service => service.GerarToken(conta))
                .Returns(new TokenInfo("token", exp));

            var cmd = new Conta.Autenticar.Command(email, senha);

            //Act
            var result = await _autenticarContaHandler.Handle(cmd,CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("token", result.Value.TokenDeAcesso);
            _usuarioRepositoryMock.Verify(r => r.Autenticar(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);
            _jwtTokenService.Verify(j => j.GerarToken(conta), Times.Once);

        }
    }
}
