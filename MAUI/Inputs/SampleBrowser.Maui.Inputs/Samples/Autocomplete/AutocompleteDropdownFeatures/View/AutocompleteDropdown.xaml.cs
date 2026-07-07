using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfAutocomplete;

public partial class AutocompleteDropdown : SampleView
{
    public AutocompleteDropdown()
    {
        InitializeComponent();
#if ANDROID || IOS
        AutocompleteDropdownMobile mobile = new AutocompleteDropdownMobile();
        this.Content = mobile.Content;
        this.OptionView = mobile.OptionView;
#else
        AutocompleteDropdownDesktop desktop = new AutocompleteDropdownDesktop();
            this.Content = desktop.Content;
            this.OptionView = desktop.OptionView;
#endif
    }
}