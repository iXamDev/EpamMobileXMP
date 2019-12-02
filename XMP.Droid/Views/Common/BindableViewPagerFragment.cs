using System;
using Android.Support.V4.App;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.Core;

namespace XMP.Droid.Views.Common
{
    public abstract class BindableViewPagerFragment<TViewModel> : Fragment
        where TViewModel : class
    {
        public TViewModel ViewModel { get; set; }

        public BindingSet<TViewModel> BindingSet { get; private set; }

        public abstract void Bind(BindingSet<TViewModel> bindingSet);

        public override void OnStart()
        {
            base.OnStart();

            if (BindingSet == null)
            {
                var bindingSet = new BindingSet<TViewModel>(ViewModel);

                Bind(bindingSet);

                BindingSet = bindingSet;

                bindingSet.Apply();
            }
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            BindingSet?.Dispose();

            BindingSet = null;
        }
    }
}
