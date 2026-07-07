namespace SampleBrowser.Maui.LiquidGlass.SfKanban
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents a page that displays and manages a Kanban board within the application.
    /// </summary>
    public partial class KanbanPage : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanPage"/> class.
        /// </summary>
        public KanbanPage()
        {
            InitializeComponent();
        }

#if IOS || MACCATALYST

        /// <summary>
        /// Overrides <see cref="OnAppearing"/> to apply platform-specific resource overrides for Kanban control.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current?.Resources is not ResourceDictionary resources)
            {
                return;
            }

            if (resources == null || resources.MergedDictionaries.Contains(resources))
            {
                return;
            }

            var kanbanResource = new ResourceDictionary
            {
                { "SfKanbanPlaceholderBackgroundColor", Colors.Transparent },
                { "SfKanbanPlaceholderSelectedBackgroundColor", Colors.Transparent },
                { "SfKanbanHeaderBackgroundColor", Colors.Transparent },
                { "SfKanbanColumnBackgroundColor", Colors.Transparent },
                { "SfKanbanCardBackgroundColor", Colors.Transparent },
                { "SfKanbanHeaderCollapsedBackgroundColor", Colors.Transparent },
                { "SfKanbanCardTagBackgroundColor", Colors.Transparent },
            };

            resources.MergedDictionaries.Add(kanbanResource);
        }
#endif
    }
}