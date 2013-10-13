using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Catrobat.IDE.Core.Services.Data;
using Catrobat.IDE.Core.Services.Storage;
using Coding4Fun.Toolkit.Controls.Common;

namespace Catrobat.IDE.Phone.Services.Storage
{
    public class ResourcesPhone : IResourceLoader
    {
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public Stream OpenResourceStream(ResourceScope project, string uri)
        {
            var projectPath = "";

            switch (project)
            {
                case ResourceScope.Core:
                    {
                        projectPath = "Catrobat.IDE.Core.";
                        var path = projectPath + uri.Replace("/", ".");
                        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                        _openedStreams.Add(stream);
                        return stream;
                    }
                case ResourceScope.IdePhone:
                {
                    projectPath = "";// "/Catrobat.IDE.Phone;component";
                        var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
                        var resource = Application.GetResourceStream(resourceUri);

                        if (resource != null)
                        {
                            var stream = resource.Stream;
                            _openedStreams.Add(stream);
                            return stream;
                        }

                        return null;
                    }
                case ResourceScope.TestsPhone:
                    {
                        projectPath = "";
                        var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
                        var resource = Application.GetResourceStream(resourceUri);

                        if (resource != null)
                        {
                            var stream = resource.Stream;
                            _openedStreams.Add(stream);
                            return stream;
                        }

                        return null;
                    }
                case ResourceScope.Resources:
                    {
                        projectPath = "Content/Resources/";
                        var resourceUri = new Uri(projectPath + uri, UriKind.Relative);
                        var resource = Application.GetResourceStream(resourceUri);

                        if (resource != null)
                        {
                            var stream = resource.Stream;
                            _openedStreams.Add(stream);
                            return stream;
                        }

                        return null;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException("project");
                    }
            }
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
            if (resourceScope != ResourceScope.IdePhone)
                throw new NotImplementedException("Only ResourceScope.IdePhone is implemented");

            return new BitmapImage(new Uri(path, UriKind.Relative));

            //try
            //{
            //    Stream s = OpenResourceStream(resourceScope, path);

            //    using (s)
            //    {
            //        var bitmapImage = new BitmapImage();
            //        bitmapImage.SetSource(s);
            //        bitmapImage.CreateOptions = BitmapCreateOptions.None;

            //        //var bitmapImage = new BitmapImage(new Uri(path, UriKind.Relative)); // TODO: check uri, and maybe add prefix

            //        var writeableBitmap = new WriteableBitmap(bitmapImage);
            //        var portableImage = new PortableImage(writeableBitmap.ToByteArray(), writeableBitmap.PixelWidth,
            //            writeableBitmap.PixelHeight);

            //        return portableImage;
            //    }
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }
    }
}