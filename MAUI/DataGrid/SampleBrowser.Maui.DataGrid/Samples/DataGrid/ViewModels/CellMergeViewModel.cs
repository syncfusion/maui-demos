using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class CellMergeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MergeCellOrderInfo>? orderList;
        private Random random = new Random();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<MergeCellOrderInfo>? OrderList
        {
            get => this.orderList;
            set
            {
                this.orderList = value;
                this.RaisePropertyChanged(nameof(OrderList));
            }
        }

        public ObservableCollection<string> ComboBoxItemsSource { get; set; }

        private string[] customerID = new string[]
        {
            "ALFKI","ANATR","ANTON","AROUT","BERGS","BLAUS","BLONP","BOLID","BONAP"
        };

        public CellMergeViewModel()
        {
            this.ComboBoxItemsSource = new ObservableCollection<string>
            {
                "iPhone 14",
                "Samsung Galaxy S23",
                "Google Pixel 7",
                "MacBook Pro",
                "Dell XPS 13",
                "Surface Pro",
                "Sony WH-1000XM5",
                "Bose QuietComfort",
                "Nintendo Switch",
                "PlayStation 5",
                "Xbox Series X",
                "Amazon Kindle",
                "Logitech MX Master",
                "Anker Power Bank",
                "GoPro HERO11"
            };

            this.OrderList = this.GetOrderDetails(50);
        }

        public ObservableCollection<MergeCellOrderInfo> GetOrderDetails(int count)
        {
            ObservableCollection<MergeCellOrderInfo> orders = new ObservableCollection<MergeCellOrderInfo>();
            string[] images = new string[]
            {
                "ellipse631.png","ellipse632.png","ellipse633.png","ellipse634.png","ellipse635.png",
                "ellipse636.png","ellipse637.png","ellipse638.png","ellipse639.png"
            };

            // Simple approach: generate randomly, then ensure at least one duplicate exists
            for (int i = 1; i <= count; i++)
            {
                var ord = new MergeCellOrderInfo()
                {
                    OrderID = 10000 + i,
                    CustomerID = customerID[this.random.Next(customerID.Length)],
                    ContactNumber = string.Format("(555)-{0:000}-{1:0000}", this.random.Next(100, 999), this.random.Next(0, 9999)),
                    UnitPrice = Math.Round(this.random.NextDouble() * 1000, 2),
                    ProductName = this.ComboBoxItemsSource[this.random.Next(this.ComboBoxItemsSource.Count)],
                    Quantity = this.random.Next(1, 100),
                    Freight = Math.Round(this.random.NextDouble() * 30, 2),
                    OrderDate = DateTime.Now.AddDays(-this.random.Next(0, 3650)),
                    IsShipped = (this.random.Next(0, 2) == 1)
                };

                var custIndex = Array.IndexOf(customerID, ord.CustomerID);
                if (custIndex >= 0)
                {
                    ord.Image = images[custIndex % images.Length];
                }
                else
                {
                    ord.Image = images[this.random.Next(images.Length)];
                }
                
                orders.Add(ord);
            }

            // If no duplicate CustomerID exists, make the last entry share the first entry's CustomerID
            var countsCheck = new Dictionary<string, int>();
            foreach (var o in orders)
            {
                if (!countsCheck.ContainsKey(o.CustomerID!))
                {
                    countsCheck[o.CustomerID!] = 0;
                    countsCheck[o.CustomerID!]++;
                }
            }

            bool hasDup = false;
            foreach (var kv in countsCheck)
            {
                if (kv.Value >= 2) 
                { 
                    hasDup = true; 
                    break; 
                }
            }

            if (!hasDup && orders.Count >= 2)
            {
                orders[orders.Count - 1].CustomerID = orders[0].CustomerID;
                var custIndex = Array.IndexOf(customerID, orders[orders.Count - 1].CustomerID);
                if (custIndex >= 0)
                    orders[orders.Count - 1].Image = images[custIndex % images.Length];
            }

            return orders;
        }

        private void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
