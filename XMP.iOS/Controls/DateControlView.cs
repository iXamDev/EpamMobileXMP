using System;
using CoreGraphics;
using FlexiMvvm.Views;
using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace XMP.iOS.Controls
{
    public class DateControlView : LayoutView
    {
        private UILabel dayLabel, monthLabel, yearLabel;

        private static int DayNumberSpacing { get; } = 6;

        private static UIFont DayFont { get; } = UIFont.SystemFontOfSize(80);

        private static UIFont MonthFont { get; } = UIFont.SystemFontOfSize(40);

        private static UIFont YearFont { get; } = UIFont.SystemFontOfSize(26);

        private UIColor textColor;
        public UIColor TextColor
        {
            get => textColor;
            set
            {
                textColor = value;

                dayLabel.TextColor = value;
                monthLabel.TextColor = value;
                yearLabel.TextColor = value;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;

                ApplyDate(date);
            }
        }

        protected nfloat IntrinsicHeight => Theme.Dimensions.DateControlHeight;

        protected nfloat IntrinsicWidth => dayLabel.Bounds.Width + DayNumberSpacing + (nfloat)Math.Max(monthLabel.Bounds.Width, monthLabel.Bounds.Width);

        public override CGSize IntrinsicContentSize => new CGSize(IntrinsicWidth, IntrinsicHeight);

        private void ApplyDate(DateTime newDate)
        {
            SetPartDateText(dayLabel, newDate.Day);
            SetPartDateText(monthLabel, newDate.ToString("MMM").ToUpper());
            SetPartDateText(yearLabel, newDate.Year);

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

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            dayLabel = SetupLabel(DayFont);

            monthLabel = SetupLabel(MonthFont);

            yearLabel = SetupLabel(YearFont);
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            AddSubviews(dayLabel, monthLabel, yearLabel);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints
            (
                dayLabel.WithSameCenterY(this),
                dayLabel.AtLeadingOf(this),

                monthLabel.AtTopOf(this, 10),
                monthLabel.AtTrailingOf(this),

                yearLabel.AtBottomOf(this, 12),
                yearLabel.AtTrailingOf(this)
            );
        }
    }
}
