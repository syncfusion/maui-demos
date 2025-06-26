#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.TreeView.Engine;

namespace SampleBrowser.Maui.TreeView.SfTreeView
{
    internal class LoadOnDemandDemoViewModel
    {
        public ObservableCollection<LoadOnDemandDemoModel> Menu { get; set; }

        public ICommand? TreeViewOnDemandCommand
        {
            get; set;
        }

        public LoadOnDemandDemoViewModel()
        {
            this.Menu = this.GetMenuItems();
            TreeViewOnDemandCommand = new Command(ExecuteOnDemandLoading, CanExecuteOnDemandLoading);
        }

        private bool CanExecuteOnDemandLoading(object sender)
        {
            var hasChildNodes = ((sender as TreeViewNode)!.Content as LoadOnDemandDemoModel)!.HasChildNodes;
            if (hasChildNodes)
                return true;
            else
                return false;
        }

        private void ExecuteOnDemandLoading(object obj)
        {
            var node = obj as TreeViewNode;
            // Skip the repeated population of child items when every time the node expands.
            if (node!.ChildNodes!.Count > 0)
            {
                node.IsExpanded = true;
                return;
            }

            node.ShowExpanderAnimation = true;
            //Animation starts for expander to show progressing of load on demand
            LoadOnDemandDemoModel? Info = node!.Content as LoadOnDemandDemoModel;
            Microsoft.Maui.Controls.Application.Current!.Dispatcher.Dispatch(async () =>
            {
                await Task.Delay(1000);
                //Fetching child items to add
                var items = GetSubMenu(Info!.ID);
                // Populating child items for the node in on-demand
                node.PopulateChildNodes(items);
                if (items.Any())
                    //Expand the node after child items are added.
                    node.IsExpanded = true;
                node.ShowExpanderAnimation = false;
            });
        }

        private ObservableCollection<LoadOnDemandDemoModel> GetMenuItems()
        {
            ObservableCollection<LoadOnDemandDemoModel> menuItems = new ObservableCollection<LoadOnDemandDemoModel>();
            menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "My Drive", HasChildNodes = true, ID = 0 });
            return menuItems;
        }

        public IEnumerable<LoadOnDemandDemoModel> GetSubMenu(int iD)
        {
            ObservableCollection<LoadOnDemandDemoModel> menuItems = new ObservableCollection<LoadOnDemandDemoModel>();
            if (iD == 0)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Documents", HasChildNodes = true, ID = 1 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Work", HasChildNodes = true, ID = 2 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Photos", HasChildNodes = true, ID = 3 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Music", HasChildNodes = true, ID = 4 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Videos", HasChildNodes = true, ID = 5 });
            }
            if (iD == 1)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Personal", HasChildNodes = true, ID = 11 });
            }
            else if (iD == 2)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "ProjectA", HasChildNodes = true, ID = 21 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "ProjectB", HasChildNodes = true, ID = 22 });
            }
            else if (iD == 3)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Family", HasChildNodes = true, ID = 31 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Friends", HasChildNodes = true, ID = 32 });
            }
            else if (iD == 4)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Playlist1", HasChildNodes = true, ID = 41 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Playlist2", HasChildNodes = true, ID = 42 });
            }
            else if (iD == 5)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Tutorial", HasChildNodes = true, ID = 51 });
            }
            else if (iD == 11)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Resume.docx", HasChildNodes = false, ID = 61 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "TravelPlans.pdf", HasChildNodes = false, ID = 62 });
            }
            else if (iD == 21)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Proposal.docx", HasChildNodes = false, ID = 71 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Presentation.pptx", HasChildNodes = false, ID = 72 });
            }
            else if (iD == 22)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Report.pdf", HasChildNodes = false, ID = 73 });
            }
            else if (iD == 31)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Beach.jpg", HasChildNodes = false, ID = 81 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Mountains.jpg", HasChildNodes = false, ID = 82 });
            }
            else if (iD == 32)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "GrouPhoto.jpg", HasChildNodes = false, ID = 91 });
            }
            else if (iD == 41)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Song1.mp3", HasChildNodes = false, ID = 101 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Song2.mp3", HasChildNodes = false, ID = 102 });
            }
            else if (iD == 42)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Song3.mp3", HasChildNodes = false, ID = 111 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Song4.mp3", HasChildNodes = false, ID = 112 });
            }
            else if (iD == 51)
            {
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Codingbasics.mp4", HasChildNodes = false, ID = 121 });
                menuItems.Add(new LoadOnDemandDemoModel() { ItemName = "Webdevelopment.mp4", HasChildNodes = false, ID = 122 });
            }
           
            return menuItems;
        }
    }
}
