#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for Sunburst.xaml
    /// </summary>
    public sealed partial class TemplateMarker : SampleView
    {
        #region Constructor
        /// <summary>
        /// Sunburst constructor
        /// </summary>
        public TemplateMarker()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = LayoutOptions.Center;
            this.btnInput.HorizontalOptions = LayoutOptions.Center;
            this.btnCreate.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Creating Excel Document
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.
            using ExcelEngine excelEngine = new();
            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Xlsx;

            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.TemplateMarker.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.TemplateMarker.xlsx";
            Assembly? assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if(input != null)
            { 
            IWorkbook workbook = application.Workbooks.Open(input);
            IWorksheet worksheet1 = workbook.Worksheets[0];

            input = assembly.GetManifestResourceStream("SampleBrowser.Maui.Resources.XlsIO.CLRObjects.xml");
            if (BaseConfig.IsIndividualSB)
               input = assembly.GetManifestResourceStream("SampleBrowser.Maui.XlsIO.Resources.XlsIO.CLRObjects.xml");
            if (input != null)
            { 
            IList<Customers>? list = GetList(input);


            #region Applying Marker
            //Create Template Marker Processor
            ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();

            marker.AddVariable("Customers", list);

            //Process the markers in the template.
            marker.ApplyMarkers();
            #endregion

            MemoryStream stream = new();

            workbook.SaveAs(stream);

            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("TemplateMarker.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
            input.Dispose();
            stream.Dispose();
            }
            }
        }
        #endregion

        #region HelperMethods

        #region View the Input file
        private void btnInput_Click(object sender, EventArgs e)
        {
            //Launching the Input Template using the default Application.[MS Excel Or Free ExcelViewer]            
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.TemplateMarker.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.TemplateMarker.xlsx";
            Assembly? assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if(input != null)
            { 
            MemoryStream stream = new();
            input.CopyTo(stream);
            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("TemplateMarker.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            input.Dispose();
            }
        }
        #endregion
        public IList<Customers> GetList(Stream fileStream)
        {
            IList<Customers> lstCustomers = new List<Customers>();
            fileStream.Position = 0;
            StreamReader reader = new(fileStream);
            IEnumerable<Customers> customers = GetData<Customers>(reader.ReadToEnd());
            //Get the Path of the Image
            Assembly? assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            Stream? imageStream;
            BinaryReader binaryReader;
            foreach (Customers cust in customers)
            {
                imageStream = assembly.GetManifestResourceStream("SampleBrowser.Maui.Resources.XlsIO.Template_Marker_Images." + cust.ImageText);
                if(BaseConfig.IsIndividualSB)
                    imageStream = assembly.GetManifestResourceStream("SampleBrowser.Maui.XlsIO.Resources.XlsIO.Template_Marker_Images." + cust.ImageText);
                if (imageStream != null)
                { 
                binaryReader = new BinaryReader(imageStream);
                byte[] hyperlinkImage = binaryReader.ReadBytes((int)imageStream.Length);
                cust.Hyperlink = new Hyperlink("https://help.syncfusion.com/file-formats/xlsio/working-with-template-markers", "", "Hyperlink", "Syncfusion", ExcelHyperLinkType.Url, hyperlinkImage);
                lstCustomers.Add(cust);
                imageStream.Dispose();
                }
            }
            return lstCustomers;
        }

        public static IEnumerable<T> GetData<T>(string xml)
        where T : Customers, new()
        {
            return XElement.Parse(xml)
               .Elements("Customers")
               .Select(c => new T
               {
                   SalesPerson = (string?)c.Element("SalesPerson"),
                   SalesJanJune = (int?)c.Element("SalesJanJune"),
                   SalesJulyDec = (int?)c.Element("SalesJulyDec"),
                   Change = (int?)c.Element("Change"),
                   ImageText = (string?)c.Element("ImageText")
               });
        }
        #endregion
    }
}