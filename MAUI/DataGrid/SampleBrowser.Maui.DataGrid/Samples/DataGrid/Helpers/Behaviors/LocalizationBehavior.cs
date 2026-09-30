using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SampleBrowser.Maui.DataGrid
{
    public class LocalizationBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            if (this.comboBox != null)
                comboBox.SelectionChanged += LocaleComboBox_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (this.comboBox != null)
                comboBox.SelectionChanged -= LocaleComboBox_SelectedIndexChanged;

            datagrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void LocaleComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.comboBox == null)
                return;

            switch (this.comboBox.SelectedIndex)
            {
                case 0:
                    ApplyCulture("en-US");
                    break;
                case 1:
                    ApplyCulture("es-ES");
                    break;
                case 2:
                    ApplyCulture("fr-FR");
                    break;
                case 3:
                    ApplyCulture("zh-CN");
                    break;
            }
        }

        private void ApplyCulture(string cultureName)
        {
            if (this.datagrid == null)
                return;

            string symbol = GetCurrencySymbol(cultureName);

            var freightColumn = this.datagrid.Columns["Freight"];
            var priceColumn = this.datagrid.Columns["Price"];
            if (freightColumn != null && priceColumn != null)
            {
                freightColumn.Format = $"{symbol}0.00";
                priceColumn.Format = $"{symbol}0";
            }

            UpdateHeaders(cultureName);

            this.datagrid.BindingContext = null;
            this.datagrid.BindingContext = new OrderInfoViewModel(cultureName);
            this.datagrid.Refresh();

        }

        private string GetCurrencySymbol(string cultureName)
        {
            return cultureName switch
            {
                "fr-FR" => "€",
                "es-ES" => "€",
                "zh-CN" => "¥",
                _ => "$"
            };
        }

        private void UpdateHeaders(string culture)
        {
            if (this.datagrid == null)
                return;

            switch (culture)
            {
                case "fr-FR":

                    this.datagrid.Columns[0].HeaderText = "Commande ID";
                    this.datagrid.Columns[1].HeaderText = "Client ID";
                    this.datagrid.Columns[2].HeaderText = "Nom";
                    this.datagrid.Columns[3].HeaderText = "Cargaison";
                    this.datagrid.Columns[4].HeaderText = "Ville";
                    this.datagrid.Columns[5].HeaderText = "Prix";

                    break;

                case "es-ES":

                    this.datagrid.Columns[0].HeaderText = "Pedido ID";
                    this.datagrid.Columns[1].HeaderText = "Clienteb ID";
                    this.datagrid.Columns[2].HeaderText = "Nombre";
                    this.datagrid.Columns[3].HeaderText = "Transporte";
                    this.datagrid.Columns[4].HeaderText = "Ciudad";
                    this.datagrid.Columns[5].HeaderText = "Precio";

                    break;

                case "zh-CN":

                    this.datagrid.Columns[0].HeaderText = "訂單編號";
                    this.datagrid.Columns[1].HeaderText = "顧客 身份";
                    this.datagrid.Columns[2].HeaderText = "姓名";
                    this.datagrid.Columns[3].HeaderText = "貨運";
                    this.datagrid.Columns[4].HeaderText = "城";
                    this.datagrid.Columns[5].HeaderText = "價錢";

                    break;

                default:

                    this.datagrid.Columns[0].HeaderText = "Order ID";
                    this.datagrid.Columns[1].HeaderText = "Customer ID";
                    this.datagrid.Columns[2].HeaderText = "Name";
                    this.datagrid.Columns[3].HeaderText = "Freight";
                    this.datagrid.Columns[4].HeaderText = "City";
                    this.datagrid.Columns[5].HeaderText = "Price";

                    break;
            }
        }
    }
}
