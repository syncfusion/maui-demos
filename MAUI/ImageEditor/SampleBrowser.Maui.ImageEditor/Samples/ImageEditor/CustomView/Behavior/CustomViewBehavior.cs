#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor;

using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Base.Converters;
using Syncfusion.Maui.ImageEditor;
using System.Reflection;
using ImageEditor = Syncfusion.Maui.ImageEditor.SfImageEditor;

public class CustomViewBehavior : Behavior<SampleView>
{
    /// <summary>
    /// Holds the image editor instance.
    /// </summary>
    private ImageEditor? imageEditor;

    /// <summary>
    /// Holds the annotation delet button instance.
    /// </summary>
    private Border? deleteButton;

    /// <summary>
    /// Holds the image names.
    /// </summary>
    private Dictionary<string, string> ImageNames;

    /// <summary>
    /// Checks whether the replace icon is clicked.
    /// </summary>
    private bool IsReplace;

    /// <summary>
    /// Holds the icons.
    /// </summary>
    private Label? Replace, BringFront, SendBack;

    /// <summary>
    /// Initializes the instance for <see cref="CustomViewBehavior"/> class.
    /// </summary>
    public CustomViewBehavior()
    {
        this.ImageNames = new Dictionary<string, string>
        {
            { "Image1", "imageeditorsticker1.png" },
            { "Image2", "imageeditorsticker2.png" },
            { "Image3", "imageeditorsticker3.png" },
            { "Image4", "imageeditorsticker4.png" },
            { "Image5", "imageeditorsticker5.png" },
            { "Image6", "imageeditorsticker6.png" },
            { "Image7", "imageeditorsticker7.png" },
            { "Image8", "imageeditorsticker8.png" },
            { "Image9", "imageeditorsticker9.png" },
            { "Image10", "imageeditorsticker10.png" }
        };
    }

    /// <inheritdoc/>
    protected override void OnAttachedTo(SampleView bindable)
    {
        base.OnAttachedTo(bindable);
        this.imageEditor = bindable.Content.FindByName<ImageEditor>("imageEditor");
        this.deleteButton = bindable.Content.FindByName<Border>("deleteButton");
        if (this.imageEditor != null)
        {
            ImageEditorToolbar topToolbar = this.CreateTopToolbar();
            this.imageEditor.Toolbars.Add(topToolbar);
            ImageEditorToolbar bottomToolbar = CreateBottomToolbar();
            this.imageEditor.Toolbars.Add(bottomToolbar);

            this.imageEditor.ToolbarItemSelected += this.OnImageEditorToolbarItemSelected;
            this.imageEditor.AnnotationSelected += OnImageEditorAnnotationSelected;
            this.imageEditor.AnnotationUnselected += this.OnImageEditorAnnotationUnselected;
            this.imageEditor.SizeChanged += OnImageEditorSizeChanged;
        }

        if (this.deleteButton != null)
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += this.OnDeleteIconTapped;
            this.deleteButton.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }

    private ImageEditorToolbar CreateTopToolbar()
    {
        ImageEditorToolbar topToolbar = new ImageEditorToolbar();
        topToolbar.Position = ToolbarPosition.Start;
        ImageEditorToolbarItem reset = new ImageEditorToolbarItem()
        {
            Name = "Reset",
            IsEnabled = false,
            View = this.CreateToolbarIcon("\ue746", "Maui Material Assets", true),
        };

        ImageEditorToolbarItem undo = new ImageEditorToolbarItem()
        {
            Name = "Undo",
            IsEnabled = false,
            View = this.CreateToolbarIcon("\ue744", "Maui Material Assets", true),
        };

        ImageEditorToolbarItem redo = new ImageEditorToolbarItem()
        {
            Name = "Redo",
            IsEnabled = false,
            View = this.CreateToolbarIcon("\ue745", "Maui Material Assets", true),
        };

        ImageEditorToolbarItem save = new ImageEditorToolbarItem()
        {
            Name = "Save",
            View = this.CreateToolbarIcon("\ue75f", "Maui Material Assets", true),
        };

        topToolbar.ToolbarItems.Add(reset);
        topToolbar.ToolbarItems.Add(undo);
        topToolbar.ToolbarItems.Add(redo);
        topToolbar.ToolbarItems.Add(save);
        return topToolbar;
    }

    private ImageEditorToolbar CreateBottomToolbar()
    {
        var bottomToolbar = new ImageEditorToolbar();
        bottomToolbar.Position = ToolbarPosition.End;

        ImageEditorToolbarItem add = new ImageEditorToolbarItem()
        {
            Name = "Add",
            View = this.CreateToolbarIcon("\ue70d", "Maui Material Assets"),
            SubToolbars = GetImagesToolbar(),
        };

        this.Replace = this.CreateToolbarIcon("\ue726", "Maui Material Assets");
        this.Replace.IsEnabled = false;
        this.BringFront = this.CreateToolbarIcon("\ue764", "ImageEditorIcon");
        this.BringFront.IsEnabled = false;
        this.SendBack = this.CreateToolbarIcon("\ue70e", "ImageEditorIcon");
        this.SendBack.IsEnabled = false;
        ImageEditorToolbarItem replace = new ImageEditorToolbarItem()
        {
            Name = "Replace",
            IsEnabled = false,
            View = this.Replace,
            SubToolbars = GetImagesToolbar(),
        };

        ImageEditorToolbarItem bringFront = new ImageEditorToolbarItem()
        {
            Name = "Bring Front",
            IsEnabled = false,
            View = this.BringFront,
        };

        ImageEditorToolbarItem SendBack = new ImageEditorToolbarItem()
        {
            Name = "Send Backward",
            IsEnabled = false,
            View = this.SendBack
        };

        bottomToolbar.ToolbarItems.Add(add);
        bottomToolbar.ToolbarItems.Add(replace);
        bottomToolbar.ToolbarItems.Add(bringFront);
        bottomToolbar.ToolbarItems.Add(SendBack);
        return bottomToolbar;
    }

    private void OnImageEditorToolbarItemSelected(object? sender, ToolbarItemSelectedEventArgs e)
    {
        if (e.ToolbarItem == null || this.imageEditor == null)
        {
            return;
        }

        if (e.ToolbarItem.Name is "Image1" or "Image2" or "Image3" or "Image4" or "Image5" or "Image6" or "Image7" or "Image8" or "Image9" or "Image10")
        {
            if (this.IsReplace)
            {
                this.imageEditor.DeleteAnnotation();
            }

            this.AddCustomView(e.ToolbarItem.Name);
            return;
        }
        else if (e.ToolbarItem.Name == "Replace")
        {
            this.IsReplace = true;
            if (this.deleteButton != null)
            {
                this.deleteButton.IsVisible = false;
            }

            return;
        }
        else if (e.ToolbarItem.Name == "Bring Front")
        {
            this.imageEditor.BringForward();
        }
        else if (e.ToolbarItem.Name == "Send Backward")
        {
            this.imageEditor.SendBackward();
        }

        this.IsReplace = false;
        if (this.deleteButton != null)
        {
            this.deleteButton.IsVisible = false;
        }
    }

    private void OnImageEditorAnnotationSelected(object? sender, AnnotationSelectedEventArgs e)
    {
        if (this.deleteButton == null || this.imageEditor == null)
        {
            return;
        }

        AbsoluteLayout.SetLayoutBounds(this.deleteButton, new Rect((this.imageEditor.Width / 2) - 25, this.imageEditor.Height - 130, 50, 50));
        this.SetIconsEnableState(true);
    }

    private void SetIconsEnableState(bool isEnable)
    {
        if (this.deleteButton != null)
        {
            this.deleteButton.IsVisible = isEnable;
        }

        if (this.Replace != null)
        {
            this.Replace.IsEnabled = isEnable;
        }

        if (this.BringFront != null)
        {
            this.BringFront.IsEnabled = isEnable;
        }

        if (this.SendBack != null)
        {
            this.SendBack.IsEnabled = isEnable;
        }
    }

    private void OnImageEditorAnnotationUnselected(object? sender, AnnotationUnselectedEventArgs e)
    {
        if (this.deleteButton == null || this.imageEditor == null)
        {
            return;
        }

        this.SetIconsEnableState(false);
    }

    private void OnImageEditorSizeChanged(object? sender, EventArgs e)
    {
        if (this.deleteButton == null || this.imageEditor == null)
        {
            return;
        }

        AbsoluteLayout.SetLayoutBounds(this.deleteButton, new Rect((this.imageEditor.Width / 2) - 25, this.imageEditor.Height - 120, 50, 50));
    }

    private void OnDeleteIconTapped(object? sender, TappedEventArgs e)
    {
        if (this.deleteButton == null || this.imageEditor == null)
        {
            return;
        }

        this.imageEditor.DeleteAnnotation();
        this.deleteButton.IsVisible = false;
    }

    private Label CreateToolbarIcon(string glyph, string fontFamily, bool isTopToolbar = false)
    {
        double height = isTopToolbar ? 20 : 22;
        var label = new Label()
        {
            Text = glyph,
            FontFamily = fontFamily,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = height,
            HeightRequest = height,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            WidthRequest = 50,
        };

        label.SetDynamicResource(Label.TextColorProperty, "SfImageEditorNormalToolbarIconColor");
        this.WireImagePropertyChange(label);
        return label;
    }

    private List<ImageEditorToolbar> GetImagesToolbar()
    {
        var toolbarItems = new List<IImageEditorToolbarItem>();
        toolbarItems.Add(new ImageEditorToolbarItem() { Name = "Back" });
        toolbarItems.Add(new ImageEditorToolbarItem() { Name = "Separator", IsEnabled = false });
        foreach (var image in this.ImageNames)
        {
            toolbarItems.Add(new ImageEditorToolbarItem() { Name = image.Key, View = this.GetStickerImage(image.Value), ShowTooltip = false });
        }

        var toolbars = new List<ImageEditorToolbar>();
        toolbars.Add(new ImageEditorToolbar() { ToolbarItems = toolbarItems, Size = 80 });
        return toolbars;
    }

    private void AddCustomView(string imageName)
    {
        string sourceName = this.ImageNames[imageName];
        if (this.imageEditor == null)
        {
            return;
        }

        Image image = new Image();
        image.Source = sourceName;
        image.WidthRequest = 100;
        image.HeightRequest = 100;
        image.Aspect = Aspect.Fill;
        this.imageEditor.AddCustomAnnotationView(image);
    }

    private Image GetStickerImage(string source)
    {
        Image image = new Image();
        image.WidthRequest = 70;
        image.Margin = 5;
        image.Source = source;
        return image;
    }

    private void WireImagePropertyChange(Label image)
    {
        image.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
            {
                double opacity = image.IsEnabled ? 1.0 : 0.5;
                image.Opacity = opacity;
            }
        };
    }

    /// <inheritdoc/>
    protected override void OnDetachingFrom(SampleView bindable)
    {
        base.OnDetachingFrom(bindable);
        if (this.imageEditor != null)
        {
            this.imageEditor.ToolbarItemSelected -= this.OnImageEditorToolbarItemSelected;
            this.imageEditor.AnnotationSelected -= OnImageEditorAnnotationSelected;
            this.imageEditor.AnnotationUnselected -= this.OnImageEditorAnnotationUnselected;
            this.imageEditor.SizeChanged -= OnImageEditorSizeChanged;
            this.imageEditor = null;
        }

        if (this.deleteButton != null)
        {
            this.deleteButton = null;
        }
    }
}