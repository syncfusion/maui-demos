#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

#if __IOS__ || __MACCATALYST__
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui
{
    /// <summary>
    /// Represents a customized <see cref="Microsoft.Maui.Controls.SearchBarHandler"/> to handle SearchText is not updated for iOS and mac platforms.
    /// </summary>
    public class ListViewSearchBarHandler:SearchBarHandler
    {
        public ListViewSearchBarHandler()
        {
        }
        protected override void ConnectHandler(MauiSearchBar nativeView)
        {
            base.ConnectHandler(nativeView);

            if (nativeView != null)
            {
                nativeView!.TextChanged += SearchBar_TextChanged;
            }
        }

        /// <summary>
        /// Raised when SearchBar Text gets changed to set the SearchText for SearchBar.
        /// </summary>
        /// <param name="sender">Intsance of <see cref="UISearchBar"/>.</param>
        /// <param name="e">The event args.</param>
        private void SearchBar_TextChanged(object? sender, UIKit.UISearchBarTextChangedEventArgs e)
        {
            this.VirtualView.Text = e.SearchText;
        }

        protected override void DisconnectHandler(MauiSearchBar nativeView)
        {
            base.DisconnectHandler(nativeView);

            if (nativeView != null)
            {
                nativeView.TextChanged -= SearchBar_TextChanged;
            }
        }
    }

}
#endif
