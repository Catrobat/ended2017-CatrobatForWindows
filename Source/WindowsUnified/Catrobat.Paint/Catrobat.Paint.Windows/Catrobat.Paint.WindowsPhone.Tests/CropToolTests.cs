using System;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Catrobat.Paint.WindowsPhone.Tests
{
    [TestClass]
    public class CropToolTests
    {
        private PocketPaintApplication app;
        private CropControl sut;

        [TestInitialize]
        [Timeout(2000)]
        public void Init()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    app = PocketPaintApplication.GetInstance();                
                    app.CropControl = new CropControl();
                    sut = app.CropControl;
                });
            while (sut == null) { }
        }

        [TestCleanup]
        public void CleanUp()
        {
            sut = null;
            app = null;
        }

        [TestMethod]
        [Ignore]
        public void AddWriteableBitmapToCanvasTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public async Task SetControlSizeTest()
        {
            await ExecuteOnUIThread(() =>
            {
                var height = 300;
                var width = 160;

                sut.SetControlSize(height, width);
                Grid grid = sut.Content as Grid;
                if (grid != null)
                {
                    Assert.AreEqual(grid.Width, width);
                    Assert.AreEqual(grid.Height, height);
                }
            });                   
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public async Task SetControlSizeMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    sut.SetControlSize(double.MaxValue, double.MaxValue));

            });
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public async Task SetControlSizeMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                sut.SetControlSize(double.MinValue, double.MinValue));

            });
        }


        

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 20.0;
                sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid) uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle) rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomVert":
                                case "rectLeftTopVert":
                                case "rectRightBottomVert":
                                case "rectRightTopVert":
                                    Assert.AreEqual(child.Height, value);
                                    break;
                                default:
                                    continue;

                            }
                        }
                    }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesHighValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 31.0;
                double maxValue = 30.0;
                sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomVert":
                                case "rectLeftTopVert":
                                case "rectRightBottomVert":
                                case "rectRightTopVert":
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;

                            }
                        }
                    }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesLowValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 4.0;
                double minValue = 5.0;
                sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomVert":
                                case "rectLeftTopVert":
                                case "rectRightBottomVert":
                                case "rectRightTopVert":
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;

                            }
                        }
                    }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double maxValue = 30.0;
                sut.SetHeightOfVerticalCornerRectangles(double.MaxValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomVert":
                                case "rectLeftTopVert":
                                case "rectRightBottomVert":
                                case "rectRightTopVert":
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;

                            }
                        }
                    }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double minValue = 5.0;
                sut.SetHeightOfVerticalCornerRectangles(double.MinValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomVert":
                                case "rectLeftTopVert":
                                case "rectRightBottomVert":
                                case "rectRightTopVert":
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;

                            }
                        }
                    }
            });
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





        public IAsyncAction ExecuteOnUIThread(DispatchedHandler action)
        {
            return Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, action);
        }

    }
}

