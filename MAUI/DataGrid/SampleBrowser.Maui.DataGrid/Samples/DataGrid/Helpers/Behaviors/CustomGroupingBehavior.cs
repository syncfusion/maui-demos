#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid;

public class CustomGroupingBehavior : Behavior<SampleView>
{
	 private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble">dataGrid type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
        this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
        this.dataGrid.GroupCaptionTextFormat = "Total Sales : {Key} - {ItemsCount} Item(s)";
        this.dataGrid.GroupColumnDescriptions.Add(new GroupColumnDescription()
            {
                ColumnName = "Total",
                KeySelector = (string ColumnName, object o) =>
                {
                    var total = ((SalesInfo)o).Total;
                    if (total > 1000 && total < 5000)
                    {
                        return ">1 K and <5 K";
                    }
                    else if (total > 5000 && total < 10000)
                    {
                        return ">5 K and <10 K";
                    }
                    else if (total > 10000 && total < 50000)
                    {
                        return ">10 K and <50 K";
                    }
                    else if (total > 20000 && total < 50000)
                    {
                        return ">20 K and <50 K";
                    }
                    else if (total > 50000)
                    {
                        return ">50 K";
                    }
                    else
                    {
                        return "<1 K";
                    }
                }
            });
            base.OnAttachedTo(bindAble);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">DataGrid type of parameter bindAble</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            this.dataGrid = null;
            base.OnDetachingFrom(bindAble);
        }
}