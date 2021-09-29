using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui
{
    public class MasterPageViewModel
    {
        #region fields

        //Navigation Drawer links
        private ObservableCollection<NavigationLinkModel> applinks;

        #endregion

        #region ctor

        public MasterPageViewModel()
        {
            GenerateNavigationLinks();
        }

        #endregion

        #region properties

        //Naviagtion Header content
        public NavigationDrawerModel AppDetails { get; set; }

        public ObservableCollection<NavigationLinkModel> AppLinks
        {
            get { return applinks; }
            set { applinks = value; }
        }

        private string Version
        {
            get
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                var assemblyName = new AssemblyName(assembly.FullName);
                return assemblyName.Version.Major + "." + assemblyName.Version.Minor + "." + assemblyName.Version.Build + "." + assemblyName.Version.Revision;
            }
        }

        #endregion

        #region methods

        public static ContentPage GetSamplesList(string controlName)
        {
            return null;
        }

        internal void GenerateNavigationLinks()
        {
            AppDetails = new NavigationDrawerModel();
            AppDetails.AppVersion = "Version " + Version;
            AppDetails.AppDesc = string.Empty;

            applinks = new ObservableCollection<NavigationLinkModel>();
            applinks.Add(new NavigationLinkModel { LinkText = "Product Page", LinkIcons = "productpage.png", LinkURL = "https://www.syncfusion.com/xamarin-ui-controls" });
            applinks.Add(new NavigationLinkModel { LinkText = "Whats New", LinkIcons = "whatsnew.png", LinkURL = "https://www.syncfusion.com/products/whatsnew/xamarin-forms" });
            applinks.Add(new NavigationLinkModel { LinkText = "Documentation", LinkIcons = "documentation.png", LinkURL = "https://help.syncfusion.com/xamarin/introduction/overview" });
        }

        #endregion
    }
}