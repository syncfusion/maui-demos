using SampleBrowser.Maui.Base;
using Syncfusion.Maui.TabView;

namespace SampleBrowser.Maui.TabView.SfTabView;

public partial class VerticalOrientation : SampleView
{
	public VerticalOrientation()
	{
		InitializeComponent();
    }

    private void placemntcomboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (sender is not Syncfusion.Maui.Inputs.SfComboBox combobox)
            return;

        if (TabView is null)
            return;

        if (combobox.SelectedItem is not string selected)
            return;

        switch (selected)
        {
            case "Left":
                TabView.TabBarPlacement = TabBarPlacement.Left;
                break;

            case "Right":
                TabView.TabBarPlacement = TabBarPlacement.Right;
                break;
        }
    }

    private void indicatorplacemntcomboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (sender is not Syncfusion.Maui.Inputs.SfComboBox combobox)
            return;

        if (TabView is null)
            return;

        if (combobox.SelectedItem is not string selected)
            return;

        switch (selected)
        {
            case "Left":
                TabView.IndicatorPlacement = TabIndicatorPlacement.Left;
                break;

            case "Right":
                TabView.IndicatorPlacement = TabIndicatorPlacement.Right;
                break;

            case "Fill":
                TabView.IndicatorPlacement = TabIndicatorPlacement.Fill;
                break;
        }
    }
}