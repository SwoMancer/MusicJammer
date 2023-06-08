using MusicJammer.Data;
using System;
using System.Collections.Generic;
using System.Text;
//using Xamarin.Forms.Platform.Android;
using MusicJammer.PseudoModel;


namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="ManagerViewModel" />.<br/>
    /// View model for <see cref="View.ManagerPage"/>
    /// </summary>
    public class ManagerViewModel : ViewModelBase
    {
        #region Fields
        public TabPseudoModel TabVM { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerViewModel"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="navigateMenuViewModel"></param>
        /// <param name="tabVM"></param>
        public ManagerViewModel(Settings settings, NavigateMenuPseudoModel navigateMenuViewModel, TabPseudoModel tabVM) : base(settings, navigateMenuViewModel)
        {
            this.TabVM = tabVM;
        }
        #endregion
        #region Methods
        public override void Activation()
        {
            base.Activation();
            TabVM = App.TabVM;
        }
        #endregion
    }
}
