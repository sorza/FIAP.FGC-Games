using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class BibliotecaJogoTests
    {
        [Fact]
        public void CriarBibliotecaJogo_Valido_DeveRetornarBibliotecaJogo()
        {
            // Arrange
            var jogo = Jogo.Criar("Test Game", 59.99m, DateTime.UtcNow.Year, "Test Developer", new List<Genero> { Genero.Criar("Ação") });
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid());

            // Act
            var bibliotecaJogo = BibliotecaJogo.Criar(biblioteca, jogo, DateTime.Now);

            // Assert
            Assert.NotNull(bibliotecaJogo);
            Assert.Equal(jogo.Id, bibliotecaJogo.JogoId);
            Assert.Equal(jogo, bibliotecaJogo.Jogo);
            Assert.Equal(biblioteca.Id, bibliotecaJogo.BibliotecaId);
            Assert.Equal(biblioteca, bibliotecaJogo.Biblioteca);
            Assert.Equal(jogo.Preco, bibliotecaJogo.ValorPago);
            Assert.True(bibliotecaJogo.DataAquisicao <= DateTime.Now);
            Assert.Equal(biblioteca.ContaId, bibliotecaJogo.Biblioteca.ContaId);
        }

        [Fact]
        public void CriarBibliotecaJogo_BibliotecaNula_DeveLancarBibliotecaNulaException()
        {
            // Arrange
            Jogo jogo = Jogo.Criar("Test Game", 59.99m, DateTime.UtcNow.Year, "Test Developer", new List<Genero> { Genero.Criar("Ação") });

            // Act & Assert
            var exception = Assert.Throws<BibliotecaNulaException>(() => BibliotecaJogo.Criar(null, jogo, DateTime.Now));
            Assert.Equal(MensagemDeErro.Biblioteca.BibliotecaNula, exception.Message);
        }

        [Fact]
        public void CriarBibliotecaJogo_JogoNulo_DeveLancarJogoNuloException()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid());

            // Act & Assert
            var exception = Assert.Throws<JogoNuloException>(() => BibliotecaJogo.Criar(biblioteca, null, DateTime.Now));
            Assert.Equal(MensagemDeErro.Biblioteca.JogoNulo, exception.Message);
        }
    }
}
