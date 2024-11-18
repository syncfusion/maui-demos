#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public class MachineDataRepository
{
    private ObservableCollection<MachineData>? machineDataList;
    public ObservableCollection<MachineData>? MachineDataCollection
    {
        get { return machineDataList; }
        set
        {
            this.machineDataList = value;

        }
    }

    public MachineDataRepository()
    {
        this.GenerateOrders();
    }


    public void GenerateOrders()
    {
        string description = "The factors that supporting the Production rate is relevant to the count produced, hence the row data is marked as normal data.";
        machineDataList = new ObservableCollection<MachineData>();
        machineDataList.Add(new MachineData("M001", 85, 120, 220, 1500, 100, description));
        machineDataList.Add(new MachineData("M002", 788, 115, 230, 1520, 105, description));
        machineDataList.Add(new MachineData("M003", 90, 118, 225, 1480, 95, description));
        machineDataList.Add(new MachineData("M004", 87, 112, 228, 1515, 110, description));
        machineDataList.Add(new MachineData("M005", 92, 116, 222, 21457, 980, description));
        machineDataList.Add(new MachineData("M006", 85, 119, 220, 1490, 102, description));
        machineDataList.Add(new MachineData("M007", 88, 114, 230, 1500, 104, description));
        machineDataList.Add(new MachineData("M008", 90, 1120, 225, 1470, 89, description));
        machineDataList.Add(new MachineData("M009", 87, 121, 228, 1505, 108, description));
        machineDataList.Add(new MachineData("M0010", 92, 117, 222, 1480, 100, description));
        machineDataList.Add(new MachineData("M0011", 86, 118, 2221, 1925, 103, description));
        machineDataList.Add(new MachineData("M0012", 89, 116, 229, 1519, 107, description));
    }
}

public class GridReport
{
    public ObservableCollection<MachineData>? DataSource { get; set; }

}
