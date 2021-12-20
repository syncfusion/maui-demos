using System;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Syncfusion.Presentation;
using System.Reflection;
using System.IO;

namespace SampleBrowser.Maui.Presentation
{
    public partial class EncryptAndDecrypt : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EncryptAndDecrypt()
        {
            InitializeComponent();
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnEncryptDocument.HorizontalOptions = LayoutOptions.Center;
            btnDecryptDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Encrypts the presentation.
        /// </summary>
        private void OnEncryptButtonClicked(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(EncryptAndDecrypt).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.SyncfusionPresentation.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            using MemoryStream ms = new();
            //Encrypts the PowerPoint Presentation file.
            presentation.Encrypt("syncfusion");
            //Saves the presentation to the memory stream.
            presentation.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Encrypt.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }

        /// <summary>
        /// Decrypts the presentation.
        /// </summary>
        private void OnDecryptButtonClicked(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(EncryptAndDecrypt).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.SyncfusionPresentation.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            using MemoryStream ms = new();
            //Decrypts the PowerPoint Presentation file.
            presentation.RemoveEncryption();
            //Saves the presentation to the memory stream.
            presentation.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Decrypt.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion
    }
}
