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
using Catrobat.IDEWindowsPhone.Views.Editor.Costumes;
using Catrobat.Core.Objects.Costumes;
using System.Collections.ObjectModel;

namespace Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites
{
    public class AddNewSpriteViewModel : ViewModelBase
    {
        #region Private Members

        private string _spriteName;
        private ObservableCollection<Sprite> _receivedSprites;

        #endregion

        #region Properties

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
            Sprite sprite = new Sprite(CatrobatContext.GetContext().CurrentProject);
            sprite.Name = SpriteName;
            _receivedSprites.Add(sprite);

            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void CancelAction()
        {
            ResetViewModel();
            Navigation.NavigateBack();
        }

        private void ReceiveSpriteListMessageAction(GenericMessage<ObservableCollection<Sprite>> message)
        {
            _receivedSprites = message.Content;
        }

        #endregion


        public AddNewSpriteViewModel()
        {
            SaveCommand = new RelayCommand(SaveAction);
            CancelCommand = new RelayCommand(CancelAction);

            Messenger.Default.Register<GenericMessage<ObservableCollection<Sprite>>>(this, ViewModelMessagingToken.SpriteListListener, ReceiveSpriteListMessageAction);
        }

        public void ResetViewModel()
        {
            SpriteName = null;
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}