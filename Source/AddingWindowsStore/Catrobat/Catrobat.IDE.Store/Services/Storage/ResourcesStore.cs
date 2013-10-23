using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Store.Services.Storage
{
    public class ResourcesStore : IResourceLoader
    {
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public Stream OpenResourceStream(ResourceScope project, string uri)
        {
            throw new NotImplementedException();

            //var projectPath = "";

            //switch (project)
            //{
            //    case ResourceScope.Core:
            //        {
            //            projectPath = "Catrobat.IDE.Core";
            //            var path = Path.Combine(projectPath, uri);
            //            path = path.Replace("\\", "/");
            //            path = path.Replace("/", ".");
            //            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            //            _openedStreams.Add(stream);
            //            return stream;
            //        }
            //    case ResourceScope.IdePhone:
            //    {
            //        projectPath = "";// "/Catrobat.IDE.Phone;component";
            //            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            //            var resource = Application.GetResourceStream(resourceUri);

            //            if (resource != null)
            //            {
            //                var stream = resource.Stream;
            //                _openedStreams.Add(stream);
            //                return stream;
            //            }

            //            return null;
            //        }
            //    case ResourceScope.TestsPhone:
            //        {
            //            projectPath = "";
            //            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            //            var resource = Application.GetResourceStream(resourceUri);

            //            if (resource != null)
            //            {
            //                var stream = resource.Stream;
            //                _openedStreams.Add(stream);
            //                return stream;
            //            }

            //            return null;
            //        }
            //    case ResourceScope.Resources:
            //        {
            //            projectPath = "Content/Resources/";
            //            var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
            //            var resource = Application.GetResourceStream(resourceUri);

            //            if (resource != null)
            //            {
            //                var stream = resource.Stream;
            //                _openedStreams.Add(stream);
            //                return stream;
            //            }

            //            return null;
            //        }
            //    default:
            //        {
            //            throw new ArgumentOutOfRangeException("project");
            //        }
            //}
        }

        public void Dispose()
        {
            foreach (var stream in _openedStreams)
            {
                stream.Dispose();
            }
        }

        public object LoadImage(ResourceScope resourceScope, string path)
        {
            throw new NotImplementedException();

            //if (resourceScope != ResourceScope.IdePhone)
            //    throw new NotImplementedException("Only ResourceScope.IdePhone is implemented");

            //var image = new BitmapImage();
            //var stream = OpenResourceStream(resourceScope, path);
            //if (stream != null)
            //{
            //    image.SetSource(stream);
            //    return image;
            //}
            //else
            //{
            //    return new BitmapImage(new Uri(path, UriKind.Relative));
            //    //return null;
            //}
            
        }
    }
}