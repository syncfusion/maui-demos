#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Dispatching;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Selection : SampleView
{
    public Selection()
	{
        InitializeComponent();
        if (Base.BaseConfig.IsIndividualSB)
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.usa_state.shp");
        }
        else
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.usa_state.shp");
        }

        sampleGrid.Remove(popup);
    }

    private void shapeLayer_ShapeSelected(object sender, ShapeSelectedEventArgs e)
    {
        this.ClearPopup();
        if (e.IsSelected && e.DataItem is PopulationDensityDetails selectedItem)
        {
            stateName.Text = selectedItem.State;
            rankTitle.Text = "Density rank";
            rank.Text = " : " + selectedItem.Rank.ToString();
            kmTitle.Text = "Density per miles";
            kilometer.Text = " : " + selectedItem.SquareMiles.ToString();
            sampleGrid.Add(popup, row: 2, column: 1);
        }
    }

    private void ClearPopup()
    {
        stateName.Text = "";
        kmTitle.Text = "";
        kilometer.Text = "";
        rankTitle.Text = "";
        rank.Text = "";
        sampleGrid.Remove(popup);
    }
}