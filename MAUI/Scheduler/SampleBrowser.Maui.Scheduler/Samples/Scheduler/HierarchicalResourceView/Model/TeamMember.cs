using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// Represents a member of the resources tree used by the Hierarchical Resource View demos.
    /// </summary>
    public class TeamMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMember"/> class.
        /// </summary>
        public TeamMember()
        {
            this.Name = string.Empty;
            this.Id = this.GetHashCode();
            this.Background = Brush.Transparent;
            this.Foreground = Brush.Transparent;
            this.ImageName = string.Empty;
            this.GroupId = null;
            this.IsNodeCollapsed = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the display name of the team member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the team member (matches a child resource's Id).
        /// </summary>
        public object? Id { get; set; }

        /// <summary>
        /// Gets or sets the parent resource's Id. Null indicates a root/parent resource.
        /// </summary>
        public object? GroupId { get; set; }

        /// <summary>
        /// Gets or sets whether this hierarchy node starts collapsed in the resource header.
        /// </summary>
        public bool IsNodeCollapsed { get; set; }

        /// <summary>
        /// Gets or sets the avatar image file name resolved through <c>SfImageSourceConverter</c>.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the header background brush.
        /// </summary>
        public Brush Background { get; set; }

        /// <summary>
        /// Gets or sets the header foreground brush.
        /// </summary>
        public Brush Foreground { get; set; }

        #endregion
    }
}