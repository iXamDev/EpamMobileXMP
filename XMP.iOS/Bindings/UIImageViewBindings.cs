using System;
using System.Diagnostics.CodeAnalysis;
using FlexiMvvm;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using UIKit;

namespace XMP.IOS.Bindings
{
    public static class UIImageViewBindings
    {
        public static TargetItemBinding<UIImageView, string> BundleImageBinding(
            this IItemReference<UIImageView> imageViewReference)
        {
            if (imageViewReference == null)
                throw new ArgumentNullException(nameof(imageViewReference));

            return new TargetItemOneWayCustomBinding<UIImageView, string>(
                imageViewReference,
                (imageView, imageBundleName) => imageView.Image = UIImage.FromBundle(imageBundleName),
                () => "Image");
        }
    }
}
