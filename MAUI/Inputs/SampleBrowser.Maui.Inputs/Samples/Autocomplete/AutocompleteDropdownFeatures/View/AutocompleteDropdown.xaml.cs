#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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