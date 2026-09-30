using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Picker.SfPicker;

public partial class Customization : SampleView
{
    public Customization()
    {
        InitializeComponent();
        this.Picker.HeaderView.Height = 50;
        this.Picker.HeaderView.Text = "Select a Color";

        this.Picker1.HeaderView.Height = 50;
        this.Picker1.HeaderView.Text = "Select a Color";

        this.Picker.SelectionView.Padding = 0;
        this.Picker.SelectionView.CornerRadius = 0;

        this.Picker1.SelectionView.Padding = 0;
        this.Picker1.SelectionView.CornerRadius = 0;
    }
}