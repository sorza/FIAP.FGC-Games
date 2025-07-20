using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar
{
    public class Validator : AbstractValidator<Query>
    {
        public Validator() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("A consulta não pode ser nula.");
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do gênero é obrigatório.")
                .NotNull().WithMessage("O ID do gênero não pode ser nulo.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID do gênero deve ser um GUID válido.");
        }
    }
}
