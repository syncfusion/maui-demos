#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.DataGrid.SfDataGrid;
using Syncfusion.Maui.DataGrid.DataPager;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.DataGrid
{
    /// <summary>
    ///  Base generic class for generalized user-defined behaviors that can respond to arbitrary conditions and events.
    ///  Type parameters:T: The type of the objects with which this Forms.Behavior`1 can be associated in the Paging samples
    /// </summary>
    public class PagingBehavior : Behavior<SampleView>
    {
        private SfDataPager? datapager;
        private Microsoft.Maui.Controls.Picker? selectionPicker;
        private SampleView? samplePage;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.samplePage = bindAble;
            this.datapager = bindAble.FindByName<SfDataPager>("dataPager");
            this.selectionPicker = bindAble.FindByName<Microsoft.Maui.Controls.Picker>("SelectionPicker");
            if (this.selectionPicker != null)
            {
                this.selectionPicker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            }
            base.OnAttachedTo(bindAble);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">A sampleView type of bindAble</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            if (this.selectionPicker != null)
            {
                this.selectionPicker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
            }

            this.datapager = null;
            base.OnDetachingFrom(bindAble);
        }

        /// <summary>
        /// Triggers while selected Index was changed, used to set a button shape.
        /// </summary>
        /// <param name="sender">OnSelectionChanged event sender</param>
        /// <param name="e">EventArgs e</param>
        private void Picker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.selectionPicker!.SelectedIndex == 0)
            {
                this.datapager!.ButtonShape = DataPagerButtonShape.Circle;
            }
            else if (this.selectionPicker.SelectedIndex == 1)
            {
                this.datapager!.ButtonShape = DataPagerButtonShape.Rectangle;
            }
        }
    }
}
