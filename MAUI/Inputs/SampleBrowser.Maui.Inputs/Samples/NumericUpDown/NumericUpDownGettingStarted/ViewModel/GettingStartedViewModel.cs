#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
using Syncfusion.Maui.Inputs;
using System.ComponentModel;

namespace SampleBrowser.Maui.Inputs.Samples.NumericUpDown.NumericUpDownGettingStarted.ViewModel
{
    internal class GettingStartedViewModel : INotifyPropertyChanged
    {
        #region Fields
        private NumericEntryUpDownPlacementMode buttonPlacement = NumericEntryUpDownPlacementMode.InlineVertical;
        private UpDownOrder buttonOrder = UpDownOrder.UpThenDown;
        private UpDownButtonAlignment buttonAlignment = UpDownButtonAlignment.Right;
        private int buttonPlacementListSelectedIndex = 1;
        private int buttonAlignmentListSelectedIndex = 1;
        private int buttonOrderListSelectedIndex = 1;
        private ObservableCollection<ProductInfo>? productInfo;
        #endregion

        #region Constructor

        public GettingStartedViewModel()
        {
            GenerateSource();
            ButtonPlacementList = [ NumericEntryUpDownPlacementMode.Inline, NumericEntryUpDownPlacementMode.InlineVertical, NumericEntryUpDownPlacementMode.Hidden];
            ButtonAlignmentList = [ UpDownButtonAlignment.Left, UpDownButtonAlignment.Right, UpDownButtonAlignment.Both];
            ButtonOrderList = [UpDownOrder.DownThenUp, UpDownOrder.UpThenDown];
        }

        #endregion

        #region Properties

        public ObservableCollection<ProductInfo>? ProductInfo
        {
            get { return productInfo; }
            set { this.productInfo = value; }
        }

        public NumericEntryUpDownPlacementMode ButtonPlacement
        {
            get { return buttonPlacement; }
            set { buttonPlacement = value; OnPropertyChanged(nameof(ButtonPlacement)); }
        }

        public UpDownOrder ButtonOrder
        {
            get { return buttonOrder; }
            set { buttonOrder = value; OnPropertyChanged(nameof(ButtonOrder)); }
        }

        public UpDownButtonAlignment ButtonAlignment
        {
            get { return buttonAlignment; }
            set { buttonAlignment = value; OnPropertyChanged(nameof(ButtonAlignment)); }
        }
        public ObservableCollection<NumericEntryUpDownPlacementMode> ButtonPlacementList { get; set; }
        public ObservableCollection<UpDownButtonAlignment> ButtonAlignmentList { get; set; }
        public ObservableCollection<UpDownOrder> ButtonOrderList { get; set; }
        public int ButtonPlacementListSelectedIndex
        {
            get { return buttonPlacementListSelectedIndex; }
            set {
                if (buttonPlacementListSelectedIndex != value)
                {
                    buttonPlacementListSelectedIndex = value;
                    ButtonPlacement = ButtonPlacementList[value];
                    OnPropertyChanged(nameof(ButtonPlacementListSelectedIndex));
                }
            }
        }
        public int ButtonAlignmentListSelectedIndex
        {
            get { return buttonAlignmentListSelectedIndex; }
            set {
                if (buttonAlignmentListSelectedIndex != value)
                {
                    buttonAlignmentListSelectedIndex = value;
                    ButtonAlignment = ButtonAlignmentList[value];
                    OnPropertyChanged(nameof(ButtonAlignmentListSelectedIndex));
                }
            }
        }

        public int ButtonOrderListSelectedIndex
        {
            get { return buttonOrderListSelectedIndex; }
            set
            {
                if (buttonOrderListSelectedIndex != value)
                {
                    buttonOrderListSelectedIndex = value;
                    ButtonOrder = ButtonOrderList[value]; 
                    OnPropertyChanged(nameof(ButtonOrderListSelectedIndex));
                }
            }
        }
        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            ProductInfoRepository productinfo = new();
            this.ProductInfo = productinfo.GetProductInfo();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
