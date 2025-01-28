using FluentValidation;
using MediatR;

namespace RoadSafety.BuildingBlocks.Application.Validation
{
	public class ValidationBehavior<TRequest, TResponse>(
		IEnumerable<IValidator<TRequest>> validators
	) : IPipelineBehavior<TRequest, TResponse>
		where TRequest : notnull
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

		public Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken
		)
		{
			if (!_validators.Any())
				return next();

			var validationResults = _validators.Select(v => v.Validate(request));
			var failures = validationResults.Where(r => !r.IsValid).SelectMany(r => r.Errors);
			var errors = failures.Select(f => new ValidationError(f.PropertyName, f.ErrorMessage));

			return errors.Any() ? throw new ValidationException(errors) : next();
		}
	}
}
