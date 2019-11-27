using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using System.Linq;
using XMP.iOS.Views.Main.Cells;

namespace XMP.iOS.Views.Main
{
    public class ContentView : LayoutView
    {
        private UIView deviderView;

        public UITableView ContentTableView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            ContentTableView = new UITableView
            {
                BackgroundColor = UIColor.White,
                RowHeight = Theme.Dimensions.MainContentVacationRequestItemHeight,
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
            };

            ContentTableView.RegisterClassForCellReuse(typeof(ContentVacationRequestItemTableViewCell), ContentVacationRequestItemTableViewCell.CellId);

            deviderView = new UIView { BackgroundColor = Theme.Colors.MenuDevider };
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(ContentTableView);

            this.AddLayoutSubview(deviderView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints
            (
                ContentTableView.FullSizeOf(this).Concat
                (
                    new FluentLayout[]
                    {
                        deviderView.AtTopOf(this),
                        deviderView.AtBottomOf(this),
                        deviderView.AtLeadingOf(this, -1),
                        deviderView.Width().EqualTo(1)
                    }
                )
            );
        }
    }
}
