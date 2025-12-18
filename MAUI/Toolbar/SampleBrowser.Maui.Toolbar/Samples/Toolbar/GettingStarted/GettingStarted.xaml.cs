#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Toolbar.SfToolbar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.ImageEditor;
    using Syncfusion.Maui.Toolbar;
    using System.ComponentModel;

    public partial class GettingStarted : SampleView
    {
        private int undoCount = 0;
        private int redoCount = 0;

        public GettingStarted()
        {
            InitializeComponent();
        }

        private void HorizontalToolbar_Tapped(object sender, ToolbarTappedEventArgs e)
        {
            var horizontalToolbarItem = e.NewToolbarItem as SfToolbarItem;
            if (horizontalToolbarItem != null)
            {
                switch (horizontalToolbarItem.Name)
                {
                    case "Browse":
                        this.LoadImageFromGallery();
                        break;
                    case "Undo":
                        this.PerformUndo();
                        break;
                    case "Redo":
                        this.PerformRedo();
                        break;
                    case "Save":
                        this.editor.Save();
                        break;
                }
            }
        }

        private void VerticalToolbar_Tapped(object sender, ToolbarTappedEventArgs e)
        {
            var verticalToolbarItem = e.NewToolbarItem as SfToolbarItem;
            if (verticalToolbarItem != null)
            {
                switch (verticalToolbarItem.Name)
                {
                    case "CustomCrop":
                        this.editor.Crop(ImageCropType.Free);
                        break;
                    case "OriginalCrop":
                        this.editor.Crop(ImageCropType.Original);
                        break;
                    case "CircleCrop":
                        this.editor.Crop(ImageCropType.Circle);
                        break;
                    case "EllipseCrop":
                        this.editor.Crop(ImageCropType.Ellipse);
                        break;
                    case "SquareCrop":
                        this.editor.Crop(ImageCropType.Square);
                        break;
                    case "3:2Crop":
                        this.editor.Crop(3, 2);
                        break;
                    case "4:3Crop":
                        this.editor.Crop(4, 3);
                        break;
                    case "5:4Crop":
                        this.editor.Crop(5, 4);
                        break;
                    case "7:5Crop":
                        this.editor.Crop(7, 5);
                        break;
                    case "16:9Crop":
                        this.editor.Crop(16, 9);
                        break;
                }
            }

            this.HorizontalToolbar.IsVisible = false;
            this.SaveCancelEditToolbar.IsVisible = true;
        }

        private async void SaveCancelEditToolbar_Tapped(object sender, ToolbarTappedEventArgs e)
        {
            var saveCancelToolbarItem = e.NewToolbarItem as SfToolbarItem;
            if (saveCancelToolbarItem != null)
            {
                if (saveCancelToolbarItem.Name == "SaveEdit")
                {
                    this.PerformSaveEdits();
                }
                else
                {
                    this.VerticalToolbar.ClearSelection();
                    this.editor.CancelEdits();
                }

                this.SaveCancelEditToolbar.IsVisible = false;
                this.HorizontalToolbar.IsVisible = true;
                await Task.Delay(500);
                this.SaveCancelEditToolbar.ClearSelection();
            }
        }

        private async void LoadImageFromGallery()
        {
#if ANDROID
            var result = await MediaPicker.PickPhotosAsync(new MediaPickerOptions() { SelectionLimit = 1 });
#else
            var result = await MediaPicker.PickPhotosAsync();
#endif
            {
                if (result == null)
                {
                    this.HorizontalToolbar.ClearSelection();
                    return;
                }

                if (result.Count != 0)
                {
                    this.editor.Source = ImageSource.FromStream(() => result[result.Count - 1].OpenReadAsync().Result);
                }
                this.HorizontalToolbar.ClearSelection();
            }
        }

        private void PerformUndo()
        {
            if (this.undo.IsEnabled && undoCount > 0)
            {
                this.editor.Undo();
                undoCount--;
                redoCount++;

                if (redoCount == 1)
                {
                    this.redo.IsEnabled = true;
                }

                if (undoCount == 0)
                {
                    this.undo.IsEnabled = false;
                }
            }
        }

        private void PerformRedo()
        {
            if (this.redo.IsEnabled && redoCount > 0)
            {
                this.editor.Redo();
                redoCount--;
                undoCount++;

                if (undoCount == 1)
                {
                    this.undo.IsEnabled = true;
                }

                if (redoCount == 0)
                {
                    this.redo.IsEnabled = false;
                }
            }
        }

        private void PerformSaveEdits()
        {
            this.VerticalToolbar.ClearSelection();
            this.editor.SaveEdits();
            undoCount++;
            redoCount = 0;

            if (undoCount == 1)
            {
                this.undo.IsEnabled = true;
                this.redo.IsEnabled = false;
            }
        }

        private async void editor_SavePickerOpening(object sender, CancelEventArgs e)
        {
            await Task.Delay(1000);
            this.HorizontalToolbar.ClearSelection();
        }
    }
}

