using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfComboBox;

public partial class ComboBoxDropdown : SampleView
{
	public ComboBoxDropdown()
	{
		InitializeComponent();
#if ANDROID || IOS
        ComboBoxDropdownMobile mobile = new ComboBoxDropdownMobile();
        this.Content = mobile.Content;
        this.OptionView = mobile.OptionView;
#else
        ComboBoxDropdownDesktop desktop = new ComboBoxDropdownDesktop();
        this.Content = desktop.Content;
        this.OptionView = desktop.OptionView;
#endif
    }
}