using Android.Views;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Main.Items;

namespace XMP.Droid.Views
{
    public partial class MainDrawerCellViewHolder : RecyclerViewBindableItemViewHolder<MainViewModel, FilterItemVM>
    {
        public MainDrawerCellViewHolder(View itemView)
            : base(itemView)
        {
        }

        public override void Bind(BindingSet<FilterItemVM> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(Title)
                .For(v => v.TextBinding())
                .To(vm => vm.Title);
        }
    }
}
