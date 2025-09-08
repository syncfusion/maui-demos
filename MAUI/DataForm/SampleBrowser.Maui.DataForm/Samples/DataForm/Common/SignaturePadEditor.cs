#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.DataForm;
using Syncfusion.Maui.Graphics.Internals;
using Syncfusion.Maui.SignaturePad;

﻿namespace SampleBrowser.Maui.DataForm.SfDataForm
{

    /// <summary>
    /// Represents the custom signature pad editor.
    /// </summary>
    public class SignaturePadEditor : IDataFormEditor
    {
        /// <summary>
        /// Holds the data form item.
        /// </summary>
        private DataFormItem? dataFormItem;

        /// <summary>
        /// Holds the signature pad instance.
        /// </summary>
        private SfSignaturePad? signaturePad;

        /// <summary>
        /// Holds the signature clear button instance.
        /// </summary>
        private Button? signatureClearButton;

        /// <inheritdoc/>
        public void CommitValue(DataFormItem dataFormItem, View view)
        {
            dataFormItem.SetValue(((SfSignaturePad)view).ToImageSource());
        }

        /// <inheritdoc/>
        public View CreateEditorView(DataFormItem dataFormItem)
        {
            VerticalStackLayout dataFormItemView = new VerticalStackLayout();
            Label dataFormLabel = new Label();
            dataFormLabel.LineBreakMode = LineBreakMode.WordWrap;
            FormattedString labelText = new FormattedString();
            dataFormLabel.Margin = new Thickness(0, 0, 0, 5);
            labelText.Spans.Add(new Span { Text = "By signing below, you agree to the ", TextColor = this.GetDynamicTextColor("SfDataFormNormalEditorTextColor"), FontSize = 14 });

            Span span = new Span { Text = "terms and conditions", TextColor = this.GetDynamicTextColor("SfDataFormFocusedEditorStroke"), FontSize = 14, TextDecorations = TextDecorations.Underline };
            span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await Launcher.OpenAsync("https://www.syncfusion.com/)")) });
            labelText.Spans.Add(span);

            dataFormLabel.FormattedText = labelText;
            Border border = new Border();
            signaturePad = new SfSignaturePad();
            signaturePad.StrokeColor = this.GetDynamicTextColor("SfDataFormNormalEditorTextColor");
            signaturePad.Background = this.GetDynamicTextColor("SfDataFormNormalBackground");

            signaturePad.DrawCompleted += OnSignaturePadEditorDrawCompleted;
            signaturePad.DrawStarted += OmSignaturePadDrawStarted;
            
            border.Content = signaturePad;
            border.Stroke = this.GetDynamicTextColor("SfDataFormNormalEditorStroke");
            border.StrokeThickness = 1;
            border.Background = this.GetDynamicTextColor("SfDataFormNormalBackground");
            border.StrokeShape = new RoundRectangle() { CornerRadius = 5 };
            this.dataFormItem = dataFormItem;
#if MACCATALYST
            border.HeightRequest = 160;
            dataFormItemView.HeightRequest = 230;
#elif WINDOWS
            border.HeightRequest = 130;
            dataFormItemView.HeightRequest = 180;
#else
            border.HeightRequest = 140;
            dataFormItemView.HeightRequest = 210;
#endif

            signatureClearButton = new Button
            {
                Text = "Clear",
                TextColor = this.GetDynamicTextColor("SfDataFormNormalEditorTextColor"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Colors.Transparent,
                HeightRequest = 30,
                FontSize = 14,
                Padding = 0,
                Background = Brush.Transparent,
                IsVisible = false
            };

            signatureClearButton.Clicked += this.OnSignatureClearButtonClicked;

            dataFormItemView.Add(dataFormLabel);
            dataFormItemView.Add(border);
            dataFormItemView.Add(signatureClearButton);
            dataFormItem.Padding = 10;

            return dataFormItemView;
        }

        public void UpdateReadyOnly(DataFormItem dataFormItem)
        {
        }

        /// <summary>
        /// Invokes on signature pad draw completed.
        /// </summary>
        /// <param name="sender">The signature pad.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSignaturePadEditorDrawCompleted(object? sender, EventArgs e)
        {
            if (sender is SfSignaturePad signaturePad)
            {
                this.CommitValue(this.dataFormItem!, signaturePad);
            }
        }

        /// <summary>
        /// Invokes on signature clear button click.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSignatureClearButtonClicked(object? sender, EventArgs e)
        {
            if (this.signaturePad != null)
            {
                this.signaturePad.Clear();
            }

            if (this.signatureClearButton != null)
            {
                this.signatureClearButton.IsVisible = false;
            }
        }

        /// <summary>
        /// Invokes on draw started at signature pad.
        /// </summary>
        /// <param name="sender">The signature pad.</param>
        /// <param name="e">The event arguments.</param>
        private void OmSignaturePadDrawStarted(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.signatureClearButton != null)
            {
                this.signatureClearButton.IsVisible = true;
            }
        }

        private Color GetDynamicTextColor(string resourceName)
        {
            if (App.Current != null && App.Current.Resources.TryGetValue(resourceName, out var colorValue) && colorValue is Color color)
            {
                return color;
            }

            // Return a default color if the resource is not found or is not a Color
            return Colors.Transparent;
        }

    }
}
