using System;
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
        private PocketPaintApplication _app;
        private CropControl _sut;

        [TestInitialize]
        [Timeout(2000)]
        public void Init()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    _app = PocketPaintApplication.GetInstance();
                    new PaintingAreaView();
                    //app.CropControl = new CropControl();
                    _sut = _app.CropControl;
                });
            while (_sut == null) { }
        }

        [TestCleanup]
        public void CleanUp()
        {
            _sut = null;
            _app = null;
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public async Task SetControlSizeTest()
        {
            await ExecuteOnUiThread(() =>
            {
                var height = 300;
                var width = 160;

                _sut.SetControlSize(height, width);
                Grid grid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    _sut.SetControlSize(double.MaxValue, double.MaxValue));

            });
        }

        [TestMethod]
        [TestCategory("SetControlSize")]
        public async Task SetControlSizeMinValueTest()
        {
            await ExecuteOnUiThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                _sut.SetControlSize(double.MinValue, double.MinValue));

            });
        }

        [TestMethod]
        [TestCategory("SetRectangleForMovementSize")]
        public async Task SetRectangleForMovementSizeTest()
        {
            await ExecuteOnUiThread(() =>
            {
                var height = 300;
                var width = 160;

                _sut.SetRectangleForMovementSize(height, width);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                    _sut.SetRectangleForMovementSize(double.MaxValue, double.MaxValue));

            });
        }

        [TestMethod]
        [TestCategory("SetRectangleForMovementSize")]
        public async Task SetRectangleForMovementSizeMinValueTest()
        {
            await ExecuteOnUiThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                _sut.SetRectangleForMovementSize(double.MinValue, double.MinValue));

            });
        }
        
        [TestMethod]
        [TestCategory("SetHeightOfVerticalCornerRectangles")]
        public async Task SetHeightOfVerticalCornerRectanglesTest()
        {
            await ExecuteOnUiThread(() =>
            {
                double value = 20.0;
                _sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 31.0;
                double maxValue = 30.0;
                _sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 4.0;
                double minValue = 5.0;
                _sut.SetHeightOfVerticalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double maxValue = 30.0;
                _sut.SetHeightOfVerticalCornerRectangles(double.MaxValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double minValue = 5.0;
                _sut.SetHeightOfVerticalCornerRectangles(double.MinValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 20.0;
                _sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                _sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 4.0;
                double minValue = 5.0;
                _sut.SetHeightOfVerticalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double maxValue = 120.0;
                _sut.SetHeightOfVerticalCenterRectangles(double.MaxValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double minValue = 5.0;
                _sut.SetHeightOfVerticalCenterRectangles(double.MinValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 120.0;
                _sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                _sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 4.0;
                double minValue = 120.0;
                _sut.SetWidthOfHorizontalCenterRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double maxValue = 120.0;
                _sut.SetWidthOfHorizontalCenterRectangles(double.MaxValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double minValue = 120.0;
                _sut.SetWidthOfHorizontalCenterRectangles(double.MinValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 120.0;
                _sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 121.0;
                double maxValue = 120.0;
                _sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double value = 4.0;
                double minValue = 120.0;
                _sut.SetWidthOfHorizontalCornerRectangles(value);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double maxValue = 120.0;
                _sut.SetWidthOfHorizontalCornerRectangles(double.MaxValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                double minValue = 120.0;
                _sut.SetWidthOfHorizontalCornerRectangles(double.MinValue);
                var maingrid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                var height = 470;
                var width = 290;
                TranslateTransform trans = new TranslateTransform
                {
                    X = 60,
                    Y = 100
                };

                _sut.SetCropControlPosition(height, width, trans);
                Grid grid = _sut.Content as Grid;
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
            await ExecuteOnUiThread(() =>
            {
                TranslateTransform trans = new TranslateTransform
                {
                    X = 60,
                    Y = 100
                };

                Assert.ThrowsException<ArgumentException>(() =>
                    _sut.SetCropControlPosition(double.MaxValue, double.MaxValue, trans));

            });
        }

        [TestMethod]
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionMinValueTest()
        {
            await ExecuteOnUiThread(() =>
            {
                TranslateTransform trans = new TranslateTransform
                {
                    X = 60,
                    Y = 100
                };

                Assert.ThrowsException<ArgumentException>(() =>
                _sut.SetCropControlPosition(double.MinValue, double.MinValue, trans));

            });
        }

        [TestMethod]
        [TestCategory("SetCropControlPosition")]
        public async Task SetCropControlPositionTransNullTest()
        {
            await ExecuteOnUiThread(() =>
            {
                Assert.ThrowsException<ArgumentException>(() =>
                _sut.SetCropControlPosition(470, 290, null));

            });
        }

        [TestMethod]
        [TestCategory("ResetAppBarButtonRectangleSelectionControl")]
        public async Task ResetAppBarButtonRectangleSelectionControlTrueTest()
        {
            await ExecuteOnUiThread(() =>
            {
                _sut.ResetAppBarButtonRectangleSelectionControl(true);
                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();

                Assert.AreEqual(appBarButtonReset.IsEnabled, true);
                

            });
        }

        [TestMethod]
        [TestCategory("ResetAppBarButtonRectangleSelectionControl")]
        public async Task ResetAppBarButtonRectangleSelectionControlFalseTest()
        {
            await ExecuteOnUiThread(() =>
            {
                _sut.ResetAppBarButtonRectangleSelectionControl(false);
                AppBarButton appBarButtonReset = PocketPaintApplication.GetInstance().PaintingAreaView.getAppBarResetButton();

                Assert.AreEqual(appBarButtonReset.IsEnabled, false);
                
            });
        }

        [TestMethod]
        [TestCategory("GetHeightOfRectangleCropSelection")]
        public async Task GetHeightOfRectangleCropSelectionInfinityTest()
        {
            await ExecuteOnUiThread(() =>
            {
                double value = _sut.GetHeightOfRectangleCropSelection();
                Assert.AreEqual(Double.IsInfinity(value), true);
            });
        }

        [TestMethod]
        [TestCategory("GetWidthOfRectangleCropSelection")]
        public async Task GetWidthOfRectangleCropSelectionInfinityTest()
        {
            await ExecuteOnUiThread(() =>
            {
                double value = _sut.GetWidthOfRectangleCropSelection();
                Assert.AreEqual(Double.IsInfinity(value), true);
            });
        }

        [TestMethod]
        [TestCategory("HasElementsPaintingAreaViews")]
        public async Task HasElementsPaintingAreaViewsTest()
        {
            await ExecuteOnUiThread(() =>
            {
                bool result = PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0;

                bool value = _sut.HasPaintingAreaViewElements();
                Assert.AreEqual(value, result);
            });
        }

        [TestMethod]
        [TestCategory("HasElementsPaintingAreaViews")]
        public async Task HasElementsPaintingAreaViewsNotTest()
        {
            await ExecuteOnUiThread(() =>
            {
                bool result = !(PocketPaintApplication.GetInstance().PaintingAreaCanvas.Children.Count > 0);

                bool value = _sut.HasPaintingAreaViewElements();
                Assert.AreNotEqual(value, result);
            });
        }

        [TestMethod]
        [TestCategory("GetXYOffsetBetweenPaintingAreaAndCropControlSelection")]
        public async Task GetXyOffsetBetweenPaintingAreaAndCropControlSelectionTest()
        {
            await ExecuteOnUiThread(() =>
            {
                Point value;
                value = _sut.GetXYOffsetBetweenPaintingAreaAndCropControlSelection();
                Assert.AreNotEqual(value, null);
                Assert.AreNotEqual(value.X, 0.0);
                Assert.AreNotEqual(value.Y, 0.0);
            });
        }

        public IAsyncAction ExecuteOnUiThread(DispatchedHandler action)
        {
            return Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, action);
        }

    }
}

