namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Kanban;

    /// <summary>
    /// Represents a sample Kanban board view with predefined workflows.
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted"/> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
            this.kanban.Workflows = new List<KanbanWorkflow>()
            {
                new KanbanWorkflow("Open", new List<object>() { "In Progress", "Closed", "Closed No Changes", "Won't Fix" }),
                new KanbanWorkflow("Postponed", new List<object>() { "Open", "In Progress", "Closed", "Closed No Changes", "Won't Fix" }),
                new KanbanWorkflow("Code Review", new List<object>() { "In Progress", "Closed", "Postponed" }),
                new KanbanWorkflow("In Progress", new List<object>() { "Code Review", "Postponed" }),
            };
        }
    }
}