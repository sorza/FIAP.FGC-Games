using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("O nome do gênero é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do gênero não pode exceder 100 caracteres.");
        }
    }
}
