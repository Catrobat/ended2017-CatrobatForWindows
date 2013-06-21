using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Storage;
using Catrobat.IDEWindowsPhone.Annotations;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDECommon.Resources.Editor;
using Microsoft.Phone.Tasks;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Catrobat.Core.Objects.Costumes;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes
{
    public class ChangeCostumeViewModel : ViewModelBase
    {
        #region Private Members

        private Costume _receivedCostume;
        private string _costumeName;

        #endregion

        #region Properties

        public Costume ReceivedCostume
        {
            get { return _receivedCostume; }
            set
            {
                if (value == _receivedCostume) return;
                _receivedCostume = value;
                RaisePropertyChanged("ReceivedCostume");
            }
        }

        public string CostumeName
        {
            get { return _costumeName; }
            set
            {
                if (value == _costumeName) return;
                _costumeName = value;
                RaisePropertyChanged("CostumeName");
                RaisePropertyChanged("IsCostumeNameValid");
            }
        }

        public bool IsCostumeNameValid
        {
            get
            {
                return CostumeName != null && CostumeName.Length >= 2;
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCostumeCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            ReceivedCostume.Name = CostumeName;
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Navigation.NavigateBack();
        }

        private void EditCostumeAction()
        {
            //TODO: navigate to Paintapp
        }

        private void ChangeCostumeNameMessageAction(GenericMessage<Costume> message)
        {
            ReceivedCostume = message.Content;
            CostumeName = ReceivedCostume.Name;
        }

        #endregion


        public ChangeCostumeViewModel()
        {
            EditCostumeCommand = new RelayCommand(EditCostumeAction);
            SaveCommand = new RelayCommand(SaveAction);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Costume>>(this, ViewModelMessagingToken.CostumeNameListener, ChangeCostumeNameMessageAction);
        }

        public void ResetViewModel()
        {
            CostumeName = "";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}