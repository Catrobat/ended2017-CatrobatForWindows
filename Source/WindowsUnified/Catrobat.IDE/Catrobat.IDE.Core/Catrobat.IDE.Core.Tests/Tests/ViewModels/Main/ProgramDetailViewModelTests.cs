using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.Tests.Services.Common;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.CatrobatObjects;
using System.Globalization;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Main
{
    [TestClass]
    public class ProgramDetailViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
            ServiceLocator.UnRegisterAll();
            ServiceLocator.Register<DispatcherServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("ViewModels.Main"), TestCategory("ExcludeGated")]
        public void NavigateToTest()
        {
            Assert.AreEqual(0, "test not implemented");
        }

        [TestMethod, TestCategory("ViewModels.Main")]
        public void EditCurrentProgramActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var program = new Program
            {
                Name = "TestProgram"
            };
            var programHeader = new LocalProgramHeader
            {
                ProjectName = "TestProgram"
            };
            var viewModel = new ProgramDetailViewModel
            {
                CurrentProgram = program
            };

            var messageContext = new GenericMessage<LocalProgramHeader>(programHeader);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            viewModel.NavigateTo();
            viewModel.EditCurrentProgramCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(SpritesViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount); 
        }

        [TestMethod, TestCategory("ViewModels.Main"), TestCategory("ExcludeGated")]
        public void PlayCurrentProjectActionTest()
        {
            //Launch Player
            Assert.AreEqual(0, "test not implemented");
        }


        [TestMethod, TestCategory("ViewModels.Main"), TestCategory("ExcludeGated")]
        public void ShareLocalProjectActionTest()
        {
            Assert.AreEqual(0, "Test also saving of current project");
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var program = new Program
            {
                Name = "TestProgram"
            };
            var viewModel = new ProgramDetailViewModel
            {
                CurrentProgram = program
            };
            viewModel.ShareLocalProgramCommand.Execute(null);
            
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProgramExportViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);    
        }

        [TestMethod, TestCategory("ViewModels.Main")]
        public void RenameProjectActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(ProgramDetailViewModel);

            var program = new Program
            {
                Name = "TestProgram"
            };
            var programHeader = new LocalProgramHeader
            {
                ProjectName = "TestProgram"
            };
            var viewModel = new ProgramDetailViewModel
            {
                CurrentProgram = program
            };

            var messageContext = new GenericMessage<LocalProgramHeader>(programHeader);
            Messenger.Default.Send(messageContext, ViewModelMessagingToken.CurrentProgramHeaderChangedListener);

            viewModel.NavigateTo();
            viewModel.RenameProgramCommand.Execute(null);

            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateTo, navigationService.CurrentNavigationType);
            Assert.AreEqual(typeof(ProgramSettingsViewModel), navigationService.CurrentView);
            Assert.AreEqual(2, navigationService.PageStackCount);  
        }

        //[TestMethod, TestCategory("ViewModels.Main")]
        //public void PinLocalProjectActionTest()
        //{
        //    // currently not in use
        //}

        
    }
}
