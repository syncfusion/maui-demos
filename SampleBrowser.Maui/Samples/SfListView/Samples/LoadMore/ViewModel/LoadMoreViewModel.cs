#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using SampleBrowser.Maui.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace SampleBrowser.Maui.SfListView
{
    public class LoadMoreViewModel : INotifyPropertyChanged
    {
        private readonly int totalItems = 22;
        private readonly ProductRepository productRepository;
        private int totalOrderedItems = 0;
        private double totalPrice = 0;

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> Products1 { get; set; }

        public ObservableCollection<Product> Orders { get; set; }

        public Command<object> AddCommand { get; set; }

        public Command<object> RemoveButtonCommand { get; set; }

        public Command<object> LoadMoreItemsCommand { get; set; }

        public Command<object> OrderListCommand { get; set; }

        public Command<object> RemoveOrderCommand { get; set; }


        public Command CheckoutCommand { get; set; }

        public int TotalOrderedItems
        {
            get { return totalOrderedItems; }
            set
            {
                if (totalOrderedItems != value)
                {
                    totalOrderedItems = value;
                    RaisePropertyChanged("TotalOrderedItems");
                }
            }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    RaisePropertyChanged("TotalPrice");
                }
            }
        }

        public LoadMoreViewModel()
        {
            productRepository = new ProductRepository();
            Products = new ObservableCollection<Product>();
            Products1 = new ObservableCollection<Product>();
            Orders = new ObservableCollection<Product>();

            GenerateSource();

            if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                AddProducts(0, 11);
            else
                AddProducts(0, 6);

            AddCommand = new Command<object>(AddQuantity);
            OrderListCommand = new Command<object>(NavigateOrdersPage);
            CheckoutCommand = new Command(CheckOut);
            RemoveOrderCommand = new Command<object>(RemoveOrder);
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
            RemoveButtonCommand = new Command<object>(RemoveButton);
        }


        private void RemoveButton(object obj)
        {
            var p = obj as Product;
            if (p.Quantity > 0 == true)
            {
                p.Quantity--;
            }
            else
            {
                p.Quantity = 0;
            }
        }

        private void RemoveOrder(object obj)
        {
            var p = obj as Product;
            p.Quantity = 0;
        }

        private async void CheckOut(object obj)
        {
            var checkout = await Application.Current.MainPage.DisplayAlert("Checkout", "Do you want to checkout?", "Yes", "No");
            if (checkout)
            {
                while (Orders.Count > 0)
                {
                    Orders[Orders.Count - 1].Quantity = 0;
                }

                await Application.Current.MainPage.DisplayAlert("", "Your order has been placed.", "OK");
#pragma warning disable CS0618 // Type or member is obsolete
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (obj != null)
                        (obj as ContentPage).Navigation.PopAsync();
                });
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        private void NavigateOrdersPage(object obj)
        {
            var sampleView = obj as SampleView;
            var ordersPage = new LoadMoreOrdersPage
            {
                BindingContext = this
            };
            sampleView.Navigation.PushAsync(ordersPage);
        }

        private void AddProducts(int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                var name = productRepository.Names[i];
                var p = new Product()
                {
                    Name = name,
                    Weight = productRepository.Weights[i],
                    Price = productRepository.Prices[i],
                    Image = productRepository.FruitNames[i] + ".jpg"
                };

                p.PropertyChanged += (s, e) =>
                {
                    var product = s as Product;
                    if (e.PropertyName == "Quantity")
                    {
                        if (Orders.Contains(product) && product.Quantity <= 0)
                            Orders.Remove(product);
                        else if (!Orders.Contains(product) && product.Quantity > 0)
                            Orders.Add(product);

                        TotalOrderedItems = Orders.Count;
                        TotalPrice = 0;
                        for (int j = 0; j < Orders.Count; j++)
                        {
                            var order = Orders[j];
                            TotalPrice += order.TotalPrice;
                        }
                    }
                };

                Products.Add(p);
            }
        }

        private void GenerateSource()
        {
            for (int i = 0; i < productRepository.Names.Length; i++)
            {
                var name = productRepository.Names[i];
                var p = new Product()
                {
                    Name = name,
                    Weight = productRepository.Weights[i],
                    Price = productRepository.Prices[i],
                    Image = productRepository.FruitNames[i] + ".jpg"
                };

                Products1.Add(p);
            }
        }

        private bool CanLoadMoreItems(object obj)
        {
            if (Products.Count >= totalItems)
                return false;
            return true;
        }

        private async void LoadMoreItems(object obj)
        {
            var listview = obj as Syncfusion.Maui.ListView.SfListView;
            listview.IsBusy = true;
            await Task.Delay(2500);

            var index = Products.Count;
            var count = index + 3 >= totalItems ? totalItems - index : 3;
            AddProducts(index, count);

            listview.IsBusy = false;
        }

        private void AddQuantity(object obj)
        {
            var p = obj as Product;
            p.Quantity += 1;
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
