using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Tests.Domain.Usuario.ObjetosDeValor
{
    public class EmailTests
    {
        [Fact]
        public void CriarEmail_DeveCriarEmailValido()
        {
            // Arrange
            string emailValido = "alexandre@zordan.com.br";

            // Act
            var email = Email.Criar(emailValido);

            // Assert
            Assert.NotNull(email);
        }

        [Fact]
        public void CriarEmail_Invalido_DeveLancarExcecao()
        {
            // Arrange
            string emailInvalido = "alexandre@zordan";
            // Act && Assert
            var exception = Assert.Throws<Fgc.Domain.Usuario.Exceptions.Email.EmailInvalidoException>(() => Email.Criar(emailInvalido));
            Assert.Equal(Fgc.Domain.Usuario.Exceptions.MensagemDeErro.Email.Invalido, exception.Message);
        }

        [Fact]
        public void CriarEmail_NuloOuVazio_DeveLancarExcecao()
        {
            // Arrange
            string emailNulo = null!;
            // Act && Assert
            var exception = Assert.Throws<Fgc.Domain.Usuario.Exceptions.Email.EmailNuloOuVazioException>(() => Email.Criar(emailNulo));
            Assert.Equal(Fgc.Domain.Usuario.Exceptions.MensagemDeErro.Email.NuloOuVazio, exception.Message);
        }

        [Fact]
        public void CriarEmail_Vazio_DeveLancarExcecao()
        {
            // Arrange
            string emailVazio = string.Empty;
            // Act && Assert
            var exception = Assert.Throws<Fgc.Domain.Usuario.Exceptions.Email.EmailNuloOuVazioException>(() => Email.Criar(emailVazio));
            Assert.Equal(Fgc.Domain.Usuario.Exceptions.MensagemDeErro.Email.NuloOuVazio, exception.Message);
        }

        [Fact]
        public void CriarEmail_Whitespace_DeveLancarExcecao()
        {
            // Arrange
            string emailWhitespace = "   ";
            // Act && Assert
            var exception = Assert.Throws<Fgc.Domain.Usuario.Exceptions.Email.EmailNuloOuVazioException>(() => Email.Criar(emailWhitespace));
            Assert.Equal(Fgc.Domain.Usuario.Exceptions.MensagemDeErro.Email.NuloOuVazio, exception.Message);
        }
    }
}
