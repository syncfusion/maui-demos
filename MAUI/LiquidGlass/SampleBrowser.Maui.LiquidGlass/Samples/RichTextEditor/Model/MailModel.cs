#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base.Converters;
using System.Reflection;

namespace SampleBrowser.Maui.LiquidGlass
{
    /// <summary>
    /// Model for storing email address and profile image.
    /// </summary>
    public class MailModel
    {
        /// <summary>
        /// Gets or sets the mail ID.
        /// </summary>
        public string MailId { get; set; }

        /// <summary>
        /// Gets or sets the profile image source.
        /// </summary>
        public ImageSource ProfileImage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailModel"/> class.
        /// </summary>
        /// <param name="mailId">The mail ID.</param>
        /// <param name="profileImage">The profile image name.</param>
        public MailModel(string mailId, string profileImage)
        {
            MailId = mailId;
            var resourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;
            ProfileImage = ImageSource.FromResource($"SampleBrowser.Maui.Base.Resources.Images.{profileImage}", resourceAssembly);
        }
    }
}
