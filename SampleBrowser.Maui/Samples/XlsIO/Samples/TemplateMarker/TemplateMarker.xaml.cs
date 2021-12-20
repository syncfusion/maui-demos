using System;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using SampleBrowser.Maui.Core;
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.XlsIO
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
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;

                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.TemplateMarker.xlsx";
                Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
                Stream input = assembly.GetManifestResourceStream(inputPath);

                IWorkbook workbook = application.Workbooks.Open(input);
                IWorksheet worksheet1 = workbook.Worksheets[0];

                input = assembly.GetManifestResourceStream("SampleBrowser.Maui.Resources.XlsIO.CLRObjects.xml");
                IList<Customers> list = GetList(input);


                #region Applying Marker
                //Create Template Marker Processor
                ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();

                marker.AddVariable("Customers", list);

                //Process the markers in the template.
                marker.ApplyMarkers();
                #endregion

                MemoryStream stream = new MemoryStream();

                workbook.SaveAs(stream);

                stream.Position = 0;
                DependencyService.Get<ISave>().SaveAndView("TemplateMarker.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
                input.Dispose();
                stream.Dispose();
            }
        }
        #endregion

        #region HelperMethods

        #region View the Input file
        private void btnInput_Click(object sender, EventArgs e)
        {
          //Launching the Input Template using the default Application.[MS Excel Or Free ExcelViewer]            
          string inputPath = "SampleBrowser.Maui.Resources.XlsIO.TemplateMarker.xlsx";
          Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
          Stream input = assembly.GetManifestResourceStream(inputPath);
          MemoryStream stream = new MemoryStream();
          input.CopyTo(stream);
          stream.Position = 0;
          DependencyService.Get<ISave>().SaveAndView("TemplateMarker.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

          input.Dispose();
        }
        #endregion
        public IList<Customers> GetList(Stream fileStream)
        {
            IList<Customers> lstCustomers = new List<Customers>();
            fileStream.Position = 0;
            StreamReader reader = new StreamReader(fileStream);
            IEnumerable<Customers> customers = GetData<Customers>(reader.ReadToEnd());
            //Get the Path of the Image
            Assembly assembly = typeof(TemplateMarker).GetTypeInfo().Assembly;
            Stream imageStream;
            BinaryReader binaryReader;
            foreach (Customers cust in customers)
            {
                imageStream = assembly.GetManifestResourceStream("SampleBrowser.Maui.Resources.XlsIO.Template_Marker_Images." + cust.ImageText);
                binaryReader = new BinaryReader(imageStream);
                byte[] hyperlinkImage = binaryReader.ReadBytes((int)imageStream.Length);
                cust.Hyperlink = new Hyperlink("https://help.syncfusion.com/file-formats/xlsio/working-with-template-markers", "", "Hyperlink", "Syncfusion", ExcelHyperLinkType.Url, hyperlinkImage);
                lstCustomers.Add(cust);
                imageStream.Dispose();
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
                   SalesPerson = (string)c.Element("SalesPerson"),
                   SalesJanJune = (int)c.Element("SalesJanJune"),
                   SalesJulyDec = (int)c.Element("SalesJulyDec"),
                   Change = (int)c.Element("Change"),
                   ImageText = (string)c.Element("ImageText")
               });
        }
        #endregion
    }
}