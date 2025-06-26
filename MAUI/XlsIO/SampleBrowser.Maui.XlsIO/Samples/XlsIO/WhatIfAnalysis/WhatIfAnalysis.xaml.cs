#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for WhatIfAnalysis.xaml
    /// </summary>
    public partial class WhatIfAnalysis : SampleView
    {
        #region Constructor
        /// <summary>
        /// WhatIfAnalysis constructor
        /// </summary>
        public WhatIfAnalysis()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnCreate.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Loads the input template
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnInput_Click(object sender, EventArgs e)
        {
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.WhatIfAnalysisTemplate.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.WhatIfAnalysisTemplate.xlsx";
            Assembly? assembly = typeof(WhatIfAnalysis).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);

            if (input != null)
            {
                MemoryStream stream = new();
                input.CopyTo(stream);

                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("WhatIfAnalysisTemplate.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                input.Dispose();
            }
        }

        /// <summary>
        /// Create Excel
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //Initialize ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Initialize IApplication and set the default application version
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Load the Excel template into IWorkbook and get the worksheet into IWorksheet
                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.WhatIfAnalysisTemplate.xlsx";
                if (BaseConfig.IsIndividualSB)
                    inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.WhatIfAnalysisTemplate.xlsx";
                Assembly? assembly = typeof(WhatIfAnalysis).GetTypeInfo().Assembly;
                Stream? input = assembly.GetManifestResourceStream(inputPath);
                if (input != null)
                {
                    IWorkbook workbook = application.Workbooks.Open(input);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Initailize list objects with different values for scenarios
                    List<object> currentChange_Values = new List<object> { 0.23, 0.8, 1.1, 0.5, 0.35, 0.2, 0.4, 0.37, 1.1, 1, 0.94, 0.75 };
                    List<object> increasedChange_Values = new List<object> { 0.45, 0.56, 0.9, 0.5, 0.58, 0.43, 0.39, 0.89, 1.45, 1.2, 0.99, 0.8 };
                    List<object> decreasedChange_Values = new List<object> { 0.3, 0.2, 0.5, 0.3, 0.5, 0.23, 0.2, 0.3, 0.8, 0.6, 0.87, 0.4 };

                    List<object> currentQuantity_Values = new List<object> { 1500, 3000, 5000, 4000, 500, 4000, 1200, 1500, 750, 750, 1200, 7900 };
                    List<object> increasedQuantity_Values = new List<object> { 1000, 5000, 4500, 3900, 10000, 8900, 8000, 3500, 15000, 5500, 4500, 4200 };
                    List<object> decreasedQuantity_Values = new List<object> { 1000, 2000, 3000, 3000, 300, 4000, 1200, 1000, 550, 650, 800, 6900 };

                    //Add scenarios in the worksheet
                    IScenarios scenarios = worksheet.Scenarios;
                    scenarios.Add("Current % of Change", worksheet.Range["F5:F16"], currentChange_Values);
                    scenarios.Add("Increased % of Change", worksheet.Range["F5:F16"], increasedChange_Values);
                    scenarios.Add("Decreased % of Change", worksheet.Range["F5:F16"], decreasedChange_Values);

                    scenarios.Add("Current Quantity", worksheet.Range["D5:D16"], currentQuantity_Values);
                    scenarios.Add("Increased Quantity", worksheet.Range["D5:D16"], increasedQuantity_Values);
                    scenarios.Add("Decreased Quantity", worksheet.Range["D5:D16"], decreasedQuantity_Values);

                    //Create Summary
                    worksheet.Scenarios.CreateSummary(worksheet.Range["L7"]);

                    //Save the Excel file
                    MemoryStream stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    SaveService saveService = new SaveService();
                    saveService.SaveAndView("WhatIfAnalysis.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                    #region Close and Dispose                
                    input.Dispose();
                    stream.Dispose();
                    #endregion
                }
            }
        }
        #endregion
    }
}