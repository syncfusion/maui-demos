#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public partial class StockAndroid : SampleView
{
    private int selected_Index = 0;
    Animation animation;

    public StockAndroid()
    {
        InitializeComponent();
        animation = new Animation();
        tapped = false;
        UpdateLayoutForOrientation();
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }

    private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        UpdateLayoutForOrientation();
    }

    void UpdateLayoutForOrientation()
    {
        var orientation = DeviceDisplay.MainDisplayInfo.Orientation;

        if (orientation == DisplayOrientation.Landscape)
        {
            landscapeHorizontalLayout.IsVisible = false;
        }
        else
        {
            landscapeHorizontalLayout.IsVisible = true;
        }
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
    }

    private void StartBubbleAnimation()
    {
        if (!tapped)
        {
            var bubbleEffect = new Animation(v => aiButton.Scale = v, 1, 1.15, Easing.CubicInOut);
            var fadeEffect = new Animation(v => aiButton.Opacity = v, 1, 0.5, Easing.CubicInOut);

            animation.Add(0, 0.5, bubbleEffect);
            animation.Add(0, 0.5, fadeEffect);
            animation.Add(0.5, 1, new Animation(v => aiButton.Scale = v, 1.15, 1, Easing.CubicInOut));
            animation.Add(0.5, 1, new Animation(v => aiButton.Opacity = v, 0.5, 1, Easing.CubicInOut));

            animation.Commit(this, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);

        }
    }

    private void DaysegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Buttons.SelectionChangedEventArgs e)
    {
        var selectedIndex = DaysegmentedControl.SelectedIndex;
        viewModel.StopUpdate = true;

        if (selectedIndex == 0)
        {
            dateTime.AutoScrollingDelta = 3;
            dateTime.AutoScrollingDeltaType = DateTimeIntervalType.Months;
            dateTime.Interval = 30;
            dateTime.IntervalType = DateTimeIntervalType.Days;
        }
        if (selectedIndex == 1)
        {
            dateTime.AutoScrollingDelta = 6;
            dateTime.AutoScrollingDeltaType = DateTimeIntervalType.Months;
            dateTime.LabelStyle.LabelFormat = "MMM dd";
            dateTime.Interval = 60;
            dateTime.IntervalType = DateTimeIntervalType.Days;
        }
        if (selectedIndex == 2)
        {
            dateTime.AutoScrollingDelta = 1;
            dateTime.AutoScrollingDeltaType = DateTimeIntervalType.Years;
            dateTime.LabelStyle.LabelFormat = "MMM yyyy";
            dateTime.Interval = 1;
            dateTime.IntervalType = DateTimeIntervalType.Months;
        }
    }

    private void comboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        var comboBox = sender as SfComboBox;
        viewModel.StopUpdate = true;

        if (comboBox != null && comboBox.SelectedValue != null)
        {
            selectedValue.Text = comboBox.SelectedValue.ToString();
            selected_Index = comboBox.SelectedIndex;

            if (comboBox.SelectedItem is CompaniesModel model)
            {
                viewModel.SelectedItem = model.Name;
            }

            viewModel.StockData = selected_Index == 1 ? viewModel.AlphabetStockData : selected_Index == 2 ? viewModel.AmazonStockData : selected_Index == 3 ? viewModel.TeslaStockData : viewModel.MsftStockData;

            viewModel.ForeCastData.Clear();
            viewModel.StopUpdate = true;
        }
    }

    private void SeriescomboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        viewModel.StopUpdate = true;

        var seriesComboBox = sender as SfComboBox;

        if (seriesComboBox != null)
        {
            if (seriesComboBox.SelectedIndex == 0)
            {
                hiLoOpenCloseSeries.IsVisible = false;
                lineSeries.IsVisible = false;

                candleSeries.IsVisible = true;

                forecastehlop.IsVisible = false;
                foreCasteLine.IsVisible = false;
                forecastCandle.IsVisible = true;
            }
            else if (seriesComboBox.SelectedIndex == 1)
            {
                candleSeries.IsVisible = false;
                lineSeries.IsVisible = false;
                hiLoOpenCloseSeries.IsVisible = true;

                forecastCandle.IsVisible = false;
                foreCasteLine.IsVisible = false;
                forecastehlop.IsVisible = true;
            }
            else
            {
                candleSeries.IsVisible = false;
                hiLoOpenCloseSeries.IsVisible = false;
                lineSeries.IsVisible = true;

                forecastCandle.IsVisible = false;
                forecastehlop.IsVisible = false;
                foreCasteLine.IsVisible = true;
            }

        }
    }

    private bool tapped;

    private void image_Loaded(object sender, EventArgs e)
    {
        StartBubbleAnimation();
    }

    private async void AIButtonClicked(object sender, EventArgs e)
    {
        tapped = true;
        StopBubbleAnimation();
        if (viewModel.IsRunning)
        {
            viewModel.StopUpdate = true;
            await Task.Delay(300);
            viewModel.ForeCastData.Clear();
        }

        AzureOpenAIService service = new AzureOpenAIService();
        var data = viewModel.StockData;

        var items = data.Take(40).ToList();
        var prompt = service.GeneratePrompt(items);
        var value = await service.GetAnswerFromGPT(prompt, viewModel, selected_Index);
        viewModel.StopUpdate = false;
        viewModel.ForeCastData.Clear();
        await viewModel.AddRangeWithDelayAsync(value, 300);
    }

    private void StopBubbleAnimation()
    {
        this.AbortAnimation("BubbleEffect");
        tapped = false;
        aiButton.Scale = 1;
        aiButton.Opacity = 1;
    }
}