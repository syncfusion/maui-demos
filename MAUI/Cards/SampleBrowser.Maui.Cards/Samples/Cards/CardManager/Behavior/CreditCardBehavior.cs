#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Cards;

namespace SampleBrowser.Maui.Cards.SfCards
{
    public class CreditCardBehavior : Behavior<SampleView>
    {
        private SfCardLayout? cardLayout;

        private Frame? firstCardDetails, secondCardDetails, thirdCardDetails, firstCardDue, secondCardDue, thirdCardDue;

        private Button? payNowButton1, payNowButton2, payNowButton3;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.cardLayout = sampleView.Content.FindByName<SfCardLayout>("cardLayout");
            this.firstCardDetails = sampleView.Content.FindByName<Frame>("firstCardDetails");
            this.secondCardDetails = sampleView.Content.FindByName<Frame>("secondCardDetails");
            this.thirdCardDetails = sampleView.Content.FindByName<Frame>("thirdCardDetails");
            this.firstCardDue = sampleView.Content.FindByName<Frame>("firstCardDue");
            this.secondCardDue = sampleView.Content.FindByName<Frame>("secondCardDue");
            this.thirdCardDue = sampleView.Content.FindByName<Frame>("thirdCardDue");
            this.payNowButton1 = sampleView.Content.FindByName<Button>("payNowButton1");
            this.payNowButton2 = sampleView.Content.FindByName<Button>("payNowButton2");
            this.payNowButton3 = sampleView.Content.FindByName<Button>("payNowButton3");


            if (this.cardLayout != null)
            {
                this.cardLayout.VisibleIndexChanged += this.CardLayout_VisibleIndexChanged;
                this.cardLayout.VisibleIndexChanging += this.CardLayout_VisibleIndexChanging;
            }

            if (this.payNowButton1 != null && this.payNowButton2 != null && this.payNowButton3 != null)
            {
                this.payNowButton1.Clicked += this.PayNowButton_Clicked;
                this.payNowButton2.Clicked += this.PayNowButton_Clicked;
                this.payNowButton3.Clicked += this.PayNowButton_Clicked;
            }
        }

        private async void PayNowButton_Clicked(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                button.Text = "PAID";
                button.IsEnabled = false;
            }

            if (App.Current?.MainPage != null)
            {
                await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
            }
        }

        private void CardLayout_VisibleIndexChanging(object? sender, CardVisibleIndexChangingEventArgs e)
        {
            if (e.NewIndex == null)
            {
                e.Cancel = true;
            }
        }

        private void CardLayout_VisibleIndexChanged(object? sender, CardVisibleIndexChangedEventArgs e)
        {
            if (this.firstCardDetails != null && this.secondCardDetails != null && this.thirdCardDetails != null && this.firstCardDue != null && this.secondCardDue != null && this.thirdCardDue != null && e.NewIndex != null)
            {
                if (e.NewIndex == 0)
                {
                    this.firstCardDue.IsVisible = false;
                    this.secondCardDue.IsVisible = false;
                    this.thirdCardDue.IsVisible = true;
                    this.firstCardDetails.IsVisible = false;
                    this.secondCardDetails.IsVisible = false;
                    this.thirdCardDetails.IsVisible = true;
                }
                else if (e.NewIndex == 1)
                {
                    this.firstCardDue.IsVisible = false;
                    this.secondCardDue.IsVisible = true;
                    this.thirdCardDue.IsVisible = false;
                    this.firstCardDetails.IsVisible = false;
                    this.secondCardDetails.IsVisible = true;
                    this.thirdCardDetails.IsVisible = false;
                }
                else if (e.NewIndex == 2)
                {
                    this.firstCardDue.IsVisible = true;
                    this.secondCardDue.IsVisible = false;
                    this.thirdCardDue.IsVisible = false;
                    this.firstCardDetails.IsVisible = true;
                    this.secondCardDetails.IsVisible = false;
                    this.thirdCardDetails.IsVisible = false;
                }
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.cardLayout != null)
            {
                this.cardLayout.VisibleIndexChanged -= this.CardLayout_VisibleIndexChanged;
                this.cardLayout.VisibleIndexChanging -= this.CardLayout_VisibleIndexChanging;
            }

            if (this.payNowButton1 != null && this.payNowButton2 != null && this.payNowButton3 != null)
            {
                this.payNowButton1.Clicked -= this.PayNowButton_Clicked;
                this.payNowButton2.Clicked -= this.PayNowButton_Clicked;
                this.payNowButton3.Clicked -= this.PayNowButton_Clicked;
            }
        }
    }
}
