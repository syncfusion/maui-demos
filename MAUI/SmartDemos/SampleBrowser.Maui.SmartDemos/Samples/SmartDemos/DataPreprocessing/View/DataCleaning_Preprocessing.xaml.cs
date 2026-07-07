using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartDemos.SmartDemos;

public partial class DataCleaning_Preprocessing : SampleView
{
    public DataCleaning_Preprocessing()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        Chart.Handler?.DisconnectHandler();
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        viewModel.IsBusy = true;
        await Task.Delay(2000);
        await viewModel.LoadCleanedDataAsync();
    }
}