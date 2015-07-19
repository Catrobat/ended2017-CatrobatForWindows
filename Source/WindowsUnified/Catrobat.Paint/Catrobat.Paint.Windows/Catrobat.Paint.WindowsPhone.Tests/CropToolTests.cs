using System;
using System.Linq;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.NativeUnitTestWizards;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.WindowsPhone.Tool;
using Catrobat.Paint.WindowsPhone.View;

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
                    PaintingAreaView view = new PaintingAreaView();
                    //app.CropControl = new CropControl();
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
        [TestCategory("SetRectangleForMovementSize")]
        public async Task SetRectangleForMovementSizeTest()
        {
            await ExecuteOnUIThread(() =>
            {
                var height = 300;
                var width = 160;

                sut.SetRectangleForMovementSize(height, width);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectRectangleCropSelection":
                                    Assert.AreEqual(child.Height, height);
                                    Assert.AreEqual(child.Width, width);

                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetRectangleForMovementSize")]
        public async Task SetRectangleForMovementSizeMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    sut.SetRectangleForMovementSize(double.MaxValue, double.MaxValue));

            });
        }

        [TestMethod]
        [TestCategory("SetRectangleForMovementSize")]
        public async Task SetRectangleForMovementSizeMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                sut.SetRectangleForMovementSize(double.MinValue, double.MinValue));

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
                {
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
                {
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
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;
                            }
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
                {
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
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;
                            }
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
                {
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
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;
                            }
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
                {
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
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCenterRectangles")]
        public async Task SetHeightOfVerticalCenterRectanglesTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 20.0;
                sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftCenter":
                                case "rectRightCenter":
                                    Assert.AreEqual(child.Height, value);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCenterRectangles")]
        public async Task SetHeightOfVerticalCenterRectanglesHighValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftCenter":
                                case "rectRightCenter":
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCenterRectangles")]
        public async Task SetHeightOfVerticalCenterRectanglesLowValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 4.0;
                double minValue = 5.0;
                sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftCenter":
                                case "rectRightCenter":
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCenterRectangles")]
        public async Task SetHeightOfVerticalCenterRectanglesMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double maxValue = 120.0;
                sut.SetHeightOfVerticalCenterRectangles(double.MaxValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftCenter":
                                case "rectRightCenter":
                                    Assert.AreEqual(child.Height, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetHeightOfVerticalCenterRectangles")]
        public async Task SetHeightOfVerticalCenterRectanglesMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double minValue = 5.0;
                sut.SetHeightOfVerticalCenterRectangles(double.MinValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftCenter":
                                case "rectRightCenter":
                                    Assert.AreEqual(child.Height, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCenterRectangles")]
        public async Task SetWidthOfHorizontalCenterRectanglesTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 120.0;
                sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectCenterTop":
                                case "rectCenterBottom":
                                    Assert.AreEqual(child.Width, value);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCenterRectangles")]
        public async Task SetWidthOfHorizontalCenterRectanglesHighValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectCenterTop":
                                case "rectCenterBottom":
                                    Assert.AreEqual(child.Width, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCenterRectangles")]
        public async Task SetWidthOfHorizontalCenterRectanglesLowValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 4.0;
                double minValue = 120.0;
                sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectCenterTop":
                                case "rectCenterBottom":
                                    Assert.AreEqual(child.Width, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCenterRectangles")]
        public async Task SetWidthOfHorizontalCenterRectanglesMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double maxValue = 120.0;
                sut.SetWidthOfHorizontalCenterRectangles(double.MaxValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectCenterTop":
                                case "rectCenterBottom":
                                    Assert.AreEqual(child.Width, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCenterRectangles")]
        public async Task SetWidthOfHorizontalCenterRectanglesMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double minValue = 120.0;
                sut.SetWidthOfHorizontalCenterRectangles(double.MinValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectCenterTop":
                                case "rectCenterBottom":
                                    Assert.AreEqual(child.Width, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCornerRectangles")]
        public async Task SetWidthOfHorizontalCornerRectanglesTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 120.0;
                sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomHorz":
                                case "rectLeftTopHorz":
                                case "rectRightBottomHorz":
                                case "rectRightTopHorz":
                                    Assert.AreEqual(child.Width, value);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCornerRectangles")]
        public async Task SetWidthOfHorizontalCornerRectanglesHighValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomHorz":
                                case "rectLeftTopHorz":
                                case "rectRightBottomHorz":
                                case "rectRightTopHorz":
                                    Assert.AreEqual(child.Width, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCornerRectangles")]
        public async Task SetWidthOfHorizontalCornerRectanglesLowValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = 4.0;
                double minValue = 120.0;
                sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomHorz":
                                case "rectLeftTopHorz":
                                case "rectRightBottomHorz":
                                case "rectRightTopHorz":
                                    Assert.AreEqual(child.Width, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCornerRectangles")]
        public async Task SetWidthOfHorizontalCornerRectanglesMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double maxValue = 120.0;
                sut.SetWidthOfHorizontalCornerRectangles(double.MaxValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomHorz":
                                case "rectLeftTopHorz":
                                case "rectRightBottomHorz":
                                case "rectRightTopHorz":
                                    Assert.AreEqual(child.Width, maxValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        [TestCategory("SetWidthOfHorizontalCornerRectangles")]
        public async Task SetWidthOfHorizontalCornerRectanglesMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double minValue = 120.0;
                sut.SetWidthOfHorizontalCornerRectangles(double.MinValue);
                var maingrid = sut.Content as Grid;
                if (maingrid != null)
                {
                    foreach (var uiElement1 in maingrid.Children)
                    {
                        var uiElement = (Grid)uiElement1;
                        foreach (var rectangle in uiElement.Children)
                        {
                            var child = (Rectangle)rectangle;
                            switch (child.Name)
                            {
                                case "rectLeftBottomHorz":
                                case "rectLeftTopHorz":
                                case "rectRightBottomHorz":
                                case "rectRightTopHorz":
                                    Assert.AreEqual(child.Width, minValue);
                                    break;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            });
        }


        [TestMethod]
        [TestCategory("GetWidthOfRectangleCropSelection")]
        [Ignore]
        public async Task GetWidthOfRectangleCropSelectionTest()
        {
            await ExecuteOnUIThread(() =>
            {
                
            if (app != null)
            {
                app.AppbarTop.BtnSelectedColorVisible(false);
                app.isBrushEraser = false;
                app.isBrushTool = false;
                app.isToolPickerUsed = true;
                PocketPaintApplication.GetInstance()
                    .PaintingAreaView.changeBackgroundColorAndOpacityOfPaintingAreaCanvas(Colors.Transparent, 1.0);
                app.PaintingAreaView.resetControls();

                app.SwitchTool(ToolType.Crop);
                app.CropControl.Visibility = Visibility.Visible;
                app.CropControl.SetCropSelection();
            }

            });

            await Task.Delay(TimeSpan.FromSeconds(7));

            await ExecuteOnUIThread(() =>
            {
                double value = sut.GetWidthOfRectangleCropSelection();
                var maingrid = sut.Content as Grid;
                double val = value;

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

