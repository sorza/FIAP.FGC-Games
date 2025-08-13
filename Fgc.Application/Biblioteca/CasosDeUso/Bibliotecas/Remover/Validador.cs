using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Remover
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("O comando não pode ser nulo.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da biblioteca não pode ser vazio.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID não é um GUID válido.");
        }
    }
}
