using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.AdicionarJogo
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(c => c.BibliotecaId)
                .NotEmpty().WithMessage("O ID da biblioteca é obrigatório.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID da biblioteca deve ser um GUID válido.");

            RuleFor(c => c.JogoId)
                .NotEmpty().WithMessage("O ID do jogo é obrigatório.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID do jogo deve ser um GUID válido.");
        }
    }
}
