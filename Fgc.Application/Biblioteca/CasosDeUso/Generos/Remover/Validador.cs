using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do gênero é obrigatório.")
                .NotNull().WithMessage("O ID do gênero não pode ser nulo.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID do gênero deve ser um GUID válido.");
        }
    }
}
