using System;
using System.Threading;
using Windows.UI.Core;
using Catrobat.Paint.WindowsPhone.Controls.UserControls;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Catrobat.Paint.WindowsPhone.View;
using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Tests
{
    [TestClass]
    public class CropToolTests
    {
        private PocketPaintApplication app;
        private CropControl sut;

        [TestInitialize]
        [Timeout(2000)]
        public async void Init()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
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
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionTest()
        {
            await ExecuteOnUIThread(() =>
            {
                var height = 470;
                var width = 290;
                TranslateTransform trans = new TranslateTransform();
                trans.X = 60;
                trans.Y = 100;

                sut.SetCropControlPosition(height, width, trans);
                Grid grid = sut.Content as Grid;
                if (grid != null)
                {
                    Assert.AreEqual(grid.Width, width);
                    Assert.AreEqual(grid.Height, height);

                    foreach (var uiElement1 in grid.Children)
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
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionMaxValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                TranslateTransform trans = new TranslateTransform();
                trans.X = 60;
                trans.Y = 100;

                Assert.ThrowsException<ArgumentException>(() =>
                    sut.SetCropControlPosition(double.MaxValue, double.MaxValue, trans));

            });
        }

        [TestMethod]
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionMinValueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                TranslateTransform trans = new TranslateTransform();
                trans.X = 60;
                trans.Y = 100;

                Assert.ThrowsException<ArgumentException>(() =>
                sut.SetCropControlPosition(double.MinValue, double.MinValue, trans));

            });
        }

        [TestMethod]
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionTransNullTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                sut.SetCropControlPosition(470, 290, null));

            });
        }

        [TestMethod]
        [TestCategory("ResetAppBarButtonRectangleSelectionControl")]
        public async Task ResetAppBarButtonRectangleSelectionControlTrueTest()
        {
            await ExecuteOnUIThread(() =>
            {
                bool value = true;
                sut.ResetAppBarButtonRectangleSelectionControl(value);
                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();

                Assert.AreEqual(appBarButtonReset.IsEnabled, value);
                

            });
        }

        [TestMethod]
        [TestCategory("ResetAppBarButtonRectangleSelectionControl")]
        public async Task ResetAppBarButtonRectangleSelectionControlFalseTest()
        {
            await ExecuteOnUIThread(() =>
            {
                bool value = false;
                sut.ResetAppBarButtonRectangleSelectionControl(value);
                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();

                Assert.AreEqual(appBarButtonReset.IsEnabled, value);
                
            });
        }

        [TestMethod]
        [TestCategory("GetHeightOfRectangleCropSelection")]
        public async Task GetHeightOfRectangleCropSelectionInfinityTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = sut.GetHeightOfRectangleCropSelection();
                Assert.AreEqual(Double.IsInfinity(value), true);
            });
        }

        [TestMethod]
        [TestCategory("GetWidthOfRectangleCropSelection")]
        public async Task GetWidthOfRectangleCropSelectionInfinityTest()
        {
            await ExecuteOnUIThread(() =>
            {
                double value = sut.GetWidthOfRectangleCropSelection();
                Assert.AreEqual(Double.IsInfinity(value), true);
            });
        }

        [TestMethod]
        [TestCategory("HasElementsPaintingAreaViews")]
        public async Task HasElementsPaintingAreaViewsTest()
        {
            await ExecuteOnUIThread(() =>
            {
                bool result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0;

                bool value = sut.HasPaintingAreaViewElements();
                Assert.AreEqual(value, result);
            });
        }

        [TestMethod]
        [TestCategory("HasElementsPaintingAreaViews")]
        public async Task HasElementsPaintingAreaViewsNotTest()
        {
            await ExecuteOnUIThread(() =>
            {
                bool result = !(PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0);

                bool value = sut.HasPaintingAreaViewElements();
                Assert.AreNotEqual(value, result);
            });
        }

        [TestMethod]
        [TestCategory("GetXYOffsetBetweenPaintingAreaAndCropControlSelection")]
        public async Task GetXYOffsetBetweenPaintingAreaAndCropControlSelectionTest()
        {
            await ExecuteOnUIThread(() =>
            {
                Point value = new Point(0.0, 0.0);
                value = sut.GetXYOffsetBetweenPaintingAreaAndCropControlSelection();
                Assert.AreNotEqual(value, null);
                Assert.AreNotEqual(value.X, 0.0);
                Assert.AreNotEqual(value.Y, 0.0);
            });
        }

        public IAsyncAction ExecuteOnUIThread(DispatchedHandler action)
        {
            return Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, action);
        }

    }
}

