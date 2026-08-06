using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public class SignatureViewmodel : INotifyPropertyChanged
{
    private PdfFileData? fileData;
    public event PropertyChangedEventHandler? PropertyChanged;

    public Command OpenCommand { get; }
    public SignatureViewmodel()
	{
        string basePath = "SampleBrowser.Maui.Resources.Pdf";
        if (BaseConfig.IsIndividualSB)
            basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf";
        Stream? stream = typeof(FormFillingViewModel).GetTypeInfo().Assembly.GetManifestResourceStream($"{basePath}.handwritten-signature.pdf");
        FileData = new PdfFileData("handwritten-signature.pdf", stream);
        OpenCommand = new Command(OnOpenCommand);
    }

    async void OnOpenCommand()
    {
        var fileData = await FileService.OpenFile("pdf");
        if (fileData != null)
        {
            FileData = fileData;
        }
    }

    public PdfFileData? FileData
    {
        get { return fileData; }
        set
        {
            if (fileData != value)
            {
                fileData = value;
                OnPropertyChanged(nameof(FileData));
            }
        }
    }

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}