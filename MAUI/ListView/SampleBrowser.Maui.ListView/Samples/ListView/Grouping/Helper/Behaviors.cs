#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Syncfusion.Maui.DataSource;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    #region Behavior
    public class SfListViewGroupingBehavior : Behavior<Syncfusion.Maui.ListView.SfListView>
    {
        #region Fields

        private Syncfusion.Maui.ListView.SfListView ListView;

        #endregion

        #region Overrides
        protected override void OnAttachedTo(Syncfusion.Maui.ListView.SfListView bindable)
        {
            ListView = bindable;
            ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as ListViewContactsInfo);
                    return item.ContactName[0].ToString();
                },
            });
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Syncfusion.Maui.ListView.SfListView bindable)
        {
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion
    }
    #endregion
}
