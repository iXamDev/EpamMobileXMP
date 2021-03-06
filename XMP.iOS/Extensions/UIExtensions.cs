﻿using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace XMP.IOS.Extensions
{
    public static class UIExtensions
    {
        public static UIButton WithTitleColorForAllStates(this UIButton self, UIColor color)
        {
            self.SetTitleColor(color, UIControlState.Normal);
            self.SetTitleColor(color, UIControlState.Highlighted);
            self.SetTitleColor(color, UIControlState.Application);
            self.SetTitleColor(color, UIControlState.Disabled);
            self.SetTitleColor(color, UIControlState.Reserved);
            self.SetTitleColor(color, UIControlState.Selected);

            return self;
        }

        public static UIButton WithTitleForAllStates(this UIButton self, string title)
        {
            self.SetTitle(title, UIControlState.Normal);
            self.SetTitle(title, UIControlState.Highlighted);
            self.SetTitle(title, UIControlState.Application);
            self.SetTitle(title, UIControlState.Disabled);
            self.SetTitle(title, UIControlState.Reserved);
            self.SetTitle(title, UIControlState.Selected);

            return self;
        }

        public static UIButton WithImageForAllStates(this UIButton self, UIImage image)
        {
            self.SetImage(image, UIControlState.Normal);
            self.SetImage(image, UIControlState.Highlighted);
            self.SetImage(image, UIControlState.Application);
            self.SetImage(image, UIControlState.Disabled);
            self.SetImage(image, UIControlState.Reserved);
            self.SetImage(image, UIControlState.Selected);

            return self;
        }

        public static Dictionary<T, nint> SetupSegmentsMapping<T>(this UISegmentedControl control, T[] variants, Func<T, string> nameProvider)
        {
            var states = variants.Select((t, i) => new { Type = t, Title = nameProvider(t), Index = i });

            foreach (var state in states)
                control.InsertSegment(state.Title, state.Index, false);

            return states.ToDictionary(key => key.Type, value => (nint)value.Index);
        }
    }
}
