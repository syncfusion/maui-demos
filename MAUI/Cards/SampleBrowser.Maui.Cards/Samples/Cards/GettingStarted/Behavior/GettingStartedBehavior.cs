#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Cards.SfCards
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Cards;
    using Syncfusion.Maui.Buttons;
    using System;
    using Syncfusion.Maui.DataSource.Extensions;
    using SfComboBox = Syncfusion.Maui.Inputs.SfComboBox;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        private SfCardView? firstCard, secondCard, thirdCard, fourthCard, fifthCard;

        private Label? cornerRadiusLabel;

        private Slider? cornerRadiusSlider;

        private SfSwitch? indicatorSwitch;

        private Grid? indicatorPositionOption;

        /// <summary>
        /// This combo box is used to choose the indicator position of the cards.
        /// </summary>
        private SfComboBox? indicatorPositionComboBox;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            this.firstCard = sampleView.Content.FindByName<SfCardView>("firstCard");
            this.secondCard = sampleView.Content.FindByName<SfCardView>("secondCard");
            this.thirdCard = sampleView.Content.FindByName<SfCardView>("thirdCard");
            this.fourthCard = sampleView.Content.FindByName<SfCardView>("fourthCard");
            this.fifthCard = sampleView.Content.FindByName<SfCardView>("fifthCard");
            this.cornerRadiusLabel = sampleView.Content.FindByName<Label>("cornerRadiusLabel");
            this.cornerRadiusSlider = sampleView.Content.FindByName<Slider>("cornerRadiusSlider");
            this.indicatorSwitch = sampleView.Content.FindByName<SfSwitch>("indicatorSwitch");
            this.indicatorPositionOption = sampleView.Content.FindByName<Grid>("indicatorPositionOption");

            this.indicatorPositionComboBox = sampleView.Content.FindByName<SfComboBox>("indicatorPositionComboBox");
            this.indicatorPositionComboBox.ItemsSource = Enum.GetValues(typeof(CardIndicatorPosition)).ToList<CardIndicatorPosition>();
            this.indicatorPositionComboBox.SelectedIndex = 0;
            this.indicatorPositionComboBox.SelectionChanged += IndicatorPositionComboBox_SelectionChanged;

            if (this.cornerRadiusSlider != null)
            {
                this.cornerRadiusSlider.ValueChanged += this.CornerRadiusSlider_ValueChanged;
            }

            if (this.indicatorSwitch != null)
            {
                this.indicatorSwitch.StateChanged += this.IndicatorSwitch_StateChanged; ;
            }
        }

        private void IndicatorSwitch_StateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                bool isIndcatorEnabled = e.NewValue.Value;
                double indicatorThickness = isIndcatorEnabled ? 7 : 0;
                if (this.firstCard != null && this.secondCard != null && this.thirdCard != null && this.fourthCard != null && this.fifthCard != null && this.indicatorPositionOption != null)
                {
                    this.firstCard.IndicatorThickness = indicatorThickness;
                    this.secondCard.IndicatorThickness = indicatorThickness;
                    this.thirdCard.IndicatorThickness = indicatorThickness;
                    this.fourthCard.IndicatorThickness = indicatorThickness;
                    this.fifthCard.IndicatorThickness = indicatorThickness;
                    if (isIndcatorEnabled)
                    {
                        this.indicatorPositionOption.IsVisible = true;
                        this.firstCard.IndicatorColor = Color.FromArgb("#E2C799");
                        this.secondCard.IndicatorColor = Color.FromArgb("#D4BBA0");
                        this.thirdCard.IndicatorColor = Color.FromArgb("#A4A8AB");
                        this.fourthCard.IndicatorColor = Color.FromArgb("#19376D");
                        this.fifthCard.IndicatorColor = Color.FromArgb("#77ABB7");
                    }
                    else
                    {
                        this.indicatorPositionOption.IsVisible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method for Cards Indication position combo box changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">Event Arguments.</param>
        private void IndicatorPositionComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && this.firstCard != null && this.secondCard != null && this.thirdCard != null && this.fourthCard != null && this.fifthCard != null)
            {
                string? selectedPosition = e.AddedItems[0].ToString();
                switch (selectedPosition)
                {
                    case "Bottom":
                        this.firstCard.IndicatorPosition = CardIndicatorPosition.Bottom;
                        this.secondCard.IndicatorPosition = CardIndicatorPosition.Bottom;
                        this.thirdCard.IndicatorPosition = CardIndicatorPosition.Bottom;
                        this.fourthCard.IndicatorPosition = CardIndicatorPosition.Bottom;
                        this.fifthCard.IndicatorPosition = CardIndicatorPosition.Bottom;
                        break;
                    case "Top":
                        this.firstCard.IndicatorPosition = CardIndicatorPosition.Top;
                        this.secondCard.IndicatorPosition = CardIndicatorPosition.Top;
                        this.thirdCard.IndicatorPosition = CardIndicatorPosition.Top;
                        this.fourthCard.IndicatorPosition = CardIndicatorPosition.Top;
                        this.fifthCard.IndicatorPosition = CardIndicatorPosition.Top;
                        break;
                    case "Left":
                        this.firstCard.IndicatorPosition = CardIndicatorPosition.Left;
                        this.secondCard.IndicatorPosition = CardIndicatorPosition.Left;
                        this.thirdCard.IndicatorPosition = CardIndicatorPosition.Left;
                        this.fourthCard.IndicatorPosition = CardIndicatorPosition.Left;
                        this.fifthCard.IndicatorPosition = CardIndicatorPosition.Left;
                        break;
                    case "Right":
                        this.firstCard.IndicatorPosition = CardIndicatorPosition.Right;
                        this.secondCard.IndicatorPosition = CardIndicatorPosition.Right;
                        this.thirdCard.IndicatorPosition = CardIndicatorPosition.Right;
                        this.fourthCard.IndicatorPosition = CardIndicatorPosition.Right;
                        this.fifthCard.IndicatorPosition = CardIndicatorPosition.Right;
                        break;
                }
            }
        }

        private void CornerRadiusSlider_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            if (this.firstCard != null && this.secondCard != null && this.thirdCard != null && this.fourthCard != null && this.fifthCard != null && this.cornerRadiusLabel != null)
            {
                this.firstCard.CornerRadius = new CornerRadius(Math.Round(e.NewValue));
                this.secondCard.CornerRadius = new CornerRadius(Math.Round(e.NewValue));
                this.thirdCard.CornerRadius = new CornerRadius(Math.Round(e.NewValue));
                this.fourthCard.CornerRadius = new CornerRadius(Math.Round(e.NewValue));
                this.fifthCard.CornerRadius = new CornerRadius(Math.Round(e.NewValue));
                this.cornerRadiusLabel.Text = "CornerRadius: " + Math.Round(e.NewValue);
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.cornerRadiusSlider != null)
            {
                this.cornerRadiusSlider.ValueChanged -= CornerRadiusSlider_ValueChanged;
            }

            if (this.indicatorSwitch != null)
            {
                this.indicatorSwitch.StateChanged -= IndicatorSwitch_StateChanged;
            }

            if (this.indicatorPositionComboBox != null)
            {
                this.indicatorPositionComboBox.SelectionChanged -= IndicatorPositionComboBox_SelectionChanged;
            }
        }
    }
}
