using System.Diagnostics.CodeAnalysis;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using XMP.IOS.Controls;

namespace XMP.IOS.Views.Login
{
    public class LoginView : LayoutView
    {
        public UIImageView BackgroundImage { get; private set; }

        public UIButton SignInButton { get; private set; }

        public UITextField LoginField { get; private set; }

        public UITextField PasswordField { get; private set; }

        public UIView ErrorMessageView { get; private set; }

        public UIImageView ErrorMessageTriangleImage { get; private set; }

        public UILabel ErrorLabel { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            SignInButton = new UIButton(UIButtonType.Custom)
                .WithStyle(Theme.ButtonStyle.Accent);

            BackgroundImage = new UIImageView(UIImage.FromBundle("LoginBackground"));

            LoginField = new PaddingTextField()
                .WithStyle();

            PasswordField = new PaddingTextField
            {
                SecureTextEntry = true
            }.WithStyle();

            ErrorLabel = new UILabel()
            {
                TextAlignment = UITextAlignment.Center,
                Lines = 0,
                TextColor = Theme.Colors.ErrorText,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            ErrorMessageView = new UIView()
            {
                BackgroundColor = Theme.Colors.OverlayBackground,
                ClipsToBounds = true
            };

            ErrorMessageView.Layer.CornerRadius = 4;

            ErrorMessageView.AddSubview(ErrorLabel);

            ErrorMessageTriangleImage = new UIImageView(UIImage.FromBundle("Triangle"));
        }

        protected override void SetupSubviewsConstraints()
        {
            ErrorMessageView.AddConstraints(ErrorLabel.FullSizeOf(ErrorMessageView, 16));
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(BackgroundImage);

            this.AddLayoutSubview(LoginField);

            this.AddLayoutSubview(PasswordField);

            this.AddLayoutSubview(SignInButton);

            this.AddLayoutSubview(ErrorMessageView);

            this.AddLayoutSubview(ErrorMessageTriangleImage);
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "Reviewed.")]
        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                BackgroundImage.FullSizeOf(this));

            this.AddConstraints(
                LoginField.AtTopOf(this, 220),
                LoginField.AtLeadingOf(this, 50),
                LoginField.AtTrailingOf(this, 50),
                LoginField.Height().EqualTo(Theme.Dimensions.TextFieldHeight),

                PasswordField.Below(LoginField, 12),
                PasswordField.WithSameLeading(LoginField),
                PasswordField.WithSameWidth(LoginField),
                PasswordField.WithSameHeight(LoginField),

                SignInButton.Below(PasswordField, 16),
                SignInButton.WithSameCenterX(this),
                SignInButton.Width().EqualTo(218),
                SignInButton.Height().EqualTo(44),

                ErrorMessageView.Above(LoginField, 36),
                ErrorMessageView.WithSameLeading(LoginField),
                ErrorMessageView.WithSameWidth(LoginField),

                ErrorMessageTriangleImage.Below(ErrorMessageView),
                ErrorMessageTriangleImage.WithSameCenterX(this));
        }
    }
}
