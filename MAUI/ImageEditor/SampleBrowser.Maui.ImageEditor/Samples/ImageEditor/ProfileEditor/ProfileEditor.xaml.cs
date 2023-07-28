#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor;

using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ImageEditor;

public partial class ProfileEditor : SampleView
{
    public ProfileEditor()
    {
        InitializeComponent();
        if (this.imageEditor != null)
        {
            this.imageEditor.ShowToolbar = false;
        }
    }

    private void OnOriginalCrop(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Crop(ImageCropType.Original);
        }
    }

    private void OnSquareCrop(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Crop(ImageCropType.Square);
        }
    }

    private void OnCircleCrop(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Crop(ImageCropType.Circle);
        }
    }

    private void OnEllipseCrop(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Crop(ImageCropType.Ellipse);
        }
    }

    private void OnCancelEdit(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.CancelEdits();
        }
    }

    private void OnSaveEdit(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.SaveEdits();
        }
    }

    private void OnReset(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Reset();
        }
    }

    private void OnSave(object sender, EventArgs e)
    {
        if (this.imageEditor != null)
        {
            this.imageEditor.Save();
        }
    }
}