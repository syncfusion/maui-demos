#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.AIAssistView;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.MarkdownViewer;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{

    public class SfAssistViewMarkdownHeaderBehavior : Behavior<SampleView>
    {
        #region Fields
        internal MarkdownViewModel? viewModel;
        private Border? border;
        int headerHeight = DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst ? 255 : 430;
        private int editorHeight = 56;
        const int minPadding = 24;
        private SampleView? sampleView;
        double padding = DeviceInfo.Platform == DevicePlatform.WinUI ? 2 : 125;
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

            border = bindable.FindByName<Border>("border");
            viewModel = bindable.BindingContext as MarkdownViewModel;
#if WINDOWS || MACCATALYST
            sampleView.PropertyChanged += this.SampleView_PropertyChanged!;
#endif
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
            border = null;
            sampleView = null;
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
                viewModel.HeaderPadding = new Thickness(0, top, 0, 24);
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
    public class MarkdownBehavior : Behavior<SfMarkdownViewer>
    {
        public MarkdownViewModel? ViewModel { get; set; }
        protected override void OnAttachedTo(SfMarkdownViewer bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Loaded += Bindable_Loaded;        
        }

        private void Bindable_Loaded(object? sender, EventArgs e)
        {
#if !ANDROID
            if (sender is SfMarkdownViewer markdownViewer)
            {
                // Set the height of the MarkdownViewer to 100% of the parent container
                markdownViewer.HeightRequest = 300;
                markdownViewer.WidthRequest = 350;
            }
#endif
        }

        protected override void OnDetachingFrom(SfMarkdownViewer bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}



