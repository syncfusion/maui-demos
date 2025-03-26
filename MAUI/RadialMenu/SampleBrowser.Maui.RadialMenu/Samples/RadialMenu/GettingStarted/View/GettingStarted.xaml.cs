#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Drawing;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Maui.RadialMenu;

namespace SampleBrowser.Maui.RadialMenu.SfRadialMenu
{
    public partial class GettingStarted : SampleView
    {
        #region Fields

        HighlightAnnotation? LightAnnotation;

        SquigglyAnnotation? SquigglyAnnotation;

        UnderlineAnnotation? UnderlineAnnotation;

        StrikeOutAnnotation? StrikeOutAnnotation;

        List<RectangleF> value = new();

        #endregion

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

        #region Annotation Methods

        HighlightAnnotation CreateHighlightAnnotation(List<RectangleF> rect)
        {
            int pageNumber = 1;

            List<RectF> rects = new List<RectF>();

            foreach (var item in rect) 
            {
                RectF newValue = new RectF(item.X, item.Y, item.Width, item.Height);
                rects.Add(newValue);
            }

            LightAnnotation = new HighlightAnnotation(pageNumber, rects);

            
            return LightAnnotation;
        }

        SquigglyAnnotation CreateSquigglyAnnotation(List<RectangleF> rect)
        {
            int pageNumber = 1;

            List<RectF> rects = new List<RectF>();

            foreach (var item in rect)
            {
                RectF newValue = new RectF(item.X, item.Y, item.Width, item.Height);
                rects.Add(newValue);
            }

            SquigglyAnnotation = new SquigglyAnnotation(pageNumber, rects);

            return SquigglyAnnotation;
        }

        UnderlineAnnotation CreateUnderlineAnnotation(List<RectangleF> rect)
        {
            int pageNumber = 1;

            List<RectF> rects = new List<RectF>();

            foreach (var item in rect)
            {
                RectF newValue = new RectF(item.X, item.Y, item.Width, item.Height);
                rects.Add(newValue);
            }

            UnderlineAnnotation = new UnderlineAnnotation(pageNumber, rects);

            return UnderlineAnnotation;
        }

        StrikeOutAnnotation CreateStrikeOutAnnotation(List<RectangleF> rect)
        {
            int pageNumber = 1;

            List<RectF> rects = new List<RectF>();

            foreach (var item in rect)
            {
                RectF newValue = new RectF(item.X, item.Y, item.Width, item.Height);
                rects.Add(newValue);
            }

            StrikeOutAnnotation = new StrikeOutAnnotation(pageNumber, rects);

            return StrikeOutAnnotation;
        }

        void AddHighlightAnnotation(List<RectangleF> bounds)
        {
            Annotation highlightAnnotation = CreateHighlightAnnotation(bounds);

            pdfViewer.AddAnnotation(highlightAnnotation);
        }

        void AddSquigglyAnnotation(List<RectangleF> bounds)
        {
            Annotation SquigglyAnnotation = CreateSquigglyAnnotation(bounds);

            pdfViewer.AddAnnotation(SquigglyAnnotation);
        }

        void AddUnderlineAnnotation(List<RectangleF> bounds)
        {
            Annotation underLineAnnotation = CreateUnderlineAnnotation(bounds);

            pdfViewer.AddAnnotation(underLineAnnotation);
        }

        void AddStrikeOutAnnotation(List<RectangleF> bounds)
        {
            Annotation strikeOutAnnotation = CreateStrikeOutAnnotation(bounds);

            pdfViewer.AddAnnotation(strikeOutAnnotation);
        }

        #endregion

        #region Events

        private void SfRadialMenuItem_ItemTapped(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.UndoCommand!.Execute(true);
        }

        private void SfRadialMenuItem_ItemTapped_1(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.RedoCommand!.Execute(true);
        }

        private void SfRadialMenuItem_ItemTapped_2(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Line;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_3(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            this.SetAnnotationModeNone();
            this.AddHighlightAnnotation(value);
        }

        private void PdfViewer_TextSelectionChanged(object? sender, TextSelectionChangedEventArgs? e)
        {
            e!.Handled = true;
            radialMenu.IsOpen = true;

            value = e.SelectedTextLineBounds;
            
        }

        private void SfRadialMenuItem_ItemTapped_4(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            this.SetAnnotationModeNone();
            this.AddSquigglyAnnotation(value);
        }

        private void SfRadialMenuItem_ItemTapped_5(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            this.SetAnnotationModeNone();
            this.AddStrikeOutAnnotation(value);
        }

        private void SfRadialMenuItem_ItemTapped_6(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            this.SetAnnotationModeNone();
            this.AddUnderlineAnnotation(value);
        }

        private void SfRadialMenuItem_ItemTapped_7(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Square;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_8(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Circle;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_9(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Line;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_10(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Arrow;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_11(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Polyline;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_12(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Polygon;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_13(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            if (LightAnnotation != null) 
            {
                LightAnnotation!.Color = Microsoft.Maui.Graphics.Color.FromArgb("#00cc1d");
            }
        }

        private void SfRadialMenuItem_ItemTapped_14(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            if (LightAnnotation != null)
            {
                LightAnnotation!.Color = Microsoft.Maui.Graphics.Color.FromArgb("#c65619");
            }
        }

        private void SfRadialMenuItem_ItemTapped_15(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            if (LightAnnotation != null)
            {
                LightAnnotation!.Color = Microsoft.Maui.Graphics.Color.FromArgb("#F17525");
            }
        }

        private void SfRadialMenuItem_ItemTapped_16(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            if (LightAnnotation != null)
            {
                LightAnnotation!.Color = Microsoft.Maui.Graphics.Color.FromArgb("#0bb4c3");
            }
        }

        private void SfRadialMenuItem_ItemTapped_17(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            if (LightAnnotation != null)
            {
                LightAnnotation!.Color = Microsoft.Maui.Graphics.Color.FromArgb("#0051d4");
            }
        }

        private void SfRadialMenuItem_ItemTapped_18(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.Ink;
            radialMenu.IsOpen = false;
        }

        private void SfRadialMenuItem_ItemTapped_19(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
        {
            pdfViewer.AnnotationMode = AnnotationMode.InkEraser;
            radialMenu.IsOpen = false;
        }

        private void SetAnnotationModeNone() 
        {
            pdfViewer.AnnotationMode = AnnotationMode.None;
            radialMenu.IsOpen = false;
        }

        #endregion

        private void rotationCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(this.radialMenu != null)
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
    }
}