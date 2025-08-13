using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Bibliotecas.Buscar
{
    public class Validador : AbstractValidator<Query>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("A consulta não pode ser nula.")
                .Must(x => !string.IsNullOrWhiteSpace(x.Id))
                .WithMessage("O ID da biblioteca não pode estar vazio ou em branco.");

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("O ID da biblioteca é obrigatório.")
               .NotNull().WithMessage("O ID da biblioteca não pode ser nulo.")
               .Must(id => Guid.TryParse(id, out _)).WithMessage("O ID da biblioteca deve ser um GUID válido.");

        }
    }
}
