using Fgc.Domain.Compartilhado.Entidades;
using Fgc.Domain.Usuario.Enums;
using Fgc.Domain.Usuario.ObjetosDeValor;
using Fgc.Domain.Usuario.Exceptions.Conta;
using Fgc.Domain.Usuario.Exceptions;

namespace Fgc.Domain.Usuario.Entidades
{
    public class Conta : Entidade
    {
        #region Construtores
        private Conta() : base(Guid.NewGuid())
        {

        }

        private Conta(Guid id, string nome, Senha senha, Email email, ETipoPerfil tipoPerfil) : base(id)
        {
            Nome = nome;
            Senha = senha;
            Email = email;
            Perfil = tipoPerfil;
        }

        #endregion

        #region Propriedades
        public string Nome { get; private set; } = string.Empty;
        public Senha Senha { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public ETipoPerfil Perfil { get; private set; }
        public bool Ativo { get; private set; } = true;

        #endregion

        #region Fábricas

        public static Conta Criar(string nome, string senha, string email, ETipoPerfil tipoPerfil)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new NomeNuloOuVazioException(MensagemDeErro.Conta.NomeNuloOuVazio);   

            if (!Enum.IsDefined(typeof(ETipoPerfil), tipoPerfil))
                throw new PerfilInvalidoException(MensagemDeErro.Conta.TipoPerfilInvalido);

            var senha_result = Senha.Criar(senha);
            var email_result = Email.Criar(email);

            return new Conta(Guid.NewGuid(), nome, senha_result, email_result, tipoPerfil);
        }

        #endregion
    }
}
