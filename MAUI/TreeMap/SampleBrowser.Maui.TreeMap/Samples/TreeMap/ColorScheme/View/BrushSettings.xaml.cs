namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents a sample view for demonstrating color mapping.
    /// </summary>
    public partial class BrushSettings : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrushSettings"/> class.
        /// </summary>
        public BrushSettings()
        {
            InitializeComponent();
            brushSettingsPicker.SelectedIndex = 3;
        }
    }
}