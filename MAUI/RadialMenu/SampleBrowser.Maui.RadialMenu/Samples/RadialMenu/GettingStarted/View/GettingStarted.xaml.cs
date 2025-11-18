#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.ImageEditor;
using Syncfusion.Maui.RadialMenu;
using ItemTappedEventArgs= Syncfusion.Maui.RadialMenu.ItemTappedEventArgs;

namespace SampleBrowser.Maui.RadialMenu.SfRadialMenu
{
    public partial class GettingStarted : SampleView
    {
        #region Constructor

        [Obsolete]
        public GettingStarted()
        {
            InitializeComponent();

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 800), TimerElapsed);
        }
        #endregion

        #region Private Methods

        [Obsolete]
        private bool TimerElapsed()
        {
            Device.BeginInvokeOnMainThread(() =>
           {
               radialMenu.IsOpen = true;
           });

            return false;
        }

        #endregion

        #region Events

        private void OnRadialMenuItem_FreeCrop(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.Crop(ImageCropType.Free);
        }

        private void OnRadialMenuItem_CircleCrop(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.Crop(ImageCropType.Circle);
        }

        private void OnRadialMenuItem_SquareCrop(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.Crop(ImageCropType.Square);
        }

        private void OnRadialMenuItem_EllipseCrop(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.Crop(ImageCropType.Ellipse);
        }

        private void OnRadialMenuItem_RectangleShape(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.Rectangle);
        }

        private void OnRadialMenuItem_CircleShape(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.Circle);
        }

        private void OnRadialMenuItem_ArrowShape(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.Arrow);
        }

        private void OnRadialMenuItem_DoubleArrowShape(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.DoubleArrow);
        }

        private void OnRadialMenuItem_LineShape(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.Line);
        }

        private void OnAddTextClicked(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddText("");
        }

        private void OnPenTapped(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.AddShape(AnnotationShape.Pen);
        }

        private void OnPenStrokeColorTapped(object sender, ItemTappedEventArgs e)
        {
            imageEditor.AddShape(AnnotationShape.Pen, new ImageEditorShapeSettings() { Color = Colors.Blue});
        }

        private void OnPenStrokeThikenessTapped(object sender, ItemTappedEventArgs e)
        {
            imageEditor.AddShape(AnnotationShape.Pen, new ImageEditorShapeSettings() { StrokeThickness = 5 });
        }

        private void OnSaveEditsTapped(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.SaveEdits();
            this.radialMenu.IsOpen = false;
        }

        private void OnResetsTapped(object sender, ItemTappedEventArgs e)
        {
            this.imageEditor.Reset();
            this.radialMenu.IsOpen = false;
        }

        #endregion

        #region Radial menu customization

        private void rotationCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (this.radialMenu != null)
            {
                radialMenu.EnableRotation = e.Value;
            }
        }

        private void draggingCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (this.radialMenu != null)
            {
                radialMenu.IsDragEnabled = e.Value;
            }
        }

        private void rimRadiusSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (this.radialMenu != null)
            {
                radialMenu.RimRadius = e.NewValue;
            }
        }



        #endregion

    }
}