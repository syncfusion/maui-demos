#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class InvisibleSignatureViewModel : INotifyPropertyChanged
    {
        private Stream? _documentStream;
        internal string keyFileName = "certificate.pfx";
        Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer;
        public event PropertyChangedEventHandler? PropertyChanged;
        public MemoryStream memoryStream = new MemoryStream();
        public MemoryStream signatureValidatedStream = new MemoryStream();
        internal Assembly assembly = typeof(InvisibleSignature).GetTypeInfo().Assembly;
        private bool isSuccessMsgVisible;
        private string validationMessage = "";
        private bool isInvalidMsgVisible;
        private bool isErrorMsgVisible;
        private bool isCompleteSigningEnable = false;
        private bool isEnableSave;
        internal string basePath;

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
        /// Gets or sets the validation message
        /// </summary>
        public string ValidationMessage
        {
            get => validationMessage;
            set
            {
                validationMessage = value;
                OnPropertyChanged("ValidationMessage");
            }
        }

        /// <summary>
        /// Gets or sets the grid visibility
        /// </summary>
        public bool IsSuccessMsgVisible
        {
            get => isSuccessMsgVisible;
            set
            {
                isSuccessMsgVisible = value;
                OnPropertyChanged("IsSuccessMsgVisible");
            }
        }

        /// <summary>
        /// Gets or sets the grid visibility
        /// </summary>
        public bool IsInvalidMsgVisible
        {
            get => isInvalidMsgVisible;
            set
            {
                isInvalidMsgVisible = value;
                OnPropertyChanged("IsInvalidMsgVisible");
            }
        }

        /// <summary>
        /// Gets or sets the grid visibility
        /// </summary>
        public bool IsErrorMsgVisible
        {
            get => isErrorMsgVisible;
            set
            {
                isErrorMsgVisible = value;
                OnPropertyChanged("IsErrorMsgVisible");
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

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public InvisibleSignatureViewModel(Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer)
        {
            string fileName = "InvisibleDigitalSignature.pdf";
            basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            DocumentStream = this.GetType().Assembly.GetManifestResourceStream(basePath + fileName);
            this.pdfViewer = pdfViewer;
            CompleteSigningCommand = new Command(OnCompleteSigningCommand);
            OpenCommand = new Command(OnOpenCommand);
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
            pdfViewer.SaveDocument(this.memoryStream);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(this.memoryStream);
            PdfPageBase loadedPage = loadedDocument.Pages[0];
            Stream? pfxFile = this.GetType().Assembly.GetManifestResourceStream(this.basePath + this.keyFileName );
            PdfCertificate pdfCertificate = new PdfCertificate(pfxFile , "password123");
            PdfSignature signature = new PdfSignature(loadedDocument, loadedPage, pdfCertificate, "Signature1");
            signatureValidatedStream = new MemoryStream();
            loadedDocument.Save(this.signatureValidatedStream);
            pdfViewer.LoadDocument(signatureValidatedStream);           

        }

        internal async void SaveDocument()
        {
            signatureValidatedStream.Position = 0;
            string fileName = "SaveDocument.pdf";
            try
            {
                if (fileName != null)
                {
                    string? filePath = await FileService.SaveAsAsync(fileName, signatureValidatedStream);
                    await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
                }
            }
            catch (Exception exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
            }
        }

        internal void ValidatedSignature(MemoryStream str) 
        {
            if (str.Length > 0)
            {
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(str);
                PdfLoadedForm form = loadedDocument.Form;
                if (form != null)
                {
                    foreach (PdfLoadedField field in form.Fields)
                    {
                        if (field is PdfLoadedSignatureField)
                        {
                            //Gets the first signature field of the PDF document.
                            PdfLoadedSignatureField? signatureField = field as PdfLoadedSignatureField;
                            if (signatureField!.IsSigned)
                            {
                                Stream? pfxFile = assembly.GetManifestResourceStream(basePath + keyFileName);
                                byte[] data = new byte[pfxFile!.Length];
                                pfxFile?.Read(data, 0, data.Length);
                                X509Certificate2Collection collection = new X509Certificate2Collection();
#if NET9_0
                                X509Certificate2 certificate = X509CertificateLoader.LoadPkcs12(data, "password123");
#else
                                X509Certificate2 certificate = new X509Certificate2(data, "password123");
#endif
                                collection.Add(certificate);
                                PdfSignatureValidationResult result = signatureField.ValidateSignature(collection);
                                if (result.IsDocumentModified)
                                {
                                    IsErrorMsgVisible = true;
                                    IsSuccessMsgVisible = false;
                                    IsInvalidMsgVisible = false;
                                    ValidationMessage = "The document has been digitally signed, but it has been modified since it was signed and at least one signature is invalid .";
                                    IsEnableSave = false;
                                }
                                else
                                {
                                    //Checks whether the signature is valid or not.
                                    if (result.IsSignatureValid)
                                    {
                                        if (result.SignatureStatus.ToString() == "Unknown")
                                        {
                                            IsErrorMsgVisible = false;
                                            IsSuccessMsgVisible = false;
                                            IsInvalidMsgVisible = true;
                                            ValidationMessage = "The document has been digitally signed and at least one signature has problem";
                                            IsEnableSave = false;
                                        }
                                        else
                                        {
                                            IsErrorMsgVisible = false;
                                            IsInvalidMsgVisible = false;
                                            ValidationMessage = "The document has been digitally signed and all the signatures are valid.";
                                            IsSuccessMsgVisible = true;
                                            IsEnableSave = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}