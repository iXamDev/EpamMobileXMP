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

        protected override void Hide(OperationContext context, OperationStatus status)
        {
            DisposeLoading();
        }

        protected override void Show(OperationContext context)
        {
            DisposeLoading();
            _loading = UserDialogs(context).Loading();
        }

        private IUserDialogs UserDialogs(OperationContext context)
        => context.DependencyProvider.Get<IUserDialogs>();

        private void DisposeLoading()
        {
            _loading?.Dispose();
            _loading = null;
        }
    }
}
