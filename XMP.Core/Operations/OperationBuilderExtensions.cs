using System;
using FlexiMvvm.Operations;

namespace XMP.Core.Operations
{
    public static class OperationBuilderExtensions
    {
        public static IOperationBuilder WithPreventRepetitiveExecutions(
            this IOperationBuilder operationBuilder)
        {
            return operationBuilder
                .WithCondition(new PreventRepetitiveExecutionsCondition())
                .WithNotification(new PreventRepetitiveExecutionsNotification());
        }

        public static IOperationBuilder WithLoadingNotification(
            this IOperationBuilder operationBuilder)
        {
            return operationBuilder
                .WithNotification(new LoadingNotification(0, 0, false));
        }
    }
}
