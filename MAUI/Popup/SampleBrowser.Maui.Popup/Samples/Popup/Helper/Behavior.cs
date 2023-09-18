#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataForm;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Popup.SfPopup
{   
    public class SampleViewBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.Popup.SfPopup? ActionSheetPopup;
        protected override void OnAttachedTo(SampleView sampleView)
        {
            ActionSheetPopup = sampleView.Resources["Actionsheet"] as Syncfusion.Maui.Popup.SfPopup;
            sampleView.LayoutChanged += SampleView_LayoutChanged;
            base.OnAttachedTo(sampleView);            
        }

        private void SampleView_LayoutChanged(object? sender, EventArgs e)
        {
            var screenWidth = DeviceDisplay.MainDisplayInfo.GetScaledScreenSize().Width;
            var screenHeight = DeviceDisplay.MainDisplayInfo.GetScaledScreenSize().Height;
#if ANDROID || IOS
            if (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
            {
                ActionSheetPopup!.WidthRequest = screenWidth;
            }
            else
            {
                ActionSheetPopup!.WidthRequest = 360;
            }
            ActionSheetPopup!.StartY = (int)(screenHeight - ActionSheetPopup!.HeightRequest);
#else
            ActionSheetPopup!.WidthRequest = 360;
#endif          
#if IOS || MACCATALYST
            ActionSheetPopup.Refresh();            
#endif

        }

        protected override void OnDetachingFrom(SampleView sampleView)
        {
            sampleView.LayoutChanged -= SampleView_LayoutChanged;
            base.OnDetachingFrom(sampleView);            
        }        
    }

    public class DataFormBehavior : Behavior<SfDataForm>
    {
        protected override void OnAttachedTo(SfDataForm dataForm)
        {
            dataForm.GenerateDataFormItem += DataForm_GenerateDataFormItem;
            base.OnAttachedTo(dataForm);
        }

        private void DataForm_GenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == "UserName")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue707",
#if ANDROID
                        FontFamily = "PopupFontIcons.ttf#",
#elif WINDOWS
                        FontFamily = "PopupFontIcons.ttf#PopupFontIcons",
#else
                        FontFamily = "PopupFontIcons",
#endif

                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Colors.Gray,
                        FontAttributes = FontAttributes.Bold,
                    };
                }
                if (e.DataFormItem.FieldName == "Email")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue706",
#if ANDROID
                        FontFamily = "PopupFontIcons.ttf#",
#elif WINDOWS
                        FontFamily = "PopupFontIcons.ttf#PopupFontIcons",
#else
                        FontFamily = "PopupFontIcons",
#endif
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Colors.Gray,                        
                    };
                }
                if (e.DataFormItem.FieldName == "Password")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue708",
#if ANDROID
                        FontFamily = "PopupFontIcons.ttf#",
#elif WINDOWS
                        FontFamily = "PopupFontIcons.ttf#PopupFontIcons",
#else
                        FontFamily = "PopupFontIcons",
#endif
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Colors.Gray,                       
                    };
                }
                if (e.DataFormItem.FieldName == "RePassword")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue708",
#if ANDROID
                        FontFamily = "PopupFontIcons.ttf#",
#elif WINDOWS
                        FontFamily = "PopupFontIcons.ttf#PopupFontIcons",
#else
                        FontFamily = "PopupFontIcons",
#endif
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Colors.Gray,                        
                    };
                }
            }
        }

        protected override void OnDetachingFrom(SfDataForm dataForm)
        {
            dataForm.GenerateDataFormItem -= DataForm_GenerateDataFormItem;
            base.OnDetachingFrom(dataForm);
        }
    }

    internal static class DeviceDisplayInfoExtensions
    {
        public static Size GetScaledScreenSize(this DisplayInfo info) =>
            new Size(info.Width / info.Density, info.Height / info.Density);
    }
}
