using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Phone.Views.Editor.Costumes;
using Catrobat.Paint;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.Services
{
    public class PictureServicePhone : IPictureService
    {
        private Action<PortableImage> _successCallback;
        private Action _errorCallback;
        private Action _cancelleCallback;

        public void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed -= Task_Completed;
            photoChooserTask.Completed += Task_Completed;
            photoChooserTask.Show();
        }

        public void TakePicture(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed -= Task_Completed;
            cameraCaptureTask.Completed += Task_Completed;
            cameraCaptureTask.Show();
        }

        public void DrawPicture(Action<PortableImage> success, Action cancelled, Action error, PortableImage imageToEdit = null)
        {
            _successCallback = success;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            WriteableBitmap bitmap = null;

            if (imageToEdit == null)
            {
                bitmap = new WriteableBitmap(
                  ServiceLocator.SystemInformationService.ScreenWidth, ServiceLocator.SystemInformationService.ScreenHeight);
            }
            else
            {
                bitmap = new WriteableBitmap(imageToEdit.Width, imageToEdit.Height);
                bitmap.FromByteArray(imageToEdit.Data);
            }

            var task = new PaintLauncherTask { CurrentImage = bitmap };
            task.OnImageChanged += OnPaintLauncherImageChanged;
            PaintLauncher.Launche(task);
        }

        private void OnPaintLauncherImageChanged(PaintLauncherTask task)
        {
            var image = task.CurrentImage;
            var portableImage = new PortableImage(image.ToByteArray(), image.PixelWidth, image.PixelHeight);

            _successCallback.Invoke(portableImage);
        }

        private void Task_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                try
                {
                    var image = new BitmapImage();
                    image.SetSource(e.ChosenPhoto);
                    var writeableBitmap = new WriteableBitmap(image);
                    var portableImage = new PortableImage(writeableBitmap.ToByteArray(), writeableBitmap.PixelWidth, writeableBitmap.PixelHeight);

                    _successCallback.Invoke(portableImage);
                }
                catch (Exception)
                {
                    _errorCallback.Invoke();
                }
            }
            else
            {
                _cancelleCallback.Invoke();
            }
        }
    }
}
