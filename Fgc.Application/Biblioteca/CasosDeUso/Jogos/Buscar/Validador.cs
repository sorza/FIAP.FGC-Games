using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Buscar
{
    public class Validador : AbstractValidator<Query>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("A consulta não pode ser nula.");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do jogo é obrigatório.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("O ID do jogo deve ser um GUID válido.");
        }
    }
}
