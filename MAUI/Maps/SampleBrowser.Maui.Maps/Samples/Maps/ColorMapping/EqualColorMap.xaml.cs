#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class EqualColorMap : SampleView
{
	public EqualColorMap()
	{
		InitializeComponent();
        if (Base.BaseConfig.IsIndividualSB)
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.europe.json");
        }
        else
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.europe.json");
        }
    }
}
