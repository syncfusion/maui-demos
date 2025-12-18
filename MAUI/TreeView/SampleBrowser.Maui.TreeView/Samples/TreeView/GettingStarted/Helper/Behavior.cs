#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.TreeView;
using Syncfusion.TreeView.Engine;
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
        private FileManagerViewModel? sortingFilteringViewModel;
        private SfEffectsViewAdv? effectsIcon;
        protected override void OnAttachedTo(SampleView bindable)
        {
            treeView = bindable.FindByName<Syncfusion.Maui.TreeView.SfTreeView?>("treeView");
            comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            if (comboBox != null)
            {
                comboBox.SelectionChanged += PositionChangedPicker_SelectionChanged;
            }

            sortingFilteringViewModel = bindable.FindByName<FileManagerViewModel>("fileMangerViewmodel");
            effectsIcon = bindable.FindByName<SfEffectsViewAdv>("iconEffects");
            if (effectsIcon != null) 
            {
                var SortImageTapped = new TapGestureRecognizer() { Command = new Command(SortImageTapped_Tapped) };
                effectsIcon.GestureRecognizers.Add(SortImageTapped);
                InitialSorting(); 
            }

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (comboBox != null)
            {
                comboBox.SelectionChanged -= PositionChangedPicker_SelectionChanged;
            }

            treeView = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void PositionChangedPicker_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.comboBox != null)
            {
                switch (this.comboBox.SelectedIndex)
                {
                    case 0:
                        ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition.Start);
                        break;
                    case 1:
                        ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition.End);
                        break;
                }
            }
        }

        private void ExpanderPositionChanged(Syncfusion.Maui.TreeView.TreeViewExpanderPosition expanderPosition)
        {
            if (this.treeView!.ExpanderPosition != expanderPosition)
            {
                this.treeView!.ExpanderPosition = expanderPosition;

            }
        }

        private void InitialSorting()
        {
            if (sortingFilteringViewModel != null)
            { 
                sortingFilteringViewModel.SortingOptions = TreeViewSortDirection.Ascending;
                this.SortItems(); 
            }
        }

        private void SortImageTapped_Tapped()
        {
            if (sortingFilteringViewModel != null)
            {
                if (sortingFilteringViewModel!.SortingOptions == TreeViewSortDirection.Descending)
                {
                    sortingFilteringViewModel.SortingOptions = TreeViewSortDirection.Ascending;
                }
                else if (sortingFilteringViewModel.SortingOptions == TreeViewSortDirection.Ascending)
                {
                    sortingFilteringViewModel.SortingOptions = TreeViewSortDirection.Descending;
                }
                
                this.SortItems();
            }
        }

        private void SortItems()
        {
            if (treeView == null || sortingFilteringViewModel == null)
            {
                return;
            }

            treeView.SortDescriptors.Clear();
            treeView.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "FolderName",
                Direction = sortingFilteringViewModel.SortingOptions 
            });
        }
    }
}
