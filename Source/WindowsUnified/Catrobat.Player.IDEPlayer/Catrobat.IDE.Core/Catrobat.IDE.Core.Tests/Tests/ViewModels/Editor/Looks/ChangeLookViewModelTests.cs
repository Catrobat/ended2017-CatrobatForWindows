using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.Tests.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Looks
{
    [TestClass]
    public class ChangeLookViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<PictureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.Register<ContextServiceTest>(TypeCreationMode.Lazy);
            
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void SaveActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            var look = new Look 
            { 
                Name = "TestLook", 
                FileName = "TestFilename"
            };
            var look2 = new Look
            {
                Name = "TestLook2",
                FileName = "TestFilename2"
            };
            var sprite = new Sprite();
            sprite.Looks.Add(look);
            sprite.Looks.Add(look2);
            var messageContext = new GenericMessage<Sprite>(sprite);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentSpriteChangedListener);

            var messageContext2 = new GenericMessage<Look>(look);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.LookListener);
            changeLookViewModel.LookName = "NewLookName";

            changeLookViewModel.SaveCommand.Execute(null);

            Assert.AreEqual("NewLookName", changeLookViewModel.ReceivedLook.Name);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void CancelActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            changeLookViewModel.CancelCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void EditLookActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();

            var look = new Look 
            { 
                Name = "TestLook", 
                FileName = "TestFilename", 
                Image = new PortableImage() 
            };
            var messageContext = new GenericMessage<Look>(look);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.LookListener);

            var program = new Program 
            { 
                Name = "TestProgram" 
            };
            var messageContext2 = new GenericMessage<Program>(program);
            Messenger.Default.Send(messageContext2, ViewModelMessagingToken.CurrentProgramChangedListener);

            changeLookViewModel.EditLookCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }

        [TestMethod, TestCategory("ViewModels.Editor")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ChangeLookViewModel);

            var changeLookViewModel = new ChangeLookViewModel();
            changeLookViewModel.LookName = "NewLookName";
            changeLookViewModel.GoBackCommand.Execute(null);

            Assert.AreEqual("", changeLookViewModel.LookName);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
