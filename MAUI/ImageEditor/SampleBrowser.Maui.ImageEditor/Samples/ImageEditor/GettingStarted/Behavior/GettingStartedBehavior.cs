#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
#if WINDOWS
    using Windows.UI.Popups;
#endif
    using ImageEditor = Syncfusion.Maui.ImageEditor.SfImageEditor;

    /// <summary>
    /// Represents the behavior for image editor getting started sample.
    /// </summary>
    internal class GettingStartedBehavior : Behavior<ImageEditor>
    {
        /// <summary>
        /// Method to handle the image loaded event.
        /// </summary>
        /// <param name="imageEditor">The image editor.</param>
        protected override void OnAttachedTo(ImageEditor imageEditor)
        {
            base.OnAttachedTo(imageEditor);
            if (imageEditor != null)
            {
#if WINDOWS
                imageEditor.ImageSaved += this.OnImageEditorImageSaved;
#endif
            }
        }

#if WINDOWS
        /// <summary>
        /// Method to handle the image saved event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnImageEditorImageSaved(object? sender, Syncfusion.Maui.ImageEditor.ImageSavedEventArgs e)
        {
            //Gets process windows handle to open the dialog in application process. 
            IntPtr windowHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            MessageDialog msgDialog = new("Do you want to view the image?", "Image has been saved successfully");
            UICommand yesCmd = new("Yes");
            msgDialog.Commands.Add(yesCmd);
            UICommand noCmd = new("No");
            msgDialog.Commands.Add(noCmd);

            WinRT.Interop.InitializeWithWindow.Initialize(msgDialog, windowHandle);

            //Showing a dialog box. 
            IUICommand cmd = await msgDialog.ShowAsync();
            if (cmd.Label == yesCmd.Label)
            {

                string folderPath = e.Location; // Replace this with the actual folder path

                Process.Start(new ProcessStartInfo
                {
                    FileName = folderPath,
                    UseShellExecute = true
                });
            }
        }

        /// <summary>
        /// Method to handle the image editor unloaded event.
        /// </summary>
        /// <param name="imageEditor">The image editor</param>
        protected override void OnDetachingFrom(ImageEditor imageEditor)
        {
            imageEditor.ImageSaved -= this.OnImageEditorImageSaved;
            base.OnDetachingFrom(imageEditor);
        }
#endif
    }
}