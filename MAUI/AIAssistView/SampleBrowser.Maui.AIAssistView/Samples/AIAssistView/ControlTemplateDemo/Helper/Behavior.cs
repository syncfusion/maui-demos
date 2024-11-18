#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
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
    
}
