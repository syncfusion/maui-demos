#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ListView;
using SelectionMode = Syncfusion.Maui.ListView.SelectionMode;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    #region SelectionBehavior

    public class SfListViewSelectionBehavior : Behavior<SampleView>
    {
        #region Fields

        private Syncfusion.Maui.ListView.SfListView ListView;
        private Grid selectAllIconParent;
        private ListViewSelectionViewModel SelectionViewModel;
        private Grid headerGrid;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(SampleView bindable)
        {
            ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            ListView.SelectionChanged += ListView_SelectionChanged;

            SelectionViewModel = new ListViewSelectionViewModel();
            ListView.BindingContext = SelectionViewModel;
            ListView.ItemsSource = SelectionViewModel.DocumentInfo;

            headerGrid = bindable.FindByName<Grid>("headerGrid");
            headerGrid.BindingContext = SelectionViewModel;

            selectAllIconParent = bindable.FindByName<Grid>("selectAllIconParent");
            var selectAllIconTapped = new TapGestureRecognizer() { Command = new Command(selectAllIconTapped_Tapped) };
            selectAllIconParent.GestureRecognizers.Add(selectAllIconTapped);
            base.OnAttachedTo(bindable);

        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            ListView.SelectionChanged -= ListView_SelectionChanged;
            ListView = null;
            selectAllIconParent = null;
            SelectionViewModel = null;
            headerGrid = null;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Events
        private void ListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            if (ListView.SelectionMode == SelectionMode.Multiple)
            {
                SelectionViewModel.HeaderInfo = ListView.SelectedItems.Count + " Documents Selected";
                for (int i = 0; i < e.AddedItems.Count; i++)
                {
                    var item = e.AddedItems[i];
                    (item as DocumentInfo).IsSelected = true;
                }
                for (int i = 0; i < e.RemovedItems.Count; i++)
                {
                    var item = e.RemovedItems[i];
                    (item as DocumentInfo).IsSelected = false;
                }

                if (ListView.SelectedItems.Count == SelectionViewModel.DocumentInfo.Count)
                    SelectionViewModel.IsAllSelected = true;
                else if (ListView.SelectedItems.Count == 0)
                {
                    SelectionViewModel.IsAllSelected = false;
                }
                else
                {
                    SelectionViewModel.IsAllSelected = null;
                }
            }
        }


        private void selectAllIconTapped_Tapped()
        {
            if (ListView.SelectedItems.Count == SelectionViewModel.DocumentInfo.Count)
            {
                this.ListView.SelectedItems.Clear();
            }
            else if (ListView.SelectedItems.Count == 0)
            {
                this.ListView.SelectAll();
            }
            else
            {
                this.ListView.SelectedItems.Clear();
            }
            UpdateSelectionTempate(null, true);
        }

        #endregion

        #region Methods
        public void UpdateSelectionTempate(ItemLongPressEventArgs args = null, bool tappedSelectAll = false)
        {
            if (ListView.SelectedItems.Count > 0 || args != null)
            {
                ListView.SelectionMode = SelectionMode.Multiple;
                if (!tappedSelectAll)
                    ListView.SelectedItems.Clear();
                if (args != null)
                {
                    var currentItem = args.DataItem as DocumentInfo;
                    if (currentItem != null)
                    {
                        currentItem.IsSelected = true;
                        this.ListView.SelectedItems.Add(currentItem);
                    }
                }
                SelectionViewModel.HeaderInfo = ListView.SelectedItems.Count + " Documents Selected";

                if (tappedSelectAll)
                {
                    if (ListView.SelectedItems.Count == SelectionViewModel.DocumentInfo.Count)
                    {
                        for (int i = 0; i < SelectionViewModel.DocumentInfo.Count; i++)
                        {
                            var item = SelectionViewModel.DocumentInfo[i];
                            (item as DocumentInfo).IsSelected = true;
                        }

                        SelectionViewModel.IsAllSelected = true;
                    }
                }

            }
            else
            {
                SelectionViewModel.HeaderInfo = "0 Documents Selected";
                for (int i = 0; i < SelectionViewModel.DocumentInfo.Count; i++)
                {
                    var item = SelectionViewModel.DocumentInfo[i];
                    (item as DocumentInfo).IsSelected = false;
                }
                SelectionViewModel.IsAllSelected = false;
            }
        }

        #endregion

    }
    #endregion
}