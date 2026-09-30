using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.DataGrid
{
    public class ResizingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;

        protected override void OnAttachedTo(SampleView bindable)
        {
            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

#if ANDROID
            if (this.dataGrid != null)
            {
                dataGrid.DefaultStyle.GridLineStrokeThickness *= (float)DeviceDisplay.MainDisplayInfo.Density;
                dataGrid.DefaultStyle.HeaderGridLineStrokeThickness *= (float)DeviceDisplay.MainDisplayInfo.Density;
            }
#endif

            this.comboBox?.SelectionChanged += ComboBox_SelectionChanged;

            base.OnAttachedTo(bindable);
        }

        private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.dataGrid != null && this.comboBox != null)
            {
                if (this.comboBox.SelectedIndex == 0)
                {
                    this.dataGrid.ColumnResizeMode = Syncfusion.Maui.DataGrid.DataGridColumnResizeMode.OnTouchUp;
                    this.dataGrid.RowResizeMode = Syncfusion.Maui.DataGrid.DataGridRowResizeMode.OnTouchUp;
                }
                else if (this.comboBox.SelectedIndex == 1)
                {
                    this.dataGrid.ColumnResizeMode = Syncfusion.Maui.DataGrid.DataGridColumnResizeMode.OnMoved;
                    this.dataGrid.RowResizeMode = Syncfusion.Maui.DataGrid.DataGridRowResizeMode.OnMoved;
                }
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.comboBox?.SelectionChanged -= ComboBox_SelectionChanged;

            this.comboBox = null;
            this.dataGrid = null;

            base.OnDetachingFrom(bindable);
        }
    }
}
