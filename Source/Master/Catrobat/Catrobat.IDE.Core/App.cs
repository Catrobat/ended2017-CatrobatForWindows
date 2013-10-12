using Cirrious.CrossCore.IoC;

namespace Catrobat.IDE.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            //CreatableTypes()
            //    .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();
				
            //RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}