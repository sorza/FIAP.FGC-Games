namespace Fgc.Domain.Biblioteca.Exceptions
{
    public class MensagemDeErro
    {
        #region Properties
        
        public static GeneroErrorMessages Genero { get; } = new();
        public static JogoErrorMessages Jogo { get; } = new();
        public static BibliotecaErrorMessages Biblioteca { get; } = new();

        #endregion

        public class GeneroErrorMessages
        {           
            public string NuloOuVazio { get; } = "O gênero não pode ser nulo ou vazio";
            public string NomeMuitoGrande { get; } = "O nome do gênero não pode ter mais de 100 caracteres";
            public string NomeMuitoPequeno { get; } = "O nome do gênero deve ter pelo menos 2 caracteres";
        }

        public class JogoErrorMessages
        {
            public string TituloNuloOuVazio { get; } = "O título do jogo não pode ser nulo ou vazio";
            public string PrecoNegativo { get; } = "O preço do jogo não pode ser negativo";
            public string DataLancamentoFutura { get; } = "A data de lançamento do jogo não pode ser no futuro";
            public string GeneroObrigatorio{ get; } = "O jogo deve possuir pelo menos um gênero";
            public string DesenvolvedoraNulaOuVazia { get; } = "A desenvolvedora não pode ser nula ou vazia";
        }

        public class BibliotecaErrorMessages
        {
            public string ContaIdVazio { get; } = "O ID da conta não pode ser vazio";
            public string JogoNulo { get; } = "O jogo não pode ser nulo";
            public string JogoNaoEncontrado { get; } = "O jogo não foi encontrado na biblioteca";
            public string JogoJaAdicionado { get; } = "O jogo já está adicionado na biblioteca";
            public string BibliotecaNula { get; } = "A biblioteca não pode ser nula";
            public string TituloNuloOuVazio { get; } = "O título do jogo não pode ser nulo ou vazio";
        }

    }
}
