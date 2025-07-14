using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Genero;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class GeneroTests
    {
        [Fact]
        public void CriarGenero_Valido_DeveRetornarGenero()
        {
            // Arrange
            string nome = "Ação";
            // Act
            var genero = Genero.Criar(nome);
            // Assert
            Assert.NotNull(genero);
            Assert.Equal(nome, genero.Nome);
        }

        [Fact]
        public void CriarGenero_NomeNuloOuVazio_DeveLancarGeneroNuloOuVazioException()
        {
            // Arrange
            string nome = string.Empty;
            // Act & Assert
            var exception = Assert.Throws<GeneroNuloOuVazioException>(() => Genero.Criar(nome));
            Assert.Equal(MensagemDeErro.Genero.NuloOuVazio, exception.Message);
        }
    }
}
