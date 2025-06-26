#region Copyright Syncfusion� Inc. 2001-2025.
// Copyright Syncfusion� Inc. 2001-2025. All rights reserved.
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
using System.IO;
using System.Reflection;
using IApplication = Syncfusion.XlsIO.IApplication;
using Color = Syncfusion.Drawing.Color;
using Syncfusion.Maui.Buttons;
using System.Collections.ObjectModel;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public sealed partial class AutoFill : SampleView
    {

        # region Constructor
        /// <summary>
        /// Chart constructor
        /// </summary>
        public AutoFill()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnGenerate.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.stepValue.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.stopValue.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.chkboxTrend.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
#endif
            autoFill.IsChecked = true;
            chkboxTrend.IsChecked = false;
            FillOptions = new List<string>();
            IsTrend = this.chkboxTrend.IsChecked;
            autoFillTypeOptions = new List<string>
        {
            "Default",
            "Copy",
            "Series",
            "Formats",
            "Values",
            "Days",
            "Weekdays",
            "Months",
            "Years",
            "Linear Trend",
            "Growth Trend"
        };

            fillSeriesOptions = new List<string>
        {
            "Linear",
            "Growth",
            "Days",
            "Weekdays",
            "Months",
            "Years",
            "AutoFill"
        };
            fillDropDown.ItemsSource = autoFillTypeOptions;
        }
        private List<string> autoFillTypeOptions;
        private List<string> fillSeriesOptions;
        #endregion
        private bool m_isTrend;
        public bool IsTrend { get { return m_isTrend; } set { m_isTrend = value; } }
        public List<string> FillOptions { get; set; }

        /// <summary>
        /// Gets the fill options.
        /// </summary>
        /// <param name="isTrend"></param>
        /// <returns></returns>
        public List<string> GetFillOptions(bool isTrend)
        {

            List<string> options;

            if (!isTrend)
            {
                options = Enum.GetValues(typeof(ExcelAutoFillType)).Cast<ExcelAutoFillType>().Select(e => $"AutoFill - {e}").Concat(Enum.GetValues(typeof(ExcelFillSeries)).Cast<ExcelFillSeries>().Select(e => $"FillSeries - {e}")).ToList();
            }
            else
            {
                options = new List<string>() { "FillSeries - Linear", "FillSeries - Growth" };
            }

            return options;
        }


        #region Events
        /// <summary>
        /// Loads the input template
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            IWorkbook workbook;
            string FillOption = this.Column1.Text != null ? this.Column1.Text : "";
            string SeriesBy = this.Column2.Text != null && this.SeriesbyCombobox.IsEnabled ? this.Column2.Text : "";
            if (autoFill.IsChecked)
                FillOption = GetAutoFillTypeName(FillOption);
            else if (fillSeries.IsChecked)
                FillOption = GetFillSeriesName(FillOption);
            string fillOption = "";
            string enumName = "";
            if (FillOption.Contains("AutoFillType -"))
            {
                enumName = FillOption.Replace("AutoFillType - ", "");
                fillOption = "AutoFill";
                workbook = GetAutoFill((ExcelAutoFillType)Enum.Parse(typeof(ExcelAutoFillType), enumName));
            }
            else
            {
                enumName = FillOption.Replace("FillSeries - ", "");
                fillOption = "FillSeries";
                workbook = GetAutoFill((ExcelFillSeries)Enum.Parse(typeof(ExcelFillSeries), enumName), SeriesBy == "Rows" ? ExcelSeriesBy.Rows : ExcelSeriesBy.Columns, this.txtboxstepValue.Text, this.txtboxstopValue.Text, this.chkboxTrend.IsChecked);
            }

            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            SaveService saveService = new SaveService();
            saveService.SaveAndView(string.Format("{0}_{1}.xlsx", fillOption, enumName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
            stream.Dispose();

        }
        /// <summary>
        /// Get the auto fill type option name.
        /// </summary>
        /// <param name="fillOption"></param>
        private string GetAutoFillTypeName(string fillOption)
        {
            switch (fillOption)
            {
                case "Default":
                case "Copy":
                case "Series":
                case "Formats":
                case "Values":
                case "Days":
                case "Weekdays":
                case "Months":
                case "Years":
                    return "AutoFillType - Fill" + fillOption;
                case "Linear Trend":
                    return "AutoFillType - " + "LinearTrend";
                case "Growth Trend":
                    return "AutoFillType - " + "GrowthTrend";
                default:
                    return fillOption;
            }
        }
        /// <summary>
        /// Get the fill series option name.
        /// </summary>
        /// <param name="fillOption"></param>
        private string GetFillSeriesName(string fillOption)
        {
            switch (fillOption)
            {
                case "Linear":
                case "Growth":
                case "Autofill":
                case "Days":
                case "Weekdays":
                case "Months":
                case "Years":
                    return "FillSeries - " + fillOption;
                default:
                    return fillOption;
            }
        }
        /// <summary>
        /// Get workbool with auto fill range
        /// </summary>
        /// <param name="autoFillOption"></param>
        /// <returns></returns>
        private IWorkbook GetAutoFill(ExcelAutoFillType autoFillOption)
        {

            //Instantiate the spreadsheet creation engine
            ExcelEngine excelEngine = new ExcelEngine();

            //Instantiate the excel application object
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;
            IWorkbook workbook = application.Workbooks.Create(1);

            IWorksheet sheet = workbook.Worksheets[0];
            sheet["A1"].Number = 2;
            sheet["A2"].Number = 4;
            sheet["A3"].Number = 6;

            switch (autoFillOption)
            {
                case ExcelAutoFillType.FillDefault:
                case ExcelAutoFillType.FillCopy:
                case ExcelAutoFillType.FillValues:
                case ExcelAutoFillType.FillSeries:
                case ExcelAutoFillType.GrowthTrend:
                case ExcelAutoFillType.LinearTrend:
                    {
                        sheet["A1:A3"].AutoFill(sheet["A4:A1000"], autoFillOption);
                        break;
                    }
                case ExcelAutoFillType.FillFormats:
                    {
                        sheet["A1"].CellStyle.Color = Color.Blue;
                        sheet["A2"].CellStyle.Color = Color.Red;
                        sheet["A3"].CellStyle.Color = Color.Chocolate;
                        sheet["A1:A3"].NumberFormat = "$0.00";
                        sheet["A1:A3"].AutoFill(sheet["A4:A1000"], autoFillOption);
                        break;
                    }
                case ExcelAutoFillType.FillMonths:
                case ExcelAutoFillType.FillDays:
                case ExcelAutoFillType.FillWeekdays:
                case ExcelAutoFillType.FillYears:
                    {
                        DateTime date = new DateTime(2025, 1, 1);
                        sheet["A1"].Value2 = date;
                        sheet["A2"].Value2 = date.AddDays(1);
                        sheet["A3"].Value2 = date.AddDays(2);
                        sheet["A1:A3"].NumberFormat = "m/d/yyyy";
                        sheet["A1:A3"].AutoFill(sheet["A4:A1000"], autoFillOption);
                        break;
                    }

            }
            sheet.UsedRange.ColumnWidth = 10;
            return workbook;
        }

        /// <summary>
        /// Table Slicer
        /// </summary>
        /// <returns>Return the created excel document as stream</returns>
        private IWorkbook GetAutoFill(ExcelFillSeries fillSeries, ExcelSeriesBy seriesBy, object? stepValue, object? stopValue, bool enableTrend)
        {
            ExcelEngine excelEngine = new ExcelEngine();

            //Step 2 : Instantiate the excel application object
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2016;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];
            if (seriesBy == ExcelSeriesBy.Columns)
            {
                sheet["A1"].Number = 2;
                sheet["A2"].Number = 4;
                sheet["A3"].Number = 6;
            }
            else
            {
                sheet["A1"].Number = 2;
                sheet["B1"].Number = 4;
                sheet["C1"].Number = 6;
            }

            stepValue = stepValue != null && txtboxstepValue.IsEnabled ? ConvertObject(txtboxstepValue.Text.ToString() ?? string.Empty) : stepValue;
            stopValue = stopValue != null && txtboxstopValue.IsEnabled ? ConvertObject(txtboxstopValue.Text.ToString() ?? string.Empty) : stopValue;

            if (stepValue as string != null && stepValue as string == "")
                stepValue = null;
            if (stopValue as string != null && stopValue as string == "")
                stopValue = null;

            switch (fillSeries)
            {
                case ExcelFillSeries.AutoFill:
                case ExcelFillSeries.Linear:
                case ExcelFillSeries.Growth:
                    break;
                case ExcelFillSeries.Years:
                case ExcelFillSeries.Days:
                case ExcelFillSeries.Weekdays:
                case ExcelFillSeries.Months:
                    {
                        if (seriesBy == ExcelSeriesBy.Columns)
                        {
                            DateTime date = new DateTime(2025, 1, 1);
                            sheet["A1"].Value2 = date;
                            sheet["A2"].Value2 = date.AddDays(1);
                            sheet["A3"].Value2 = date.AddDays(2);
                            sheet["A1:A3"].NumberFormat = "m/d/yyyy";
                        }
                        else
                        {
                            DateTime date = new DateTime(2025, 1, 1);
                            sheet["A1"].Value2 = date;
                            sheet["B1"].Value2 = date.AddDays(1);
                            sheet["C1"].Value2 = date.AddDays(2);
                            sheet["A1:C1"].NumberFormat = "m/d/yyyy";
                        }

                        break;
                    }

            }
            if (seriesBy == ExcelSeriesBy.Columns)
            {
                if (enableTrend)
                    sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, enableTrend);
                else if (stepValue == null && stopValue == null)
                {
                    if (fillSeries == ExcelFillSeries.AutoFill)
                    {
                        sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, enableTrend);
                    }
                }
                else if (stepValue == null && stopValue != null || stepValue != null && stopValue == null)
                {
                    bool isStepValue = stepValue != null;
                    if (isStepValue)
                    {
                        if (stepValue is DateTime)
                            sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, isStepValue);
                        else if (stepValue is double)
                            sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (double)stepValue, isStepValue);
                    }
                    else
                    {
                        if (stopValue is DateTime)
                            sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (DateTime)stopValue, isStepValue);
                        else if (stopValue is double)
                            sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (double)stopValue, isStepValue);
                    }
                }
                else
                {
                    if (stepValue is DateTime && stopValue is DateTime)
                        sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, (DateTime)stopValue);
                    else if (stepValue is DateTime && stopValue is double)
                        sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, (double)stopValue);
                    else if (stepValue is double && stopValue is double)
                        sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (double)stepValue, (double)stopValue);
                    else if (stepValue is double && stopValue is DateTime)
                        sheet[1, 1, 1000, 1].FillSeries(seriesBy, fillSeries, (double)stepValue, (DateTime)stopValue);
                }
            }
            else
            {
                if (enableTrend)
                    sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, enableTrend);
                else if (stepValue == null && stopValue == null)
                {
                    if (fillSeries == ExcelFillSeries.AutoFill)
                    {
                        sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, enableTrend);
                    }
                }
                else if (stepValue == null && stopValue != null || stepValue != null && stopValue == null)
                {
                    bool isStepValue = stepValue != null;
                    if (isStepValue)
                    {
                        if (stepValue is DateTime)
                            sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, isStepValue);
                        else if (stepValue is double)
                            sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (double)stepValue, isStepValue);
                    }
                    else
                    {
                        if (stopValue is DateTime)
                            sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (DateTime)stopValue, isStepValue);
                        else if (stopValue is double)
                            sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (double)stopValue, isStepValue);
                    }
                }
                else
                {
                    if (stepValue is DateTime && stopValue is DateTime)
                        sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, (DateTime)stopValue);
                    else if (stepValue is DateTime && stopValue is double)
                        sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (DateTime)stepValue, (double)stopValue);
                    else if (stepValue is double && stopValue is double)
                        sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (double)stepValue, (double)stopValue);
                    else if (stepValue is double && stopValue is DateTime)
                        sheet[1, 1, 1, 1000].FillSeries(seriesBy, fillSeries, (double)stepValue, (DateTime)stopValue);
                }
            }
            sheet.UsedRange.ColumnWidth = 10;
            return workbook;
        }

        /// <summary>
        /// converts the input to respective object
        /// </summary>
        /// <param name="value">input</param>
        /// <returns></returns>
        private object ConvertObject(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (double.TryParse(value, out double d)) return d;
            if (DateTime.TryParse(value, out DateTime dt)) return dt;
            return value;
        }
        /// <summary>
        /// On event changed for fill type radio box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillTypeChanged(object sender, Microsoft.Maui.Controls.CheckedChangedEventArgs e)
        {

            var radioButton = sender as RadioButton;

            if (radioButton == autoFill)
            {
                // Update ComboBox items for AutoFillType
                fillDropDown.ItemsSource = autoFillTypeOptions;
                fillDropDown.SelectedIndex = 0; // Reset selection to default
                chkboxTrend.IsChecked = false;
                chkboxTrend.IsEnabled = false;
                SeriesbyCombobox.SelectedIndex = 0;
                Column2.IsEnabled = false;
                txtboxstepValue.Text = string.Empty;
                txtboxstopValue.Text = string.Empty;
                txtboxstepValue.IsEnabled = false;
                txtboxstopValue.IsEnabled = false;
            }
            else if (radioButton == fillSeries)
            {
                // Update ComboBox items for FillSeries
                fillDropDown.ItemsSource = fillSeriesOptions;
                fillDropDown.SelectedIndex = 0; // Reset selection to default
                chkboxTrend.IsEnabled = true;
                Column2.IsEnabled = true;
                if (fillDropDown.Text != "AutoFill")
                {
                    txtboxstepValue.IsEnabled = true;
                    txtboxstopValue.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// On event changed for fill option drop down box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillOptionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var combobox = sender as SfComboBox;
            if ((fillSeries.IsChecked && fillDropDown.Text != null && fillDropDown.Text == "AutoFill") || (chkboxTrend.IsEnabled && chkboxTrend.IsChecked && fillSeries.IsChecked && fillDropDown.Text != null && (fillDropDown.Text == "Linear" || fillDropDown.Text == "Growth")))
            {
                txtboxstepValue.IsEnabled = false;
                txtboxstopValue.IsEnabled = false;
                txtboxstepValue.Text = "";
                txtboxstopValue.Text = "";
            }
            else if (fillSeries.IsChecked && fillDropDown.Text != null)
            {
                txtboxstepValue.IsEnabled = true;
                txtboxstopValue.IsEnabled = true;
            }
            if (fillSeries.IsChecked && fillDropDown.Text != null && (fillDropDown.Text == "Linear" || fillDropDown.Text == "Growth"))
            {
                chkboxTrend.IsEnabled = true;
            }
            else if (fillSeries.IsChecked && fillDropDown.Text != null && fillDropDown.Text != "Linear" && fillDropDown.Text != "Growth")
            {
                chkboxTrend.IsEnabled = false;
                chkboxTrend.IsChecked = false;
            }
        }

        /// <summary>
        /// On event changed for Trend check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTrendCheckBoxChanged(object sender, Microsoft.Maui.Controls.CheckedChangedEventArgs e)
        {
            // Disable/enable the text entries based on checkbox state
            stepValue.IsEnabled = !e.Value;
            stopValue.IsEnabled = !e.Value;

            // Get reference to the ComboBox inside the TextInputLayout
            var comboBox = fillDropDown as Syncfusion.Maui.Inputs.SfComboBox;

            if (comboBox != null)
            {
                if (e.Value)
                {
                    txtboxstepValue.IsEnabled = false;
                    txtboxstopValue.IsEnabled = false;
                    txtboxstepValue.Text = "";
                    txtboxstopValue.Text = "";
                }
                else
                {
                    txtboxstepValue.IsEnabled = true;
                    txtboxstopValue.IsEnabled = true;
                }
            }
            #endregion
        }
    }
}