using Syncfusion.Maui.DataGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            if (countryName != null && viewModel!.ShipCity.ContainsKey(countryName!))
            {
                string[] shipcities = null!;
                viewModel.ShipCity.TryGetValue(countryName!, out shipcities!);
                return shipcities.ToList();
            }
            if (countryName == null)
            {
                orderinfo.ShipCity = null;
            }

            return null!;
        }
    }

    /// <summary>
    /// Converter to convert count values to boolean (count > 0 = true, count = 0 = false)
    /// </summary>
    public class CountToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count > 0;
            }
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter to convert count values to opacity (count = 0 = 0.4, count > 0 = 1.0)
    /// </summary>
    public class CountToOpacityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count > 0 ? 1.0 : 0.4;
            }
            return 1.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
