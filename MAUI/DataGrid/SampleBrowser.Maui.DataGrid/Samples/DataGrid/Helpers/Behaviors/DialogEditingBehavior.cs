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
    class DialogEditingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private OrderInfoViewModel? orderInfoViewModel;
        private ObservableCollection<OrderInfo>? orderInfo;  

        private int swipedRowIndex;

        private SfPopup? popup;

        Entry? ordeIDEntry, nameEntry, cityEntry, countryEntry;


        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            orderInfoViewModel = new OrderInfoViewModel();
            datagrid.ItemsSource = orderInfoViewModel?.OrdersInfo; 
            orderInfo = datagrid.ItemsSource as ObservableCollection<OrderInfo>;
            InitializePopup();
            datagrid.CellTapped += Datagrid_CellTapped; 
        }

        private void Datagrid_CellTapped(object? sender, Syncfusion.Maui.DataGrid.DataGridCellTappedEventArgs e)
        {
            swipedRowIndex = e.RowColumnIndex.RowIndex;
            EditButtonPressed();
        }

        void InitializePopup()
        {

            var backgroundColorLight = Color.FromArgb("#F3EDF7");
            var backgroundColorDark = Color.FromArgb("#2A2831");
            var textColorLight = Color.FromArgb("#6750A4");
            var textColorDark = Color.FromArgb("#D0BCFF");

            popup = new SfPopup
            {
                HeaderTitle = "Edit Details",
                ShowFooter = true,
                WidthRequest = 342,
                HeightRequest = 400,
                FooterHeight = 68,
            };
            popup.SetAppThemeColor(VisualElement.BackgroundColorProperty, backgroundColorLight, backgroundColorDark);

            popup.HeaderTemplate = new DataTemplate(() =>
            {
                var headerGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Auto }
                    },
                    HeightRequest = 90,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill
                };
                headerGrid.SetAppThemeColor(VisualElement.BackgroundColorProperty, backgroundColorLight, backgroundColorDark);

                var titleLabel = new Label
                {
                    Text = "Edit Details",
                    FontSize = 24,
                    Padding = new Thickness(26, 18, 12, 8),
                    VerticalTextAlignment = TextAlignment.Center
                };

                Grid.SetColumn(titleLabel, 0);
                headerGrid.Add(titleLabel);
                return headerGrid;
            });

            popup.FooterTemplate = new DataTemplate(() =>
            {
                var footerGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = new GridLength(184) }
                    },
                    HeightRequest = 68,
                    Padding = new Thickness(15, 12, 15, 32),
                    ColumnSpacing = 8,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill
                };
                footerGrid.SetAppThemeColor(VisualElement.BackgroundColorProperty, backgroundColorLight, backgroundColorDark);

                var cancelButton = new Button
                {
                    Text = "Cancel",
                    HeightRequest = 40,
                    WidthRequest = 84,
                    CornerRadius = 20,
                    BorderWidth = 1,
                    TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#6750A4"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    BorderColor = Colors.Black,
                    Command = new Command(PopupCancel),
                    Background = Colors.Transparent
                };
                cancelButton.SetAppThemeColor(Button.BorderColorProperty, Color.FromArgb("#79747E"), Color.FromArgb("#938F99"));
                cancelButton.SetAppThemeColor(Label.TextColorProperty, Color.FromArgb("#6750A4"), Color.FromArgb("#D0BCFF"));

                var saveButton = new Button
                {
                    Text = "Save",
                    HeightRequest = 40,
                    WidthRequest = 84,
                    CornerRadius = 20,                      
                    TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#6750A4"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.End,
                    Command = new Command(PopupSave),
                };

                saveButton.SetAppThemeColor(VisualElement.BackgroundColorProperty, Color.FromArgb("#6750A4"), Color.FromArgb("#D0BCFF"));
                saveButton.SetAppThemeColor(Label.TextColorProperty, Color.FromArgb("#FFFFFF"), Color.FromArgb("#381E72"));

                var buttonsLayout = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    },
                    ColumnSpacing = 8,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 184
                };

                buttonsLayout.Add(cancelButton, 0, 0);
                buttonsLayout.Add(saveButton, 1, 0);

                Grid.SetColumn(buttonsLayout, 1);
                footerGrid.Add(buttonsLayout);
                return footerGrid;
            });

            // Set the content template
            popup.ContentTemplate = new DataTemplate(() =>
            {
                // Create the grid
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(82) },
                        new ColumnDefinition { Width = new GridLength(260) },
                    },
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    }
                };
                grid.SetAppThemeColor(VisualElement.BackgroundColorProperty, backgroundColorLight, backgroundColorDark);

                int heightRequest = 42;
                int widthRequest = 82;
                grid.Padding = new Thickness(15, 5, 15, 18);

                grid.Add(new Label { Text = "Order ID", HeightRequest = heightRequest, FontSize = 16, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center, Margin = new Thickness(10, 22, 0, 26) }, 0, 0);
                ordeIDEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 184,
                    FontSize = 16,
                    Margin = new Thickness(0, 22, 0, 26),
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                ordeIDEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                ordeIDEntry.SetAppThemeColor(VisualElement.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(ordeIDEntry, 1, 0);

                grid.Add(new Label { Text = "Name", HeightRequest = heightRequest, FontSize = 16, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center, Margin = new Thickness(10, 22, 0, 26) }, 0, 1);
                nameEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 184,
                    FontSize = 16,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                nameEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                nameEntry.SetAppThemeColor(VisualElement.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(nameEntry, 1, 1);

                grid.Add(new Label { Text = "City", HeightRequest = heightRequest, FontSize = 16, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center, Margin = new Thickness(10, 22, 0, 26) }, 0, 2);
                cityEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 184,
                    FontSize = 16,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                cityEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                cityEntry.SetAppThemeColor(VisualElement.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(cityEntry, 1, 2);

                grid.Add(new Label { Text = "Country", HeightRequest = heightRequest, FontSize = 16, WidthRequest = widthRequest, HorizontalTextAlignment = TextAlignment.Start, VerticalTextAlignment = TextAlignment.Center, Margin = new Thickness(10, 22, 0, 26) }, 0, 3);
                countryEntry = new Entry
                {
                    HeightRequest = heightRequest,
                    WidthRequest = 184,
                    FontSize = 16,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                };
                countryEntry.SetAppThemeColor(Entry.PlaceholderColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));
                countryEntry.SetAppThemeColor(VisualElement.BackgroundColorProperty, Color.FromArgb("#E7E0EC"), Color.FromArgb("#49454F"));

                grid.Add(countryEntry, 1, 3);

                return grid;
            });

        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            datagrid!.CellTapped -= Datagrid_CellTapped;
            orderInfo = null;
            datagrid!.ItemsSource = null;
            datagrid = null;
            orderInfoViewModel = null;
        }

        internal void EditButtonPressed()
        {
            if (orderInfo != null && swipedRowIndex > 0)
            {
                popup?.Show();
                var orderInfo = this.orderInfo[swipedRowIndex - 1];
                ordeIDEntry!.Text = orderInfo.ID;
                nameEntry!.Text = orderInfo.CustomerID;
                cityEntry!.Text = orderInfo.ShipCity;
                countryEntry!.Text = orderInfo.ShipCountry;
            }
        }

        private void PopupSave()
        {
            if (orderInfo != null)
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

            popup!.IsOpen = false;
        }

        private void PopupCancel()
        {
            if (orderInfo != null)
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

            popup!.IsOpen = false;
        }

    }
}
