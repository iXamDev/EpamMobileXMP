using System;
using Microsoft.Win32;
using Android.Support.V7.Widget;
using Android.Graphics;
using Android.Views;
using Android.Util;
using Android.Support.V4.Content;

namespace XMP.Droid.Views.Main.Items
{
    public class MainRequesttemDecoration : RecyclerView.ItemDecoration
    {
        Paint paint;

        int sideSpace;

        public MainRequesttemDecoration()
        {
            paint = new Paint()
            {
                StrokeWidth = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 1f, Application.Context.Resources.DisplayMetrics),
                Color = new Color(ContextCompat.GetColor(Application.Context, Resource.Color.devider))
            };

            paint.SetStyle(Paint.Style.Stroke);

            sideSpace = Application.Context.Resources.GetDimensionPixelSize(Resource.Dimension.request_items_devider_side_space);
        }

        public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;

            int childCount = parent.GetAdapter().ItemCount;
            for (int i = 0; i < childCount; i++)
            {
                var child = parent.GetChildAt(i);

                if (child == null)
                    return;

                var lp = (RecyclerView.LayoutParams)child.LayoutParameters;

                int top = (int)(child.Top + lp.TopMargin + lp.BottomMargin + child.TranslationY);

                c.DrawLine(left + ((i + 1 < childCount) ? sideSpace : 0), top, right, top, paint);
            }
        }
    }
}
