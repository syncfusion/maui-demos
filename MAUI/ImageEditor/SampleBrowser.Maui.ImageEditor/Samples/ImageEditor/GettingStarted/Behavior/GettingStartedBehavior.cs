#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using ImageEditor = Syncfusion.Maui.ImageEditor.SfImageEditor;
    
    /// <summary>
    /// Represents the behavior for image editor getting started sample.
    /// </summary>
    internal class GettingStartedBehavior : Behavior<ImageEditor>
    {
        protected override void OnAttachedTo(ImageEditor imageEditor)
        {
            base.OnAttachedTo(imageEditor);
            if (imageEditor != null )
            {
                imageEditor.ImageSaving += this.OnImageEditorImageSaving;
            }
        }

        /// <summary>
        /// Invokes on image saving.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnImageEditorImageSaving(object? sender, Syncfusion.Maui.ImageEditor.ImageSavingEventArgs e)
        {
            using MemoryStream ms = new();
            e.ImageStream.CopyTo(ms);
            ms.Position = 0;
            ImageSaveService.SaveAndView("EditedImage.png", "application/image", ms);
            e.Cancel = true;
        }

        protected override void OnDetachingFrom(ImageEditor imageEditor)
        {
            imageEditor.ImageSaving -= this.OnImageEditorImageSaving;
            base.OnDetachingFrom(imageEditor);
        }
    }
}