using System.ComponentModel;

namespace SampleBrowser.Maui.Inputs.Samples.NumericUpDown.NumericUpDownGettingStarted.Model
{
    internal class ProductInfo : INotifyPropertyChanged
    {
        private double price;

        private double totalPrice;

        private int count = 0;

        private string productName = "Hello";

        private string productImage = "pizza.jpg";

        private string productDescription = "Hi";

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                TotalPrice = value * Count;
                OnPropertyChanged(nameof(Price));
            }
        }
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                TotalPrice = value * Price;
                OnPropertyChanged(nameof(Count));
            }
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string ProductImage
        {
            get { return productImage; }
            set
            {
                productImage = value;
                OnPropertyChanged(nameof(ProductImage));
            }
        }

        public string ProductDescription
        {
            get { return productDescription; }
            set
            {
                productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }

        public ProductInfo()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
