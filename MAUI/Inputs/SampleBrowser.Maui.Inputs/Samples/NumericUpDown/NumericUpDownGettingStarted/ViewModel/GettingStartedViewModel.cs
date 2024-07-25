#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Inputs.Samples.NumericUpDown.NumericUpDownGettingStarted.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Inputs.Samples.NumericUpDown.NumericUpDownGettingStarted.ViewModel
{
    internal class GettingStartedViewModel
    {
        #region Fields

        private ObservableCollection<ProductInfo>? productInfo;

        #endregion

        #region Constructor

        public GettingStartedViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ProductInfo>? ProductInfo
        {
            get { return productInfo; }
            set { this.productInfo = value; }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            ProductInfoRepository productinfo = new();
            this.ProductInfo = productinfo.GetProductInfo();
        }

        #endregion
    }
}
