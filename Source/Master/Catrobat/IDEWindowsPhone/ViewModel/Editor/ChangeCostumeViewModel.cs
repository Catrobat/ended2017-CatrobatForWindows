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

namespace Catrobat.IDEWindowsPhone.ViewModel
{
    public class ChangeCostumeViewModel : ViewModelBase
    {
        private readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

        #region Private Members

        private Costume _costume;
        private string _costumeName;

        #endregion

        #region Commands

        public RelayCommand EditCostumeCommand
        {
            get;
            private set;
        }

        #endregion

        #region Actions

        private void SaveAction()
        {
            Costume.Name = CostumeName;

            Cleanup();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            Cleanup();
            Navigation.NavigateBack();
        }

        private void EditCostumeAction()
        {
            //TODO: navigate to Paintapp
        }

        private void ChangeCostumeNameMessageAction(GenericMessage<Costume> message)
        {
            Costume = message.Content;
            CostumeName = Costume.Name;
        }

        #endregion

        #region Events

        public void SaveEvent()
        {
            SaveAction();
        }

        public void CancelEvent()
        {
            CancelAction();
        }


        #endregion

        #region Properties

        public Costume Costume
        {
            get { return _costume; }
            set
            {
                if (value == _costume) return;
                _costume = value;
                RaisePropertyChanged("Costume");
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

        public ChangeCostumeViewModel()
        {
            EditCostumeCommand = new RelayCommand(EditCostumeAction);

            Messenger.Default.Register<GenericMessage<Costume>>(this, ViewModelMessagingToken.ChangeCostumeNameViewModel, ChangeCostumeNameMessageAction);
        }

        public override void Cleanup()
        {
            Costume = null;

            base.Cleanup();
        }
    }
}