using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace XMP.Droid.Controls
{
    [Register("xmp.droid.controls.DateControlLayout")]
    public class DateControlLayout : FrameLayout
    {
        private TextView _dayText;

        private TextView _monthText;

        private TextView _yearText;

        private DateTime _date;

        private Color _color;

        public DateControlLayout(Context context)
            : base(context)
        {
            Initialize(context, null);
        }

        public DateControlLayout(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public DateControlLayout(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;

                ApplyDate(value);
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;

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
            _dayText = view.FindViewById<TextView>(Resource.Id.day_text);

            _monthText = view.FindViewById<TextView>(Resource.Id.month_text);

            _yearText = view.FindViewById<TextView>(Resource.Id.year_text);
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
            SetPartDateText(_dayText, newDate.Day);
            SetPartDateText(_monthText, newDate.ToString("MMM").ToUpper());
            SetPartDateText(_yearText, newDate.Year);
        }

        private void ApplyColor(Color value)
        {
            _dayText.SetTextColor(value);
            _monthText.SetTextColor(value);
            _yearText.SetTextColor(value);
        }

        private void SetPartDateText(TextView label, int dateInt)
        => SetPartDateText(label, dateInt.ToString());

        private void SetPartDateText(TextView label, string dateText)
        => label.Text = dateText;
    }
}
