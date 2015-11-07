using System;
using System.Collections.Generic;
using Catrobat_Player.NativeComponent;
using System.Linq;

namespace Catrobat.Player.StandAlone.Objects
{
    public class Project : IProject
    {
        private List<Object> objects;
        private string name;
        public Project(string name)
        {
            objects = new List<Object>();
            objects.Add(new Object("TestObject1"));
            objects.Add(new Object("TestObject2"));
            this.name = name;
        }

        public string Name { get { return name; } set { } }

        public IList<IObject> Objects
        {
            get
            {
                return objects.Cast<IObject>().ToList();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<IObject> Variables
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        public string Platform
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MediaLicense
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceName
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CatrobatLanguageVersion
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationVersion
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationName
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationBuildName
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int PlatformVersion
        {
            get
            {
                return 3;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public long DateTimeUpload
        {
            get
            {
                return 3;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ApplicationBuildNumber
        {
            get
            {
                return 3;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserHandle
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Url
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<string> Tags
        {
            get
            {
                return new List<string>() { "Tag1", "Tag2", "Tag3" };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ScreenWidth
        {
            get
            {
                return 3;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ScreenHeight
        {
            get
            {
                return 3;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string RemixOf
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ProgramName
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ProgramLicense
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TargetPlatform
        {
            get
            {
                return "Test";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void PersistProjectStructure()
        {
            NativeWrapper.SetProject(this);
        }
    }
}
