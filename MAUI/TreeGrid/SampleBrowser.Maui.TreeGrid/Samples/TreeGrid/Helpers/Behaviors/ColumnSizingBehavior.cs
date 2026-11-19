using SampleBrowser.Maui.Base;
using Syncfusion.Maui.TreeGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.TreeGrid
{
    public class ColumnSizingBehavior: Behavior<SampleView>
    {
        private Syncfusion.Maui.TreeGrid.SfTreeGrid? treeGrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            treeGrid = bindable.FindByName<Syncfusion.Maui.TreeGrid.SfTreeGrid?>("treeGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += ColumnSizingPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= ColumnSizingPicker_SelectedIndexChanged;

            treeGrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void ColumnSizingPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
            {
                case 0:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.Fill);
                    break;
                case 1:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.Auto);
                    break;
                case 2:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.FitByCell);
                    break;
                case 3:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.FitByHeader);
                    break;
                case 4:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.LastColumnFill);
                    break;
                case 5:
                    SetTreeGridColumnWidthMode(TreeGridColumnWidthMode.None);
                    break;
            }
        }

        private void SetTreeGridColumnWidthMode(TreeGridColumnWidthMode TreeGridColumnWidthMode)
        {
            if (this.treeGrid != null && this.treeGrid.ColumnWidthMode != TreeGridColumnWidthMode)
            {
                this.treeGrid.ColumnWidthMode = TreeGridColumnWidthMode;
            }
        }
    }
}
