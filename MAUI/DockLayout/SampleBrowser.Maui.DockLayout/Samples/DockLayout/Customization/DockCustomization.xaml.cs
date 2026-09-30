using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.DockLayout.SfDockLayout
{
    public partial class DockCustomization : SampleView
    {
        public DockCustomization()
        {
            InitializeComponent();

#if WINDOWS || MACCATALYST
         // Live weather Sample

#elif ANDROID || IOS
          // Email Layout Sample
            
#endif
        }
    }
}