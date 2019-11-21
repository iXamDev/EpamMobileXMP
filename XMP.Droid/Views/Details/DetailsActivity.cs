using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Details;
using Android.App;
using Android.OS;
using Android.Views;
using FlexiMvvm.Bindings;

namespace XMP.Droid.Views.Details
{
    [Activity]
    public class DetailsActivity : BindableAppCompatActivity<DetailsViewModel>
    {
        private DetailsActivityViewHolder ViewHolder { get; set; }

        public DetailsActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_details);

            ViewHolder = new DetailsActivityViewHolder(this);
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(ViewHolder.SaveButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.SaveCmd);
        }
    }
}
