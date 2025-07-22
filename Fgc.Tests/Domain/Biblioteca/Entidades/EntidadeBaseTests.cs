using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class EntidadeBaseTests
    {
        [Fact]
        public void CompararGenerosComTitulosIguais_DeveRetornarVerdadeiro()
        {
            // Arrange            
            var entidade1 = Genero.Criar("Ação");
            var entidade2 = Genero.Criar("Ação");
            // Act
            bool saoIguais = entidade1 == entidade2;
            // Assert
            Assert.True(saoIguais);
        }

        [Fact]
        public void DataCriacao_Entidades_AoCriarNaoDeveSerNuloOuVazio()
        {
            // Arrange
            var genero = Genero.Criar("Aventura");
            var jogo = Jogo.Criar("The Legend of Zelda: Breath of the Wild", 50, 2025, "Nintendo", new List<Genero> { genero });
            // Act && Assert            
            Assert.NotEqual(DateTime.MinValue, jogo.DataCriacao);
            Assert.NotEqual(DateTime.MinValue, genero.DataCriacao);
        }
    }
}
