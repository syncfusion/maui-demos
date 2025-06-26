#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;


namespace SampleBrowser.Maui.DataGrid.SfDataGrid
{
    public partial class Serialization : SampleView
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "DataGrid.xml");
        string resetPath = Path.Combine(FileSystem.AppDataDirectory, "ResetFile.xml");

        public Serialization()
        {
            InitializeComponent();
            using (var file = File.Create(resetPath))
            {
                dataGrid.Serialize(file);
            }
        }

        private void Serialize(object sender, EventArgs e)
        {
            var options = new DataGridSerializationOptions
            {
                SerializeGrouping = this.SerializeGrouping.IsChecked,
                SerializeSorting = this.SerializeSorting.IsChecked,
                SerializeGroupSummaries = this.SerializeGroupSummaries.IsChecked,
                SerializeTableSummaries = this.SerializeTableSummaries.IsChecked,
            };


            using (var file = File.Create(filePath))
            {
                dataGrid.Serialize(file, options);
            }
        }

        private void Deserialize(object sender, EventArgs e)
        {
            var options = new DataGridDeserializationOptions
            {
                DeserializeGrouping = this.DeserializeGrouping.IsChecked,
                DeserializeSorting = this.DeserializeSorting.IsChecked,
                DeserializeGroupSummaries = this.DeserializeGroupSummaries.IsChecked,
                DeserializeTableSummaries = this.DeserializeTableSummaries.IsChecked,
            };

            if (File.Exists(filePath))
            {
                using (var file = File.OpenRead(filePath))
                {
                    dataGrid.Deserialize(file, options);
                }
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            if (File.Exists(resetPath))
            {
                using (var file = File.OpenRead(resetPath))
                {
                    dataGrid.Deserialize(file);
                }
            }
        }

        private void AddorRemoveGroup(object sender, EventArgs e)
        {
            serializationPopupBehavior.ShowPopup();
        }
    }
}