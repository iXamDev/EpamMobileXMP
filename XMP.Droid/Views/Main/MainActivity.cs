using System;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.ViewModels;
using FlexiMvvm.Views;
using NN.Droid.Core.Attributes;
using XMP.Core.ViewModels.Main;
using XMP.Droid.Adapters;
using XMP.Droid.Bindings;
using XMP.Droid.Views.Main.Items;

namespace XMP.Droid.Views.Main
{
    [ActivityPresentation]
    [Activity(Label = "All Requests")]
    public class MainActivity : BindableAppCompatActivity<MainViewModel>
    {
        private ActionBarDrawerToggle _toggle;

        private RecyclerPlainAdapter<MainDrawerCellViewHolder> _drawerAdapter;

        private RecyclerPlainAdapter<MainRequestCellViewHolder> _requestsAdapter;

        private MainActivityViewHolder ViewHolder { get; set; }

        private TextView DrawerUserNameText { get; set; }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (_toggle.OnOptionsItemSelected(item))
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
                .Bind(_drawerAdapter)
                .For(v => v.ItemsBinding())
                .To(vm => vm.FilterItems);

            bindingSet
                .Bind(_drawerAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.FilterCmd);

            bindingSet
                .Bind(_requestsAdapter)
                .For(v => v.ItemsBinding())
                .To(vm => vm.RequestItems);

            bindingSet
                .Bind(_requestsAdapter)
                .For(v => v.ItemClickedBinding())
                .To(vm => vm.ShowDetailsCmd);

            ViewModel.CloseMenuInteraction.RequestedWeakSubscribe(OnCloseMenuInteraction);
        }

        public override void OnBackPressed()
        {
            if (!CloseDrawer())
                base.OnBackPressed();
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

            _drawerAdapter = new RecyclerPlainAdapter<MainDrawerCellViewHolder>(ViewHolder.DrawerRecycler, Resource.Layout.cell_main_drawer);

            ViewHolder.DrawerRecycler.SetAdapter(_drawerAdapter);
            ViewHolder.DrawerRecycler.HasFixedSize = true;
            ViewHolder.DrawerRecycler.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            _requestsAdapter = new RecyclerPlainAdapter<MainRequestCellViewHolder>(ViewHolder.DrawerRecycler, Resource.Layout.cell_main_request);

            ViewHolder.RequestsRecycler.AddItemDecoration(new MainRequesttemDecoration());
            ViewHolder.RequestsRecycler.SetAdapter(_requestsAdapter);
            ViewHolder.RequestsRecycler.HasFixedSize = true;
            ViewHolder.RequestsRecycler.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
        }

        private void SetupDrawer(DrawerLayout drawer, Android.Support.V7.Widget.Toolbar toolbar)
        {
            _toggle = new ActionBarDrawerToggle(this, drawer, toolbar, 0, 0);

            drawer.AddDrawerListener(_toggle);

            _toggle.SyncState();
        }

        private void OnCloseMenuInteraction(object sender, EventArgs e)
        => CloseDrawer();

        private bool CloseDrawer()
        {
            if (ViewHolder.Drawer.IsDrawerOpen(GravityCompat.Start))
            {
                ViewHolder.Drawer.CloseDrawer(GravityCompat.Start);
                return true;
            }

            return false;
        }
    }
}
