using System;
using System.IO;
using System.Windows.Navigation;
using Windows.Phone.Storage.SharedAccess;

namespace Catrobat.IDEWindowsPhone.Misc
{
    internal class AssociationUriMapper : UriMapperBase
    {
        private string _tempUri;

        public override Uri MapUri(Uri uri)
        {
            _tempUri = uri.ToString();

            // File association launch
            if (_tempUri.Contains("/FileTypeAssociation"))
            {
                // Get the file ID (after "fileToken=").
                var fileIdIndex = _tempUri.IndexOf("fileToken=", StringComparison.InvariantCulture) + 10;
                var fileId = _tempUri.Substring(fileIdIndex);

                // Get the file name.
                var incomingFileName = SharedStorageAccessManager.GetSharedFileName(fileId);

                // Get the file extension.
                var incomingFileType = Path.GetExtension(incomingFileName);

                // Map the .sdkTest1 and .sdkTest2 files to different pages.
                switch (incomingFileType)
                {
                    case ".catroid":
                    case ".catrobat":
                        return new Uri("/Views/Main/ProjectImportView.xaml?fileToken=" + fileId, UriKind.Relative);
                    default:
                        return new Uri("/Views/Main/MainView.xaml", UriKind.Relative);
                }
            }
            // Otherwise perform normal launch.
            return uri;
        }
    }
}