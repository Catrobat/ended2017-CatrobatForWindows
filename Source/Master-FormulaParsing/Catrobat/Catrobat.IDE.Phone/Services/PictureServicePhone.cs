using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.Paint.Phone.Old;
using Coding4Fun.Toolkit.Controls.Common;
using Microsoft.Phone.Tasks;

namespace Catrobat.IDE.Phone.Services
{
    public class PictureServicePhone : IPictureService
    {
        private readonly Semaphore _semaphore = new Semaphore(0, 1);
        private PictureServiceResult _result;

        public Task<PictureServiceResult> ChoosePictureFromLibraryAsync()
        {
            var task = Task.Run(() =>
            {
                _semaphore.WaitOne();
                return _result;
            });

            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed -= Task_CompletedAsync;
            photoChooserTask.Completed += Task_CompletedAsync;
            photoChooserTask.Show();

            return task;
        }

        public Task<PictureServiceResult> TakePictureAsync()
        {
            var task = Task.Run(() =>
            {
                _semaphore.WaitOne();
                return _result;
            });

            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed -= Task_CompletedAsync;
            cameraCaptureTask.Completed += Task_CompletedAsync;
            cameraCaptureTask.Show();

            return task;
        }

        public Task<PictureServiceResult> DrawPictureAsync(PortableImage imageToEdit = null)
        {
            BitmapSource bitmap = null;

            if (imageToEdit != null)
                bitmap = (BitmapSource)imageToEdit.ImageSource;

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
        }

        private void Task_CompletedAsync(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                try
                {
                    var image = new BitmapImage();
                    var memoryStream = new MemoryStream();
                    e.ChosenPhoto.CopyTo(memoryStream);
                    image.SetSource(memoryStream);

                    var writeableBitmap = new WriteableBitmap(image);
                    var portableImage = new PortableImage(writeableBitmap)
                    {
                        Width = writeableBitmap.PixelWidth, 
                        Height = writeableBitmap.PixelHeight,
                        EncodedData = memoryStream
                    };


                    _result = new PictureServiceResult
                    {
                        Image = portableImage,
                        Status = PictureServiceStatus.Success
                    };
                }
                catch (Exception)
                {
                    _result = new PictureServiceResult
                    {
                        Image = null,
                        Status = PictureServiceStatus.Error
                    };
                }
            }
            else
            {
                _result = new PictureServiceResult
                {
                    Image = null,
                    Status = PictureServiceStatus.Cancelled
                };
            }

            _semaphore.Release(1);
        }
    }
}
