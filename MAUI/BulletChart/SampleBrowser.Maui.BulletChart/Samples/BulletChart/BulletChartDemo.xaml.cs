using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.BulletChart.BulletChart;

public partial class BulletChartDemo : SampleView
{
    public BulletChartDemo()
    {
        InitializeComponent();

#if IOS
        contentView.HeightRequest = 650;
#endif
    }

    private void Uri_Tapped(object? sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://en.wikipedia.org/wiki/Bullet_graph");
            Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception)
        {
        }
    }
}