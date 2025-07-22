using Fgc.Domain.Biblioteca.Entidades;
using FluentValidation;

namespace Fgc.Application.Biblioteca.CasosDeUso.Jogos.Atualizar
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

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O nome do jogo é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome do jogo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("O preço do jogo deve ser maior que zero.");

            RuleFor(x => x.AnoLancamento)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("A data de lançamento não pode ser no futuro.");

            RuleFor(x => x.Desenvolvedora)
                .NotEmpty()
                .WithMessage("O nome da desenvolvedora é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome da desenvolvedora deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Generos)
                .NotEmpty()
                .WithMessage("Pelo menos um gênero é obrigatório.");

            RuleForEach(x => x.Generos)
                .ChildRules(genero =>
                {
                    genero.RuleFor(g => g.Id)
                        .NotEmpty()
                        .WithMessage("O ID do gênero é obrigatório.");
                    genero.RuleFor(g => g.Genero)
                        .NotEmpty()
                        .WithMessage("O nome do gênero é obrigatório.")
                        .MaximumLength(Genero.NomeMaxLength)
                        .WithMessage($"O nome do gênero deve ter no máximo {Genero.NomeMaxLength} caracteres.")
                        .MinimumLength(Genero.NomeMinLength)
                        .WithMessage($"O nome do gênero deve ter no mínimo {Genero.NomeMinLength} caracteres.");
                });          

        }
    }
}
