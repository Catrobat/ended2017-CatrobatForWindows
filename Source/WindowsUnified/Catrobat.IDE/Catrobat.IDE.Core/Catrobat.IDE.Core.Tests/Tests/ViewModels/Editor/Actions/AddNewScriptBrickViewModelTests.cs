using System;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.Core.Tests.Tests.ViewModels.Editor.Actions
{
    [TestClass]
    public class AddNewScriptBrickViewModelTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            ServiceLocator.NavigationService = new NavigationServiceTest();
        }

        [TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        public void AddNewScriptBrickActionTest()
        {
            //TODO
            throw new NotImplementedException("Test not yet implemented");
        }

        [TestMethod, TestCategory("ViewModels.Editor"), TestCategory("ExcludeGated")]
        public void OnLoadBrickViewActionTest()
        {
            //TODO
            throw new NotImplementedException("Test not yet implemented");
        }
        
        [TestMethod, TestCategory("ViewModels.Editor")]
        public void GoBackActionTest()
        {
            var navigationService = (NavigationServiceTest)ServiceLocator.NavigationService;
            navigationService.PageStackCount = 1;
            navigationService.CurrentNavigationType = NavigationServiceTest.NavigationType.Initial;
            navigationService.CurrentView = typeof(AddNewScriptBrickViewModel);

            // private member also affected by back-action
            var action = new CommentBrick()
            {
                Value = "TestValue"   
            };
            var collection = new BrickCollection();
            collection.Add(action);
            var viewModel = new AddNewScriptBrickViewModel
            {
                BrickCollection = collection
            };
            viewModel.GoBackCommand.Execute(null);

            Assert.IsNull(viewModel.BrickCollection);
            Assert.AreEqual(NavigationServiceTest.NavigationType.NavigateBack, navigationService.CurrentNavigationType);
            Assert.AreEqual(null, navigationService.CurrentView);
            Assert.AreEqual(0, navigationService.PageStackCount);
        }
    }
}
