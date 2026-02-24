using FluentValidation;
using MediatR;
using Redarbor.Application.Common.Results;
using System.Net;

namespace Redarbor.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators
    ) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
                .Select(d => d.Validate(context))
                .SelectMany(e => e.Errors)
                .Where(d => d is not null)
                .ToList();

        if (failures.Any() && typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var innerType = typeof(TResponse).GetGenericArguments()[0];
            var failure = typeof(Result<>)
                .MakeGenericType(innerType)
                .GetMethod(nameof(Result<object>.Failure), new[] { typeof(string), typeof(int) })!
                .Invoke(null, new object[]
                {
                    string.Join(Environment.NewLine, failures.Select(f => f.ErrorMessage)),
                    (int)HttpStatusCode.BadRequest
                });

            return (TResponse)failure!;
        }
        return await next();
    }
}
