using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SizeOptions : SampleView
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SizeOptions()
        {
            InitializeComponent();
        }
        #endregion

        private void Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.SizeFactor = e.NewValue;
        }
    }
}