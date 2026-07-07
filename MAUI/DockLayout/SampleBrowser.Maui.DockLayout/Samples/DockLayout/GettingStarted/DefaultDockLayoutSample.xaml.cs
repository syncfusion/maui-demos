using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.DockLayout.SfDockLayout
{
    public partial class DefaultDockLayoutSample : SampleView
    {
        public DefaultDockLayoutSample()
        {
            InitializeComponent();
        }

        private void OnLastChildCheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (docklayout is null)
                return;

            docklayout.ShouldExpandLastChild = e.Value;
        }

        private void OnHorizontalSpacingChanged(object? sender, ValueChangedEventArgs e)
        {
            if (sender is not Slider slider || docklayout is null)
                return;

            docklayout.HorizontalSpacing = slider.Value;
        }

        private void OnVerticalSpacingChanged(object? sender, ValueChangedEventArgs e)
        {
            if (sender is not Slider slider || docklayout is null)
                return;

            docklayout.VerticalSpacing = slider.Value;
        }

        private void OnDirectionSelectionChanged(object? sender, Syncfusion.Maui.Inputs.ComboBoxValueChangedEventArgs e)
        {
            if (sender is Syncfusion.Maui.Inputs.SfComboBox comboBox && comboBox.SelectedItem is FlowDirection selectedDirection && docklayout is not null)
            {
                docklayout.FlowDirection = selectedDirection;
            }
        }
    }

    public class DockLayoutViewModel
    {
        public List<FlowDirection> Directions { get; }

        public DockLayoutViewModel()
        {
            Directions = new List<FlowDirection>
            {
                FlowDirection.LeftToRight,
                FlowDirection.RightToLeft,
                FlowDirection.MatchParent
            };
        }
    }
}