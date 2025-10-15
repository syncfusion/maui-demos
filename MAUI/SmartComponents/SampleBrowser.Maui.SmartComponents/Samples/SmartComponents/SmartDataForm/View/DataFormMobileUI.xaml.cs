#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{

	public partial class DataFormMobileUI : ContentView
	{
		public DataFormMobileUI()
		{
			InitializeComponent();
		}

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var templateItem = (sender as Border)?.BindingContext as TemplateItem;
            var bindingContext = this.ParentGrid.BindingContext as DataFormGeneratorModel;

            if (templateItem != null && bindingContext != null)
            {
                entry.Text = templateItem.Description;
                bindingContext.FormTitle = templateItem.Title;
            }
        }
    }
}