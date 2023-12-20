#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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

        private Grid? firstCardTransactionDetails, secondCardTransactionDetails, thirdCardTransactionDetails;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.cardLayout = sampleView.Content.FindByName<SfCardLayout>("cardLayout");
            this.firstCardTransactionDetails = sampleView.Content.FindByName<Grid>("firstCardTransactionDetails");
            this.secondCardTransactionDetails = sampleView.Content.FindByName<Grid>("secondCardTransactionDetails");
            this.thirdCardTransactionDetails = sampleView.Content.FindByName<Grid>("thirdCardTransactionDetails");

            if (this.cardLayout != null)
            {
                this.cardLayout.VisibleIndexChanged += this.CardLayout_VisibleIndexChanged;
                this.cardLayout.VisibleIndexChanging += this.CardLayout_VisibleIndexChanging;
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
            if (this.firstCardTransactionDetails != null && this.secondCardTransactionDetails != null && this.thirdCardTransactionDetails != null && e.NewIndex != null)
            {
                this.firstCardTransactionDetails.IsVisible = false;
                this.secondCardTransactionDetails.IsVisible = false;
                this.thirdCardTransactionDetails.IsVisible = false;

                if (e.NewIndex == 0)
                {
                    this.thirdCardTransactionDetails.IsVisible = true;
                }
                else if (e.NewIndex == 1)
                {
                    this.secondCardTransactionDetails.IsVisible = true;
                }
                else if (e.NewIndex == 2)
                {
                    this.firstCardTransactionDetails.IsVisible = true;
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
        }
    }
}
