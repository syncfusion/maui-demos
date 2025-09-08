#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class SampleViewBehavior : Behavior<SampleView>
    {
        #region Fields
        internal ControlTemplateViewModel? viewModel;
        private Border? rootBorder;
        private SampleView? sampleView;
        double padding = DeviceInfo.Platform == DevicePlatform.WinUI ? 2 : 125;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(SampleView bindable)
        {
            rootBorder = bindable.FindByName<Border>("rootBorder");
            sampleView = bindable;
            viewModel = bindable.BindingContext as ControlTemplateViewModel;
#if WINDOWS || MACCATALYST
            sampleView.PropertyChanged += this.View_PropertyChanged!;
#endif
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
#if WINDOWS || MACCATALYST
            sampleView!.PropertyChanged -= View_PropertyChanged!;
#endif
            viewModel = null;
            sampleView = null;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region CallBacks
#if WINDOWS || MACCATALYST
        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "Height" && viewModel != null && sampleView != null)
            {
                double borderHeight = 0;
#if WINDOWS
                borderHeight = sampleView.Height - padding;
#elif MACCATALYST
               borderHeight = sampleView.Height - ( 2 * padding );
#endif
                rootBorder!.HeightRequest = borderHeight;
            }

        }
#endif
#endregion
    }

    class DotAnimationBehavior : Behavior<HorizontalStackLayout>
    {
        private Ellipse? dot1, dot2, dot3, dot4;
        private bool isAnimating = true;
        private readonly double animationDuration = 120;
        private Color DotDefaultColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["FlyoutBackground"] : (Color)App.Current.Resources["FlyoutBackgroundLight"];
        private Color AnimationColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundSelected"] : (Color)App.Current.Resources["PrimaryBackgroundSelectedLight"];

        protected override void OnAttachedTo(HorizontalStackLayout bindable)
        {
            base.OnAttachedTo(bindable);

            // Initialize dots by finding them by their names
            dot1 = bindable.FindByName<Ellipse>("dot1");
            dot2 = bindable.FindByName<Ellipse>("dot2");
            dot3 = bindable.FindByName<Ellipse>("dot3");
            dot4 = bindable.FindByName<Ellipse>("dot4");

            this.StartAnimation();
        }

        protected override void OnDetachingFrom(HorizontalStackLayout bindable)
        {
            // Stop animation and clear dot references
            isAnimating = false;
            dot1 = dot2 = dot3 = dot4 = null;
            base.OnDetachingFrom(bindable);
        }

        private async void StartAnimation()
        {
            while (isAnimating)
            {
                if (dot1 != null)
                {
                    await AnimateDot(dot1);
                }
                if (dot2 != null)
                {
                    await AnimateDot(dot2);
                }
                if (dot3 != null)
                {
                    await AnimateDot(dot3);
                }
                if (dot4 != null)
                {
                    await AnimateDot(dot4);
                }
            }
        }

        private async Task AnimateDot(Ellipse dot)
        {
            // Perform animation on the dot
            dot.Fill = new SolidColorBrush(AnimationColor);
            await dot.TranslateTo(0, -2, (uint)animationDuration);
            await dot.TranslateTo(0, 0, (uint)animationDuration);
            dot.Fill = new SolidColorBrush(DotDefaultColor);
        }
    }
}
