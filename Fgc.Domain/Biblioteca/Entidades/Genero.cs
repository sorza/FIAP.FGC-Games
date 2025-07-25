﻿using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Genero;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Genero : Entidade
    {
        #region Campos
        private readonly List<Jogo> _jogos = [];

        public const int NomeMaxLength = 100;
        public const int NomeMinLength = 2;

        #endregion

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
        public IReadOnlyCollection<Jogo> Jogos => _jogos;

        #endregion

        #region Fábricas

        public static Genero Criar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new GeneroNuloOuVazioException(MensagemDeErro.Genero.NuloOuVazio);

            if (nome.Length > NomeMaxLength)
                throw new GeneroNomeMuitoGrandeException(MensagemDeErro.Genero.NomeMuitoGrande);

            if (nome.Length < NomeMinLength)
                throw new GeneroNomeMuitoPequenoException(MensagemDeErro.Genero.NomeMuitoPequeno);

            return new Genero(Guid.NewGuid(), nome);
        }

        public static Genero Criar(Guid id, string nome) => new Genero(id, nome);        

        #endregion

        #region Sobrecargas        
        public override string ToString() => Nome;
        public override bool Equals(object? obj)
        {
            if (obj is not Genero genero)
                return false;
            return Nome == genero.Nome;
        }
        public override int GetHashCode() => Nome.GetHashCode();
        

        #endregion

        #region Implicit Operators
        public static implicit operator string(Genero genero) => genero.Nome;
        #endregion

        #region Métodos
        public void Atualizar(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new GeneroNuloOuVazioException(MensagemDeErro.Genero.NuloOuVazio);
            Nome = nome;
            AtualizarDataAlteracao();
        }       
        #endregion
    }
}
