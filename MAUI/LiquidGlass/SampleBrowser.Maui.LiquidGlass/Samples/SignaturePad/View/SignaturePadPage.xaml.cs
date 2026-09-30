using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfSignaturePad
{
    public partial class SignaturePadPage : SampleView
    {
        public SignaturePadPage()
        {
            InitializeComponent();
        }

        private void OnSignatureContainerTapped(object? sender, EventArgs e)
        {
            Popup.IsOpen = true;
        }
    }
}
