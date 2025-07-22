using Fgc.Domain.Compartilhado.ObjetosDeValor;
using Fgc.Domain.Usuario.Exceptions;
using Fgc.Domain.Usuario.Exceptions.Senha;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Fgc.Domain.Usuario.ObjetosDeValor
{
    public sealed partial record Senha : ObjetoDeValor
    {
        #region Constantes

        public const int MaxLength = 50;
        public const int MinLength = 8;
        public const string Pattern = @"^(?=.{8,50}$)(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).*$";

        #endregion

        #region Propriedades     
        public string Hash { get; private set; } = string.Empty;
        #endregion

        #region Construtores

        private Senha()
        {

        }

        private Senha(string hash) => Hash = hash;

        #endregion

        #region Fábricas
        public static Senha Criar(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password == string.Empty)
                throw new SenhaNulaOuVaziaException(MensagemDeErro.Senha.NulaOuVazia);

            if (password.Length is < MinLength or > MaxLength)
                throw new SenhaInvalidaException(MensagemDeErro.Senha.Invalida);

            if (!SenhaRegex().IsMatch(password))
                throw new SenhaInvalidaException(MensagemDeErro.Senha.Invalida);

            var hasher = new Rfc2898DeriveBytes(
                                password,
                                saltSize: 16,
                                iterations: 100_000,
                                HashAlgorithmName.SHA256);

            var salt = hasher.Salt;
            var hashed = hasher.GetBytes(32);
            var result = Convert.ToBase64String(salt.Concat(hashed).ToArray());

            return new Senha(result);

        }

        #endregion

        #region Métodos
        public bool Verificar(string password)
        {
            var data = Convert.FromBase64String(Hash);
            var salt = data[..16];
            var hashed = data[16..];

            using var hasher = new Rfc2898DeriveBytes(
                password, salt, 100_000, HashAlgorithmName.SHA256);

            return hasher.GetBytes(32).SequenceEqual(hashed);
        }
        #endregion

        #region Operators

        public static implicit operator string(Senha senha) => senha.ToString();


        #endregion

        #region Sobrecargas

        public override string ToString() => Hash;

        #endregion

        #region Outros
        [GeneratedRegex(Pattern)]
        private static partial Regex SenhaRegex();

        #endregion
    }
}
