namespace Fgc.Domain.Usuario.Exceptions
{
    public class MensagemDeErro
    {
        #region Properties
        public static EmailErrorMessages Email { get; } = new();
        public static SenhaErrorMessages Senha { get; } = new();
        public static ContaErrorMessages Conta { get; } = new();

        #endregion       

        public class EmailErrorMessages
        {
            public string NuloOuVazio { get; } = "O email deve ser informado";
            public string Invalido { get; } = "O email informado é inválido";
        }

        public class SenhaErrorMessages
        {
            public string NulaOuVazia { get; } = "A senha deve ser informada";
            public string Invalida { get; } = "A senha informada é inválido";
        }

        public class ContaErrorMessages
        {
            public string NomeNuloOuVazio { get; } = "O nome deve ser informado";         
            public string TipoPerfilInvalido { get; } = "Tipo de perfil inválido";
        }
    }
}
