#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class SmartFillViewModel : INotifyPropertyChanged
    {
        public string UserDetail1 { get; set; }
        public string UserDetail2 { get; set; }
        public string UserDetail3 { get; set; }
        // Backing field for the PdfFile property
        private Stream? _pdfFile;
        // Property to hold the PDF file stream
        public Stream? PdfFile
        {
            get { return _pdfFile; }
            set
            {
                _pdfFile = value;
                // Notify that PdfFile property has changed
                OnPropertyChange(nameof(PdfFile));
            }
        }

        // Event handler for property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Method to notify listeners of property changes
        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor to initialize the view model
        public SmartFillViewModel()
        {
            string basePath = "SampleBrowser.Maui.Resources.Pdf";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.SmartComponents.Samples.SmartComponents.SmartFillForm.PDF";

            // Load a PDF file as a resource stream
            _pdfFile = this.GetType().Assembly.GetManifestResourceStream($"{basePath}.smart-form.pdf");
            UserDetail1 = "Hi, this is John. You can contact me at john123@gmail.com. I am male, born on February 20, 2005. I want to subscribe to the newsletter and take courses, specifically a machine learning course. I am from Alaska.";
            UserDetail2 = "S David here. You can reach me at David123@gmail.com. I am male, born on March 15, 2003. I would like to subscribe to the newsletter and am interested in taking a digital marketing course. I am from New York.";
            UserDetail3 = "Hi, this is Alice. You can contact me at alice456@gmail.com. I am female, born on July 15, 1998. I don’t want to subscribe to the newsletter but do want to take courses, specifically a cloud computing course. I am from Texas.";
        }
    }
}