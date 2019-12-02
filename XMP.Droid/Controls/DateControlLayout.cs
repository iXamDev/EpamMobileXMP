using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Content;
namespace XMP.Droid.Controls
{
    [Register("xmp.droid.controls.DateControlLayout")]
    public class DateControlLayout : FrameLayout
    {
        public DateControlLayout(Context context) : base(context)
        {
            Initialize(context, null);
        }

        public DateControlLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public DateControlLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        private TextView dayText, monthText, yearText;

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;

                ApplyDate(value);
            }
        }

        private Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;

                ApplyColor(value);
            }
        }

        private void Initialize(Context context, IAttributeSet attrs)
        {
            var view = LayoutInflater.From(context).Inflate(Resource.Layout.control_date, this, true);

            InitializeSubviews(view);

            if (attrs != null)
                ParseAttrs(attrs);
        }

        private void InitializeSubviews(View view)
        {
            dayText = view.FindViewById<TextView>(Resource.Id.day_text);

            monthText = view.FindViewById<TextView>(Resource.Id.month_text);

            yearText = view.FindViewById<TextView>(Resource.Id.year_text);
        }

        private void ParseAttrs(IAttributeSet attrs)
        {
            var styleAttrs = Context.ObtainStyledAttributes(attrs, Resource.Styleable.DateControlLayout);
            if (styleAttrs != null)
            {
                Color = styleAttrs.GetColor(Resource.Styleable.DateControlLayout_color, Color.Red);

                styleAttrs.Recycle();
            }
        }

        private void ApplyDate(DateTime newDate)
        {
            SetPartDateText(dayText, newDate.Day);
            SetPartDateText(monthText, newDate.ToString("MMM").ToUpper());
            SetPartDateText(yearText, newDate.Year);
        }

        private void ApplyColor(Color value)
        {
            dayText.SetTextColor(value);
            monthText.SetTextColor(value);
            yearText.SetTextColor(value);
        }

        private void SetPartDateText(TextView label, int dateInt)
        => SetPartDateText(label, dateInt.ToString());

        private void SetPartDateText(TextView label, string dateText)
        => label.Text = dateText;
    }
}
