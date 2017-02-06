using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace NetConnect.CustomViews
{
    class ImageViewScaling : ImageView
    {
        public ImageViewScaling(Context cont)
            :base(cont)
        {

        }
        public ImageViewScaling(Context cont, IAttributeSet attrs)
            : base(cont, attrs)
        {

        }
        public ImageViewScaling(Context cont, IAttributeSet attrs, int defStyle)
            : base(cont, attrs, defStyle)
        {

        }
        public ImageViewScaling(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            :base(context,attrs,defStyleAttr,defStyleRes)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension(MeasuredWidth, MeasuredHeight);
        }
    }
}