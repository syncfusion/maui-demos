#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

#if __ANDROID__ || __IOS__ || __MACCATALYST__
using Syncfusion.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.SfListView
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
#endif
