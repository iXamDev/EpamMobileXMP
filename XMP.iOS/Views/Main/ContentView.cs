using System.Linq;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using XMP.iOS.Views.Main.Cells;

namespace XMP.iOS.Views.Main
{
    public class ContentView : LayoutView
    {
        private UIView _deviderView;

        public UITableView ContentTableView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = Theme.Colors.ScreenBackground;

            ContentTableView = new UITableView
            {
                BackgroundColor = UIColor.White,
                RowHeight = Theme.Dimensions.MainContentVacationRequestItemHeight,
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
            };

            ContentTableView.RegisterClassForCellReuse(typeof(ContentVacationRequestItemTableViewCell), ContentVacationRequestItemTableViewCell.CellId);

            _deviderView = new UIView { BackgroundColor = Theme.Colors.MenuDevider };
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(ContentTableView);

            this.AddLayoutSubview(_deviderView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                ContentTableView.FullSizeOf(this).Concat(
                    new FluentLayout[]
                    {
                        _deviderView.AtTopOf(this),
                        _deviderView.AtBottomOf(this),
                        _deviderView.AtLeadingOf(this, -1),
                        _deviderView.Width().EqualTo(Theme.Dimensions.DeviderWidth)
                    }));
        }
    }
}
