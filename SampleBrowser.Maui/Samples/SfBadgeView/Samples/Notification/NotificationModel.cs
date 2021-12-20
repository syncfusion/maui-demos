using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleBrowser.Maui.SfBadgeView
{
    public class NotificationModel
    {
        /// <summary>
        /// Gets or sets the image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Time
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the Count for BadgeView
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }
    }
}
