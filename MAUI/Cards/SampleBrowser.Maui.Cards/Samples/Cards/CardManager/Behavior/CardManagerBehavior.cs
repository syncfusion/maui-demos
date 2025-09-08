#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Cards;

namespace SampleBrowser.Maui.Cards.SfCards
{
    public class CardManagerBehavior : Behavior<SampleView>
    {
        private SfCardLayout? cardLayout;

        private Border? firstCardDetails, secondCardDetails, thirdCardDetails, firstCardDue, secondCardDue, thirdCardDue, cardManagerFrame, cardDetailsFrame;

        private Button? payNowButton1, payNowButton2, payNowButton3;

        private Label? amountLabel, dueLabel, creditsLabel;

        private SfCardView? firstCard, secondCard, thirdCard;

        private ImageButton? backButton;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.cardLayout = sampleView.Content.FindByName<SfCardLayout>("cardLayout");
            this.firstCardDetails = sampleView.Content.FindByName<Border>("firstCardDetails");
            this.secondCardDetails = sampleView.Content.FindByName<Border>("secondCardDetails");
            this.thirdCardDetails = sampleView.Content.FindByName<Border>("thirdCardDetails");
            this.firstCardDue = sampleView.Content.FindByName<Border>("firstCardDue");
            this.secondCardDue = sampleView.Content.FindByName<Border>("secondCardDue");
            this.thirdCardDue = sampleView.Content.FindByName<Border>("thirdCardDue");
            this.payNowButton1 = sampleView.Content.FindByName<Button>("payNowButton1");
            this.payNowButton2 = sampleView.Content.FindByName<Button>("payNowButton2");
            this.payNowButton3 = sampleView.Content.FindByName<Button>("payNowButton3");
            this.cardManagerFrame = sampleView.Content.FindByName<Border>("cardManagerFrame");
            this.cardDetailsFrame = sampleView.Content.FindByName<Border>("cardDetailsFrame");
            this.amountLabel = sampleView.Content.FindByName<Label>("amountLabel");
            this.dueLabel = sampleView.Content.FindByName<Label>("dueLabel");
            this.creditsLabel = sampleView.Content.FindByName<Label>("creditsLabel");
            this.firstCard = sampleView.Content.FindByName<SfCardView>("firstCard");
            this.secondCard = sampleView.Content.FindByName<SfCardView>("secondCard");
            this.thirdCard = sampleView.Content.FindByName<SfCardView>("thirdCard");
            this.backButton = sampleView.Content.FindByName<ImageButton>("backButton");


            if (this.cardLayout != null)
            {
                this.cardLayout.VisibleIndexChanged += this.CardLayout_VisibleIndexChanged;
                this.cardLayout.VisibleIndexChanging += this.CardLayout_VisibleIndexChanging;
                this.cardLayout.Tapped += this.CardLayout_Tapped;
            }

            if (this.payNowButton1 != null && this.payNowButton2 != null && this.payNowButton3 != null && this.backButton != null)
            {
                this.payNowButton1.Clicked += this.PayNowButton_Clicked;
                this.payNowButton2.Clicked += this.PayNowButton_Clicked;
                this.payNowButton3.Clicked += this.PayNowButton_Clicked;
                this.backButton.Clicked += this.BackButton_Clicked;
            }
        }

        private void BackButton_Clicked(object? sender, EventArgs e)
        {
            if (this.cardLayout != null && this.cardDetailsFrame != null && this.cardManagerFrame != null)
            {
                this.cardManagerFrame.IsVisible = true;
                this.cardDetailsFrame.IsVisible = false;
            }
        }

        private void CardLayout_Tapped(object? sender, TappedEventArgs e)
        {
            if (this.cardLayout != null && this.cardDetailsFrame != null && this.cardManagerFrame != null && this.firstCard != null && this.secondCard != null && this.thirdCard != null)
            {
                SfCardView? card = e.Parameter as SfCardView;
                int tappedIndex;
                if (card != null && this.amountLabel != null && this.dueLabel != null && this.creditsLabel != null)
                {
                    tappedIndex = this.cardLayout.Children.IndexOf(card);
                    if (tappedIndex == 0)
                    {
                        this.cardManagerFrame.IsVisible = false;
                        this.cardDetailsFrame.IsVisible = true;
                        this.firstCard.IsVisible = false;
                        this.secondCard.IsVisible = false;
                        this.thirdCard.IsVisible = true;
                        this.amountLabel.Text = "$360";
                        this.dueLabel.Text = "Due in 5 days";
                        this.creditsLabel.Text = "10";
                    }
                    else if (tappedIndex == 1)
                    {
                        this.cardManagerFrame.IsVisible = false;
                        this.cardDetailsFrame.IsVisible = true;
                        this.firstCard.IsVisible = false;
                        this.secondCard.IsVisible = true;
                        this.thirdCard.IsVisible = false;
                        this.amountLabel.Text = "$380";
                        this.dueLabel.Text = "Due in 3 days";
                        this.creditsLabel.Text = "15";
                    }
                    else if (tappedIndex == 2)
                    {
                        this.cardManagerFrame.IsVisible = false;
                        this.cardDetailsFrame.IsVisible = true;
                        this.firstCard.IsVisible = true;
                        this.secondCard.IsVisible = false;
                        this.thirdCard.IsVisible = false;
                        this.amountLabel.Text = "$300";
                        this.dueLabel.Text = "Due in 2 days";
                        this.creditsLabel.Text = "12";
                    }
                }
            }
        }

        private async void PayNowButton_Clicked(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button != null && this.dueLabel != null)
            {
                button.Text = "PAID";
                button.IsEnabled = false;
            }

            if (App.Current?.Windows.Count > 0)
            {
                var mainPage = App.Current.Windows[0].Page;
                if (mainPage != null)
                {
                    await mainPage.DisplayAlert("", "Payment Successful", "OK");
                }
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
                this.cardLayout.Tapped -= this.CardLayout_Tapped;
            }

            if (this.payNowButton1 != null && this.payNowButton2 != null && this.payNowButton3 != null && this.backButton != null)
            {
                this.payNowButton1.Clicked -= this.PayNowButton_Clicked;
                this.payNowButton2.Clicked -= this.PayNowButton_Clicked;
                this.payNowButton3.Clicked -= this.PayNowButton_Clicked;
                this.backButton.Clicked -= this.BackButton_Clicked;
            }
        }
    }
}
