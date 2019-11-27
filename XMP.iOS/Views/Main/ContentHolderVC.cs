using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using UIKit;
using XMP.Core.ViewModels.Main;
using XMP.iOS.Views.Main.Cells;

namespace XMP.iOS.Views.Main
{
    public class ContentHolderVC : UIViewController
    {
        public new ContentView View
        {
            get => (ContentView)base.View;
            set => base.View = value;
        }

        private TableViewObservablePlainSource ContentItemsSource { get; set; }

        public override void LoadView()
        {
            View = new ContentView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ContentItemsSource = new TableViewObservablePlainSource(View.ContentTableView, (arg) => ContentVacationRequestItemTableViewCell.CellId);

            View.ContentTableView.Source = ContentItemsSource;
        }

        public void Bind(BindingSet<MainViewModel> bindingSet, MainViewModel viewModel)
        {
            ContentItemsSource.ItemsContext = viewModel;

            bindingSet.Bind(ContentItemsSource)
                .For(v => v.ItemsBinding())
                .To(vm => vm.RequestItems);

            bindingSet.Bind(ContentItemsSource)
                .For(v => v.RowSelectedBinding())
                .To(vm => vm.FilterCmd);
        }
    }
}
