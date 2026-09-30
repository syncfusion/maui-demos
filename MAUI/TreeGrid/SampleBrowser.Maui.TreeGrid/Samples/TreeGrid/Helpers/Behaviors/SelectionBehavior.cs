using SampleBrowser.Maui.Base;
using Syncfusion.Maui.TreeGrid;

namespace SampleBrowser.Maui.TreeGrid
{
    internal class SelectionBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.TreeGrid.SfTreeGrid? treeGrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            treeGrid = bindable.FindByName<Syncfusion.Maui.TreeGrid.SfTreeGrid?>("treeGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            var items = new List<string> { "None", "Single", "SingleDeselect", "Multiple" };

            comboBox.ItemsSource = items;
            comboBox.SelectedIndex = 3;
            comboBox.SelectionChanged += SelectionPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= SelectionPicker_SelectedIndexChanged;

            treeGrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void SelectionPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
            {
                case 0:
                    SetTreeGridSelectionMode(TreeGridSelectionMode.None);
                    break;
                case 1:
                    SetTreeGridSelectionMode(TreeGridSelectionMode.Single);
                    break;
                case 2:
                    SetTreeGridSelectionMode(TreeGridSelectionMode.SingleDeselect);
                    break;
                case 3:
                    SetTreeGridSelectionMode(TreeGridSelectionMode.Multiple);
                    break;
            }
        }

        private void SetTreeGridSelectionMode(TreeGridSelectionMode TreeGridSelectionMode)
        {
            if (this.treeGrid != null && this.treeGrid.SelectionMode != TreeGridSelectionMode)
            {
                this.treeGrid.SelectionMode = TreeGridSelectionMode;
            }
        }
    }
}
