using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeedOptions : SampleView
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SpeedOptions()
        {
            InitializeComponent();
        }
        #endregion

        private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.DurationFactor = e.NewValue;
        }
    }
}