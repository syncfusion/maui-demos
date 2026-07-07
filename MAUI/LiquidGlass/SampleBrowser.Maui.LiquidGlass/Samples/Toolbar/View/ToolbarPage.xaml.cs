using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfToolbar;

public partial class ToolbarPage : SampleView
{
	public ToolbarPage()
	{
		InitializeComponent();
	}

#if IOS || MACCATALYST
 
        /// <summary>
        /// Overrides <see cref="OnAppearing"/> to apply platform-specific resource overrides for Kanban control.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current?.Resources is not ResourceDictionary resources)
            {
                return;
            }
 
            if (resources == null || resources.MergedDictionaries.Contains(resources))
            {
                return;
            }
 
            var toolbarResource = new ResourceDictionary
            {
                { "SfToolbarNormalBackground", Colors.Transparent },
            };
 
            resources.MergedDictionaries.Add(toolbarResource);
        }
#endif
}