using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SignaturePad.SfSignaturePad
{
    public partial class GettingStarted : SampleView
    {
        public GettingStarted()
        {
            InitializeComponent();
        }

        private void OnSignatureContainerTapped(object? sender, EventArgs e)
        {
            Popup.IsOpen = true;
        }
    }
}
