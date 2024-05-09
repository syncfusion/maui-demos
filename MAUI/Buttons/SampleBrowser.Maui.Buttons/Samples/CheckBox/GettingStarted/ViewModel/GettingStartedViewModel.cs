#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Buttons.Samples.CheckBox;

public class GettingStartedViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #region Properties

    private bool brandFilterChecked1;
    public bool BrandFilterChecked1
    {
        get { return brandFilterChecked1; }
        set
        {
            if (brandFilterChecked1 != value)
            {
                brandFilterChecked1 = value;
                OnPropertyChanged(nameof(BrandFilterChecked1));
                UpdateFilteredProducts();
            }
        }
    }

    private bool brandFilterChecked2 = true;
    public bool BrandFilterChecked2
    {
        get { return brandFilterChecked2; }
        set
        {
            if (brandFilterChecked2 != value)
            {
                brandFilterChecked2 = value;
                OnPropertyChanged(nameof(BrandFilterChecked2));
                UpdateFilteredProducts();
            }
        }
    }

    private bool brandFilterChecked3 = true;
    public bool BrandFilterChecked3
    {
        get { return brandFilterChecked3; }
        set
        {
            if (brandFilterChecked3 != value)
            {
                brandFilterChecked3 = value;
                OnPropertyChanged(nameof(BrandFilterChecked3));
                UpdateFilteredProducts();
            }
        }
    }

    private bool brandFilterChecked4;
    public bool BrandFilterChecked4
    {
        get { return brandFilterChecked4; }
        set
        {
            if (brandFilterChecked4 != value)
            {
                brandFilterChecked4 = value;
                OnPropertyChanged(nameof(BrandFilterChecked4));
                UpdateFilteredProducts();
            }
        }
    }

    private bool sizeFilterChecked2 = true;
    public bool SizeFilterChecked2
    {
        get { return sizeFilterChecked2; }
        set
        {
            if (sizeFilterChecked2 != value)
            {
                sizeFilterChecked2 = value;
                OnPropertyChanged(nameof(SizeFilterChecked2));
                UpdateFilteredProducts();
            }
        }
    }

    private bool sizeFilterChecked3 = true;
    public bool SizeFilterChecked3
    {
        get { return sizeFilterChecked3; }
        set
        {
            if (sizeFilterChecked3 != value)
            {
                sizeFilterChecked3 = value;
                OnPropertyChanged(nameof(SizeFilterChecked3));
                UpdateFilteredProducts();
            }
        }
    }

    private bool sizeFilterChecked4;
    public bool SizeFilterChecked4
    {
        get { return sizeFilterChecked4; }
        set
        {
            if (sizeFilterChecked4 != value)
            {
                sizeFilterChecked4 = value;
                OnPropertyChanged(nameof(SizeFilterChecked4));
                UpdateFilteredProducts();
            }
        }
    }

    private bool sizeFilterChecked5;
    public bool SizeFilterChecked5
    {
        get { return sizeFilterChecked5; }
        set
        {
            if (sizeFilterChecked5 != value)
            {
                sizeFilterChecked5 = value;
                OnPropertyChanged(nameof(SizeFilterChecked5));
                UpdateFilteredProducts();
            }
        }
    }

    private bool colorFilterChecked1;
    public bool ColorFilterChecked1
    {
        get { return colorFilterChecked1; }
        set
        {
            if (colorFilterChecked1 != value)
            {
                colorFilterChecked1 = value;
                OnPropertyChanged(nameof(colorFilterChecked1));
                UpdateFilteredProducts();
            }
        }
    }

    private bool colorFilterChecked2 = true;
    public bool ColorFilterChecked2
    {
        get { return colorFilterChecked2; }
        set
        {
            if (colorFilterChecked2 != value)
            {
                colorFilterChecked2 = value;
                OnPropertyChanged(nameof(colorFilterChecked2));
                UpdateFilteredProducts();
            }
        }
    }

    private bool colorFilterChecked3 = true;
    public bool ColorFilterChecked3
    {
        get { return colorFilterChecked3; }
        set
        {
            if (colorFilterChecked3 != value)
            {
                colorFilterChecked3 = value;
                OnPropertyChanged(nameof(colorFilterChecked3));
                UpdateFilteredProducts();
            }
        }
    }

    private bool colorFilterChecked4;
    public bool ColorFilterChecked4
    {
        get { return colorFilterChecked4; }
        set
        {
            if (colorFilterChecked4 != value)
            {
                colorFilterChecked4 = value;
                OnPropertyChanged(nameof(colorFilterChecked4));
                UpdateFilteredProducts();
            }
        }
    }

    private bool colorFilterChecked5;
    public bool ColorFilterChecked5
    {
        get { return colorFilterChecked5; }
        set
        {
            if (colorFilterChecked5 != value)
            {
                colorFilterChecked5 = value;
                OnPropertyChanged(nameof(colorFilterChecked5));
                UpdateFilteredProducts();
            }
        }
    }

    public ObservableCollection<Product> Products1 { get; set; }

    public ObservableCollection<Product> Products2 { get; set; }
    public ObservableCollection<Product> Products3 { get; set; }
    public ObservableCollection<Product> Products4 { get; set; }

    private ObservableCollection<Product>? filtered;
    public ObservableCollection<Product>? Filtered
    {
        get { return filtered; }
        set
        {
            if (filtered != value)
            {
                filtered = value;
                OnPropertyChanged(nameof(Filtered));
            }
        }
    }

    #endregion

    #region Constructor

    public GettingStartedViewModel()
    {
        Products1 = new ObservableCollection<Product>();
        Products2 = new ObservableCollection<Product>();
        Products3 = new ObservableCollection<Product>();
        Products4 = new ObservableCollection<Product>();

#if ANDROID || IOS
        string description1 = "Lightweight Sneakers";
        string description2 = "Stylish Smart Sneakers";
#else
        string description1 = "Lightweight Sneakers for Men";
        string description2 = "Stylish Smart Sneakers for Men";
#endif

        Products1.Add(new Product("CAMPUS", "brownshoe-01.png", "6   7   8", Colors.Brown, description1));
        Products1.Add(new Product("CAMPUS", "greenshoe-02.png", "6   7   8   9   10", Colors.Green, description2));
        Products1.Add(new Product("CAMPUS", "redshoe-03.png", "7   8   9   10 ", Colors.Red, description2));
        Products1.Add(new Product("CAMPUS", "sandalshoe-04.png", "9   10", Colors.Tan, description2));
        Products1.Add(new Product("CAMPUS", "violetshoe-05.png", "6   7", Colors.Violet, description1));
        Products1.Add(new Product("CAMPUS", "brownshoe-02.png", "7   8   9", Colors.Brown, description2));
        Products1.Add(new Product("CAMPUS", "greenshoe-03.png", "6   7   8   9", Colors.Green, description2));
        Products1.Add(new Product("CAMPUS", "redshoe-04.png", "7   8   9   10", Colors.Red, description2));
        Products1.Add(new Product("CAMPUS", "sandalshoe-05.png", "6   7   8   9   10", Colors.Tan, description1));
        Products1.Add(new Product("CAMPUS", "violetshoe-01.png", "6   9   10", Colors.Violet, description1));

        Products2.Add(new Product("Skechers", "brownshoe-03.png", "6   7   8   9   10", Colors.Brown, description2));
        Products2.Add(new Product("Skechers", "greenshoe-04.png", "8   9   10", Colors.Green, description2));
        Products2.Add(new Product("Skechers", "redshoe-05.png", "6   7   9   10", Colors.Red, description1));
        Products2.Add(new Product("Skechers", "sandalshoe-01.png", "6   7   8   9   10", Colors.Tan, description1));
        Products2.Add(new Product("Skechers", "violetshoe-02.png", "6   7   10", Colors.Violet, description2));
        Products2.Add(new Product("Skechers", "brownshoe-04.png", "6   7   8   9   10", Colors.Brown, description2));
        Products2.Add(new Product("Skechers", "greenshoe-05.png", "6   7   8", Colors.Green, description1));
        Products2.Add(new Product("Skechers", "redshoe-01.png", "6   7   8   9   10", Colors.Red, description1));
        Products2.Add(new Product("Skechers", "sandalshoe-02.png", "8   9   10", Colors.Tan, description2));
        Products2.Add(new Product("Skechers", "violetshoe-03.png", "6   7   8   9   10", Colors.Violet, description2));

        Products3.Add(new Product("RED TAPE", "brownshoe-05.png", "6   7   8   9   10", Colors.Brown, description1));
        Products3.Add(new Product("RED TAPE", "greenshoe-01.png", "8   9   10", Colors.Green, description1));
        Products3.Add(new Product("RED TAPE", "redshoe-02.png", "6   7   8   9   10", Colors.Red, description2));
        Products3.Add(new Product("RED TAPE", "sandalshoe-03.png", "6   8   9   10", Colors.Tan, description2));
        Products3.Add(new Product("RED TAPE", "violetshoe-04.png", "6   7   8   9   10", Colors.Violet, description2));
        Products3.Add(new Product("RED TAPE", "brownshoe-01.png", "6   9   10", Colors.Brown, description1));
        Products3.Add(new Product("RED TAPE", "greenshoe-02.png", "6   7   8   9   10", Colors.Green, description2));
        Products3.Add(new Product("RED TAPE", "redshoe-03.png", "6   7   8", Colors.Red, description2));
        Products3.Add(new Product("RED TAPE", "sandalshoe-04.png", "6   7   8   9   10", Colors.Tan, description2));
        Products3.Add(new Product("RED TAPE", "violetshoe-05.png", "6   7   8   10", Colors.Violet, description1));

        Products4.Add(new Product("Roadster", "brownshoe-02.png", "6   7   8", Colors.Brown, description2));
        Products4.Add(new Product("Roadster", "greenshoe-03.png", "6   7   8   9   10", Colors.Green, description2));
        Products4.Add(new Product("Roadster", "redshoe-04.png", "6   9   10", Colors.Red, description2));
        Products4.Add(new Product("Roadster", "sandalshoe-05.png", "6   7   8   9   10", Colors.Tan, description1));
        Products4.Add(new Product("Roadster", "violetshoe-01.png", "6   9   10", Colors.Violet, description1));
        Products4.Add(new Product("Roadster", "brownshoe-03.png", "6   7   8   9   10", Colors.Brown, description2));
        Products4.Add(new Product("Roadster", "greenshoe-04.png", "8   9   10", Colors.Green, description2));
        Products4.Add(new Product("Roadster", "redshoe-05.png", "6   7   8   9   10", Colors.Red, description1));
        Products4.Add(new Product("Roadster", "sandalshoe-01.png", "6   7   10", Colors.Tan, description1));
        Products4.Add(new Product("Roadster", "violetshoe-02.png", "6   7   9   10", Colors.Violet, description2));

        UpdateFilteredProducts();
    }

    #endregion

    #region Methods

    private void UpdateFilteredProducts()
    {
        Filtered = GetFilteredProducts();
        OnPropertyChanged(nameof(Filtered));
    }

    private ObservableCollection<Product> GetFilteredProducts()
    {
        var filteredList = new ObservableCollection<Product>();

        if (brandFilterChecked1)
        {
            foreach (var product in Products1)
            {
                if (IsColorChecked(product) || IsColorUnfiltered())
                    filteredList.Add(product);
            }
        }
        if (brandFilterChecked2)
        {
            foreach (var product in Products2)
            {
                if (IsColorChecked(product) || IsColorUnfiltered())
                    filteredList.Add(product);
            }
        }
        if (brandFilterChecked3)
        {
            foreach (var product in Products3)
            {
                if (IsColorChecked(product) || IsColorUnfiltered())
                    filteredList.Add(product);
            }
        }
        if (brandFilterChecked4)
        {
            foreach (var product in Products4)
            {
                if (IsColorChecked(product) || IsColorUnfiltered())
                    filteredList.Add(product);
            }
        }
        if (!brandFilterChecked1 && !brandFilterChecked2 && !brandFilterChecked3 && !brandFilterChecked4)
        {
            filteredList = GetProducts();
        }

        List<Product> products = new List<Product>();
        products = filteredList.OrderBy(x => Random.Shared.Next()).ToList<Product>();
        filteredList = this.GetCollection(products);

        return filteredList;
    }

    private ObservableCollection<Product> GetCollection(List<Product> list)
    {
        var filteredList = new ObservableCollection<Product>();
        foreach (var product in list)
        {
            filteredList.Add(product);
        }
        return filteredList;
    }

    private ObservableCollection<Product> GetProducts()
    {
        var filteredList = new ObservableCollection<Product>();
        foreach (var product in Products1)
        {
            if (IsColorChecked(product) || IsColorUnfiltered())
                filteredList.Add(product);
        }
        foreach (var product in Products2)
        {
            if (IsColorChecked(product) || IsColorUnfiltered())
                filteredList.Add(product);
        }
        foreach (var product in Products3)
        {
            if (IsColorChecked(product) || IsColorUnfiltered())
                filteredList.Add(product);
        }
        foreach (var product in Products4)
        {
            if (IsColorChecked(product) || IsColorUnfiltered())
                filteredList.Add(product);
        }
        return filteredList;
    }

    private bool IsColorChecked(Product product)
    {
        return ((colorFilterChecked1 && product.Color == Colors.Brown) || (colorFilterChecked2 && product.Color == Colors.Green) || (colorFilterChecked3 && product.Color == Colors.Red) || (colorFilterChecked4 && product.Color == Colors.Tan) || (colorFilterChecked5 && product.Color == Colors.Violet));
    }

    private bool IsColorUnfiltered()
    {
        return (!colorFilterChecked1 && !colorFilterChecked2 && !colorFilterChecked3 && !colorFilterChecked4 && !colorFilterChecked5);
    }

    #endregion

    #region Model

    public class Product
    {
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public Color Color { get; set; }
        public string Image { get; set; }

        public Product(string brand, string image, string size, Color color, string description)
        {
            Brand = brand;
            Description = description;
            Size = size;
            Color = color;
            Image = image;
        }
    }
    #endregion
}
