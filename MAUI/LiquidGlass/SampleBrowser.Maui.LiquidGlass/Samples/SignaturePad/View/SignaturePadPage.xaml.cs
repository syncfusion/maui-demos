#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfSignaturePad
{
    public partial class SignaturePadPage : SampleView
    {
        public SignaturePadPage()
        {
            InitializeComponent();
        }

        private void OnSignatureContainerTapped(object sender, EventArgs e)
        {
            Popup.IsOpen = true;
        }
    }
}
