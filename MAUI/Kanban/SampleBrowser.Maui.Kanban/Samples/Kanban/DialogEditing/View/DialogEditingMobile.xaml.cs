namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Kanban;

    /// <summary>
    /// Represents a dialog edit mobile class.
    /// </summary>
    public partial class DialogEditingMobile : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogEditingMobile"/> class.
        /// </summary>
        public DialogEditingMobile()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// This method triggered when the add new card button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnAddNewCardButtonClicked(object? sender, EventArgs e)
        {
            this.ViewModel.OpenCreateDialog();
        }

        /// <summary>
        /// This method triggered when the kanban card tapped.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The kanban tapped events args.</param>
        private void OnKanbanCardTapped(object? sender, KanbanCardTappedEventArgs e)
        {
            if (e.Data is KanbanModel tappedCard)
            {
                this.ViewModel.OpenEditDialog(tappedCard);
            }
        }
    }
}