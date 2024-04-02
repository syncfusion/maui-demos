#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Maui.TabView;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StampViewMobile : StampView
{
    public event EventHandler<StampDialogMobileEventArgs?>? CreateStampMobileClicked;

    internal void Initialize()
    {
        InitializeComponent();
        AssignControls();
        tabView.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object? sender, TabSelectionChangedEventArgs e)
    {
        if (e.OldIndex != e.NewIndex)
        {
            if (sender is View view && view.Parent is Border border)
            {
                if (StandardStamps.IsSelected)
                {
                    StandardStamps.TextColor = Color.FromArgb("#FF6750A4");
                    CustomStamps.TextColor = Color.FromArgb("#49454F");
                }
                else if (CustomStamps.IsSelected)
                {
                    StandardStamps.TextColor = Color.FromArgb("#49454F");
                    CustomStamps.TextColor = Color.FromArgb("#FF6750A4");
                }

            }
        }
    }

    void AssignControls()
    {
        CustomStampListLayout = CustomStampsList;
    }

    private void ListView_Tapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (StampHelper != null)
        {
            if (e.DataItem is StandardStamps standardStamp && standardStamp.LabelText != null)
            {
                stampType = GetStampType(standardStamp.LabelText);
                StampMode = true;
            }
        }
        if (Parent != null && Parent.BindingContext is CustomToolbarViewModel viewModel)
            viewModel.IsStampViewVisible = false;
        OnStampSelected();
    }

    public void CreateStampMobile_Clicked(object sender, EventArgs e)
    {
        StampDialogMobileEventArgs stampDialogMobileEventArgs = new StampDialogMobileEventArgs(false);
        CreateStampMobileClicked?.Invoke(this, stampDialogMobileEventArgs);
    }
}

public class StampDialogMobileEventArgs : EventArgs
{
    public bool? IsVisible { get; set; }
    public StampDialogMobileEventArgs(bool isVisible)
    {
        this.IsVisible = isVisible;
    }
}