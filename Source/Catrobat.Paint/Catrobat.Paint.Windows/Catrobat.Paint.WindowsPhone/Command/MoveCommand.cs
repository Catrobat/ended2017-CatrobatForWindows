using Windows.UI.Xaml.Media;

namespace Catrobat.Paint.WindowsPhone.Command
{
    class MoveCommand : CommandBase
    {
        private TranslateTransform _translateTransform;

        public MoveCommand(TranslateTransform translateTransform)
        {
            _translateTransform = translateTransform;
        }

        public override bool ReDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform);
            //for (int i = 0; i < transformGroup.Children.Count; i++)
            //{
            //    if (transformGroup.Children[i].GetType() == typeof(TranslateTransform))
            //    {
            //        transformGroup.Children.RemoveAt(i);
            //    }
            //}

            transformGroup.Children.Add(_translateTransform);

            PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().GridWorkingSpace.UpdateLayout();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateArrange();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateMeasure();

            return true;
        }

        public override bool UnDo()
        {
            TransformGroup transformGroup = ((TransformGroup)PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform);

            for (int i = 0; i < transformGroup.Children.Count; i++)
            {
                if (transformGroup.Children[i].GetType() == typeof(TranslateTransform))
                {
                    transformGroup.Children.RemoveAt(i);
                }
            }

            double offsetX = transformGroup.Value.OffsetX;
            double offsetY = transformGroup.Value.OffsetY;

            var defaultTranslateTransform = new TranslateTransform();
            defaultTranslateTransform.X -= offsetX - 48;
            defaultTranslateTransform.Y -= offsetY - 69;

            transformGroup.Children.Add(defaultTranslateTransform);

            PocketPaintApplication.GetInstance().GridWorkingSpace.RenderTransform = transformGroup;
            PocketPaintApplication.GetInstance().GridWorkingSpace.UpdateLayout();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateArrange();
            PocketPaintApplication.GetInstance().GridWorkingSpace.InvalidateMeasure();

            return true;
        }
    }
}
