#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.ImageEditor;
    using ImageEditor = Syncfusion.Maui.ImageEditor.SfImageEditor;

    /// <summary>
    /// Represents the behavior for image filter sample.
    /// </summary>
    internal class ImageFilterBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the image editor instance.
        /// </summary>
        private ImageEditor? imageEditor;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.imageEditor = sampleView.Content.FindByName<ImageEditor>("imageEditor");
        }
    }
}