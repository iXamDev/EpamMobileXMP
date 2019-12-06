using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FlexiMvvm.Operations;
using System;

namespace XMP.Core.Operations
{
    public class PreventRepetitiveExecutionsCondition : OperationCondition
    {
        public override Task<bool> CheckAsync(OperationContext context, CancellationToken cancellationToken)
        {
            lock (context.CustomData)
            {
                var running = PreventRepetitiveExecutionsHelper.IsRunning(context.CustomData);

                if (!running)
                {
                    PreventRepetitiveExecutionsHelper.SetIsRunning(context.CustomData, true);

                    return Task.FromResult(true);
                }

                throw new OperationCanceledException();
            }
        }
    }
}