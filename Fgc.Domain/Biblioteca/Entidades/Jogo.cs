﻿using Fgc.Domain.Biblioteca.Exceptions;
using Fgc.Domain.Biblioteca.Exceptions.Jogo;
using Fgc.Domain.Compartilhado.Entidades;

namespace Fgc.Domain.Biblioteca.Entidades
{
    public class Jogo : Entidade
    {
        #region Campos
        private readonly List<Genero> _generos = [];
        #endregion

        #region Construtores
        private Jogo() : base(Guid.NewGuid())
        {
            
        }

        private Jogo(Guid id, string titulo, decimal preco, int anoLancamento, string desenvolvedora, List<Genero> generos) : base(id)
        {
            Titulo = titulo;
            Preco = preco;
            AnoLancamento = anoLancamento;
            _generos = generos;
            Desenvolvedora = desenvolvedora;
        }
        #endregion

        #region Propriedades
        public string Titulo { get; private set; } = string.Empty;      
        public decimal Preco { get; private set; }
        public int AnoLancamento { get; private set; }        
        public string Desenvolvedora { get; private set; } = string.Empty;
        public IReadOnlyCollection<Genero> Generos => _generos;

        #endregion

        #region Fábricas

        public static Jogo Criar(string titulo, decimal preco, int anoLancamento, string desenvolvedora, List<Genero> generos)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new TituloNuloOuVazioException(MensagemDeErro.Jogo.TituloNuloOuVazio);
            if (preco < 0)
                throw new PrecoNegativoException(MensagemDeErro.Jogo.PrecoNegativo);
            if (anoLancamento > DateTime.UtcNow.Year)
                throw new DataLancamentoFuturaException(MensagemDeErro.Jogo.DataLancamentoFutura);
            if (generos == null || generos.Count == 0)
                throw new GeneroObrigatorioException(MensagemDeErro.Jogo.GeneroObrigatorio);
            if (string.IsNullOrWhiteSpace(desenvolvedora))
                throw new DesenvolvedoraNulaOuVaziaException(MensagemDeErro.Jogo.DesenvolvedoraNulaOuVazia);

            return new Jogo(Guid.NewGuid(), titulo, preco, anoLancamento, desenvolvedora, generos);
        }

        #endregion

        #region Overrides

        public override bool Equals(object? obj)
        {
            if( obj is not Jogo jogo)
                return false;
            return Id == jogo.Id && Titulo == jogo.Titulo && Preco == jogo.Preco &&
                   Desenvolvedora == jogo.Desenvolvedora &&
                   _generos.SequenceEqual(jogo._generos);
        }

        public override int GetHashCode() => HashCode.Combine(Id, Titulo, Preco, Desenvolvedora, _generos);

        #endregion

        #region Métodos
        public void AdicionarGenero(Genero genero)
        {
            if (_generos.Contains(genero)) return;
            _generos.Add(genero);
            AtualizarDataAlteracao();
        }
        public void RemoverGenero(Genero genero)
        {
            if (!_generos.Contains(genero)) return;
            _generos.Remove(genero);
            AtualizarDataAlteracao();
        }
        public void Atualizar(string titulo, decimal preco, int anoLancamento, string desenvolvedora, List<Genero> generos)
        {            
            Titulo = titulo;
            Preco = preco;
            AnoLancamento = anoLancamento;
            _generos.Clear();
            _generos.AddRange(generos);
            Desenvolvedora = desenvolvedora;
            AtualizarDataAlteracao();
        }
        public void LimparGeneros()
        {
            _generos.Clear();
            AtualizarDataAlteracao();
        }
        #endregion

    }
}
