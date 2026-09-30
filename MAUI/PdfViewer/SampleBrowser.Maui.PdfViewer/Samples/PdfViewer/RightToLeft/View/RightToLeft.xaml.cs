using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using System.Globalization;
using System.Resources;
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RightToLeft : SampleView
{
    CultureInfo initialCulture;
    public RightToLeft()
    {
        InitializeComponent();

        // Backup the device's current culture before setting the new culture to demonstrate RTL.
        initialCulture = CultureInfo.CurrentUICulture;
        
        //Set the device's culture to Arabic to demonstrate RTL.
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar-AE");

		string basePath = "SampleBrowser.Maui.Resources";
		if (BaseConfig.IsIndividualSB)
			basePath = "SampleBrowser.Maui.PdfViewer.Samples.PdfViewer.RightToLeft.Localization";
        SfPdfViewerResources.ResourceManager = new ResourceManager($"{basePath}.SfPdfViewer", Application.Current!.GetType().Assembly);
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        //Restore the device's original culture while navigating out of the RightToLeft sample. 
        CultureInfo.DefaultThreadCurrentUICulture = initialCulture;
    }
}