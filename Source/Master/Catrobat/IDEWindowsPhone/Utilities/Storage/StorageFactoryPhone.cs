using System;
using Catrobat.Core.Utilities.Storage;
using Catrobat.IDEWindowsPhone.Utilities.Helpers;

namespace Catrobat.IDEWindowsPhone.Utilities.Storage
{
    public class StorageFactoryPhone : IStorageFactory
    {
        public IStorage CreateStorage()
        {
            var storage = new StoragePhone();

            switch (ResolutionHelper.CurrentResolution)
            {
                case Resolutions.WVGA:
                    storage.SetImageMaxThumbnailWidthHeight(150);
                    break;
                case Resolutions.WXGA:
                    storage.SetImageMaxThumbnailWidthHeight(220);
                    break;
                case Resolutions.HD720P:
                    storage.SetImageMaxThumbnailWidthHeight(220);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return storage;
        }
    }
}