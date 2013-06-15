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

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites
{
    public class ChangeSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private Sprite _receivedSprite;
        private string _spriteName;

        #endregion

        #region Properties

        public Sprite ReceivedSprite
        {
            get { return _receivedSprite; }
            set
            {
                if (value == _receivedSprite) return;
                _receivedSprite = value;
                RaisePropertyChanged("ReceivedSprite");
            }
        }

        public string SpriteName
        {
            get { return _spriteName; }
            set
            {
                if (value == _spriteName) return;
                _spriteName = value;
                RaisePropertyChanged("SpriteName");
                RaisePropertyChanged("IsSpriteNameValid");
            }
        }

        public bool IsSpriteNameValid
        {
            get
            {
                return SpriteName != null && SpriteName.Length >= 2;
            }
        }

        #endregion

        #region Commands

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
            ReceivedSprite.Name = SpriteName;

            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void ChangeSpriteNameMessageAction(GenericMessage<Sprite> message)
        {
            ReceivedSprite = message.Content;
            SpriteName = ReceivedSprite.Name;
        }

        #endregion


        public ChangeSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<Sprite>>(this, ViewModelMessagingToken.SpriteNameListener, ChangeSpriteNameMessageAction);
        }

        public void ResetViewModel()
        {
            SpriteName = "";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}