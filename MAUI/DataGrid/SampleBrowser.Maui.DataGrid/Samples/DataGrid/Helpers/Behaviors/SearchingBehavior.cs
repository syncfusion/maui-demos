#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid
{
    public class SearchingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private Syncfusion.Maui.Inputs.SfMaskedEntry? filterText;
        private Label? findNext;
        private Label? findPrevious;
        private Label? clear;
        private Syncfusion.Maui.Buttons.SfSwitch? allowFilterSwitch;
        private Syncfusion.Maui.Buttons.SfSwitch? allowCaseSensitiveSwitch;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble"></param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.findNext = bindAble.FindByName<Label>("findNext");
            this.findPrevious = bindAble.FindByName<Label>("findPrevious");
            this.clear = bindAble.FindByName<Label>("clear");
            this.filterText = bindAble.FindByName<Syncfusion.Maui.Inputs.SfMaskedEntry>("filterText");
            this.allowFilterSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("allowFiltering");
            this.allowCaseSensitiveSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("allowCaseSensitive");

            if(this.filterText.Children[0] is Entry entry)
            {
                entry.TextChanged += entry_TextChanged;
            }
            this.allowFilterSwitch.StateChanged += AllowFilter_StateChanged;
            this.allowCaseSensitiveSwitch.StateChanged += AllowMatchCase_StateChanged;
            base.OnAttachedTo(bindAble);
        }

        private void entry_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (this.dataGrid != null)
            {
                this.dataGrid.SearchController.Search(e.NewTextValue);
                if (e.NewTextValue == string.Empty)
                {
                    this.dataGrid.Refresh();
                }
            }
            bool isTextEntered = !string.IsNullOrWhiteSpace(e.NewTextValue);

            if(this.findNext != null)
            {
                this.findNext.IsEnabled = isTextEntered;
                findNext.Opacity = isTextEntered ? 1 : 0;
            }
            
            if(this.findPrevious != null)
            {
                this.findPrevious.IsEnabled = isTextEntered;
                findPrevious.Opacity=isTextEntered ? 1 : 0; 
            }
            if(this.clear != null)
            {
                this.clear.IsEnabled=isTextEntered;
                clear.Opacity=isTextEntered ? 1 : 0;
            }
        }

        private void AllowMatchCase_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (this.dataGrid != null && e.NewValue != null)
            {
                this.dataGrid.SearchController.AllowCaseSensitive = (bool)e.NewValue;
            }
        }

        private void AllowFilter_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (this.dataGrid != null && e.NewValue != null)
            {
                this.dataGrid.SearchController.AllowFiltering = (bool)e.NewValue;
            }
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble parameter</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            if (this.allowFilterSwitch != null)
            {
                this.allowFilterSwitch.StateChanged -= AllowFilter_StateChanged;
            }

            if (this.allowCaseSensitiveSwitch != null)
            {
                this.allowCaseSensitiveSwitch.StateChanged -= AllowMatchCase_StateChanged;
            }

            this.dataGrid = null;
            this.allowFilterSwitch = null;
            this.allowCaseSensitiveSwitch = null;

            base.OnDetachingFrom(bindAble);
        }
    }
}
