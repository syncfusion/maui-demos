using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialSlider
{
    public partial class LabelsAndTicksRadialSlider : SampleView
    {
        public LabelsAndTicksRadialSlider()
        {
            InitializeComponent();
        }

        #region Events

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            clockGauge.Handler?.DisconnectHandler();
        }

        private void labelsAndTicksPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 2.4)
                e.Cancel = true;
            else
            {
                clockAnnotationLabel.Text = e.NewValue.ToString() + (e.NewValue > 1 ? " hrs" : " hr");
            }
        }
        #endregion
    }
}