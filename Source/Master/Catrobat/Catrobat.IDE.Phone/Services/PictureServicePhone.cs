using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.Paint;
using Coding4Fun.Toolkit.Controls.Common;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.Services
{
    public class PictureServicePhone : IPictureService
    {
        private Action<PortableImage> _successCallback;
        private Func<PortableImage, Task> _successCallbackAsync;
        private Action _errorCallback;
        private Action _cancelleCallback;

        public void ChoosePictureFromLibrary(Action<PortableImage> success, Action cancelled, Action error)
        {
            _successCallback = success;
            _successCallbackAsync = null;
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
            _successCallbackAsync = null;
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
            _successCallbackAsync = null;
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
                bitmap = new WriteableBitmap((BitmapSource) imageToEdit.ImageSource);
                //bitmap.FromByteArray(imageToEdit.Data);
            }

            var task = new PaintLauncherTask { CurrentImage = bitmap };
            task.OnImageChanged += OnPaintLauncherImageChanged;
            PaintLauncher.Launche(task);
        }


        public void ChoosePictureFromLibraryAsync(Func<PortableImage, Task> success, Action cancelled, Action error)
        {
            _successCallbackAsync = success;
            _successCallback = null;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed -= Task_Completed;
            photoChooserTask.Completed += Task_Completed;
            photoChooserTask.Show();
        }

        public void TakePictureAsync(Func<PortableImage, Task> success, Action cancelled, Action error)
        {
            _successCallbackAsync = success;
            _successCallback = null;
            _cancelleCallback = cancelled;
            _errorCallback = error;

            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed -= Task_Completed;
            cameraCaptureTask.Completed += Task_Completed;
            cameraCaptureTask.Show();
        }


        private readonly Semaphore _semaphore = new Semaphore(0, 1);
        private PictureServiceResult _result;

        public Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            BitmapSource bitmap = null;

            if (imageToEdit == null)
            {
                //bitmap = new WriteableBitmap(
                //  ServiceLocator.SystemInformationService.ScreenWidth, ServiceLocator.SystemInformationService.ScreenHeight);
            }
            else
            {
                bitmap = (BitmapSource)imageToEdit.ImageSource;
                //bitmap.FromByteArray(imageToEdit.Data);
            }

            var task = Task.Run(() =>
            {
                _semaphore.WaitOne();
                return _result;
            });

            var paintLauncherTask = new PaintLauncherTask { CurrentImage = bitmap };
            paintLauncherTask.OnImageChanged += OnPaintLauncherImageChanged;

            ServiceLocator.DispatcherService.RunOnMainThread(() => 
                PaintLauncher.Launche(paintLauncherTask));
            
            return task;
        }


        private void OnPaintLauncherImageChanged(PaintLauncherTask task)
        {
            var image = task.CurrentImage;
            var portableImage = new PortableImage(image.ToBitmapImage())
            {
                Width = image.PixelWidth,
                Height = image.PixelHeight
            };

            _result = new PictureServiceResult 
            { 
                Image = portableImage,
                Status = PictureServiceStatus.Success
            };

            _semaphore.Release(1);

            //if (_successCallback != null)
            //    _successCallback.Invoke(portableImage);
            //else if (_successCallbackAsync != null)
            //    _successCallbackAsync.Invoke(portableImage);
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

                    if (_successCallback != null)
                        _successCallback.Invoke(portableImage);
                    else if (_successCallbackAsync != null)
                        _successCallbackAsync.Invoke(portableImage);
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
