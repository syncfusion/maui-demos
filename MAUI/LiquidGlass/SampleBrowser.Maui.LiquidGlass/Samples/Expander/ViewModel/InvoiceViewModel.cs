#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.LiquidGlass.SfExpander;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfExpander
{
    public class InvoiceViewModel
    {
        #region Fields

        string[] items = new string[]
        {
        "2018 Subaru Outback",
        "All-Weather Mats",
        "Door Edge Guard Kit",
        "Rear Bumper Cover",
        "Wheel Locks",
        "Gas Full Tank"
        };

        string[] prices = new string[]
        {
            "$35,705.00",
            "$101.00",
            "$162.00",
            "$107.00",
            "$81.00",
            "$64.00"
        };

        #endregion

        #region Constructor

        public InvoiceViewModel()
        {
            ItemInfo = new ObservableCollection<InvoiceItem>();
            for (int i = 0; i < items.Count(); i++)
            {
                var invoiceItem = new InvoiceItem();
                invoiceItem.ItemName = items[i];
                invoiceItem.ItemPrice = prices[i];
                ItemInfo.Add(invoiceItem);
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<InvoiceItem> ItemInfo { get; set; }

        #endregion
    }
}
