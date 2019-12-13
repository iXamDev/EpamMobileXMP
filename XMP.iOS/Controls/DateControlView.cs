using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.IOS.Controls
{
    public class DateControlView : LayoutView
    {
        private UILabel _dayLabel;

        private UILabel _monthLabel;

        private UILabel _yearLabel;

        private UIColor _textColor;

        private DateTime _date;

        public UIColor TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;

                _dayLabel.TextColor = value;
                _monthLabel.TextColor = value;
                _yearLabel.TextColor = value;
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;

                ApplyDate(_date);
            }
        }

        public override CGSize IntrinsicContentSize => new CGSize(IntrinsicWidth, IntrinsicHeight);

        protected nfloat IntrinsicHeight => Theme.Dimensions.DateControlHeight;

        protected nfloat IntrinsicWidth => _dayLabel.Bounds.Width + DayNumberSpacing + (nfloat)Math.Max(_monthLabel.Bounds.Width, _monthLabel.Bounds.Width);

        private static int DayNumberSpacing { get; } = 6;

        private static UIFont DayFont { get; } = UIFont.SystemFontOfSize(80);

        private static UIFont MonthFont { get; } = UIFont.SystemFontOfSize(40);

        private static UIFont YearFont { get; } = UIFont.SystemFontOfSize(26);

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            _dayLabel = SetupLabel(DayFont);

            _monthLabel = SetupLabel(MonthFont);

            _yearLabel = SetupLabel(YearFont);
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            AddSubviews(_dayLabel, _monthLabel, _yearLabel);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                _dayLabel.WithSameCenterY(this),
                _dayLabel.AtLeadingOf(this),
                _monthLabel.AtTopOf(this, 10),
                _monthLabel.AtTrailingOf(this),
                _yearLabel.AtBottomOf(this, 12),
                _yearLabel.AtTrailingOf(this));
        }

        private void ApplyDate(DateTime newDate)
        {
            SetPartDateText(_dayLabel, newDate.Day);
            SetPartDateText(_monthLabel, newDate.ToString("MMM").ToUpper());
            SetPartDateText(_yearLabel, newDate.Year);

            InvalidateIntrinsicContentSize();
        }

        private void SetPartDateText(UILabel label, int dateInt)
        => SetPartDateText(label, dateInt.ToString());

        private void SetPartDateText(UILabel label, string dateText)
        {
            label.Text = dateText;

            label.SizeToFit();
        }

        private UILabel SetupLabel(UIFont font)
        {
            var style = new Theme.UILabelStyle
            {
                Lines = 1,
                Font = font
            };

            return new UILabel().WithStyle(style);
        }
    }
}
