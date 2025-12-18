#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using System.ComponentModel;
    using System.Reflection;
    using SampleBrowser.Maui.Base;
    using SampleBrowser.Maui.Base.Converters;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.ImageEditor;

    /// <summary>
    /// Represents the profile editor view with image editing functionality.
    /// </summary>
    public partial class ProfileEditor : SampleView
    {
        #region Fields

        /// <summary>
        /// The ImageEditor instance used for editing images.
        /// </summary>
        private SfImageEditor? imageEditor;

        /// <summary>
        /// Indicates whether the image has been removed.
        /// </summary>
        private bool isImageRemoved;

        /// <summary>
        /// Stores the original image in bytes.
        /// </summary>
        private byte[] originalBytes = Array.Empty<byte>();

        /// <summary>
        /// Indicates whether to load original image.
        /// </summary>
        private bool shouldReloadOriginalImage = true;

        /// <summary>
        /// The ChipGroup instance used to get chip items.
        /// </summary>
        private SfChipGroup? chipGroup;

        /// <summary>
        /// The remove chip item.
        /// </summary>
        private SfChip? removeChip;

        /// <summary>
        /// The reset chip item.
        /// </summary>
        private SfChip? resetChip;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileEditor"/> class.
        /// </summary>
        public ProfileEditor()
        {
            InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Invoked when the <see cref="ProfileEditor"/> is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnLoaded(object? sender, EventArgs e)
        {
            await this.UpdateImageForAvatarView();
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnEditBadgeClicked(object sender, EventArgs e)
        {
            this.popup.IsOpen = true;
        }

        /// <summary>
        /// Invoked when the close button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnCloseClicked(object sender, EventArgs e)
        {
            if (this.imageEditor == null)
            {
                return;
            }

            if (this.imageEditor.HasUnsavedEdits)
            {
                this.imageEditor.CancelEdits();
            }
            else
            {
                this.imageEditor.Reset();
            }

            this.popup.IsOpen = false;
        }

        /// <summary>
        /// Invoked when the popup is opening.
        /// </summary>
        /// <param name="sender">The popup object.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void OnPopupOpening(object sender, CancelEventArgs e)
        {
            if (this.imageEditor == null || this.editButton == null || this.removeChip == null || this.resetChip == null)
            {
                return;
            }

            this.editButton.IsVisible = false;

            if (this.shouldReloadOriginalImage)
            {
                this.ReloadOriginalImageAsync().ContinueWith(task => this.shouldReloadOriginalImage = false,
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                // To disable the remove and reset chips if the image is removed.
                if (this.isImageRemoved)
                {
                    this.removeChip.IsEnabled = false;
                    this.resetChip.IsEnabled = false;
                }
                else
                {
                    this.imageEditor.Crop(ImageCropType.Circle);
                }
            }
        }

        /// <summary>
        /// Invoked when the popup is closing.
        /// </summary>
        /// <param name="sender">The popup object.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void OnPopupClosing(object sender, CancelEventArgs e)
        {
            this.editButton.IsVisible = true;
        }

        /// <summary>
        /// Invoked when the chip items are clicked.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnChipClicked(object sender, EventArgs e)
        {
            if (sender is not SfChip chip)
            {
                return;
            }

            if (chip.Text == "Change")
            {
                this.UpdateChangeAction();
            }
            else if (chip.Text == "Remove")
            {
                this.UpdateRemoveAction();
            }
            else if (chip.Text == "Reset")
            {
                this.UpdateResetAction();
            }
            else if (chip.Text == "Save")
            {
                this.UpdateSaveAction();
            }
        }

        /// <summary>
        /// Invoked when the image loaded to the image editor.
        /// </summary>
        /// <param name="sender">The image editor object.</param>
        /// <param name="e">The event arguments.</param>
        private void OnImageEditorLoaded(object sender, EventArgs e)
        {
            if (sender is SfImageEditor editor)
            {
                this.imageEditor = editor;
                this.imageEditor.Crop(ImageCropType.Circle);
            }
        }

        /// <summary>
        /// Invoked when the chip group is loaded.
        /// </summary>
        /// <param name="sender">The chip group.</param>
        /// <param name="e">The event arguments.</param>
        private void OnChipGroupLoaded(object sender, EventArgs e)
        {
            if (sender is not SfChipGroup group || group.Items == null)
            {
                return;
            }

            this.chipGroup = group;
            foreach (var item in this.chipGroup.Items)
            {
                if (item.Text == "Remove")
                {
                    this.removeChip = item;
                }
                else if (item.Text == "Reset")
                {
                    this.resetChip = item;
                }
            }
        }

        /// <summary>
        /// Invoked when the image saving process begins.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The image saving event arguments.</param>
        private async void OnImageEditorSaving(object sender, ImageSavingEventArgs e)
        {
            e.Cancel = true;
            if (e.ImageStream == null)
            {
                return;
            }

            if (e.ImageStream.CanSeek)
            {
                e.ImageStream.Position = 0;
            }

            using (var memoryStream = new MemoryStream())
            {
                await e.ImageStream.CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();
                this.avatar.ImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            this.shouldReloadOriginalImage = true;
            this.popup.IsOpen = false;
        }

        /// <summary>
        /// Invoked when the image save picker is opened.
        /// </summary>
        /// <param name="sender">The image editor object.</param>
        /// <param name="e">The cancel event arguments.</param>
        private void OnImageEditorSavePickerOpening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to update the image in the editor by picking a new photo.
        /// </summary>
        private async void UpdateChangeAction()
        {
            var results = await MediaPicker.PickPhotosAsync(new MediaPickerOptions
            {
                Title = "Please select an image",
            });

            var result = results?.FirstOrDefault();
            if (this.imageEditor == null || result == null || this.removeChip == null || this.resetChip == null)
            {
                return;
            }

            using (var stream = await result.OpenReadAsync())
            {
                originalBytes = await GetMemoryStreamBytes(stream);
            }

            this.imageEditor?.Source = ImageSource.FromStream(() => new MemoryStream(originalBytes, writable: false));
            this.isImageRemoved = false;
            this.removeChip.IsEnabled = true;
            this.resetChip.IsEnabled = true;
            this.shouldReloadOriginalImage = true;
        }

        /// <summary>
        /// Method to remove the image in the editor.
        /// </summary>
        private void UpdateRemoveAction()
        {
            if (this.imageEditor == null)
            {
                return;
            }

            Assembly assembly = typeof(SfImageSourceConverter).GetTypeInfo().Assembly;
            var imagePath = "SampleBrowser.Maui.Base.Resources.Images.defaultprofileview.png";
            var imageSource = ImageSource.FromResource(imagePath, assembly);
            this.avatar.ImageSource = imageSource;
            this.imageEditor.Source = imageSource;

            this.isImageRemoved = true;
            this.shouldReloadOriginalImage = false;
            this.popup.IsOpen = false;
        }

        /// <summary>
        /// Method to reset the edits in the editor.
        /// </summary>
        private void UpdateResetAction()
        {
            if (this.imageEditor == null)
            {
                return;
            }

            if (this.imageEditor.HasUnsavedEdits)
            {
                this.imageEditor.CancelEdits();
            }
            else
            {
                this.imageEditor.Reset();
            }

            if (!this.isImageRemoved)
            {
                this.imageEditor.Crop(ImageCropType.Circle);
            }
        }

        /// <summary>
        /// Method to save the edited image in the editor.
        /// </summary>
        private async void UpdateSaveAction()
        {
            if (this.imageEditor == null)
            {
                return;
            }

            if (this.imageEditor.HasUnsavedEdits && !this.isImageRemoved)
            {
                this.imageEditor.SaveEdits();
                await Task.Delay(DeviceInfo.Platform == DevicePlatform.WinUI ? 500 : 200);
            }

            this.imageEditor.Save();
        }

        /// <summary>
        /// Method to reload the original image into the editor instance on Android to prevent blank rendering issues.
        /// </summary>
        private async Task ReloadOriginalImageAsync()
        {
            if (this.imageEditor == null)
            {
                return;
            }

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                this.imageEditor.Reset();
                await Task.Delay(200);
                
                if(!this.isImageRemoved)
                {
                    this.imageEditor.Crop(ImageCropType.Circle);
                }
            });
        }

        /// <summary>
        /// Method to convert image stream to byte array.
        /// </summary>
        /// <param name="stream">The stream of the image.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The stream array.</returns>
        private async Task<byte[]> GetMemoryStreamBytes(Stream stream, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Method to set image from avatar control.
        /// </summary>
        private async Task UpdateImageForAvatarView()
        {
            var imageSource = this.avatar?.ImageSource;
            if (imageSource == null)
            {
                return;
            }

            var imageBytes = await GetStreamImageBytesAsync(imageSource);
            if (imageBytes.Length > 0)
            {
                this.originalBytes = imageBytes;
            }
        }

        /// <summary>
        /// Method to get stream from byte array.
        /// </summary>
        /// <param name="imageSource">The image source.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The image stream bytes.</returns>
        private async Task<byte[]> GetStreamImageBytesAsync(ImageSource imageSource, CancellationToken cancellationToken = default)
        {
            if (imageSource is StreamImageSource streamImageSource)
            {
                using var stream = await streamImageSource.Stream(cancellationToken);
                if (stream == null)
                {
                    return Array.Empty<byte>();
                }

                return await GetMemoryStreamBytes(stream, cancellationToken);
            }

            return Array.Empty<byte>();
        }

        #endregion
    }
}