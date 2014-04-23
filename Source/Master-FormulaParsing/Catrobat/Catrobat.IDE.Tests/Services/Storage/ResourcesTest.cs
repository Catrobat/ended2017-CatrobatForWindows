using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services.Storage;
using System;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Tests.Misc.Storage;

namespace Catrobat.IDE.Tests.Services.Storage
{
    public class ResourcesTest : IResourceLoader
    {
        private readonly List<Stream> _openedStreams = new List<Stream>();

        public Stream OpenResourceStream(ResourceScope project, string uri)
        {
            string projectPath = "";

            string basePath = BasePathHelper.GetTestBasePathWithBranch();

            switch (project)
            {
                case ResourceScope.Core:
                    projectPath = "Core/";
                    break;
                case ResourceScope.TestCommon:
                    projectPath = "Catrobat.IDE.Tests/";

                    if (!Directory.Exists(basePath + "/" + projectPath + "/SampleData")) // For testing on Build server
                        projectPath += "Binaries/";

                    break;
                case ResourceScope.Resources:
                    projectPath = "Catrobat.IDE.Phone/Content/Resources/";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("project");
            }

            Stream stream = File.Open(basePath + projectPath + uri, FileMode.Open, FileAccess.Read);
            _openedStreams.Add(stream);
            return stream;
        }

        public void Dispose()
        {
            foreach (var stream in _openedStreams)
            {
                stream.Close();
                stream.Dispose();
            }
        }


        public PortableImage LoadImage(ResourceScope resourceScope, string path)
        {
            throw new NotImplementedException();
        }


        public Task<Stream> OpenResourceStreamAsync(ResourceScope resourceScope, string uri)
        {
            return Task.Run(() => OpenResourceStream(resourceScope, uri));
        }

        public Task<PortableImage> LoadImageAsync(ResourceScope resourceScope, string path)
        {
            return Task.Run(() => LoadImage(resourceScope, path));
        }
    }
}
