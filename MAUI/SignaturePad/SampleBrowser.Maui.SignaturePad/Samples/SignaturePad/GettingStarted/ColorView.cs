#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.Maui.Controls.Shapes;
using Control = Syncfusion.Maui.SignaturePad;

namespace SampleBrowser.Maui.SignaturePad.SfSignaturePad
{
    public class ColorSegment : GraphicsView
    {
        public static readonly BindableProperty ColorProperty =
              BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorSegment));

        public static readonly BindableProperty IsSelectedProperty =
              BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ColorSegment),
                  false, propertyChanged: OnSelectionChanged);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public ColorSegment()
        {
            Drawable = new ColorDrawable(this);

            TapGestureRecognizer tapGesture = new();
            tapGesture.Tapped += OnTapped;

            GestureRecognizers.Add(tapGesture);
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            if (Handler == null)
            {
                if (GestureRecognizers.Count > 0 && GestureRecognizers[0] is TapGestureRecognizer tapGesture)
                {
                    tapGesture.Tapped -= OnTapped;

                    GestureRecognizers.Remove(tapGesture);
                }
            }
        }

        private static void OnSelectionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorSegment)bindable).Invalidate();
        }

        private void OnTapped(object? sender, EventArgs e)
        {
            IsSelected = true;
            if (Parent is ColorView colorView)
            {
                colorView.SelectedIndex = colorView.Children.IndexOf(this);
            }

            Invalidate();
        }

        private class ColorDrawable : IDrawable
        {
            private readonly ColorSegment segment;

            public ColorDrawable(ColorSegment colorSegment)
            {
                segment = colorSegment;
            }

            public void Draw(ICanvas canvas, RectF bounds)
            {
                if (segment.IsSelected)
                {
                    canvas.FillColor = segment.Color;
                    canvas.FillEllipse(bounds.Inflate(-4, -4));

                    canvas.FillColor = Colors.Transparent;
                    canvas.StrokeColor = segment.Color;
                    canvas.StrokeSize = 2;
                    canvas.DrawEllipse(bounds.Inflate(-1, -1));
                }
                else
                {
                    canvas.FillColor = segment.Color;
                    canvas.FillEllipse(bounds);
                }
            }
        }
    }

    public class ColorView : HorizontalStackLayout
    {
        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(ColorView),
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty SelectedIndexProperty =
              BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ColorView),
                  0, propertyChanged: OnSelectedIndexChanged);

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (SelectedIndex >= 0 && SelectedIndex < Children.Count
                && Children[SelectedIndex] is ColorSegment segment)
            {
                SelectedColor = segment.Color;
            }
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorView)bindable).OnSelectedIndexChanged(oldValue, newValue);
        }

        private void OnSelectedIndexChanged(object oldValue, object newValue)
        {
            int childCount = Children.Count;

            int oldIndex = (int)oldValue;
            if (oldIndex >= 0 && oldIndex < childCount && Children[oldIndex] is ColorSegment oldSegment)
            {
                oldSegment.IsSelected = false;
            }

            int newIndex = (int)newValue;
            if (newIndex >= 0 && newIndex < childCount && Children[newIndex] is ColorSegment newSegment)
            {
                SelectedColor = newSegment.Color;
            }
        }
    }
}
