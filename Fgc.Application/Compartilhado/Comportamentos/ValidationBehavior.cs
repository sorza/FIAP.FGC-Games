using Fgc.Application.Compartilhado.CasosDeUso.Abstracoes;
using FluentValidation;
using MediatR;

namespace Fgc.Application.Compartilhado.Comportamentos
{
    public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
                .ToList();

            if (validationErrors.Any())
                throw new ValidationException(validationErrors);

            return await next();
        }
    }
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationError> errors)
            : base("Falha na validação dos dados.")
            => Errors = errors;

        public IEnumerable<ValidationError> Errors { get; }
    }
    
    public sealed record ValidationError(string Propriedade, string Erro);

}
