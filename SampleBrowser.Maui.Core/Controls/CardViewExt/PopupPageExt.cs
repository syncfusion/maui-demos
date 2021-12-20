#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace SampleBrowser.Maui.Core
{
    public class PopUpPageExt : ContentPage
    {
        private Grid? rootGrid;

        SampleView? SampleView;

        CardViewExt? cardViewExt;

        public PopUpPageExt(View? view, CardViewExt? card)
        {       
            this.BackgroundColor = Colors.White;
            this.Padding = new Thickness(10);
            this.rootGrid = new Grid() { Padding = 10 };
            var samplePageType = GetSamplePage(card);
            SampleView = Activator.CreateInstance(samplePageType!.SampleType) as SampleView;
            var newView = GetSampleView(SampleView, view, card);
            this.cardViewExt = card;
            this.rootGrid.Children.Add(newView);
            this.Content = this.rootGrid;
        }


        private View? GetSampleView(SampleView? newView, View? view, CardViewExt? card)
        {
            var verticalStackLayout = newView?.ScrollView?.Content as VerticalStackLayout;

            if (verticalStackLayout != null && verticalStackLayout.Children.Count > 0)
            {
                foreach (var item in verticalStackLayout.Children)
                {
                    if (item is CardViewExt)
                    {
                        if ((item as CardViewExt)?.Title == card?.Title)
                        {

                            CardViewExt duplicatedCardView = (CardViewExt)item;
                            newView?.OnExpandedViewAppearing(duplicatedCardView!.MainContent as View);
                            return duplicatedCardView.MainContent;
                        }
                    }
                }
            }
            return view;
        }

        private SamplePage? GetSamplePage(View? view)
        {
            if (view?.Parent is SamplePage)
            {
                return view.Parent as SamplePage;
            }
            else
            {
                if (view?.Parent != null)
                    return GetSamplePage(view.Parent as View);
            }
            return null;
        }

        protected override void OnDisappearing()
        {
            this.rootGrid?.Children.Clear();

            if(this.cardViewExt !=null && this.SampleView!=null && this.cardViewExt.MainContent != null)
            {
                this.SampleView.OnExpandedViewDisappearing(this.cardViewExt.MainContent);
            }
            this.rootGrid = null;
            this.Content = null;
            base.OnDisappearing();
        }
    }
}