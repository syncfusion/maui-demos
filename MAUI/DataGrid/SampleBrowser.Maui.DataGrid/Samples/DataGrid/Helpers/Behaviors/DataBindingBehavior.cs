#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class DataBindingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        private OrderInfoViewModel? orderInfoViewModel;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindable">SampleView type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            this.orderInfoViewModel = new OrderInfoViewModel();

            this.comboBox.SelectionChanged += this.SelectionPicker_SelectedIndexChanged!;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindable">A sampleView type of bindAble</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.comboBox!.SelectionChanged -= this.SelectionPicker_SelectedIndexChanged!;

            this.datagrid = null;
            this.comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Triggers while selected Index was changed, used to set a Collection
        /// </summary>
        /// <param name="sender">OnSelectionChanged event sender</param>
        /// <param name="e">EventArgs e</param>
        private void SelectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox!.SelectedIndex == 0)
            {
                this.datagrid!.Columns.Clear();
                this.datagrid.ItemsSource = this.orderInfoViewModel!.OrdersInfo;
                DataGridTextColumn orderIdColumn = new DataGridTextColumn();
                orderIdColumn.MappingName = "OrderID";
                orderIdColumn.HeaderText = "Order ID";
                orderIdColumn.MinimumWidth = 120;

                DataGridTextColumn shipCountryColumn = new DataGridTextColumn();
                shipCountryColumn.MappingName = "ShipCountry";
                shipCountryColumn.HeaderText = "Country";
                shipCountryColumn.MinimumWidth = 120;

                DataGridTextColumn customerColumn = new DataGridTextColumn();
                customerColumn.MappingName = "CustomerID";
                customerColumn.HeaderText = "Name";
                customerColumn.MinimumWidth = 120;

                DataGridTextColumn cityColumn = new DataGridTextColumn();
                cityColumn.MappingName = "ShipCity";
                cityColumn.HeaderText = "City";
                cityColumn.MinimumWidth = 120;

                DataGridCheckBoxColumn checkBoxColumn = new DataGridCheckBoxColumn();
                checkBoxColumn.MappingName = "IsClosed";
                checkBoxColumn.HeaderText = "Is Online";
                checkBoxColumn.MinimumWidth = 120;

                this.datagrid.Columns.Add(orderIdColumn);
                this.datagrid.Columns.Add(customerColumn);
                this.datagrid.Columns.Add(cityColumn);
                this.datagrid.Columns.Add(shipCountryColumn);
                this.datagrid.Columns.Add(checkBoxColumn);
            }
            else if (this.comboBox.SelectedIndex == 1)
            {
                this.datagrid!.Columns.Clear();
                this.datagrid.ItemsSource = this.GetDataTable();
                DataGridTextColumn customerId = new DataGridTextColumn();
                customerId.MappingName = "CustomerID";
                customerId.HeaderText = "ID";

                DataGridTextColumn companyName = new DataGridTextColumn();
                companyName.MappingName = "Company";
                companyName.HeaderText = "Company";

                DataGridTextColumn contactName = new DataGridTextColumn();
                contactName.MappingName = "ContactName";
                contactName.HeaderText = "Contact Name";

                DataGridTextColumn city = new DataGridTextColumn();
                city.MappingName = "City";
                city.HeaderText = "City";

                this.datagrid.Columns.Add(customerId);
                this.datagrid.Columns.Add(companyName);
                this.datagrid.Columns.Add(contactName);
                this.datagrid.Columns.Add(city);
            }
        }

        /// <summary>
        /// Create the DataTable
        /// </summary>
        /// <returns>Data Table</returns>
        private DataTable GetDataTable()
        {
            DataTable employeeCollection = new DataTable();
            employeeCollection.Columns.Add("CustomerID", typeof(string));
            employeeCollection.Columns.Add("Company", typeof(string));
            employeeCollection.Columns.Add("ContactName", typeof(string));
            employeeCollection.Columns.Add("City", typeof(string));
            employeeCollection.Rows.Add("ALFKI", "Alferds Futterkiste", "Maria Anders", "Berlin");
            employeeCollection.Rows.Add("ANATR", "Ana Trujilo Emparedados y Hela", "Ana Trujilo", "Mexico D.F.");
            employeeCollection.Rows.Add("ANTON", "Antonio Moreno Taqueria", "Antonio Moreno", "Mexico D.F.");
            employeeCollection.Rows.Add("AROUT", "Around the Horn", "Thomas Hardy", "London");
            employeeCollection.Rows.Add("BERGS", "Berglunds Snabbkop", "Christina Berglund", "Lulea");
            employeeCollection.Rows.Add("BLAUS", "Blauer see Delikatessen", "Hanna Moss", "Mannheim");
            employeeCollection.Rows.Add("BLONP", "Blondel Pere et Fils", "Erederique Citeaux", "Strasbourg");
            employeeCollection.Rows.Add("BOLID", "Bolids Comidas Preparadas", "Martin Sommer", "Madrid");
            employeeCollection.Rows.Add("BONP", "Bon App", "Laurence Lebihan", "Marseille");
            employeeCollection.Rows.Add("BOTTM", "Bottom-Dollar Markets", "Elizabeth Lincoln", "Tsawassen");
            employeeCollection.Rows.Add("BSBEV", "B's Beverages", "Victoria Ashworth", "London");
            employeeCollection.Rows.Add("CACTU", "Cactus Comidas para llevar", "Patricio Simpson", "Bueno Aires");
            employeeCollection.Rows.Add("CENTC", "Centro Comercial Moctezuma", "Francisco Chang", "Mexico D.F.");
            employeeCollection.Rows.Add("CHOPS", "Chop-Suey Chinese", "Yang Wang", "Bern");
            employeeCollection.Rows.Add("COMMI", "Comercio Minerio", "Pedro Afonso", "Sao Paulo");
            employeeCollection.Rows.Add("CONSH", "Consolidated Holdings", "Elizabeth Brown", "London");
            employeeCollection.Rows.Add("DRACD", "Drachenblut Entier", "Sven Ottlieb", "Aachen");
            employeeCollection.Rows.Add("DUMON", "Dumonde Entier", "Janine Labrune", "Nantes");
            employeeCollection.Rows.Add("EASTC", "Eastern Connection", "Ann Devon", "London");
            employeeCollection.Rows.Add("ERNSH", "Ernst Handel", "Roland Mendel", "Graz");
            return employeeCollection;
        }
    }
}
