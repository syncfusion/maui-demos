#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor;

using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ImageEditor;

public partial class ImageFilters : SampleView
{
    public ImageFilters()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Invokes on filter image editor instance loaded.
    /// </summary>
    /// <param name="sender">The image editor.</param>
    /// <param name="e">The event arguments.</param>
    private void ImageEditorImageLoaded(object sender, EventArgs e)
    {
        var imageEditor = sender as SfImageEditor;
        var filterModel = imageEditor?.BindingContext as ImageFilterModel;
        if (imageEditor == null || filterModel == null)
        {
            return;
        }

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

        filterModel.IsBusy = false;
    }
}