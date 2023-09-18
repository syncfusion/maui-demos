#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.TreeView
{
    public class SfTreeViewGettingStartedBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.TreeView.SfTreeView? treeView;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;


        protected override void OnAttachedTo(SampleView bindable)
        {
            treeView = bindable.FindByName<Syncfusion.Maui.TreeView.SfTreeView?>("treeView");
            comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += PositionChangedPicker_SelectionChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= PositionChangedPicker_SelectionChanged;
            treeView = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void PositionChangedPicker_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            switch (this.comboBox!.SelectedIndex) 
            {
                case 0:
                    ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition.Start);
                    break;
                case 1:
                    ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition.End);
                    break;
            }

        }

        private void ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition expanderPosition)
        {
            if (this.treeView!.ExpanderPosition != expanderPosition)
            {
                this.treeView!.ExpanderPosition = expanderPosition;

            }
        }
    }
}
