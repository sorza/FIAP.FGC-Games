using Fgc.Domain.Usuario.Exceptions;
using Fgc.Domain.Usuario.Exceptions.Senha;
using Fgc.Domain.Usuario.ObjetosDeValor;

namespace Fgc.Tests.Domain.Usuario.ObjetosDeValor
{
    public class SenhaTests
    {
        [Fact]
        public void CriarSenha_Valida()
        {
            //Arrange
            string senhaValida = "134685#ad";

            //Act
            var senha = Senha.Criar(senhaValida);

            //Assert
            Assert.NotNull(senha);
        }

        [Fact]
        public void CriarSenha_Nula_DeveLancarExcessao()
        {
            //Arrange
            string senhaNula = null!;

            //Act && Assert
            var exception = Assert.Throws<SenhaNulaOuVaziaException>(() => Senha.Criar(senhaNula!));
            Assert.Equal(MensagemDeErro.Senha.NulaOuVazia, exception.Message);
        }

        [Fact]
        public void CriarSenha_Vazia_DeveLancarExcessao()
        {
            //Arrange
            string senhaVazia = string.Empty!;

            //Act && Assert
            var exception = Assert.Throws<SenhaNulaOuVaziaException>(() => Senha.Criar(senhaVazia!));
            Assert.Equal(MensagemDeErro.Senha.NulaOuVazia, exception.Message);
        }

        [Fact]
        public void CriarSenha_ComEspacosEmBranco_DeveLancarExcessao()
        {
            //Arrange
            string senhaEspacos = "        ";

            //Act && Assert
            var exception = Assert.Throws<SenhaNulaOuVaziaException>(() => Senha.Criar(senhaEspacos!));
            Assert.Equal(MensagemDeErro.Senha.NulaOuVazia, exception.Message);
        }

        [Fact]
        public void CriarSenha_ComMenosde8Caracteres_DeveLancarExcessao()
        {
            //Arrange
            string senha = "1234567";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_SomenteNumerica_DeveLancarExcessao()
        {
            //Arrange
            string senha = "12345678";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_SomenteAlfabetica_DeveLancarExcessao()
        {
            //Arrange
            string senha = "abcdefghi";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_AlfanumericaSemCaracteresEspeciais_DeveLancarExcessao()
        {
            //Arrange
            string senha = "12345abc";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_NumericaComCaracteresEspeciaisSemLetras_DeveLancarExcessao()
        {
            //Arrange
            string senha = "1234567@";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_AlfabeticaComCaracteresEspeciaisSemNumeros_DeveLancarExcessao()
        {
            //Arrange
            string senha = "abcdefghi@";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

        [Fact]
        public void CriarSenha_ApenasComCaracteresEspeciais_DeveLancarExcessao()
        {
            //Arrange
            string senha = "(*&¨#&¨$#)@";

            //Act && Assert
            var exception = Assert.Throws<SenhaInvalidaException>(() => Senha.Criar(senha));
            Assert.Equal(MensagemDeErro.Senha.Invalida, exception.Message);
        }

    }
}
