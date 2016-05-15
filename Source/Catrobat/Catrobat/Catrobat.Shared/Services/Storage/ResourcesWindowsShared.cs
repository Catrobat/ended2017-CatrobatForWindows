using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.WindowsShared.Services.Storage
{
    public class ResourcesWindowsShared : IResourceLoader
    {
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public async Task<Stream> OpenResourceStreamAsync(ResourceScope project, string uri)
        {
            var projectPath = "";

            switch (project)
            {
                case ResourceScope.Core:
                    {
                        throw new NotImplementedException();
                        //projectPath = "Catrobat.IDE.Core";
                        //var path = Path.Combine(projectPath, uri);
                        //path = path.Replace("\\", "/");
                        //path = path.Replace("/", ".");
                        //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
                        //_openedStreams.Add(stream);
                        //return stream;
                    }
                case ResourceScope.Ide:
                    {
                        try
                        {
                            uri = uri.Replace("\\", "/");
                            const string prefix = "ms-appx:///";
                            var file = await StorageFile.GetFileFromApplicationUriAsync(
                                new Uri(Path.Combine(prefix, uri), UriKind.Absolute));
                            var randomAccessStream = await file.OpenReadAsync();
                            var stream = randomAccessStream.AsStream();
                            _openedStreams.Add(stream);
                            return stream;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
   
                    }
                case ResourceScope.TestsPhone:
                    {
                        throw new NotImplementedException();
                    }
                case ResourceScope.Resources:
                    {
                        const string prefix = "ms-appx:///Content/Resources";
                        var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(Path.Combine(prefix, uri), UriKind.Absolute));
                        var randomAccessStream = await file.OpenReadAsync();
                        var stream = randomAccessStream.AsStream();
                        _openedStreams.Add(stream);
                        return stream;
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

        public Task<PortableImage> LoadImageAsync(ResourceScope resourceScope, string path)
        {
            return new Task<PortableImage>(()=> new PortableImage(0,0));

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

        public Stream OpenResourceStream(ResourceScope project, string uri)
        {
            var task = OpenResourceStreamAsync(project, uri);
            task.Wait();
            return task.Result;
        }

        public PortableImage LoadImage(ResourceScope resourceScope, string path)
        {
            var task = LoadImageAsync(resourceScope, path);
            task.Wait();
            return task.Result;
        }
    }
}