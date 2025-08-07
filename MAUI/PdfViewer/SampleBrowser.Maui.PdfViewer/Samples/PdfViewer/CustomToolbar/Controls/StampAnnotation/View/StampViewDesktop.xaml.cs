#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Layouts;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StampViewDesktop : StampView
{
    public event EventHandler<StampDialogEventArgs?>? CreateStampClicked;
    HorizontalStackLayout? customStampMenuItem;

    internal void Initialize()
    {
        InitializeComponent();
        AssignControls();
        ContextMenu.ItemTapped += ContextMenu_ItemTapped;
        ContextMenu.WidthRequest = 176;
        ContextMenu.AddItem(CreateItem("Standard Stamps", "\uE706", 122, 40));
        customStampMenuItem = CreateItem("Custom Stamps", "\uE706", 120, 40);
        ContextMenu.AddItem(customStampMenuItem);
        customStampMenuItem.IsVisible = false;
        ContextMenu.AddSeparator();
        ContextMenu.AddItem(CreateItem("Create Stamps", "\uE70d", 120, 40));
        this.ParentChanged += StampViewDesktop_ParentChanged;
        AnnotationSecondaryToolbar.stampViewDesktop = this;
    }

    public void ShowCustomStamp()
    {
        if (CustomStampListLayout != null && customStampMenuItem != null && CustomStampListLayout.Count != 0)
        {
            customStampMenuItem.IsVisible = true;
        }
    }
    private void StampViewDesktop_ParentChanged(object? sender, EventArgs e)
    {
        if(this.Parent!=null)
        {
            this.Parent.PropertyChanged += Parent_PropertyChanged;
        }
    }

    private void Parent_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            if(this.Parent is Grid grid)
            {
                if (grid.IsVisible == false)
                {
                    if (StandardStampMenu != null && StandardStampMenu.IsVisible == true)
                    {
                        HideStandardStamps();
                    }
                    if (CustomStampMenu != null && CustomStampMenu.IsVisible == true)
                    {
                        HideCustomStamps();
                    }
                }
                else
                {
                    if(CustomStampListLayout!=null && customStampMenuItem!=null)
                    {
                        if (CustomStampListLayout.Count == 0)
                        {
                            if (customStampMenuItem.Children[0] is Label label && customStampMenuItem.Children[1] is Label icon)
                            {
                                customStampMenuItem.InputTransparent = true;
                                label.TextColor = Colors.Gray;
                                label.Opacity = 0.5f;
                                icon.TextColor = Colors.Gray;
                                icon.Opacity = 0.5f;
                            }
                        }
                        else
                        {
                            if (customStampMenuItem.Children[0] is Label label && customStampMenuItem.Children[1] is Label icon)
                            {
                                customStampMenuItem.InputTransparent = false;
                                label.TextColor = Colors.Black;
                                label.Opacity = 1;
                                icon.TextColor = Colors.Black;
                                icon.Opacity = 1;
                            }
                        }
                    }
                }
            }
        }
    }

    void AssignControls()
    {
        CustomStampListLayout = CustomStampsList;
    }

    private void StampMenu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            if (StandardStampMenu != null && StandardStampMenu.IsVisible == true)
            {
                HideStandardStamps();
            }
            if (CustomStampMenu != null && CustomStampMenu.IsVisible == true)
            {
                HideCustomStamps();
            }
        }
    }

    void HideStandardStamps()
    {
        StandardStampMenu.IsVisible = false;
    }

    void ShowStandardStamps()
    {
        StandardStampMenu.IsVisible = true;
        if (CustomStampMenu.IsVisible == true)
            HideCustomStamps();
    }

    void HideCustomStamps()
    {
        CustomStampMenu.IsVisible = false;
    }

    void ShowCustomStamps()
    {
        CustomStampMenu.IsVisible = true;
        if (StandardStampMenu.IsVisible == true)
            HideStandardStamps();
    }

    private void StandardStampMenu_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            StandardStampMenuItems.SelectedItem = null;
        }
    }

    private void Stamp_ItemTapped(object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
        if (StampHelper != null)
        {
            if (e.Item is StandardStamps standardStamp && standardStamp.LabelText != null)
            {
                stampType = GetStampType(standardStamp.LabelText);
                StampMode = true;
            }
        }
        if (Parent != null && Parent.BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.IsStampListVisible = false;
        }
        if (StandardStampMenu.IsVisible == true)
        {
            HideStandardStamps();
        }
        if(CustomStampMenu.IsVisible==true)
        {
            HideCustomStamps();
        }
        OnStampSelected();
    }

    HorizontalStackLayout CreateItem(string text, string icon, double textWidth, double iconWidth)
    {
        HorizontalStackLayout horizontalStackLayout = new HorizontalStackLayout();
        horizontalStackLayout.HeightRequest = 40;
        Label label1 = new Label()
        {
            Text = text,
            WidthRequest = textWidth,
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(16, 8, 0, 8),
            FontSize = 16
        };
        Label label2 = new Label()
        {
            Text = icon,
            WidthRequest = iconWidth,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 16,
            FontFamily = "MauiMaterialAssets"
        };
        horizontalStackLayout.Children.Add(label1);
        horizontalStackLayout.Children.Add(label2);
        return horizontalStackLayout;
    }

    private void ContextMenu_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (e.TappedItem is HorizontalStackLayout stack)
        {
            if (stack.Children[0] is Label label)
            {
                if (label.Text == "Standard Stamps")
                {
                    if (StandardStampMenu.IsVisible)
                    {
                        HideStandardStamps();
                    }
                    else
                    {
                        ShowStandardStamps();
                    }
                }
                else if (label.Text == "Custom Stamps")
                {
                    if (CustomStampListLayout != null && CustomStampListLayout!.Count != 0)
                    {
                        if (CustomStampMenu.IsVisible)
                            HideCustomStamps();
                        else
                            ShowCustomStamps();
                    }
                }
                else if (label.Text == "Create Stamps")
                {
                    StampDialogEventArgs stampDialogEventArgs = new StampDialogEventArgs(false);
                    CreateStampClicked?.Invoke(this, stampDialogEventArgs);
                }
            }
        }
    }
}

public class StampDialogEventArgs : EventArgs
{
    public bool? IsVisible { get; set; }
    public StampDialogEventArgs(bool isVisible)
    {
        this.IsVisible = isVisible;
    }
}

