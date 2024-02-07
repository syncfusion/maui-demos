#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SampleBrowser.Maui.DataGrid;

public class SalesInfoViewModel : INotifyPropertyChanged
{
    #region Field
    private ObservableCollection<SalesInfo>? dailySalesDetails;

    private ObservableCollection<object>? selectedItems;
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the SalesInfoViewModel class.
    /// </summary>
    public SalesInfoViewModel()
    {
        var salesInfoRepository = new SalesInfoRepository();
        this.dailySalesDetails = salesInfoRepository.GetSalesDetailsByDay(60);
        this.SelectedItems = new ObservableCollection<object>();
        this.SelectedItems.Add(this.DailySalesDetails![3]);
    }
    #endregion

    #region Event
    /// <summary>
    /// Event that is fired when the property in this class is changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region ItemsSource
    /// <summary>
    /// Gets or sets the value of DailySalesDetails
    /// </summary>
    public ObservableCollection<SalesInfo>? DailySalesDetails
    {
        get
        {
            return this.dailySalesDetails;
        }

        set
        {
            this.dailySalesDetails = value;
        }
    }

    /// <summary>
    /// Gets or sets the value of SelectedItems
    /// </summary>
    public ObservableCollection<object>? SelectedItems
    {
        get
        {
            return this.selectedItems;
        }

        set
        {
            this.selectedItems = value;
            this.RaisePropertyChnaged("SelectedItems");
        }
    }

    /// <summary>
    /// Method that is called when the property in this class is changed.
    /// </summary>
    /// <param name="name">Property name</param>
    public void RaisePropertyChnaged(string name)
    {
        if (this.PropertyChanged != null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    #endregion
}