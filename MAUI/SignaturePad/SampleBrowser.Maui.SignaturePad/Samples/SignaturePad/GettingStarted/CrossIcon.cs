#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SignaturePad.SfSignaturePad
{
    public class CrossIcon : GraphicsView
    {
        public static readonly BindableProperty PaddingProperty =
              BindableProperty.Create(nameof(Padding), typeof(double), typeof(CrossIcon),
                  0d, propertyChanged: OnPaddingChanged);

        public static readonly BindableProperty TextColorProperty =
              BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CrossIcon),
                  Colors.Black, propertyChanged: OnTextColorChanged);

        public event EventHandler? Clicked;

        public CrossIcon()
        {
            Drawable = new CrossIconDrawable(this);

            TapGestureRecognizer tapGesture = new();
            tapGesture.Tapped += OnTapped;

            GestureRecognizers.Add(tapGesture);
        }

        public double Padding
        {
            get { return (double)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
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

        private static void OnPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CrossIcon)bindable).Invalidate();
        }

        private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CrossIcon)bindable).Invalidate();
        }

        private void OnTapped(object? sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class CrossIconDrawable : IDrawable
    {
        private readonly CrossIcon icon;

        public CrossIconDrawable(CrossIcon icon)
        {
            this.icon = icon;
        }

        public void Draw(ICanvas canvas, RectF bounds)
        {
            bounds = bounds.Inflate(-(float)icon.Padding, -(float)icon.Padding);

            canvas.StrokeColor = icon.TextColor;
            canvas.StrokeSize = 1.5f;
            canvas.DrawLine(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
            canvas.DrawLine(bounds.Right, bounds.Top, bounds.Left, bounds.Bottom);
        }
    }
}
