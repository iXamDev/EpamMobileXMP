﻿// <auto-generated />
// ReSharper disable All
using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using JetBrains.Annotations;

namespace XMP.Droid.Views
{
    public partial class DetailsActivityViewHolder
    {
        [NotNull] private readonly Activity activity;

        [CanBeNull] private RelativeLayout toolbarLikeLayout;
        [CanBeNull] private TextView toolbarTitle;
        [CanBeNull] private Button saveButton;

        public DetailsActivityViewHolder([NotNull] Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        [NotNull]
        public RelativeLayout ToolbarLikeLayout =>
            toolbarLikeLayout ?? (toolbarLikeLayout = activity.FindViewById<RelativeLayout>(Resource.Id.toolbar_like_layout));

        [NotNull]
        public TextView ToolbarTitle =>
            toolbarTitle ?? (toolbarTitle = activity.FindViewById<TextView>(Resource.Id.toolbar_title));

        [NotNull]
        public Button SaveButton =>
            saveButton ?? (saveButton = activity.FindViewById<Button>(Resource.Id.save_button));
    }

    /*
    "LayoutDefinitionOptions" are not specified for "main_drawer_header" layout file therefore view holder can't be generated for it.
    public partial class DrawerHeaderMainViewHolder
    {
    }
    */

    public partial class LoginActivityViewHolder
    {
        [NotNull] private readonly Activity activity;

        [CanBeNull] private EditText loginEdit;
        [CanBeNull] private EditText passwordEdit;
        [CanBeNull] private Button signInButton;
        [CanBeNull] private LinearLayout errorOverlayView;
        [CanBeNull] private TextView errorText;
        [CanBeNull] private ImageView errorOverlayTriangleImage;

        public LoginActivityViewHolder([NotNull] Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        [NotNull]
        public EditText LoginEdit =>
            loginEdit ?? (loginEdit = activity.FindViewById<EditText>(Resource.Id.login_edit));

        [NotNull]
        public EditText PasswordEdit =>
            passwordEdit ?? (passwordEdit = activity.FindViewById<EditText>(Resource.Id.password_edit));

        [NotNull]
        public Button SignInButton =>
            signInButton ?? (signInButton = activity.FindViewById<Button>(Resource.Id.sign_in_button));

        [NotNull]
        public LinearLayout ErrorOverlayView =>
            errorOverlayView ?? (errorOverlayView = activity.FindViewById<LinearLayout>(Resource.Id.error_overlay_view));

        [NotNull]
        public TextView ErrorText =>
            errorText ?? (errorText = activity.FindViewById<TextView>(Resource.Id.error_text));

        [NotNull]
        public ImageView ErrorOverlayTriangleImage =>
            errorOverlayTriangleImage ?? (errorOverlayTriangleImage = activity.FindViewById<ImageView>(Resource.Id.error_overlay_triangle_image));
    }

    public partial class MainActivityViewHolder
    {
        [NotNull] private readonly Activity activity;

        [CanBeNull] private Android.Support.Design.Widget.AppBarLayout appbar;
        [CanBeNull] private Android.Support.V7.Widget.Toolbar toolbar;
        [CanBeNull] private Android.Support.V4.Widget.DrawerLayout drawer;
        [CanBeNull] private Android.Support.Design.Widget.FloatingActionButton fab;
        [CanBeNull] private Android.Support.Design.Widget.NavigationView navitionView;

        public MainActivityViewHolder([NotNull] Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));

            this.activity = activity;
        }

        [NotNull]
        public Android.Support.Design.Widget.AppBarLayout Appbar =>
            appbar ?? (appbar = activity.FindViewById<Android.Support.Design.Widget.AppBarLayout>(Resource.Id.appbar));

        [NotNull]
        public Android.Support.V7.Widget.Toolbar Toolbar =>
            toolbar ?? (toolbar = activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));

        [NotNull]
        public Android.Support.V4.Widget.DrawerLayout Drawer =>
            drawer ?? (drawer = activity.FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer));

        [NotNull]
        public Android.Support.Design.Widget.FloatingActionButton Fab =>
            fab ?? (fab = activity.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab));

        [NotNull]
        public Android.Support.Design.Widget.NavigationView NavitionView =>
            navitionView ?? (navitionView = activity.FindViewById<Android.Support.Design.Widget.NavigationView>(Resource.Id.navition_view));
    }

}
// ReSharper restore All
