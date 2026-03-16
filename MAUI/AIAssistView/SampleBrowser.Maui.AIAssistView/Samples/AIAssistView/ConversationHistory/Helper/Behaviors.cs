#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class SfAIAssistViewConversationBehavior : Behavior<SampleView>
    {
        #region Fields
        internal GettingStartedViewModel? viewModel;
        private Border? border;
        int headerHeight = DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst ? 255 : 430;
        private int editorHeight = 56;
        const int minPadding = 24;
        private SampleView? sampleView;
        double padding = DeviceInfo.Platform == DevicePlatform.WinUI ? 2 : 125;
        private CustomAssistView? customAssistView;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            sampleView = bindable;
#if WINDOWS || MACCATALYST

            var samplename = bindable.GetType();
            if (samplename.Name == "CustomEditor")
            {
                editorHeight = 76;
            }
#endif
            this.customAssistView = bindable.FindByName<CustomAssistView>("sfAIAssistView");
            if (this.customAssistView != null)
            {
                this.customAssistView.PropertyChanged += OnCustomAssistViewPropertyChanged;
            }

            border = bindable.FindByName<Border>("border");
            viewModel = bindable.BindingContext as GettingStartedViewModel;
#if WINDOWS || MACCATALYST
            sampleView.PropertyChanged += this.SampleView_PropertyChanged!;
# endif
            if (border != null)
            {
                border.PropertyChanged += this.View_PropertyChanged!;
            }

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
#if WINDOWS || MACCATALYST
            sampleView!.PropertyChanged -= this.SampleView_PropertyChanged!;
#endif
            border!.PropertyChanged -= View_PropertyChanged!;
            if (this.customAssistView != null)
            {
                this.customAssistView.PropertyChanged -= OnCustomAssistViewPropertyChanged;
            }

            this.customAssistView = null;
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

        private void OnCustomAssistViewPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CustomAssistView.AssistItems) && this.viewModel != null && this.customAssistView != null)
            {
                if (this.customAssistView.AssistItems != null && this.customAssistView.AssistItems.Count > 0)
                {
                    this.viewModel.IsEditorVisible = true;
                }
                else
                {
                    this.viewModel.IsEditorVisible = false;
                }
            }
        }

#if WINDOWS || MACCATALYST
        private void SampleView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Height" && viewModel != null && sampleView != null && this.border != null)
            {
                double borderHeight = 0;
#if WINDOWS
                borderHeight = sampleView.Height - padding;
#elif MACCATALYST
                borderHeight = sampleView.Height - (2 * padding);
#endif
                this.border!.HeightRequest = borderHeight;
            }
        }
#endif
        #endregion

    }
}
