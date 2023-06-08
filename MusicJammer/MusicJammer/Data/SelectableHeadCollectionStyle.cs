using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MusicJammer.Data
{
    /// <summary>
    /// Used to select a colour for select parts of views.
    /// </summary>
    [Obsolete]
    public class SelectableHeadCollectionStyle
    {
        public static readonly BindableProperty HighlightColorProperty = BindableProperty.CreateAttached("HighlightColor", typeof(string), typeof(SelectableHeadCollectionStyle), "#fff", propertyChanged: onChangedMethod);
        private static void onChangedMethod(BindableObject bindable, object oldValue, object newValue)
        {
            var color = (bool)newValue ? Style.ArylideYellow.Hex : Style.BarbiePink.Hex;
            var entry = bindable as Entry;
            entry.SetValue(Entry.BackgroundColorProperty, color);
        }

        public static bool GetIsSelected(BindableObject view)
        {
            return (bool)view.GetValue(HighlightColorProperty);
        }

        public static void SetIsSelected(BindableObject view, bool value)
        {
            view.SetValue(HighlightColorProperty, value);
        }
    }
}
