using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Atualizar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("O comando de atualização da biblioteca não pode ser nulo.");
            
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O id da biblioteca não pode ser vazio.")
                .Must(id => Guid.TryParse(id, out _)).WithMessage("O id da biblioteca deve ser um GUID válido.");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O título da biblioteca não pode ser vazio.")
                .MaximumLength(100)
                .WithMessage("O título da biblioteca não pode exceder 150 caracteres.");
        }
    }
}
