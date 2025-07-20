using Fgc.Domain.Biblioteca.Entidades;
using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");

            RuleFor(g => g.Genero)
                .NotEmpty()
                .WithMessage("O nome do gênero é obrigatório.")
                .MaximumLength(Genero.NomeMaxLength)
                .WithMessage($"O nome do gênero deve ter no máximo {Genero.NomeMaxLength} caracteres.")
                .MinimumLength(Genero.NomeMinLength)
                .WithMessage($"O nome do gênero deve ter no mínimo {Genero.NomeMinLength} caracteres.");
        }
    }
}
