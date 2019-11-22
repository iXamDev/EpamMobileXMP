using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.iOS.Views.Main
{
    public class MenuView : LayoutView
    {
        public UIButton SignInButton { get; private set; }


        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            SignInButton = new UIButton(UIButtonType.System);
            SignInButton.SetTitle("Sign in", UIControlState.Normal);
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(SignInButton);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                SignInButton.WithSameCenterX(this),
                SignInButton.WithSameCenterY(this));
        }
    }
}
