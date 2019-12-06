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
using Android.Widget;
using XMP.Droid.Bindings;
using XMP.Droid.Adapters;
using FlexiMvvm.ViewModels;
using XMP.Droid.Views.Main.Items;

namespace XMP.Droid.Views.Main
{
    [Activity(Label = "All Requests")]
    public class MainActivity : BindableAppCompatActivity<MainViewModel>
    {
        private ActionBarDrawerToggle toggle;

        private MainActivityViewHolder ViewHolder { get; set; }

        private TextView DrawerUserNameText { get; set; }

        private RecyclerPlainAdapter<MainDrawerCellViewHolder> drawerAdapter;

        private RecyclerPlainAdapter<MainRequestCellViewHolder> requestsAdapter;

        private void SetupDrawer(DrawerLayout drawer, Android.Support.V7.Widget.Toolbar toolbar)
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

            DrawerUserNameText = FindViewById<TextView>(Resource.Id.drawer_user_name_text);

            drawerAdapter = new RecyclerPlainAdapter<MainDrawerCellViewHolder>(ViewHolder.DrawerRecycler, Resource.Layout.cell_main_drawer);

            ViewHolder.DrawerRecycler.SetAdapter(drawerAdapter);
            ViewHolder.DrawerRecycler.HasFixedSize = true;
            ViewHolder.DrawerRecycler.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            requestsAdapter = new RecyclerPlainAdapter<MainRequestCellViewHolder>(ViewHolder.DrawerRecycler, Resource.Layout.cell_main_request);

            ViewHolder.RequestsRecycler.AddItemDecoration(new MainRequesttemDecoration());
            ViewHolder.RequestsRecycler.SetAdapter(requestsAdapter);
            ViewHolder.RequestsRecycler.HasFixedSize = true;
            ViewHolder.RequestsRecycler.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
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
                .To(vm => vm.AddCmd);

            bindingSet
                .Bind(ViewHolder.Toolbar)
                .For(v => v.TitleBinding())
                .To(vm => vm.ScreenTitle);

            bindingSet
                .Bind(DrawerUserNameText)
                .For(v => v.TextBinding())
                .To(vm => vm.UserName);

            bindingSet
                .Bind(drawerAdapter)
                .For(v => v.ItemsBinding())
                .To(vm => vm.FilterItems);

            bindingSet
                .Bind(drawerAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.FilterCmd);

            bindingSet
                .Bind(requestsAdapter)
                .For(v => v.ItemsBinding())
                .To(vm => vm.RequestItems);

            bindingSet
                .Bind(requestsAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.ShowDetailsCmd);

            ViewModel.CloseMenuInteraction.RequestedWeakSubscribe(OnCloseMenuInteraction);
        }

        private void OnCloseMenuInteraction(object sender, EventArgs e)
        => CloseDrawer();

        private bool CloseDrawer()
        {
            if (this.ViewHolder.Drawer.IsDrawerOpen(GravityCompat.Start))
            {
                this.ViewHolder.Drawer.CloseDrawer(GravityCompat.Start);
                return true;
            }

            return false;
        }

        public override void OnBackPressed()
        {
            if (!CloseDrawer())
                base.OnBackPressed();
        }
    }
}
