using System;
using CoreGraphics;
using UIKit;

namespace XMP.IOS.Controls
{
    public class PaddingTextField : UITextField
    {
        public UIEdgeInsets Insets { get; set; } = new UIEdgeInsets(0, 8, 0, 8);

        public override CGRect TextRect(CGRect forBounds)
        => Insets.InsetRect(forBounds);

        public override CGRect PlaceholderRect(CGRect forBounds)
        => Insets.InsetRect(forBounds);

        public override CGRect EditingRect(CGRect forBounds)
        => Insets.InsetRect(forBounds);
    }
}
