using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.Model;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.ViewModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Inputs.SfComboBox;

public partial class ComboBoxMultiSelection : SampleView
{
    public ComboBoxMultiSelection()
    {
        InitializeComponent();
        
#if ANDROID || IOS
        ComboBoxMultiSelectionMobile mobile = new ComboBoxMultiSelectionMobile();
        this.Content = mobile.Content;
        this.OptionView = mobile.OptionView;
#else
        ComboBoxMultiSelectionDesktop desktop = new ComboBoxMultiSelectionDesktop();
        this.Content = desktop.Content;
        this.OptionView = desktop.OptionView;
#endif
    }
}