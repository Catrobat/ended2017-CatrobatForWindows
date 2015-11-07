using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catrobat.Player.StandAlone.Objects
{
    public class Object : IObject
    {
        public Object(string name)
        {
            this.name = name;
        }

        private string name;
        public string Name { get { return name; } set { } }

        public IList<IUserVariable> UserVariables
        {
            get
            {
                return new List<IUserVariable>() { new UserVariable() };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<IStartScript> StartScripts
        {
            get
            {
                return new List<IStartScript>() { new StartScript() };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<ILook> Looks
        {
            get
            {
                return new List<ILook>() { new Look() };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<IBroadcastScript> BroadcastScripts
        {
            get
            {
                return new List<IBroadcastScript>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<IWhenScript> WhenScripts
        {
            get
            {
                return new List<IWhenScript>();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
