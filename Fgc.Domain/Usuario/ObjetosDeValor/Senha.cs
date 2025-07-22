using Fgc.Domain.Compartilhado.ObjetosDeValor;
using Fgc.Domain.Usuario.Exceptions.Senha;
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
            if (string.IsNullOrWhiteSpace(password))
                throw new SenhaNulaOuVaziaException("Senha não pode ser vazia.");

            if (password.Length is < MinLength or > MaxLength)
                throw new SenhaInvalidaException(
                    $"Senha deve ter entre {MinLength} e {MaxLength} caracteres.");

            if (!SenhaRegex().IsMatch(password))
                throw new SenhaInvalidaException(
                    "Senha deve conter letras, números e caracteres especiais.");

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
     
        #region Outros
        [GeneratedRegex(Pattern)]
        private static partial Regex SenhaRegex();

        #endregion
    }
}
