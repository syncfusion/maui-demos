using System.ComponentModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class MergeCellOrderInfo : INotifyPropertyChanged
    {
        private int orderID;
        private string? customerID;
        private string? contactNumber;
        private string? image;
        private double unitPrice;
        private string? productName;
        private int quantity;
        private double freight;
        private DateTime orderDate;
        private bool isShipped;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int OrderID
        {
            get => this.orderID;
            set
            {
                this.orderID = value;
                this.RaisePropertyChanged(nameof(OrderID));
            }
        }

        public string? CustomerID
        {
            get => this.customerID;
            set
            {
                this.customerID = value;
                this.RaisePropertyChanged(nameof(CustomerID));
            }
        }

        public string? ContactNumber
        {
            get => this.contactNumber;
            set
            {
                this.contactNumber = value;
                this.RaisePropertyChanged(nameof(ContactNumber));
            }
        }

        public string? Image
        {
            get => this.image;
            set
            {
                this.image = value;
                this.RaisePropertyChanged(nameof(Image));
            }
        }

        public double UnitPrice
        {
            get => this.unitPrice;
            set
            {
                this.unitPrice = value;
                this.RaisePropertyChanged(nameof(UnitPrice));
            }
        }

        public string? ProductName
        {
            get => this.productName;
            set
            {
                this.productName = value;
                this.RaisePropertyChanged(nameof(ProductName));
            }
        }

        public int Quantity
        {
            get => this.quantity;
            set
            {
                this.quantity = value;
                this.RaisePropertyChanged(nameof(Quantity));
            }
        }

        public double Freight
        {
            get => this.freight;
            set
            {
                this.freight = value;
                this.RaisePropertyChanged(nameof(Freight));
            }
        }

        public DateTime OrderDate
        {
            get => this.orderDate;
            set
            {
                this.orderDate = value;
                this.RaisePropertyChanged(nameof(OrderDate));
            }
        }

        public bool IsShipped
        {
            get => this.isShipped;
            set
            {
                this.isShipped = value;
                this.RaisePropertyChanged(nameof(IsShipped));
            }
        }

        private void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
