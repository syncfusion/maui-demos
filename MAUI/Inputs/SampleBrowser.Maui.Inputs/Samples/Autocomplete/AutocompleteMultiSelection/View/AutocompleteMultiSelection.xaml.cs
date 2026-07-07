using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfAutocomplete
{

    public partial class AutocompleteMultiSelection : SampleView
    {
        public AutocompleteMultiSelection()
        {
            InitializeComponent();
#if ANDROID || IOS
            AutocompleteMultiSelectionMobile mobile = new AutocompleteMultiSelectionMobile();
            this.Content = mobile.Content;
            this.OptionView = mobile.OptionView;
#else
            AutocompleteMultiSelectionDesktop desktop = new AutocompleteMultiSelectionDesktop();
            this.Content = desktop.Content;
            this.OptionView = desktop.OptionView;
#endif
        }
    }
}