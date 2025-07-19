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

        [Fact]
        public void CriarGenero_NomeMuitoGrande_DeveLancarGeneroNomeMuitoGrandeException()
        {
            // Arrange
            string nome = new string('A', 101);
            // Act & Assert
            var exception = Assert.Throws<GeneroNomeMuitoGrandeException>(() => Genero.Criar(nome));
            Assert.Equal(MensagemDeErro.Genero.NomeMuitoGrande, exception.Message);
        }

        [Fact]
        public void CriarGenero_NomeMuitoPequeno_DeveLancarGeneroNomeMuitoPequenoException()
        {
            // Arrange
            string nome = "A";
            // Act & Assert
            var exception = Assert.Throws<GeneroNomeMuitoPequenoException>(() => Genero.Criar(nome));
            Assert.Equal(MensagemDeErro.Genero.NomeMuitoPequeno, exception.Message);
        }
    }
}
