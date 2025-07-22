using Fgc.Domain.Usuario.Exceptions;
using Fgc.Domain.Usuario;
using Fgc.Domain.Usuario.Entidades;
using Fgc.Domain.Usuario.Enums;
using Fgc.Domain.Usuario.Exceptions.Conta;
using Fgc.Domain.Usuario.Exceptions.Email;
using Fgc.Domain.Usuario.Exceptions.Senha;

namespace Fgc.Tests.Domain.Usuario.Entidades
{
    public class ContaTests
    {
        [Fact]
        public void CriarConta_Valida()
        {
            //Arrange
            string nome = "João da Silva";
            string email = "joao@dasilva.com";
            string senha = "123456#ad";

            //Act
            var conta = Conta.Criar(nome, senha, email, ETipoPerfil.Comum);

            //Assert
            Assert.NotNull(conta);
        }

        [Fact]
        public void CriarConta_NomeVazio_DeveLancarExcessao()
        {
            //Arrange
            string nome = string.Empty;
            string email = "joao@dasilva.com";
            string senha = "123456#ad";

            //Act
            var exception = Assert.Throws<NomeNuloOuVazioException>(() => Conta.Criar(nome, senha, email, ETipoPerfil.Comum));
            Assert.Equal(exception.Message, MensagemDeErro.Conta.NomeNuloOuVazio);
        }       

        [Fact]
        public void CriarConta_NomeNulo_DeveLancarExcessao()
        {
            //Arrange
            string nome = null!;
            string email = "joao@dasilva.com";
            string senha = "123456#ad";

            //Act
            var exception = Assert.Throws<NomeNuloOuVazioException>(() => Conta.Criar(nome, senha, email, ETipoPerfil.Comum));
            Assert.Equal(exception.Message, MensagemDeErro.Conta.NomeNuloOuVazio);
        }

        [Fact]
        public void CriarConta_EmailInvalido_DeveLancarExcessao()
        {
            //Arrange
            string nome = "João da Silva";
            string email = "joao@dasilva";
            string senha = "123456#ad";

            //Act
            var exception = Assert.Throws<EmailInvalidoException>(() => Conta.Criar(nome, senha, email, ETipoPerfil.Comum));
            Assert.Equal(exception.Message, MensagemDeErro.Email.Invalido);
        }

        [Fact]
        public void CriarConta_SenhaInvalida_DeveLancarExcessao()
        {
            //Arrange
            string nome = "João da Silva";
            string email = "joao@dasilva";
            string senha = "123456ad";

            //Act
            var exception = Assert.Throws<SenhaInvalidaException>(() => Conta.Criar(nome, senha, email, ETipoPerfil.Comum));
            Assert.Equal(exception.Message, MensagemDeErro.Senha.Invalida);
        }
    }
}
