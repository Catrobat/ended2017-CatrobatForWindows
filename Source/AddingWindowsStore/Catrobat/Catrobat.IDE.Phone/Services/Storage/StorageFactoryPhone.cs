using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Phone.Services.Storage
{
    public class StorageFactoryPhone : IStorageFactory
    {
        public IStorage CreateStorage()
        {
            var storage = new StoragePhone();

            var minDimension = Math.Min(ServiceLocator.SystemInformationService.ScreenHeight,
                ServiceLocator.SystemInformationService.ScreenWidth);

            if (minDimension <= 400)
            {
                storage.SetImageMaxThumbnailWidthHeight(150);
            }
            else if (minDimension <= 768)
            {
                storage.SetImageMaxThumbnailWidthHeight(220);
            }
            else
            {
                storage.SetImageMaxThumbnailWidthHeight(350);
            }

            return storage;
        }
    }
}