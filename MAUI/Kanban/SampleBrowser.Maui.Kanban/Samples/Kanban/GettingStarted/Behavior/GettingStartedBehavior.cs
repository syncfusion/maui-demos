namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Kanban;

    /// <summary>
    /// Represents the behavior for changing flow direction in sample view.
    /// </summary>
    public class GettingStartedBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The kanban instance.
        /// </summary>
        private SfKanban? kanban;

        /// <summary>
        /// The flow direction combobox instance.
        /// </summary>
        private SfComboBox? comboBox;

        #endregion

        #region Override methods

        /// <summary>
        /// Invoked when behavior is attached to a view.
        /// </summary>
        /// <param name="sampleView">The sample view to which the behavior is attached.</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            this.kanban = sampleView.Content.FindByName<SfKanban>("kanban");
            this.comboBox = sampleView.FindByName<SfComboBox>("flowDirectionComboBox");

            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged += this.OnFlowDirectionComboBoxSelectionChanged;
            }
        }

        /// <summary>
        /// Invoked when behavior is detached from a view.
        /// </summary>
        /// <param name="sampleView">The sample view from which the behavior is detached.</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.kanban != null)
            {
                this.kanban = null;
            }

            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged -= this.OnFlowDirectionComboBoxSelectionChanged;
                this.comboBox = null;
            }
        }

        #endregion

        #region Property Changed

        /// <summary>
        /// Occurs when the flow direction value is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnFlowDirectionComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (this.kanban == null || this.comboBox == null || this.comboBox.SelectedItem == null || sender is not SfComboBox)
            {
                return;
            }

            string selectedItem = this.comboBox.SelectedItem.ToString() ?? string.Empty;
            switch (selectedItem)
            {
                case "LeftToRight":
                    this.kanban.FlowDirection = FlowDirection.LeftToRight;
                    break;

                case "RightToLeft":
                    this.kanban.FlowDirection = FlowDirection.RightToLeft;
                    break;
            }
        }

        #endregion
    }
}