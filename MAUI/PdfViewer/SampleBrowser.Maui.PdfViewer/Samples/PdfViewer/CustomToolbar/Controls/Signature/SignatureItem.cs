using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureItem : INotifyPropertyChanged
    {
        public View View { get; set; }
        bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        internal SignatureItem(View signatureView)
        {
            View = signatureView;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
