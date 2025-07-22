using Fgc.Domain.Compartilhado.ObjetosDeValor;
using Fgc.Domain.Usuario.Exceptions;
using Fgc.Domain.Usuario.Exceptions.Email;
using System.Text.RegularExpressions;

namespace Fgc.Domain.Usuario.ObjetosDeValor
{
    public sealed partial record Email : ObjetoDeValor
    {
        #region Constantes

        public const int MaxLength = 160;
        public const int MinLength = 6;
        public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        #endregion

        #region Propriedades
        public string Address { get; private set; } = string.Empty;
        #endregion

        #region Construtores

        private Email()
        {

        }

        private Email(string address)
        {
            Address = address;
        }
        #endregion

        #region Fábricas

        public static Email Criar(string address)
        {
            if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
                throw new EmailNuloOuVazioException(MensagemDeErro.Email.NuloOuVazio);

            address = address.Trim();
            address = address.ToLower();

            if (!EmailRegex().IsMatch(address))
                throw new EmailInvalidoException(MensagemDeErro.Email.Invalido);

            return new Email(address);

        }

        #endregion

        #region Operators

        public static implicit operator string(Email email) => email.ToString();


        #endregion

        #region Sobrecargas

        public override string ToString() => Address;

        #endregion

        #region Outros

        [GeneratedRegex(Pattern)]
        private static partial Regex EmailRegex();

        #endregion
    }
}
