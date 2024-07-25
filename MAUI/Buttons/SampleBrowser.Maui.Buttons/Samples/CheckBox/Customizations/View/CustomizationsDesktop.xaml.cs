#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Buttons.CheckBox;
using Syncfusion.Maui.Buttons;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class CustomizationsDesktop : SampleView
{
    ViewModel viewModel;
    public CustomizationsDesktop()
	{
		InitializeComponent();
       
        viewModel = new ViewModel();
        this.BindingContext = viewModel;
    }
    private void TextColorSegment_Clicked(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#FFFFFF");
        text1.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;
    }

    private void TextColorSegment_Clicked_1(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#C76B00");
        text1.Stroke = Colors.Transparent;
        text2.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;
    }

    private void TextColorSegment_Clicked_2(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#A007A3");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;
    }

    private void TextColorSegment_Clicked_3(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#00C737");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;
    }

    private void TextColorSegment_Clicked_4(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#000000");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;

    }
    private void TextColorSegment_Clicked_5(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#0012B2");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text7.Stroke = Colors.Transparent;
        text8.Stroke = Colors.Transparent;

    }
    private void TextColorSegment_Clicked_6(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#D1B400");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        text8.Stroke = Colors.Transparent;

    }
    private void TextColorSegment_Clicked_7(object sender, EventArgs e) {
        viewModel.TextColor = Color.FromArgb("#CC0000");
        text1.Stroke = Colors.Transparent;
        text2.Stroke = Colors.Transparent;
        text3.Stroke = Colors.Transparent;
        text4.Stroke = Colors.Transparent;
        text5.Stroke = Colors.Transparent;
        text6.Stroke = Colors.Transparent;
        text7.Stroke = Colors.Transparent;
        text8.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];

    }
    private void CheckedColorSegment_Clicked(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#FFFFFF");
        checked1.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }

    private void CheckedColorSegment_Clicked_1(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#C76B00");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }

    private void CheckedColorSegment_Clicked_2(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#A007A3");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }

    private void CheckedColorSegment_Clicked_3(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#00C737");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }

    private void CheckedColorSegment_Clicked_4(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#000000");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }
    private void CheckedColorSegment_Clicked_5(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#0012B2");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke = Colors.Transparent;
    }
    private void CheckedColorSegment_Clicked_6(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#D1B400");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        checked8.Stroke = Colors.Transparent;
    }
    private void CheckedColorSegment_Clicked_7(object sender, EventArgs e) {
        checkBox.CheckedColor = Color.FromArgb("#CC0000");
        checked1.Stroke = Colors.Transparent;
        checked2.Stroke = Colors.Transparent;
        checked3.Stroke = Colors.Transparent;
        checked4.Stroke = Colors.Transparent;
        checked5.Stroke = Colors.Transparent;
        checked6.Stroke = Colors.Transparent;
        checked7.Stroke = Colors.Transparent;
        checked8.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
    }

    private void UncheckedColorSegment_Clicked(object sender, EventArgs e) {

        checkBox.UncheckedColor = Color.FromArgb("#FFFFFF");
        unchecked1.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;
    }
    private void UncheckedColorSegment_Clicked_1(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#C76B00");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;
    }

    private void UncheckedColorSegment_Clicked_2(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#A007A3");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;

    }

    private void UncheckedColorSegment_Clicked_3(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#00C737");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;
    }

    private void UncheckedColorSegment_Clicked_4(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#000000");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;

    }
    private void UncheckedColorSegment_Clicked_5(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#0012B2");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke = Colors.Transparent;

    }
    private void UncheckedColorSegment_Clicked_6(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#D1B400");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        unchecked8.Stroke = Colors.Transparent;

    }
    private void UncheckedColorSegment_Clicked_7(object sender, EventArgs e) {
        checkBox.UncheckedColor = Color.FromArgb("#CC0000");
        unchecked1.Stroke = Colors.Transparent;
        unchecked2.Stroke = Colors.Transparent;
        unchecked3.Stroke = Colors.Transparent;
        unchecked4.Stroke = Colors.Transparent;
        unchecked5.Stroke = Colors.Transparent;
        unchecked6.Stroke = Colors.Transparent;
        unchecked7.Stroke = Colors.Transparent;
        unchecked8.Stroke =  Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];

    }
}