using Fgc.Domain.Biblioteca.Exceptions.Genero;
using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Genero : Entidade
    {
        

        #region Construtores
        private Genero() : base(Guid.NewGuid())
        {
           
        }      
        private Genero(Guid id, string nome) : base(id)
        {            
            Nome = nome;
        }
        #endregion

        #region Propriedades

        public string Nome { get; private set; } = string.Empty;

        #endregion

        #region Fábricas

        public static Genero Criar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new GeneroNuloOuVazioException(MensagemDeErro.Genero.NuloOuVazio);

            return new Genero(Guid.NewGuid(), nome);
        }

        #endregion

        #region Sobrecargas        
        public override string ToString() => Nome;

        #endregion

        #region Implicit Operators
        public static implicit operator string(Genero genero) => genero.Nome;
        #endregion
    }
}
