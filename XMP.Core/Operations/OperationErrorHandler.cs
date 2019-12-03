using System;
using System.Threading;
using System.Threading.Tasks;
using FlexiMvvm.Operations;
namespace XMP.Core.Operations
{
    public class OperationErrorHandler : IErrorHandler
    {
        public Task HandleAsync(OperationContext context, OperationError<Exception> error, CancellationToken cancellationToken)
        {
            //TODO
            Console.WriteLine($"{nameof(OperationErrorHandler)} : {error?.Exception?.Message}");

            return Task.FromResult(0);
        }
    }
}
