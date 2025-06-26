#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;
using System.Globalization;

namespace SampleBrowser.Maui.SignaturePad.SfSignaturePad
{
    public class ProductDetails
    {
        public ProductDetails(string product, double price, int quantity, double total)
        {
            ProductName = product;
            Price = price;
            Quantity = quantity;
            TotalAmount = total;
        }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double TotalAmount { get; set; }
    }

    public class InvoiceViewModel : INotifyPropertyChanged
    {
        private ImageSource? imageSource;

        private double minimumStrokeThickness;

        private double maximumStrokeThickness;

        private Color selectedColor = Color.FromArgb("#000000");

        private double labelOpacity = 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        public InvoiceViewModel()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                minimumStrokeThickness = 3d;
                maximumStrokeThickness = 5d;
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                minimumStrokeThickness = 4d;
                maximumStrokeThickness = 6d;
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                minimumStrokeThickness = 1.5d;
                maximumStrokeThickness = 2.5d;
            }

            UserName = "Abraham Swearegin,";
            Place = "9920 Bridge Parkway,";
            State = "San Mateo, California,";
            Country = "United States.";

            InvoiceNo = 2058557939;

            InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");

            PurchaseDetails = new List<ProductDetails>()
            {
                 new ProductDetails("Jersey", 49.99, 3, 149.97),
                 new ProductDetails("AWC Logo Cap", 8.9, 2, 17.98),
                 new ProductDetails("ML Fork", 175.49,  6, 1052.94)
            };
        }

        public string UserName { get; private set; }

        public string Place { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public int InvoiceNo { get; private set; }

        public string InvoiceDate { get; private set; }

        public string GrandTotal
        {
            get
            {
                double grandTotal = 0d;
                foreach (ProductDetails productDetails in PurchaseDetails)
                {
                    grandTotal += productDetails.TotalAmount;
                }

                return "$" + grandTotal.ToString();
            }
        }

        public double MinimumStrokeThickness
        {
            get { return minimumStrokeThickness; }
            set
            {
                minimumStrokeThickness = value;
                OnPropertyChanged(nameof(MinimumStrokeThickness));
            }
        }

        public double MaximumStrokeThickness
        {
            get { return maximumStrokeThickness; }
            set
            {
                maximumStrokeThickness = value;
                OnPropertyChanged(nameof(MaximumStrokeThickness));
            }
        }

        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        public List<ProductDetails> PurchaseDetails { get; private set; }

        public ImageSource? ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public double LabelOpacity
        {
            get { return labelOpacity; }
            set
            {
                labelOpacity = value;
                OnPropertyChanged(nameof(LabelOpacity));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
