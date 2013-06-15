using System;
using System.Windows;
using Catrobat.IDEWindowsPhone.Misc;
using Microsoft.Phone.Controls;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sounds;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sounds
{
    public partial class AddNewSoundView : PhoneApplicationPage
    {
        private readonly AddNewSoundViewModel _addNewSoundViewModel = ServiceLocator.Current.GetInstance<AddNewSoundViewModel>();

        public AddNewSoundView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            _addNewSoundViewModel.ResetViewModel();
        }
    }
}
