#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Gauges.SfDigitalGauge
{
    using SampleBrowser.Maui.Base;
    using Microsoft.Maui.Controls;
    using Syncfusion.Maui.Sliders;

    internal class GettingStartedBehavior : Behavior<SampleView>
    {
        //Sliders.
        private SfSlider? characterHeight;
        private SfSlider? characterWidth;
        private SfSlider? characterSpacing;
        private SfSlider? characterStrokeWidth;
        private SfSlider? disabledSegmentAlpha;

        // Borders
        private Border? segmentStrokeColor1;
        private Border? segmentStrokeColor2;
        private Border? segmentStrokeColor3;
        private Border? segmentStrokeColor4;
        private Border? segmentStrokeColor5;
        private Border? segmentStrokeColor6;

        private Border? disabledSegmentStrokeColor1;
        private Border? disabledSegmentStrokeColor2;
        private Border? disabledSegmentStrokeColor3;
        private Border? disabledSegmentStrokeColor4;
        private Border? disabledSegmentStrokeColor5;
        private Border? disabledSegmentStrokeColor6;

        // Grid.
        private Grid? strokeColorGrid;
        private Grid? diabledStrokeColorGrid;

        private ViewModel? viewModel;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.disabledSegmentAlpha = bindable.Content.FindByName<SfSlider>("disabledSegmentAlpha");
            this.characterWidth = bindable.Content.FindByName<SfSlider>("characterWidth");
            this.characterHeight = bindable.Content.FindByName<SfSlider>("characterHeight");
            this.characterStrokeWidth = bindable.Content.FindByName<SfSlider>("strokeWidth");
            this.characterSpacing = bindable.Content.FindByName<SfSlider>("characterSpacing");

            // Borders
            this.segmentStrokeColor1 = bindable.Content.FindByName<Border>("segmentStrokeColor1");
            this.segmentStrokeColor2 = bindable.Content.FindByName<Border>("segmentStrokeColor2");
            this.segmentStrokeColor3 = bindable.Content.FindByName<Border>("segmentStrokeColor3");
            this.segmentStrokeColor4 = bindable.Content.FindByName<Border>("segmentStrokeColor4");
            this.segmentStrokeColor5 = bindable.Content.FindByName<Border>("segmentStrokeColor5");
            this.segmentStrokeColor6 = bindable.Content.FindByName<Border>("segmentStrokeColor6");

            this.disabledSegmentStrokeColor1 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor1");
            this.disabledSegmentStrokeColor2 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor2");
            this.disabledSegmentStrokeColor3 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor3");
            this.disabledSegmentStrokeColor4 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor4");
            this.disabledSegmentStrokeColor5 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor5");
            this.disabledSegmentStrokeColor6 = bindable.Content.FindByName<Border>("disabledSegmentStrokeColor6");

            // Grid
            this.strokeColorGrid = bindable.Content.FindByName<Grid>("strokeColorGrid");
            this.diabledStrokeColorGrid = bindable.Content.FindByName<Grid>("diabledStrokeColorGrid");

            // View Model
            this.viewModel = bindable.Content.FindByName<ViewModel>("viewModel");

            TapGestureRecognizer enableSegmentGesture = new TapGestureRecognizer();
            enableSegmentGesture.Tapped += SegmentStrokeColorChanged;

            TapGestureRecognizer disabledSegementGesture = new TapGestureRecognizer();
            disabledSegementGesture.Tapped += DisabledSegmentStrokeColorChanged;

            // Add gesture for borders
            this.segmentStrokeColor1.GestureRecognizers.Add(enableSegmentGesture);
            this.segmentStrokeColor2.GestureRecognizers.Add(enableSegmentGesture);
            this.segmentStrokeColor3.GestureRecognizers.Add(enableSegmentGesture);
            this.segmentStrokeColor4.GestureRecognizers.Add(enableSegmentGesture);
            this.segmentStrokeColor5.GestureRecognizers.Add(enableSegmentGesture);
            this.segmentStrokeColor6.GestureRecognizers.Add(enableSegmentGesture);

            this.disabledSegmentStrokeColor1.GestureRecognizers.Add(disabledSegementGesture);
            this.disabledSegmentStrokeColor2.GestureRecognizers.Add(disabledSegementGesture);
            this.disabledSegmentStrokeColor3.GestureRecognizers.Add(disabledSegementGesture);
            this.disabledSegmentStrokeColor4.GestureRecognizers.Add(disabledSegementGesture);
            this.disabledSegmentStrokeColor5.GestureRecognizers.Add(disabledSegementGesture);
            this.disabledSegmentStrokeColor6.GestureRecognizers.Add(disabledSegementGesture);

            // Events
            this.characterWidth.ValueChanged += this.CharacterWidthChanged;
            this.characterHeight.ValueChanged += this.CharacterHeightChanged;
            this.characterStrokeWidth.ValueChanged += this.CharacterStrokeWidthChanged;
            this.characterSpacing.ValueChanged += this.CharacterSpacingChanged;
            this.disabledSegmentAlpha.ValueChanged += this.DisabledSegmentAlphaChanged;
        }

        private void SegmentStrokeColorChanged(object? sender, TappedEventArgs e)
        {
            Border? border = (Border?)sender;
            if (border == null)
            {
                return;
            }

            if (this.strokeColorGrid != null)
            {
                foreach (var item in this.strokeColorGrid!.Children)
                {
                    if (item == border)
                    {
                        border.Stroke = Colors.Black;
                        border.StrokeThickness = 2;
                    }
                    else
                    {
                        ((Border)item).StrokeThickness = 0;
                    }
                }
            }

            if (this.viewModel != null)
            {
                this.viewModel.CharacterStroke = border.BackgroundColor;
            }
        }

        private void DisabledSegmentStrokeColorChanged(object? sender, TappedEventArgs e)
        {
            Border? border = (Border?)sender;
            if (border == null)
            {
                return;
            }

            if (this.diabledStrokeColorGrid != null)
            {
                foreach (var item in this.diabledStrokeColorGrid!.Children)
                {
                    if (item == border)
                    {
                        border.Stroke = Colors.Black;
                        border.StrokeThickness = 2;
                    }
                    else
                    {
                        ((Border)item).StrokeThickness = 0;
                    }
                }
            }

            if (this.viewModel != null)
            {
                this.viewModel.DisabledSegmentStroke = border.BackgroundColor;
            }
        }

        private void DisabledSegmentAlphaChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                this.viewModel.DisabledSegmentAlpha = (float)e.NewValue;
            }
        }

        private void CharacterWidthChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                this.viewModel.CharacterWidth = e.NewValue;
            }
        }

        private void CharacterHeightChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                this.viewModel.CharacterHeight = e.NewValue;
            }
        }

        private void CharacterSpacingChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                this.viewModel.CharacterSpacing = e.NewValue;
            }
        }

        private void CharacterStrokeWidthChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                this.viewModel.StrokeWidth = e.NewValue;
            }
        }
    }
}
