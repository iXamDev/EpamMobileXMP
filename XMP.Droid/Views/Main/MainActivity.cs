using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Main;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.V4.View;

namespace XMP.Droid.Views.Main
{
    [Activity(Label = "All Requests")]
    public class MainActivity : BindableAppCompatActivity<MainViewModel>
    {
        private ActionBarDrawerToggle toggle;

        private MainActivityViewHolder ViewHolder { get; set; }

        private void SetupDrawer(DrawerLayout drawer, Toolbar toolbar)
        {
            toggle = new ActionBarDrawerToggle(this, drawer, toolbar, 0, 0);

            drawer.AddDrawerListener(toggle);

            toggle.SyncState();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            ViewHolder = new MainActivityViewHolder(this);

            SetSupportActionBar(ViewHolder.Toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetHomeButtonEnabled(true);

            SetupDrawer(ViewHolder.Drawer, ViewHolder.Toolbar);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (toggle.OnOptionsItemSelected(item))
                return true;

            return base.OnOptionsItemSelected(item);
        }

        public override void Bind(BindingSet<MainViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(ViewHolder.Fab)
                .For(v => v.ClickBinding())
                .To(vm => vm.TestCmd);
        }

        public override void OnBackPressed()
        {
            if (this.ViewHolder.Drawer.IsDrawerOpen(GravityCompat.Start))
            {
                this.ViewHolder.Drawer.CloseDrawer(GravityCompat.Start);
            }
            else
                base.OnBackPressed();
        }
    }
}
