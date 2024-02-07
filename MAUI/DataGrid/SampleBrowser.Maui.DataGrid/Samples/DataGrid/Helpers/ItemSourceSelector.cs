#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class ItemSourceSelector : IItemsSourceSelector
    {
        public IEnumerable GetItemsSource(object record, object dataContext)
        {
            if (record == null)
            {
                return null!;
            }

            var orderinfo = record as DealerInfo;
            var countryName = orderinfo!.ShipCountry;
            var viewModel = dataContext as DealerInfoViewModel;

            // Returns ShipCity collection based on ShipCountry.
            if (viewModel!.ShipCity.ContainsKey(countryName!))
            {
                string[] shipcities = null!;
                viewModel.ShipCity.TryGetValue(countryName!, out shipcities!);
                return shipcities.ToList();
            }

            return null!;
        }
    }
}
