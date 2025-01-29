#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    class SwipingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private OrderInfoViewModel? orderInfoViewModel;
        private ObservableCollection<OrderInfo>? orderInfo;  

        private int swipedRowIndex;

        private SfPopup? popup;

        Entry? ordeIDEntry, nameEntry, cityEntry, countryEntry;


        protected override void OnAttachedTo(SampleView bindable)
        {
            this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.orderInfoViewModel = new OrderInfoViewModel();
            this.datagrid.ItemsSource = this.orderInfoViewModel?.OrdersInfo; 
            this.orderInfo = this.datagrid.ItemsSource as ObservableCollection<OrderInfo>;
            InitializePopup();
            this.datagrid.SwipeEnded += Datagrid_SwipeEnded;
        }

        void InitializePopup()
        {

            popup = new SfPopup
            {
                HeaderTitle = "Edit Details",
                ShowFooter = true,
                WidthRequest = 320,
                HeightRequest = 360,
                FooterHeight = 80,
                AcceptButtonText = "Save",
                AcceptCommand = new Command(PopupSave),
                DeclineCommand = new Command(PopupCancel),
                DeclineButtonText = "Cancel",
                AppearanceMode = PopupButtonAppearanceMode.TwoButton
            };

            // Set the content template
            popup.ContentTemplate = new DataTemplate(() =>
            {
                // Create the grid
                var grid = new Grid
                {
                    ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(100) },
                    new ColumnDefinition { Width = new GridLength(170) },
                },
                    RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                }
                };

                int heightRequest = 37;
                int widthRequest = 100;
                grid.Padding = new Thickness(25, 8, 25, 18);

                grid.Add(new Label { Text = "Order ID", HeightRequest = heightRequest, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center, Margin = new Thickness(0, 20, 0, 24) }, 0, 0);
                ordeIDEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 170,
                    Margin = new Thickness(0, 20, 0, 24),
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                ordeIDEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                ordeIDEntry.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(ordeIDEntry, 1, 0);

                grid.Add(new Label { Text = "Name", HeightRequest = heightRequest, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center }, 0, 1);
                nameEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 170,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                nameEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                nameEntry.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(nameEntry, 1, 1);

                grid.Add(new Label { Text = "City", HeightRequest = heightRequest, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center }, 0, 2);
                cityEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 170,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                cityEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                cityEntry.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(cityEntry, 1, 2);

                grid.Add(new Label { Text = "Country", HeightRequest = heightRequest, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center }, 0, 3);
                countryEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 170,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                countryEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                countryEntry.SetAppThemeColor(Entry.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(countryEntry, 1, 3);

                return grid;
            });

        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.datagrid!.SwipeEnded -= Datagrid_SwipeEnded;
            this.orderInfo = null;
            this.datagrid!.ItemsSource = null;
            this.datagrid = null;
            this.orderInfoViewModel = null;
        }

        private void Datagrid_SwipeEnded(object? sender, Syncfusion.Maui.DataGrid.DataGridSwipeEndedEventArgs e)
        {
            this.swipedRowIndex = e.RowIndex;
        }

        internal void EditButtonPressed()
        {
            if (this.orderInfo != null)
            {
                popup?.Show();
                var orderInfo = this.orderInfo[swipedRowIndex - 1];
                ordeIDEntry!.Text = orderInfo.ID;
                nameEntry!.Text = orderInfo.CustomerID;
                cityEntry!.Text = orderInfo.ShipCity;
                countryEntry!.Text = orderInfo.ShipCountry;
            }
        }

        internal void DeleteButtonPressed()
        {
            this.orderInfo?.RemoveAt(swipedRowIndex - 1);
        }

        private void PopupSave()
        {
            if (this.orderInfo != null)
            {
                var orderInfo = this.orderInfo[swipedRowIndex - 1];
                orderInfo.ID = ordeIDEntry!.Text;
                orderInfo.CustomerID = nameEntry!.Text;
                orderInfo.ShipCity = cityEntry!.Text;
                orderInfo.ShipCountry = countryEntry!.Text;
            }

            if (datagrid != null)
            {
                datagrid.ResetSwipeOffset();
            }
        }

        private void PopupCancel()
        {
            if (this.orderInfo != null)
            {
                var orderInfo = this.orderInfo[swipedRowIndex - 1];
                ordeIDEntry!.Text = orderInfo.ID;
                nameEntry!.Text = orderInfo.CustomerID;
                cityEntry!.Text = orderInfo.ShipCity;
                countryEntry!.Text = orderInfo.ShipCountry;
            }

            if (datagrid != null)
            {
                datagrid.ResetSwipeOffset();
            }
        }

    }
}
