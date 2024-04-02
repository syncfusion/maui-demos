#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataSource.Extensions;

namespace SampleBrowser.Maui.ListView.SfListView
{
    #region CustomGroupComparer

    /// <summary>
    /// Comparer class helps to sort the groups based on Data property.
    /// </summary>
    public class CustomGroupComparer : IComparer<GroupResult>
    {
        public int Compare(GroupResult? x, GroupResult? y)
        {
            var xenum = (GroupName)x!.Key;
            var yenum = (GroupName)y!.Key;

            if (xenum.CompareTo(yenum) == -1)
            {
                return -1;
            }
            else if (xenum.CompareTo(yenum) == 1)
            {
                return 1;
            }

            return 0;
        }
    }

    #endregion
}
