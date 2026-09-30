using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.DataGrid.Exporting;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Printing : SampleView
{
    private IPrintService? _printService;

    public Printing()
    {
        InitializeComponent();

        _printService = new PrintService();
    }

    private async void Button_Clicked(object? sender, EventArgs e)
    {   
        MemoryStream stream = new MemoryStream();
        DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
        DataGridPdfExportingOption option = new DataGridPdfExportingOption();
        var pdfDoc = pdfExport.ExportToPdf(this.dataGrid, option);
        pdfDoc.Save(stream);
        pdfDoc.Close(true);

        string fileName = "ExportFeature.pdf";
        string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        File.WriteAllBytes(filePath, stream.ToArray());

        if (_printService != null)
            await _printService.PrintAsync(filePath);
    }
}