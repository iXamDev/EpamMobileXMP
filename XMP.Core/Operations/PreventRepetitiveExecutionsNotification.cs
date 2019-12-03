using System;
using System.Runtime.CompilerServices;
using FlexiMvvm.Operations;
using JetBrains.Annotations;

namespace XMP.Core.Operations
{
    public class PreventRepetitiveExecutionsNotification : OperationNotification
    {
        public PreventRepetitiveExecutionsNotification() : base(0, 0, true)
        {
        }

        protected override void Hide(OperationContext context, OperationStatus status)
        {
            lock (context.CustomData)
                PreventRepetitiveExecutionsHelper.SetIsRunning(context.CustomData, false);
        }

        protected override void Show(OperationContext context)
        {
            lock (context.CustomData)
                PreventRepetitiveExecutionsHelper.SetIsRunning(context.CustomData, true);
        }
    }
}
