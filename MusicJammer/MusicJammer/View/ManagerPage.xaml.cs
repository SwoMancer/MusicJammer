using MusicJammer.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="ManagerPage" />.<br />
    /// Handles user needs to handel tabs.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerPage"/> class.
        /// </summary>
        public ManagerPage()
        {
            InitializeComponent();

            App.ManagerVM.Activation();

            BindingContext = App.ManagerVM;
        }
        #endregion
        #region Events
        /// <summary>
        /// If add tab button is clicked.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private async void AddTabButton_Clicked(object sender, EventArgs e)
        {
            //If name is empty...
            if (String.IsNullOrEmpty(NameTabEntry.Text) || String.IsNullOrWhiteSpace(NameTabEntry.Text))
            {
                await DisplayAlert("Input error", "You need to input a name and can not leave the entry empty.", "Okay");
                return;
            }

            string name = NameTabEntry.Text.Trim().ToLower();

            //If name is to long...
            if (name.Length > 50)
                await DisplayAlert("Input error", "Your name for the tab can be at most 50 charters.", "Okay");


            //Beautify user input...
            if (name.Contains(" "))
            {
                Dictionary<int, char> indexs = new Dictionary<int, char>();
                for (int i = 0; i < name.Length - 1; i++)
                {
                    if (name.ToCharArray()[i].Equals(' '))
                        indexs.Add(i + 1, name.ToCharArray()[i + 1]);
                }
                char[] nameArray = name.ToCharArray();

                foreach (KeyValuePair<int, char> kvp in indexs)
                    nameArray[kvp.Key] = Char.ToUpper(kvp.Value);

                name = "";
                foreach (char item in nameArray)
                    name += item;

            }
            //Capitalize first character...
            if (name.Length > 2 && !char.IsNumber(name.ToCharArray()[0]))
                name = Char.ToUpper(name.ToCharArray()[0]) + name.Substring(1);

            //Name is already in use
            if (App.ManagerVM.TabVM.TabItems.Contains(App.TabVM.TabItems.Where(i => i.Name == name).FirstOrDefault()))
                await DisplayAlert("Input error", "A tab with that name already exists; try another title for that tab.", "Okay");

            //Create...
            App.ManagerVM.TabVM.CreateNewTab(name);
            NameTabEntry.Text = "";
            App.ManagerVM.TabVM.Save();
        }

        /// <summary>
        /// The on apply template event.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GradientStop gradientStop;

            gradientStop = (GradientStop)GetTemplateChild("gradientStopTop");
            gradientStop.Color = Color.FromHex(App.ManagerVM.Settings.MainColor.Hex);
            gradientStop = (GradientStop)GetTemplateChild("gradientStopDown");
            gradientStop.Color = Color.FromHex(App.ManagerVM.Settings.MainColor.Hex);
        }
        #endregion
    }
}
