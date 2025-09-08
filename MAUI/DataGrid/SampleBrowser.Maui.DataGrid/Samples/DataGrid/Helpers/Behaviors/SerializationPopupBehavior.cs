#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class SerializationPopupBehavior : Behavior<SampleView>
    {
        private SfPopup? popup;

        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;

        private readonly string[] labels = new[]
        {
        "Order ID", "Customer ID", "Customer Name", "Ship City", "Ship Country", "Freight"
        };

        private readonly string[] groupColumnNames = new[]
        {
        "OrderID", "EmployeeID", "FirstName", "ShipCity", "ShipCountry", "Freight"
        };

        // Track checkbox states because popup creates checkboxes fresh every time
        private bool[] checkboxStates = new bool[6];

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid"); ;
            InitializePopup();
        }

        void InitializePopup()
        {
            popup = new SfPopup
            {
                HeaderTitle = "Column Options",
                ShowFooter = true,
                WidthRequest = 320,
                HeightRequest = 360,
                FooterHeight = 80,
                AcceptButtonText = "OK",
                AcceptCommand = new Command(SaveSelections),
                DeclineCommand = new Command(CancelSelections),
                DeclineButtonText = "Cancel",
                AppearanceMode = PopupButtonAppearanceMode.TwoButton,
            };
        }

        private void UpdateCheckboxStatesFromGrid()
        {
            if (dataGrid == null || dataGrid.View == null)
                return;

            var groupedColumns = dataGrid.View.GroupDescriptions.Select(g => g.PropertyName).ToList();

            for (int i = 0; i < groupColumnNames.Length; i++)
            {
                checkboxStates[i] = groupedColumns.Contains(groupColumnNames[i]);
            }
        }

        public void ShowPopup()
        {
            if (popup == null || dataGrid == null)
                return;

            UpdateCheckboxStatesFromGrid();
            popup.ContentTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    Padding = new Thickness(20, 10),
                    RowSpacing = 10,
                    ColumnSpacing = 3
                };

                for (int i = 0; i < groupColumnNames.Length; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 35 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    var theme = Application.Current!.RequestedTheme;

                    Color checkBoxColor;

                    if (theme == AppTheme.Dark)
                    {
                        checkBoxColor = Color.FromArgb("#D0BCFF");  // darkThemeLeftColor
                    }
                    else
                    {
                        checkBoxColor = Color.FromArgb("#6750A4");  // lightThemeLeftColor
                    }

                    var checkBox = new CheckBox
                    {
                        IsChecked = checkboxStates[i], // Set from current state
                        VerticalOptions = LayoutOptions.Center,
                        Color = checkBoxColor,
                    };

                    int index = i; // capture variable for closure
                    checkBox.CheckedChanged += (s, e) =>
                    {
                        checkboxStates[index] = e.Value;
                    };

                    var label = new Label
                    {
                        Text = labels[i],
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start
                    };

                    grid.Add(checkBox, 0, i);
                    grid.Add(label, 1, i);
                }

                return grid;
            });
            popup.Show();
        }

        private void SaveSelections()
        {
            if (dataGrid == null)
                return;

            var selectedColumns = groupColumnNames
                .Where((col, index) => checkboxStates[index])
                .ToList();

            dataGrid.GroupColumnDescriptions.Clear();

            foreach (var column in selectedColumns)
            {
                dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription
                {
                    ColumnName = column
                });
            }

            popup?.Dismiss();
        }

        private void CancelSelections()
        {
            UpdateCheckboxStatesFromGrid(); // revert checkbox states to actual grid grouping
            popup?.Dismiss();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            popup = null;
            dataGrid = null;
        }
        
    }
}
