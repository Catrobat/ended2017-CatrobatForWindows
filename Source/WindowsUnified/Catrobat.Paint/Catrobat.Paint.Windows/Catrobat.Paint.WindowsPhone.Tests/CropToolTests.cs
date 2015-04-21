using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Catrobat.Paint.WindowsPhone.Tests
{
    [TestClass]
    public class CropToolTests
    {
        [TestMethod]
        [Ignore]
        public void AddWriteableBitmapToCanvasTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public void SetControlSizeTest()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PocketPaintApplication app = PocketPaintApplication.GetInstance();
                app.CropControl = new CropControl();
                CropControl sut = app.CropControl;              
                sut.SetControlSize(300, 160);
            }
            );
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public void SetControlSizeMaxValueTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PocketPaintApplication app = PocketPaintApplication.GetInstance();
                app.CropControl = new CropControl();
                CropControl sut = app.CropControl;
                try
                {
                    sut.SetControlSize(double.MaxValue, double.MaxValue);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
            ));
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public void SetControlSizeMinValueTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PocketPaintApplication app = PocketPaintApplication.GetInstance();
                app.CropControl = new CropControl();
                CropControl sut = app.CropControl;
                sut.SetControlSize(double.MinValue, double.MinValue);
            }
            ));
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public void SetControlSizeNaNValueTest()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PocketPaintApplication app = PocketPaintApplication.GetInstance();
                app.CropControl = new CropControl();
                CropControl sut = app.CropControl;
                sut.SetControlSize(double.NaN, double.NaN);
            }
            );
        }

        [TestMethod]
        [Ignore]

        public void ChangeHeightOfUiElementsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]

        public void ChangeMarginBottomOfUiElementsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void ChangeMarginLeftOfUiElementsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void ChangeMarginRightOfUiElementsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void ChangeMarginTopOfUiElementsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void rectRectangleCropSelection_ManipulationDeltaTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void rectRectangleCropSelection_DoubleTappedTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void ResetAppBarButtonRectangleSelectionControlTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void HasElementsPaintingAreaViewsTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void GetRectangleCropSelectionHeightTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void GetRectangleCropSelectionWidthTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void GetLeftTopCoordinateRectangleCropSelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void SetLeftTopNullPointCropSelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void GetLeftTopNullPointCropSelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void CropImageTest()
        {
            Assert.Fail();
        }

    }
}

