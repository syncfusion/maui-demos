namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.Core.Internals;
    using Syncfusion.Maui.Kanban;

    /// <summary>
    /// Represents a dialog edit desktop class.
    /// </summary>
    public partial class DialogEditingDesktop : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogEditingDesktop"/> class.
        /// </summary>
        public DialogEditingDesktop()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// This method is triggered when the Add New Card button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnAddNewCardButtonClicked(object? sender, EventArgs e)
        {
            this.ViewModel.OpenCreateDialog();
        }

        /// <summary>
        /// This method is triggered when the kanban card is clicked..
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

    internal class SfEffectsViewAdv : SfEffectsView, ITouchListener, IGestureListener
    {
        public SfEffectsViewAdv()
        {
        }

        public new void OnTouch(PointerEventArgs e)
        {
            if (e.Action == PointerActions.Entered)
            {
                this.ApplyEffects(SfEffects.Highlight, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
            else if (e.Action == PointerActions.Released)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Cancelled)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Exited)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Pressed)
            {
                this.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
        }

        internal void ForceRemoveEffects()
        {
            this.Reset();
        }
    }
}