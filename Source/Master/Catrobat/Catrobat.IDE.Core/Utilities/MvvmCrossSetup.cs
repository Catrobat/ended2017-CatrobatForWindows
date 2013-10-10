using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.IoC;

namespace Catrobat.IDE.Core.Utilities
{
    public class MvvmCrossSetup
    {
        public static readonly MvvmCrossSetup Instance = new MvvmCrossSetup();

        public void EnsureInit()
        {
            if (MvxSimpleIoCContainer.Instance != null)
                return;

            var ioc = MvxSimpleIoCContainer.Initialize();
        }

    }
}
