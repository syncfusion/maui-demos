namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents the view for getting started with a sample.
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted"/> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
            selectionModePicker.ItemsSource = viewModel.SelectionModeInfo;
            textFormatOptionInfoPicker.ItemsSource = viewModel.TextFormatOptionInfo;
            layoutTypePicker.SelectedIndex = 0;
            selectionModePicker.SelectedIndex = 1;
            textFormatOptionInfoPicker.SelectedIndex = 0;
        }
    }
}
