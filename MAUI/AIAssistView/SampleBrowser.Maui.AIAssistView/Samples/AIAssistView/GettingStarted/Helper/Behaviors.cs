#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class SfAssistViewHeaderBehavior : Behavior<SampleView>
    {
        #region Fields
        internal GettingStartedViewModel? viewModel;
        private Border? border;
        int headerHeight = DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst ? 255 : 416;
        const int editorHeight = 56;
        const int minPadding = 24;
        private SampleView? sampleView;
        double padding = DeviceInfo.Platform == DevicePlatform.WinUI ? 2 : 125;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            sampleView = bindable;
            border = bindable.FindByName<Border>("border");
            viewModel = bindable.BindingContext as GettingStartedViewModel;
#if WINDOWS || MACCATALYST
            sampleView.PropertyChanged += this.SampleView_PropertyChanged!;
# endif
            border.PropertyChanged += this.View_PropertyChanged!;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
#if WINDOWS || MACCATALYST
            sampleView!.PropertyChanged -= this.SampleView_PropertyChanged!;
#endif
            border!.PropertyChanged -= View_PropertyChanged!;
            viewModel = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBacks
        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height" && viewModel != null)
            {
                double top = ((sender as Border)!.Height - editorHeight) / 2 - (headerHeight / 2);
                top = top < minPadding ? minPadding : top;
                viewModel.HeaderPadding = new Thickness(0, top, 0, 0);
            }
        }

#if WINDOWS || MACCATALYST
        private void SampleView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height" && viewModel != null && sampleView != null)
            {
                double borderHeight = 0;
#if WINDOWS
                borderHeight = sampleView.Height - padding;
#elif MACCATALYST
               borderHeight = sampleView.Height - (2 * padding);
#endif
               border!.HeightRequest = borderHeight;
            }
        }
#endif
#endregion

    }
}
