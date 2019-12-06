using FlexiMvvm.Operations;
using Acr.UserDialogs;

namespace XMP.Core.Operations
{
    public class LoadingNotification : OperationNotification
    {
        public LoadingNotification(int delay, int minDuration, bool isCancelable) : base(delay, minDuration, isCancelable)
        {
        }

        private IProgressDialog loading;

        private IUserDialogs UserDialogs(OperationContext context)
        => context.DependencyProvider.Get<IUserDialogs>();

        protected override void Hide(OperationContext context, OperationStatus status)
        {
            DisposeLoading();
        }

        private void DisposeLoading()
        {
            loading?.Dispose();
            loading = null;
        }

        protected override void Show(OperationContext context)
        {
            DisposeLoading();
            loading = UserDialogs(context).Loading();
        }
    }
}