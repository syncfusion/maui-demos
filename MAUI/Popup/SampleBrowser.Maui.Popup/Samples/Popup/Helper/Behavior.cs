#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
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
            this.ActionSheetPopup = sampleView.Resources["Actionsheet"] as Syncfusion.Maui.Popup.SfPopup;

            sampleView.SizeChanged += SampleView_SizeChanged;

            base.OnAttachedTo(sampleView);
        }

        private void SampleView_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is SampleView sampleView)
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
        }

        protected override void OnDetachingFrom(SampleView sampleView)
        {
            // Unsubscribe from SizeChanged
            sampleView.SizeChanged -= SampleView_SizeChanged;
            this.ActionSheetPopup = null;
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
                        Text = "\ue760",
                        FontFamily = "MauiSampleFontIcon",
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Application.Current?.RequestedTheme == AppTheme.Light ? Color.FromArgb("#49454F") : Color.FromArgb("#CAC4D0"),
                        FontAttributes = FontAttributes.Bold,
                    };
                }
                if (e.DataFormItem.FieldName == "Email")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue75C",
                        FontFamily = "MauiSampleFontIcon",
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Application.Current?.RequestedTheme == AppTheme.Light ? Color.FromArgb("#49454F") : Color.FromArgb("#CAC4D0")
                    };
                }
                if (e.DataFormItem.FieldName == "Password")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue751",
                        FontFamily = "MauiSampleFontIcon",
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Application.Current?.RequestedTheme == AppTheme.Light ? Color.FromArgb("#49454F") : Color.FromArgb("#CAC4D0"),
                    };
                }
                if (e.DataFormItem.FieldName == "RePassword")
                {
                    e.DataFormItem.LeadingView = new Label()
                    {
                        Text = "\ue751",
                        FontFamily = "MauiSampleFontIcon",
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 24,
                        HeightRequest = 24,
                        TextColor = Application.Current?.RequestedTheme == AppTheme.Light ? Color.FromArgb("#49454F") : Color.FromArgb("#CAC4D0"),
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

    // Behavior to handle row selection in the Simple popup list.
    public class ItemSelectionBehavior : Behavior<Grid>
    {
        TapGestureRecognizer? tap;

        protected override void OnAttachedTo(Grid bindable)
        {
            base.OnAttachedTo(bindable);
            tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => OnTapped(bindable);
            bindable.GestureRecognizers.Add(tap);
        }

        protected override void OnDetachingFrom(Grid bindable)
        {
            if (tap != null)
            {
                bindable.GestureRecognizers.Remove(tap);
                tap = null;
            }
            base.OnDetachingFrom(bindable);
        }

        private void OnTapped(Grid grid)
        {
            var vm = FindViewModel(grid);
            var item = grid.BindingContext as SimpleModel;
            if (vm == null || item == null) return;

            foreach (var user in vm.UserDetails)
                user.IsSelected = false;
            item.IsSelected = true;
        }

        private GettingStartedViewModel? FindViewModel(Element? element)
        {
            while (element != null)
            {
                if (element is BindableObject bindable && bindable.BindingContext is GettingStartedViewModel vm)
                    return vm;
                element = element.Parent;
            }
            return null;
        }
    }

    // Behavior to handle ringtone selection in the Confirmation popup list.
    public class RingtoneSelectionBehavior : Behavior<Grid>
    {
        TapGestureRecognizer? tap;

        protected override void OnAttachedTo(Grid bindable)
        {
            base.OnAttachedTo(bindable);
            tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => OnTapped(bindable);
            bindable.GestureRecognizers.Add(tap);
        }

        protected override void OnDetachingFrom(Grid bindable)
        {
            if (tap != null)
            {
                bindable.GestureRecognizers.Remove(tap);
                tap = null;
            }
            base.OnDetachingFrom(bindable);
        }

        private void OnTapped(Grid grid)
        {
            var vm = FindViewModel(grid);
            var item = grid.BindingContext as ConfirmationModel;
            if (vm == null || item == null) return;

            foreach (var ringtone in vm.RingtoneList)
                ringtone.SelectedRingtone = false;
            item.SelectedRingtone = true;
        }

        private GettingStartedViewModel? FindViewModel(Element? element)
        {
            while (element != null)
            {
                if (element is BindableObject bindable && bindable.BindingContext is GettingStartedViewModel vm)
                    return vm;
                element = element.Parent;
            }
            return null;
        }
    }

    internal static class DeviceDisplayInfoExtensions
    {
        public static Size GetScaledScreenSize(this DisplayInfo info) =>
            new Size(info.Width / info.Density, info.Height / info.Density);
    } 
}
