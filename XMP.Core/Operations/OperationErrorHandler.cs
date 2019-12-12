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
#if DEBUG
            Console.WriteLine($"{nameof(OperationErrorHandler)} : {error?.Exception?.Message}");
#endif
            return Task.FromResult(0);
        }
    }
}
