#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.Chips.SfChip;

public partial class ChipGettingStartedMobile : SampleView
{
    public ChipGettingStartedMobile()
    {
        InitializeComponent();
    }
    private void SfChipGroup_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
    {
        if (sender != null && ((sender as SfChipGroup)?.BindingContext as ChipViewModel) != null)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!string.IsNullOrEmpty("SelectedAddOnItems"))
            {
                var viewModel = ((sender as SfChipGroup).BindingContext as ChipViewModel);
                if (viewModel.SelectedAddOnItems.Contains("Fast Charge"))
                {
                    viewModel.FastChargePrice = 399;
                }
                if (viewModel.SelectedAddOnItems.Contains("512 MB SD Card"))
                {
                    viewModel.SDCardPrice = 599;
                }
                if (viewModel.SelectedAddOnItems.Contains("2 Years Extended Warranty"))
                {
                    viewModel.WarrentyPrice = 799;
                }
                if (!viewModel.SelectedAddOnItems.Contains("Fast Charge"))
                {
                    viewModel.FastChargePrice = 0;
                }
                if (!viewModel.SelectedAddOnItems.Contains("512 MB SD Card"))
                {
                    viewModel.SDCardPrice = 0;
                }
                if (!viewModel.SelectedAddOnItems.Contains("2 Years Extended Warranty"))
                {
                    viewModel.WarrentyPrice = 0;
                }
                if (viewModel.SelectedAddOnItems.Contains("Fast Charge") || viewModel.SelectedAddOnItems.Contains("512 MB SD Card") || viewModel.SelectedAddOnItems.Contains("2 Years Extended Warranty"))
                {
                    viewModel.TotalAmount = viewModel.FastChargePrice + viewModel.SDCardPrice + viewModel.WarrentyPrice;
                    viewModel.TotalPrice = "$ " + viewModel.TotalAmount;
                }
                else
                {
                    viewModel.TotalAmount = viewModel.FinalPrice;
                    viewModel.TotalPrice = "$ " + viewModel.TotalAmount;
                }
            }
        ((sender as SfChipGroup).BindingContext as ChipViewModel).FinalAmount = ((sender as SfChipGroup).BindingContext as ChipViewModel).TotalAmount;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}