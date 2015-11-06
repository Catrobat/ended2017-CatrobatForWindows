using Catrobat_Player.NativeComponent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catrobat.Player.StandAlone.Objects
{
    public class UserVariable : IUserVariable
    {
        public string Name
        {
            get
            {
                return "var";
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
