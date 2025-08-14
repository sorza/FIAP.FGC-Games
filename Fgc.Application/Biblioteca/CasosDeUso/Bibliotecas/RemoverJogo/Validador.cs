using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.RemoverJogo
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x.JogoId)
                .NotEmpty()
                .WithMessage("O ID do jogo não pode ser vazio.")
                .NotNull()
                .WithMessage("O ID do jogo não pode ser nulo.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("O ID do jogo deve ser um GUID válido.");

            RuleFor(x => x.BibliotecaId)
                .NotEmpty()
                .WithMessage("O ID da biblioteca não pode ser vazio.")
                .NotNull()
                .WithMessage("O ID da biblioteca não pode ser nulo.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("O ID da biblioteca deve ser um GUID válido.");
        }
    }
}
