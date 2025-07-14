namespace Fgc.Domain.Biblioteca.Exceptions
{
    public class MensagemDeErro
    {
        #region Properties
        
        public static GeneroErrorMessages Genero { get; } = new();
        public static JogoErrorMessages Jogo { get; } = new();

        #endregion

        public class GeneroErrorMessages
        {           
            public string NuloOuVazio { get; } = "O gênero não pode ser nulo ou vazio";
        }

        public class JogoErrorMessages
        {
            public string TituloNuloOuVazio { get; } = "O título do jogo não pode ser nulo ou vazio";
            public string PrecoNegativo { get; } = "O preço do jogo não pode ser negativo";
            public string DataLancamentoFutura { get; } = "A data de lançamento do jogo não pode ser no futuro";
            public string GeneroObrigatorio{ get; } = "O jogo deve possuir pelo menos um gênero";
            public string DesenvolvedoraNulaOuVazia { get; } = "A desenvolvedora não pode ser nula ou vazia";
        }
    }
}
