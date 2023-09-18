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
    using Syncfusion.Maui.ListView;
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

        /// <summary>
        /// Holds the image filter collection view instance.
        /// </summary>
        private SfListView? ImageFilters;

        /// <summary>
        /// Holds whether the filter ToolBar is open or not.
        /// </summary>
        private bool isSaveIconEnabled;

        /// <summary>
        /// Holds whether the crop ToolBar is open or not.
        /// </summary>
        private bool isCropEnabled;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.imageEditor = sampleView.Content.FindByName<ImageEditor>("imageEditor");
            this.ImageFilters = sampleView.Content.FindByName<SfListView>("filtersList");

            if (this.imageEditor != null)
            {
                this.imageEditor.ToolbarItemSelected += this.OnImageEditorToolbarItemSelected;
            }

            if (this.ImageFilters != null)
            {
                this.ImageFilters.SelectionChanged += this.OnImageFiltersSelectionChanged;
                if (this.imageEditor != null && this.ImageFilters != null && this.imageEditor != null && this.ImageFilters.ItemsSource is List<ImageFilterModel> imageFilters)
                {
                    foreach (var item in imageFilters)
                    {
                        item.ImageSource = this.imageEditor.Source;
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.imageEditor != null)
            {
                this.imageEditor.ImageLoaded -= this.OnImageEditorLoaded;
                this.imageEditor.ToolbarItemSelected -= this.OnImageEditorToolbarItemSelected;
                this.imageEditor = null;
            }

            if (this.ImageFilters != null)
            {
                this.ImageFilters.SelectionChanged -= this.OnImageFiltersSelectionChanged;
                this.ImageFilters = null;
            }
        }

        /// <summary>
        /// Invokes on image editor image loaded.
        /// </summary>
        /// <param name="sender">The image editor.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnImageEditorLoaded(object? sender, EventArgs e)
        {
            await this.SetFilterImages();
        }

        /// <summary>
        /// Invokes on ToolBar item selection.
        /// </summary>
        /// <param name="sender">The image editor.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnImageEditorToolbarItemSelected(object? sender, ToolbarItemSelectedEventArgs e)
        {
            if (e.ToolbarItem == null || this.imageEditor == null)
            {
                return;
            }

            if (e.ToolbarItem.Name == "SaveEdit")
            {
                this.imageEditor.SaveEdits();
                this.isCropEnabled = false;
                this.isSaveIconEnabled = false;
                this.SetSaveEditIconVisibility(false);
                await this.SetFilterImages();
            }
            else if (e.ToolbarItem.Name == "Effects")
            {
                this.ImageFilters?.SelectedItems?.Clear();
                if (this.isCropEnabled)
                {
                    this.imageEditor.CancelEdits();
                    this.isCropEnabled = false;
                    this.isSaveIconEnabled = true;
                }
                else if (this.isSaveIconEnabled)
                {
                    this.imageEditor.CancelEdits();
                    this.isSaveIconEnabled = false;
                }
                else
                {
                    this.isSaveIconEnabled = true;
                }

                this.SetSaveEditIconVisibility(this.isSaveIconEnabled);
            }
            else if (e.ToolbarItem.Name == "Crop")
            {
                if (this.isCropEnabled)
                {
                    this.imageEditor.CancelEdits();
                    this.isCropEnabled = false;
                    this.isSaveIconEnabled = false;
                }
                else
                {
                    if (this.isSaveIconEnabled)
                    {
                        this.imageEditor.ImageEffect(ImageEffect.None);
                    }

                    this.imageEditor.Crop(ImageCropType.Free);
                    this.isCropEnabled = true;
                    this.isSaveIconEnabled = true;
                }

                this.SetSaveEditIconVisibility(this.isSaveIconEnabled);
            }
            else if (e.ToolbarItem.Name is not "Filters")
            {
                if (this.isCropEnabled || isSaveIconEnabled)
                {
                    this.isCropEnabled = false;
                    this.isSaveIconEnabled = false;
                    this.imageEditor.ImageEffect(ImageEffect.None);
                    this.imageEditor.CancelEdits();
                    this.SetSaveEditIconVisibility(this.isSaveIconEnabled);
                }

                await this.SetFilterImages();
            }
            else if (e.ToolbarItem.Name == "Browse")
            {
                this.WireImageLoadedEvent();
            }
        }

        /// <summary>
        /// Method wires image loaded event.
        /// </summary>
        private void WireImageLoadedEvent()
        {
            if (this.imageEditor != null)
            {
                this.imageEditor.ImageLoaded -= this.OnImageEditorLoaded;
                this.imageEditor.ImageLoaded += this.OnImageEditorLoaded;
            }
        }

        /// <summary>
        /// On image filter selection.
        /// </summary>
        /// <param name="sender">The collection view.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnImageFiltersSelectionChanged(object? sender, ItemSelectionChangedEventArgs? e)
        {
            ImageFilterModel? filterModel = e?.AddedItems?.FirstOrDefault() as ImageFilterModel;
            if (filterModel == null || this.imageEditor == null)
            {
                return;
            }

            this.imageEditor.CancelEdits();
#if IOS
            await Task.Delay(200);
#else
            await Task.Delay(0);
#endif
            if (filterModel.Effect == "Brightness")
            {
                imageEditor.ImageEffect(ImageEffect.Brightness, -0.2);
            }
            else if (filterModel.Effect == "Hue")
            {
                imageEditor.ImageEffect(ImageEffect.Hue, 1);
            }
            else if (filterModel.Effect == "Grayscale")
            {
                imageEditor.ImageEffect(ImageEffect.Saturation, -1);
            }
            else if (filterModel.Effect == "Blur")
            {
                imageEditor.ImageEffect(ImageEffect.Blur, 0.5);
            }
            else if (filterModel.Effect == "Contrast")
            {
                imageEditor.ImageEffect(ImageEffect.Contrast, 1);
            }
            else if (filterModel.Effect == "Exposure")
            {
                imageEditor.ImageEffect(ImageEffect.Exposure, 0.3);
            }
            else if (filterModel.Effect == "Sharpen")
            {
                imageEditor.ImageEffect(ImageEffect.Sharpen, 1);
            }
            else if (filterModel.Effect == "Opacity")
            {
                imageEditor.ImageEffect(ImageEffect.Opacity, 0.5);
            }
        }

        /// <summary>
        /// Method to set save edit icon visibility.
        /// </summary>
        /// <param name="saveIconVisibility">True if save edit is visible.</param>
        private void SetSaveEditIconVisibility(bool saveIconVisibility)
        {
            if (this.imageEditor == null)
            {
                return;
            }

            var imageEditorToolbar = this.imageEditor.Toolbars.FirstOrDefault();
            if (imageEditorToolbar == null)
            {
                return;
            }

            var groupItems = imageEditorToolbar.ToolbarItems.OfType<ImageEditorToolbarGroupItem>();
            foreach (var groupItem in groupItems)
            {
                foreach (var toolbarItem in groupItem.Items)
                {
                    if (toolbarItem.Name == "SaveEdit")
                    {
                        toolbarItem.IsVisible = saveIconVisibility;
                    }
                    else
                    {
                        toolbarItem.IsVisible = !saveIconVisibility;
                    }
                }
            }
        }

        /// <summary>
        /// Method to filter image sources.
        /// </summary>
        /// <returns>The task status.</returns>
        private async Task SetFilterImages()
        {
            if (this.ImageFilters != null && this.imageEditor != null && this.ImageFilters.ItemsSource is List<ImageFilterModel> imageFilters)
            {
                await Task.Delay(500);
                var imageSource = await this.imageEditor.GetImageStream();
                if (imageSource == null)
                {
                    return;
                }

                imageSource.Position = 0;
                var imageBytes = this.GetBytesFromStream(imageSource);
                foreach (var item in imageFilters)
                {
                    item.IsBusy = true;
                    item.ImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
            }
        }

        /// <summary>
        /// Converts the stream into bytes.
        /// </summary>
        /// <param name="stream">The image stream.</param>
        /// <returns>The image bytes.</returns>
        private byte[] GetBytesFromStream(Stream stream)
        {
            byte[] bytes, buffer = new byte[32];
            List<byte> totalStream = new List<byte>();
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                totalStream.AddRange(buffer.Take(read));
            }

            bytes = totalStream.ToArray();
            return bytes;
        }
    }
}