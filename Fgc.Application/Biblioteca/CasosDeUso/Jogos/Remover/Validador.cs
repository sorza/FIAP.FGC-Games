using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Remover
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("O comando não pode ser nulo.");
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do jogo é obrigatório.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("O ID do jogo deve ser um GUID válido.");
        }
    }
}
