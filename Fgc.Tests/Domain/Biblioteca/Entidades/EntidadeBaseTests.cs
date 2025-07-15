using Fgc.Domain.Biblioteca.Entidades;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class EntidadeBaseTests
    {
        [Fact]
        public void CompararGenerosComTitulosIguais_DeveRetornarFalso()
        {
            // Arrange            
            var entidade1 = Genero.Criar("Ação");
            var entidade2 = Genero.Criar("Ação");
            // Act
            bool saoIguais = entidade1 == entidade2;
            // Assert
            Assert.False(saoIguais);
        }

        [Fact]
        public void CompararIdsIguais_DeveRetornarVerdadeiro()
        {
            // Arrange            
            var entidade1 = Genero.Criar("Ação");
            var id = entidade1.Id;

            // Act
            bool saoIguais = entidade1.Equals(id);
            // Assert
            Assert.True(saoIguais);
        }
    }
}
