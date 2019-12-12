using FlexiMvvm.Operations;

namespace XMP.Core.Operations
{
    public class PreventRepetitiveExecutionsNotification : OperationNotification
    {
        public PreventRepetitiveExecutionsNotification()
            : base(0, 0, false)
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
