using FluentValidation;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser um endereço de email válido.");

            RuleFor(x => x.senha)
                .NotNull().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(50).WithMessage("A senha deve ter no máximo 50 caracteres.")
                .Matches(@"^(?=.{8,50}$)(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).*$").WithMessage("A senha deve conter letras, números e caracteres especiais.");
        }
    }
}
