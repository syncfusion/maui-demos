#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class FormFillingViewModel : INotifyPropertyChanged
    {
        private PdfFileData? fileData;
        public event PropertyChangedEventHandler? PropertyChanged;
       
        public Command OpenCommand { get; }
        public FormFillingViewModel()
        {
            string basePath = "SampleBrowser.Maui.Resources.Pdf";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf";
            Stream? stream = typeof(FormFillingViewModel).GetTypeInfo().Assembly.GetManifestResourceStream($"{basePath}.form_document.pdf");
            FileData = new PdfFileData("form_document.pdf", stream);
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
}
