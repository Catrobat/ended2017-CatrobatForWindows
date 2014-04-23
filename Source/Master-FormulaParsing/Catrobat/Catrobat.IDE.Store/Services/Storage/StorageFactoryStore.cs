using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Store.Services.Storage
{
    public class StorageFactoryStore : IStorageFactory
    {
        public IStorage CreateStorage()
        {
            var storage = new StorageStore();

            var minDimension = Math.Min(ServiceLocator.SystemInformationService.ScreenHeight,
                ServiceLocator.SystemInformationService.ScreenWidth);

            if (minDimension <= 700)
            {
                storage.SetImageMaxThumbnailWidthHeight(300);
            }
            else if (minDimension <= 1400)
            {
                storage.SetImageMaxThumbnailWidthHeight(500);
            }
            else
            {
                storage.SetImageMaxThumbnailWidthHeight(700);
            }

            return storage;
        }
    }
}