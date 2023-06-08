using MusicJammer.Data;
using MusicJammer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms;
using MusicJammer.PseudoModel;


namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="MusicMasterViewModel" />.<br/>
    /// View model for <see cref="View.MusicMasterPage"/>
    /// </summary>
    public class MusicMasterViewModel : ViewModelBase
    {
        #region Fields
        public TabPseudoModel tabVM { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MusicMasterViewModel"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="navigateMenuViewModel"></param>
        /// <param name="tabVM"></param>
        public MusicMasterViewModel(Settings settings, NavigateMenuPseudoModel navigateMenuViewModel, TabPseudoModel tabVM) : base(settings, navigateMenuViewModel)
        {
            this.tabVM = tabVM;
        }
        #endregion
        #region Methods
        public override void Activation()
        {
            tabVM = App.TabVM;
            base.Activation();
        }
        #endregion
    }
}
