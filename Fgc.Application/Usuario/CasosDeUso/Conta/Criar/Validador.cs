using FluentValidation;

namespace Fgc.Application.Usuario.CasosDeUso.Conta.Criar
{
    public class Validador : AbstractValidator<Command>
    {
        public Validador() 
        {
            RuleFor(x => x)
                .NotNull().WithMessage("O comando não pode ser nulo.");

            RuleFor(c => c.nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

            RuleFor(c => c.senha)
                .NotNull().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .MaximumLength(50).WithMessage("A senha deve ter no máximo 50 caracteres.")
                .Matches(@"^(?=.{8,50}$)(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).*$").WithMessage("A senha deve conter letras, números e caracteres especiais.");

            RuleFor(c => c.email)
                .NotNull().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser um endereço de email válido.");
        }

    }
}
