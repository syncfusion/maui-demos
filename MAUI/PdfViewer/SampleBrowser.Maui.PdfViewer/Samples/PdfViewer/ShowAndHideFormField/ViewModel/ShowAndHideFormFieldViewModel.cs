#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.PdfViewer.Samples.PdfViewer.ShowAndHideFormField.View;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class ShowAndHideFormFieldViewModel : INotifyPropertyChanged
    {
        private Stream? _documentStream;
        Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer;
        public event PropertyChangedEventHandler? PropertyChanged;
        internal MemoryStream memoryStream = new MemoryStream();
        internal Assembly assembly = typeof(InvisibleSignature).GetTypeInfo().Assembly;
        private bool isCompleteSigningEnable = false;
        private bool isEnableSave;
        internal string basePath;
        private bool isListVisible = false;
        private bool isEnableProfile = true;
        private float profileGridOpacity = 1;
        private ObservableCollection<ListItem> listItems;
        private ListItem? selectedItem;

        public Command CompleteSigningCommand { get; }
        public Command OpenCommand { get; }

        /// <summary>
        /// Gets or sets the PDF document as a stream. 
        /// </summary>
        public Stream? DocumentStream
        {
            get => _documentStream;
            set
            {
                _documentStream = value;
                OnPropertyChanged("DocumentStream");
            }
        }

        /// <summary>
        /// Gets or sets the Enable the save button
        /// </summary>
        public bool IsEnableSave
        {
            get => isEnableSave;
            set
            {
                isEnableSave = value;
                OnPropertyChanged("IsEnableSave");
            }
        }

        public bool IsEnableProfile
        {
            get => isEnableProfile;
            set
            {
                isEnableProfile = value;
                OnPropertyChanged("IsEnableProfile");
            }
        }
        public bool IsListVisible
        {
            get => isListVisible;
            set
            {
                isListVisible = value;
                OnPropertyChanged("IsListVisible");
            }
        }

        public float ProfileGridOpacity
        {
            get => profileGridOpacity;
            set
            {
                profileGridOpacity = value;
                OnPropertyChanged("ProfileGridOpacity");
            }
        }

        public ObservableCollection<ListItem> ListItems
        {
            get => listItems;
            set
            {
                listItems = value;
                OnPropertyChanged("ListItems");
            }
        }

        public ListItem? SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;

                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(EllipseColor));
                OnPropertyChanged("SelectedItem");
            }
        }


        /// <summary>
        /// Gets or sets the button visibility
        /// </summary>
        public bool IsCompleteSigningEnable
        {
            get => isCompleteSigningEnable;
            set
            {
                isCompleteSigningEnable = value;
                OnPropertyChanged("IsCompleteSigningEnable");
            }
        }
        
        public string Name => selectedItem?.Name ?? string.Empty;
        public string Email => selectedItem?.Email ?? string.Empty;
        public Color EllipseColor => selectedItem?.EllipseColor ?? Colors.Transparent;

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public ShowAndHideFormFieldViewModel(Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer)
        {
            string fileName = "eSign_filling.pdf";
            basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            DocumentStream = this.GetType().Assembly.GetManifestResourceStream(basePath + fileName);
            this.pdfViewer = pdfViewer;
            CompleteSigningCommand = new Command(OnCompleteSigningCommand);
            OpenCommand = new Command(OnOpenCommand);
            listItems = new ObservableCollection<ListItem>
            {
                new ListItem("Andrew Fuller", "andrew@mycompany.com", "dropdown_icon.png", Colors.Red, true),
                new ListItem("Anne Dodsworth", "anne@mycompany.com", "dropdown_icon.png", Colors.Green, false),
            };
            selectedItem = listItems.FirstOrDefault();
        }



        async void OnOpenCommand()
        {
            var fileData = await FileService.OpenFile("pdf");
            if (fileData != null)
            {
                DocumentStream = fileData.Stream;
            }
        }

        private void OnCompleteSigningCommand()
        {
            foreach(var form in pdfViewer.FormFields)
            {
                form.ReadOnly = false;
                form.FlattenOnSave = true;
                if(form is TextFormField text)
                {
                    foreach (var widget in text.Widgets)
                    {
                        widget.BackgroundColor = Colors.White;
                        widget.BorderColor = Colors.White;
                    }
                }
            }
            pdfViewer.SaveDocument(this.memoryStream);
            pdfViewer.LoadDocument(this.memoryStream);
            foreach (var form in pdfViewer.FormFields)
            {
               form.ReadOnly = true;
            }
            IsEnableSave = true;
            IsEnableProfile = false;
            ProfileGridOpacity = 0.5f;

        }

        internal async void SaveDocument()
        {
            this.memoryStream.Position = 0;
            string fileName = "SaveDocument.pdf";
            try
            {
                if (fileName != null)
                {
                    string? filePath = await FileService.SaveAsAsync(fileName, this.memoryStream);
                    await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
                    IsEnableSave=false;

                }
            }
            catch (Exception exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
            }
        }

        internal async void ShowDialog(string title, string message)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert(title, message, "OK");
        }


        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}