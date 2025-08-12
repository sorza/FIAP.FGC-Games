using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Biblioteca.Criar
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O título é obrigatório.")
                .MaximumLength(150)
                .WithMessage($"O título deve ter no máximo 150 caracteres.")
                .MinimumLength(2)
                .WithMessage($"O título deve ter no mínimo 2 caracteres.");

            RuleFor(x => x.ContaId)
                .NotEmpty()
                .WithMessage("A conta associada é obrigatória.");
        }
    }
}
