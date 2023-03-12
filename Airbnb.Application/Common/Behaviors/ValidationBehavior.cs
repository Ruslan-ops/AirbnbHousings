using AutoMapper.Configuration;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
        {
            Console.WriteLine("###### VALL CREATE");

            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("^^^^^ VALIDATION HANDLE");
            var context = new ValidationContext<TRequest>(request);
            var failers = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors).Where(error =>  error != null).ToList();
            if(failers.Any())
            {
                throw new ValidationException(failers);
            }
            return next();
        }
    }
}
