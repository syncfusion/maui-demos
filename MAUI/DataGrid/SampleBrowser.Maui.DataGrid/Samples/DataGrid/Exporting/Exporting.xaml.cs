#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.DataGrid.Exporting;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;
public partial class Exporting : SampleView
{
	public Exporting()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        #region Pdf Document
        MemoryStream stream = new MemoryStream();
        DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
        DataGridPdfExportingOption option = new DataGridPdfExportingOption();
        var pdfDoc = pdfExport.ExportToPdf(this.dataGrid, option);
        pdfDoc.Save(stream);
        pdfDoc.Close(true);
        SaveService saveService = new();
        saveService.SaveAndView("ExportFeature.pdf", "application/pdf", stream);
        #endregion
    }



    private void Button_Clicked_1(object sender, EventArgs e)
    {
        #region Excel Document
        DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
        DataGridExcelExportingOption option = new DataGridExcelExportingOption();
        var excelEngine = excelExport.ExportToExcel(this.dataGrid, option);
        var workbook = excelEngine.Excel.Workbooks[0];
        MemoryStream stream = new MemoryStream();
        workbook.SaveAs(stream);
        workbook.Close();
        excelEngine.Dispose();
        string OutputFilename = "ExportFeature.xlsx";
        SaveService saveService = new();
        saveService.SaveAndView(OutputFilename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
        #endregion
    }
}