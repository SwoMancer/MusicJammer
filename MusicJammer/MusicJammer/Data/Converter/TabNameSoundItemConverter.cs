using System;
using System.Globalization;
using Xamarin.Forms;

namespace MusicJammer.Data.Converter
{
    /// <summary>
    /// Defines the <see cref="TabNameSoundItemConverter" />.<br/>
    /// <para>
    /// See <see cref="Model.MultiBinding.TabNameSoundItem"/> for its multi binding class.
    /// </para>
    /// Resources:
    /// <list type="bullet">
    /// <item><see href="https://docs.microsoft.com/en-us/answers/questions/619166/how-to-use-command-parameter-with-multiple-compone.html">Docs.microsoft.com: how to use command parameter with multiple compone</see>.</item>
    /// <item><see href="https://stackoverflow.com/questions/64244447/multibinding-in-xamarin-forms-element-is-null">Stackoverflow.com: multibinding in xamarin forms element is null</see>.</item>
    /// </list>
    /// </summary>
    public class TabNameSoundItemConverter : IMultiValueConverter
    {

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="values">The values<see cref="object[]"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values.Length == 2)
            {
                string name = values[0].ToString();
                Model.SoundItem item = (Model.SoundItem)values[1];
                return new Model.MultiBinding.TabNameSoundItem { Name = name, SoundItem = item };
            }
            return new Model.MultiBinding.TabNameSoundItem { Name = "xxx", SoundItem = new Model.SoundItem("xxx", "xxx", "xxx") };
        }

        /// <summary>
        /// The Convert Back.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetTypes">The targetTypes<see cref="Type[]"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
