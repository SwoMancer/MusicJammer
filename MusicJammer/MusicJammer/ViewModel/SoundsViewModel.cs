using MusicJammer.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MusicJammer.PseudoModel;


namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="SoundsViewModel" />.<br/>
    /// View model for <see cref="View.SoundsPage"/>
    /// </summary>
    public class SoundsViewModel : ViewModelBase
    {
        #region Fields
        public TabPseudoModel tabVM { get; set; }
        #endregion
        #region Constructor
        public SoundsViewModel(Settings settings, NavigateMenuPseudoModel navigateMenuViewModel, TabPseudoModel tabVM) : base(settings, navigateMenuViewModel)
        {
            this.tabVM = tabVM;
        }
        #endregion
        #region Methods
        public override void Activation()
        {
            base.Activation();
            tabVM = App.TabVM;
        }
        #endregion
    }
}
