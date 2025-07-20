using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");

            RuleFor(x => x.Genero)
                .NotNull().WithMessage("O comando não pode ser nulo.")
                .NotEmpty().WithMessage("O nome do gênero é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do gênero não pode exceder 100 caracteres.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do gênero é obrigatório.")
                .NotNull().WithMessage("O ID do gênero não pode ser nulo.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID do gênero deve ser um GUID válido.");
        }
    }
}
