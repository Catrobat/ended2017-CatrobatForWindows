using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using System.IO;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;
using Catrobat.IDE.Phone.Views.Editor.Sounds;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.Services
{
    public class SoundServicePhone : ISoundService
    {
        private readonly Semaphore _semaphore = new Semaphore(0, 1);
        private SoundServiceResult _result;

        public void Finished(SoundServiceResult result)
        {
            _result = result;
            _semaphore.Release(1);
        }

        public Task<SoundServiceResult> CreateSoundFromRecorder(Sprite sprite)
        {
            var task = Task.Run(() =>
            {
                _semaphore.WaitOne();
                return _result;
            });

            var message = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);

            ServiceLocator.NavigationService.NavigateTo<SoundRecorderViewModel>();

            return task;
        }

        public async Task<SoundServiceResult> CreateSoundFromMediaLibrary(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
