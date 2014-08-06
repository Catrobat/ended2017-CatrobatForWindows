using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;


namespace Catrobat.IDE.WindowsPhone.TestsUI
{
    /// <summary>
    /// Basic UI Tests
    /// Deloy application - and run the application - do not navigate anywhere - run tests - order of the tests does 
    /// matter if you don't want to start the application each time
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void NavigateToLocalProjectHubSectionViaAllHubSectionsUITest()
        {
            XamlWindow myXamlWindow = XamlWindow.Launch("{45908950-905F-4A90-867A-03DDC82B656B}:App:45908950-905f-4a90-867a-03ddc82b656b_bcp11qa1rfadr!App");
            var mainHub = this.UIMap.UIPocketCodeIDEWindow.UIMainHubHub;
            Point absoluteStartPoint = new Point(mainHub.BoundingRectangle.Right - 10, mainHub.BoundingRectangle.Top + mainHub.BoundingRectangle.Height / 2);
            Point absoluteEndPoint = new Point(mainHub.BoundingRectangle.Left + 10, mainHub.BoundingRectangle.Top + mainHub.BoundingRectangle.Height / 2);
            Gesture.Slide(absoluteStartPoint, absoluteEndPoint);
            Gesture.Slide(absoluteStartPoint, absoluteEndPoint);
            this.UIMap.AssertNavigateToLocalProgramsHubSection();
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void NavigateToOnlineProjectHubSectionUITest()
        {
            XamlWindow myXamlWindow = XamlWindow.Launch("{45908950-905F-4A90-867A-03DDC82B656B}:App:45908950-905f-4a90-867a-03ddc82b656b_bcp11qa1rfadr!App");
            var mainHub = this.UIMap.UIPocketCodeIDEWindow.UIMainHubHub;
            Point relativeStartPoint = new Point(mainHub.BoundingRectangle.Width - 10, mainHub.BoundingRectangle.Height / 2);
            Point relativeEndPoint = new Point(10, mainHub.BoundingRectangle.Height / 2);
            Gesture.Slide(mainHub, relativeStartPoint, relativeEndPoint);
            this.UIMap.AssertNavigateToOnlineProgramsHubSection();
        }

        [TestMethod, TestCategory("ExcludeGated")]
        public void NavigateToAddNewProgramUITest()
        {
            XamlWindow myXamlWindow = XamlWindow.Launch("{45908950-905F-4A90-867A-03DDC82B656B}:App:45908950-905f-4a90-867a-03ddc82b656b_bcp11qa1rfadr!App");
            Gesture.Tap(this.UIMap.UIItemWindow.UINewButton);
            this.UIMap.AssertNavigateToNewProgramm();
        }

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
