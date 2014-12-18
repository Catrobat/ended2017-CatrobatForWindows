using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class ZoomCommand : CommandBase
    {
        private TransformGroup _transformGroup;

        public ZoomCommand(TransformGroup transformGroup)
        {
            _transformGroup = transformGroup;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroupWithAllKindOfTransforms = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);
            if (_transformGroup.Children.Count == 0)
            {
                transformGroupWithAllKindOfTransforms.Children.Clear();
                PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();
                PocketPaintApplication.GetInstance().PaintingAreaView.setEnableOfAppBarButtonResetZoom(false);
            }
            else
            {
                for (int i = 0; i < _transformGroup.Children.Count; i++)
                {
                    transformGroupWithAllKindOfTransforms.Children.Add(_transformGroup.Children[i]);
                }
                PocketPaintApplication.GetInstance().PaintingAreaView.setEnableOfAppBarButtonResetZoom(true);
            }
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroupWithAllKindOfTransforms;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }

        public override bool UnDo()
        {
            if (CommandManager.GetInstance().doesCommandTypeExistInUndoList(typeof(ZoomCommand)))
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setEnableOfAppBarButtonResetZoom(true);
            }
            else
            {
                PocketPaintApplication.GetInstance().PaintingAreaView.setEnableOfAppBarButtonResetZoom(false);
            }
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform);

            transformGroup.Children.Clear();

            PocketPaintApplication.GetInstance().PaintingAreaView.setSizeOfPaintingAreaViewCheckered();

            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.UpdateLayout();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateArrange();
            PocketPaintApplication.GetInstance().PaintingAreaCheckeredGrid.InvalidateMeasure();

            return true;
        }
    }
}
