#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class SfListViewLoadMoreBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.ListView.SfListView? listView;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            listView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView?>("listView");
            comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += PositionChangedPicker_SelectionChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= PositionChangedPicker_SelectionChanged;
            listView = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void PositionChangedPicker_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
            {
                case 0:
                    LoadMoreOptionChanged(Syncfusion.Maui.ListView.LoadMoreOption.AutoOnScroll);
                    break;
                case 1:
                    LoadMoreOptionChanged(Syncfusion.Maui.ListView.LoadMoreOption.Manual);
                    break;
            }
        }

        private void LoadMoreOptionChanged(Syncfusion.Maui.ListView.LoadMoreOption loadMoreOption)
        {
            if (this.listView!.LoadMoreOption != loadMoreOption)
            {
                this.listView!.LoadMoreOption = loadMoreOption;
            }
        }
    }

}

