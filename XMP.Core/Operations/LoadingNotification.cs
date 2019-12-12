using Acr.UserDialogs;
using FlexiMvvm.Operations;

namespace XMP.Core.Operations
{
    public class LoadingNotification : OperationNotification
    {
        private IProgressDialog _loading;

        public LoadingNotification(int delay, int minDuration, bool isCancelable)
            : base(delay, minDuration, isCancelable)
        {
        }

        private IUserDialogs UserDialogs(OperationContext context)
        => context.DependencyProvider.Get<IUserDialogs>();

        protected override void Hide(OperationContext context, OperationStatus status)
        {
            DisposeLoading();
        }

        private void DisposeLoading()
        {
            _loading?.Dispose();
            _loading = null;
        }

        protected override void Show(OperationContext context)
        {
            DisposeLoading();
            _loading = UserDialogs(context).Loading();
        }
    }
}
