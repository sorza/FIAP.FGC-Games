using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Biblioteca;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class BibliotecaTests
    {
        [Fact]
        public void CriarBiblioteca_ContaIdValido_DeveRetornarBiblioteca()
        {
            // Arrange
            Guid contaId = Guid.NewGuid();

            // Act
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(contaId,"Titulo");

            // Assert
            Assert.NotNull(biblioteca);
            Assert.Equal(contaId, biblioteca.ContaId);
        }

        [Fact]
        public async void CriarBiblioteca_ContaIdVazio_DeveLancarContaIdVazioException()
        {
            // Arrange
            Guid contaId = Guid.Empty;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ContaIdVazioException>(() 
                => Task.FromResult(Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(contaId, "Titulo")));

            Assert.Equal(MensagemDeErro.Biblioteca.ContaIdVazio, exception.Message);
        }

        [Fact]
        public void AdicionarJogo_JogoValido_DeveAdicionarJogoNaBiblioteca()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Titulo");
            var jogo = Jogo.Criar("Test Game", 59.99m, DateTime.UtcNow.Year, "Test Developer", new List<Genero> { Genero.Criar("Ação"), Genero.Criar("Aventura")});
            
            // Act
            biblioteca.AdicionarJogo(jogo);
            
            // Assert
            Assert.Contains(biblioteca.Jogos, j => j.JogoId == jogo.Id);
        }

        [Fact]
        public void AdicionarJogo_JogoNulo_DeveLancarJogoNuloException()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(),"Titulo");
            
            // Act & Assert
            var exception = Assert.Throws<JogoNuloException>(() => biblioteca.AdicionarJogo(null));
            Assert.Equal(MensagemDeErro.Biblioteca.JogoNulo, exception.Message);
        }

        [Fact]
        public void AtualizarTitulo_TituloValido_DeveAtualizarTituloDaBiblioteca()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Titulo Antigo");
            string novoTitulo = "Novo Titulo";
            // Act
            biblioteca.AtualizarTitulo(novoTitulo);
            // Assert
            Assert.Equal(novoTitulo, biblioteca.Titulo);
        }

        [Fact]
        public void AtualizarTitulo_TituloNuloOuVazio_DeveLancarTituloNuloOuVazioException()
        {
            // Arrange
            var biblioteca = Fgc.Domain.Biblioteca.Entidades.Biblioteca.Criar(Guid.NewGuid(), "Titulo Antigo");
            
            // Act & Assert
            var exception = Assert.Throws<TituloNuloOuVazioException>(() => biblioteca.AtualizarTitulo(string.Empty));
            Assert.Equal(MensagemDeErro.Biblioteca.TituloNuloOuVazio, exception.Message);
        }
    }
}
