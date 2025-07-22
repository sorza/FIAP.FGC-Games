using Fgc.Domain.Biblioteca.Entidades;
using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Jogo;

namespace Fgc.Tests.Domain.Biblioteca.Entidades
{
    public class JogoTests
    {
        [Fact]
        public void CriarJogo_Valido_DeveRetornarJogo()
        {
            // Arrange
            string nome = "The Legend of Zelda: Breath of the Wild";
            string desenvolvedora = "Nintendo";
            int anoLancamento = 2000;
            var generos = new List<Genero>();
            var genero =Genero.Criar("Aventura");
            generos.Add(genero);

            // Act
            var jogo = Jogo.Criar(nome,50,anoLancamento, desenvolvedora, generos);

            // Assert
            Assert.NotNull(jogo);
            Assert.Equal(nome, jogo.Titulo);
            Assert.Equal(desenvolvedora, jogo.Desenvolvedora);
            Assert.Equal(anoLancamento, jogo.AnoLancamento);
            Assert.Equal(generos, jogo.Generos);
        }

        [Fact]
        public void CriarJogo_TituloNuloOuVazio_DeveLancarTituloNuloOuVazioException()
        {
            // Arrange
            string titulo = string.Empty;
            string desenvolvedora = "Nintendo";
            int anoLancamento = 2002;
            var generos = new List<Genero>();
            var genero = Genero.Criar("Aventura");
            generos.Add(genero);

            // Act & Assert
            var exception = Assert.Throws<TituloNuloOuVazioException>(() => Jogo.Criar(titulo, 50, anoLancamento, desenvolvedora, generos));
            Assert.Equal(MensagemDeErro.Jogo.TituloNuloOuVazio, exception.Message);
        }

        [Fact]
        public void CriarJogo_PrecosNegativo_DeveLancarPrecoNegativoException()
        {
            // Arrange
            string titulo = "The Legend of Zelda: Breath of the Wild";
            string desenvolvedora = "Nintendo";
            int anoLacamento = 2000;
            var generos = new List<Genero>();
            var genero = Genero.Criar("Aventura");
            generos.Add(genero);
            // Act & Assert
            var exception = Assert.Throws<PrecoNegativoException>(() => Jogo.Criar(titulo, -10, anoLacamento, desenvolvedora, generos));
            Assert.Equal(MensagemDeErro.Jogo.PrecoNegativo, exception.Message);
        }

        [Fact]
        public void CriarJogo_DataLancamentoFutura_DeveLancarDataLancamentoFuturaException()
        {
            // Arrange
            string titulo = "The Legend of Zelda: Breath of the Wild";
            string desenvolvedora = "Nintendo";
            int anoLancamento = 2030;
            var generos = new List<Genero>();
            var genero = Genero.Criar("Aventura");
            generos.Add(genero);
            // Act & Assert
            var exception = Assert.Throws<DataLancamentoFuturaException>(() => Jogo.Criar(titulo, 50, anoLancamento, desenvolvedora, generos));
            Assert.Equal(MensagemDeErro.Jogo.DataLancamentoFutura, exception.Message);
        }

        [Fact]
        public void CriarJogo_GeneroObrigatorio_DeveLancarGeneroObrigatorioException()
        {
            // Arrange
            string titulo = "The Legend of Zelda: Breath of the Wild";
            string desenvolvedora = "Nintendo";
            int anoLancamento = 2000;
            var generos = new List<Genero>();
            // Act & Assert
            var exception = Assert.Throws<GeneroObrigatorioException>(() => Jogo.Criar(titulo, 50, anoLancamento, desenvolvedora, generos));
            Assert.Equal(MensagemDeErro.Jogo.GeneroObrigatorio, exception.Message);
        }

        [Fact]
        public void CriarJogo_DesenvolvedoraNulaOuVazia_DeveLancarDesenvolvedoraNulaOuVaziaException()
        {
            // Arrange
            string titulo = "The Legend of Zelda: Breath of the Wild";
            string desenvolvedora = string.Empty;
            int anoLancamento = 2000;
            var generos = new List<Genero>();
            var genero = Genero.Criar("Aventura");
            generos.Add(genero);
            // Act & Assert
            var exception = Assert.Throws<DesenvolvedoraNulaOuVaziaException>(() => Jogo.Criar(titulo, 50, anoLancamento, desenvolvedora, generos));
            Assert.Equal(MensagemDeErro.Jogo.DesenvolvedoraNulaOuVazia, exception.Message);
        }

        [Fact]
        public void AdicionarGenero_JogoComGeneroExistente_NaoDeveAdicionar()
        {
            // Arrange
            var genero = Genero.Criar("Aventura");
            var jogo = Jogo.Criar("The Legend of Zelda: Breath of the Wild", 50, 2000, "Nintendo", new List<Genero> { genero });
            // Act
            jogo.AdicionarGenero(genero);
            // Assert
            Assert.Single(jogo.Generos);
        }

        [Fact]
        public void AdicionarGenero_JogoComGeneroNovo_DeveAdicionar()
        {
            // Arrange
            var genero1 = Genero.Criar("Aventura");
            var genero2 = Genero.Criar("Ação");
            var jogo = Jogo.Criar("The Legend of Zelda: Breath of the Wild", 50, 2000, "Nintendo", new List<Genero> { genero1 });
            // Act
            jogo.AdicionarGenero(genero2);
            // Assert
            Assert.Equal(2, jogo.Generos.Count);
            Assert.Contains(genero2, jogo.Generos);
        }

        [Fact]
        public void RemoverGenero_JogoComGeneroExistente_DeveRemover()
        {
            // Arrange
            var genero = Genero.Criar("Aventura");
            var jogo = Jogo.Criar("The Legend of Zelda: Breath of the Wild", 50, 2000, "Nintendo", new List<Genero> { genero });
            // Act
            jogo.RemoverGenero(genero);
            // Assert
            Assert.Empty(jogo.Generos);
        }          

        [Fact]
        public void Atualizar_Jogo_DeveAtualizarPropriedades()
        {
            // Arrange
            var genero = Genero.Criar("Aventura");
            var jogo = Jogo.Criar("The Legend of Zelda: Breath of the Wild", 50, 2000, "Nintendo", new List<Genero> { genero });

            var novoTitulo = "The Legend of Zelda: Tears of the Kingdom";
            var novoPreco = 60m;
            var novaDataLancamento = 2025;
            var novaDesenvolvedora = "Nintendo EPD";
            var novosGeneros = new List<Genero> { Genero.Criar("Aventura"), Genero.Criar("RPG") };
            // Act
            jogo.Atualizar(novoTitulo, novoPreco, novaDataLancamento, novaDesenvolvedora, novosGeneros);
            // Assert
            Assert.Equal(novoTitulo, jogo.Titulo);
            Assert.Equal(novoPreco, jogo.Preco);
            Assert.Equal(novaDataLancamento, jogo.AnoLancamento);
            Assert.Equal(novaDesenvolvedora, jogo.Desenvolvedora);
            Assert.Equal(novosGeneros.Count, jogo.Generos.Count);
        }        
    }
}
