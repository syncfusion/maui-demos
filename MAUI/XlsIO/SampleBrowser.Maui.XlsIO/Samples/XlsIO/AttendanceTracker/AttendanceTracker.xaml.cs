#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
using System.Globalization;
using System.IO;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for AttendanceTracker.xaml
    /// </summary>
    public partial class AttendanceTracker : SampleView
    {
        #region Fields
        /// <summary>
        /// File Name
        /// </summary>
        readonly string[] _columnNames;
        /// <summary>
        /// EmployeeDetails List
        /// </summary>
        private List<EmployeeDetails>? _employeeAttendanceList;
        #endregion

        # region Constructor
        /// <summary>
        /// AttendanceTracker constructor
        /// </summary>
        public AttendanceTracker()
        {
            this.InitializeComponent();
            _columnNames = new string[] { "Employee Name", "Supervisor", "Present Count", "Leave Count", "Absent Count", "Unplanned %", "Planned %" };
#if ANDROID || IOS
            this.btnCreate.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates spreadsheet
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event</param>
        /// <param name="e">Contains the event data</param>    
        private void btnCreate_Click(object sender, EventArgs e)
        {
            AttendanceDetailsGenerator attendanceDetailsGenerator = new();
            _employeeAttendanceList = attendanceDetailsGenerator.GetEmployeeAttendanceDetails(2019, 01);
            string OutputFilename = "AttendanceTracker.xlsx";

            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            using ExcelEngine excelEngine = new();
            //Step 2 : Instantiate the excel application object.
            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;

            //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
            //The new workbook will have 1 worksheets
            application.EnableIncrementalFormula = true;
            application.DefaultVersion = ExcelVersion.Xlsx;
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];

            DateTime dateTime = DateTime.Now;
            string monthName = dateTime.ToString("MMM", CultureInfo.InvariantCulture);
            worksheet.Name = monthName + "-" + dateTime.Year;

            CreateHeaderRow(worksheet);//Format header row
            FillAttendanceDetails(worksheet);
            ApplyConditionFormatting(worksheet);

            #region Apply Styles
            worksheet.Range["A1:AL1"].RowHeight = 24;
            worksheet.Range["A2:AL31"].RowHeight = 20;
            worksheet.Range["A1:B1"].ColumnWidth = 20;
            worksheet.Range["C1:G1"].ColumnWidth = 16;
            worksheet.Range["H1:AL31"].ColumnWidth = 4;

            worksheet.Range["A1:AL31"].CellStyle.Font.Bold = true;
            worksheet.Range["A1:AL31"].CellStyle.Font.Size = 12;
            worksheet.Range["A2:AL31"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(64, 64, 64);
            worksheet.Range["A1:AL31"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

            worksheet.Range["A1:AL1"].CellStyle.Font.Color = ExcelKnownColors.White;
            worksheet.Range["A1:AL1"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(58, 56, 56);

            worksheet.Range["A1:B31"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            worksheet.Range["C2:G31"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            worksheet.Range["H1:AL31"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

            worksheet.Range["A2:B31"].CellStyle.IndentLevel = 1;
            worksheet.Range["A1:G1"].CellStyle.IndentLevel = 1;

            worksheet.Range["A1:AL1"].BorderAround(ExcelLineStyle.Medium, Syncfusion.Drawing.Color.LightGray);
            worksheet.Range["A1:AL1"].BorderInside(ExcelLineStyle.Medium, Syncfusion.Drawing.Color.LightGray);

            worksheet.Range["A2:G31"].BorderAround(ExcelLineStyle.Medium, Syncfusion.Drawing.Color.LightGray);
            worksheet.Range["A2:G31"].BorderInside(ExcelLineStyle.Medium, Syncfusion.Drawing.Color.LightGray);

            worksheet.Range["H2:AL31"].BorderInside(ExcelLineStyle.Medium, ExcelKnownColors.White);
            #endregion

            MemoryStream stream = new();
            workbook.Version = ExcelVersion.Xlsx;
            workbook.SaveAs(stream);
            stream.Position = 0;

            SaveService saveService = new();
            saveService.SaveAndView(OutputFilename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            #region Stream Dispose               
            stream.Dispose();
            #endregion
        }
        #endregion

        #region HelperMethods
        /// <summary>
        /// Apply the conditonal format using workbook
        /// </summary>
        /// <param name="worksheet">Worksheet used to get the range and set the conditional formats</param>
        private void ApplyConditionFormatting(IWorksheet worksheet)
        {
            IConditionalFormats statusCondition = worksheet["H2:AL31"].ConditionalFormats;

            IConditionalFormat leaveCondition = statusCondition.AddCondition();
            leaveCondition.FormatType = ExcelCFType.CellValue;
            leaveCondition.Operator = ExcelComparisonOperator.Equal;
            leaveCondition.FirstFormula = "\"L\"";
            leaveCondition.BackColorRGB = Syncfusion.Drawing.Color.FromArgb(253, 167, 92);

            IConditionalFormat absentCondition = statusCondition.AddCondition();
            absentCondition.FormatType = ExcelCFType.CellValue;
            absentCondition.Operator = ExcelComparisonOperator.Equal;
            absentCondition.FirstFormula = "\"A\"";
            absentCondition.BackColorRGB = Syncfusion.Drawing.Color.FromArgb(255, 105, 124);

            IConditionalFormat presentCondition = statusCondition.AddCondition();
            presentCondition.FormatType = ExcelCFType.CellValue;
            presentCondition.Operator = ExcelComparisonOperator.Equal;
            presentCondition.FirstFormula = "\"P\"";
            presentCondition.BackColorRGB = Syncfusion.Drawing.Color.FromArgb(67, 233, 123);

            IConditionalFormat weekendCondition = statusCondition.AddCondition();
            weekendCondition.FormatType = ExcelCFType.CellValue;
            weekendCondition.Operator = ExcelComparisonOperator.Equal;
            weekendCondition.FirstFormula = "\"WE\"";
            weekendCondition.BackColorRGB = Syncfusion.Drawing.Color.FromArgb(240, 240, 240);

            IConditionalFormats presentSummaryCF = worksheet["C2:C31"].ConditionalFormats;
            IConditionalFormat presentCountCF = presentSummaryCF.AddCondition();
            presentCountCF.FormatType = ExcelCFType.DataBar;
            IDataBar dataBar = presentCountCF.DataBar;
            dataBar.BarColor = Syncfusion.Drawing.Color.FromArgb(61, 242, 142);

            IConditionalFormats leaveSummaryCF = worksheet["D2:D31"].ConditionalFormats;
            IConditionalFormat leaveCountCF = leaveSummaryCF.AddCondition();
            leaveCountCF.FormatType = ExcelCFType.DataBar;
            dataBar = leaveCountCF.DataBar;
            dataBar.BarColor = Syncfusion.Drawing.Color.FromArgb(242, 71, 23);

            IConditionalFormats absentSummaryCF = worksheet["E2:E31"].ConditionalFormats;
            IConditionalFormat absentCountCF = absentSummaryCF.AddCondition();
            absentCountCF.FormatType = ExcelCFType.DataBar;
            dataBar = absentCountCF.DataBar;
            dataBar.BarColor = Syncfusion.Drawing.Color.FromArgb(255, 10, 69);

            IConditionalFormats unplannedSummaryCF = worksheet["F2:F31"].ConditionalFormats;
            IConditionalFormat unplannedCountCF = unplannedSummaryCF.AddCondition();
            unplannedCountCF.FormatType = ExcelCFType.DataBar;
            dataBar = unplannedCountCF.DataBar;
            dataBar.MaxPoint.Type = ConditionValueType.HighestValue;
            dataBar.BarColor = Syncfusion.Drawing.Color.FromArgb(142, 142, 142);

            IConditionalFormats plannedSummaryCF = worksheet["G2:G31"].ConditionalFormats;
            IConditionalFormat plannedCountCF = plannedSummaryCF.AddCondition();
            plannedCountCF.FormatType = ExcelCFType.DataBar;
            dataBar = plannedCountCF.DataBar;
            dataBar.MaxPoint.Type = ConditionValueType.HighestValue;
            dataBar.BarColor = Syncfusion.Drawing.Color.FromArgb(56, 136, 254);

        }
        /// <summary>
        /// Used to fill the attendance details
        /// </summary>
        /// <param name="worksheet">Worksheet used to get the range and fill attendance details</param>
        private void FillAttendanceDetails(IWorksheet worksheet)
        {
            int rowIndex = 2;
            if(_employeeAttendanceList != null)
            { 
            foreach (EmployeeDetails empDetails in _employeeAttendanceList)
            {

                worksheet["A" + rowIndex].Text = empDetails.Name;
                worksheet["B" + rowIndex].Text = empDetails.Supervisor;
                for (int colIndex = 0; colIndex < empDetails.Attendances.Count; colIndex++)
                {
                    worksheet[rowIndex, colIndex + 8].Text = empDetails.Attendances[colIndex];
                }
                rowIndex++;
            }
            }
            //Data validation for list
            IDataValidation validation = worksheet.Range["H2:AL31"].DataValidation;
            validation.ListOfValues = new string[] { "P", "A", "L", "WE" };


            worksheet["C2:C31"].Formula = "=CountIf('H2:AL2',\"P\")";
            worksheet["D2:D31"].Formula = "=CountIf('H2:AL2',\"L\")";
            worksheet["E2:E31"].Formula = "=CountIf('H2:AL2',\"A\")";
            worksheet["F2:F31"].Formula = "=E2/(C2+D2+E2)";
            worksheet["G2:G31"].Formula = "=D2/(C2+D2+E2)";
            worksheet["F2:G31"].NumberFormat = ".00 %";
        }
        /// <summary>
        /// Used to create the header row
        /// </summary>
        /// <param name="worksheet">Worksheet used to create the header row</param>
        private void CreateHeaderRow(IWorksheet worksheet)
        {
            for (int i = 0; i < _columnNames.Length; i++)
            {
                worksheet[1, i + 1].Text = _columnNames[i];
            }
            worksheet["H1"].DateTime = new DateTime(2019, 1, 1);

            worksheet["I1:AL1"].Formula = "=H1+1";
            worksheet["H1:AL1"].NumberFormat = "d";
        }

        #endregion
    }
    #region HelperClasses
    /// <summary>
    /// Return the list of employee details
    /// </summary>
    public class EmployeeDetails
    {
        #region Fields
        public string? Name { get; set; }
        public string? Supervisor { get; set; }
        public List<string> Attendances { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// EmployeeDetails constructor
        /// </summary>
        public EmployeeDetails()
        {
            Attendances = new List<string>();
        }
        #endregion
    }

    /// <summary>
    /// Get the attendance details and return the list
    /// </summary>
    public class AttendanceDetailsGenerator
    {
        #region Fields
        private readonly List<EmployeeDetails> _employeeAttendanceList;
        readonly string[] _dayStatus;
        readonly string[] _supervisor;
        readonly string[] _employeeNames;
        #endregion

        #region Constructor
        /// <summary>
        /// AttendanceDetailsGenerator constructor
        /// </summary>
        public AttendanceDetailsGenerator()
        {
            _employeeAttendanceList = new List<EmployeeDetails>();
            _dayStatus = new string[] { "P", "L", "P", "A", "P" };
            _supervisor = new string[] { "Mary Saveley", "Liz Nixon", "Liu Wong", "Michael Holz" };
            _employeeNames = new string[] { "Maria Anders", "Ana Trujillo", "Antonio Moreno", "Thomas Hardy", "Christina Berglund", "Hanna Moos",
                                            "Frederique Citeaux", "Martin Sommer", "Laurence Lebihan", "Elizabeth Lincoln", "Victoria Ashworth", "Patricio Simpson",
                                            "Francisco Chang", "Yang Wang", "Pedro Afonso", "Elizabeth Brown", "Steve Rogers", "Ann Devon",
                                            "Philip Cramer", "Daniel Tonini", "Annette Roulet", "John Smith", "Maria Larsson", "Howard Stark",
                                            "Peter Franken", "Aria Cruz", "Philip Gary", "Fran Willamson", "Howard Snyde", "Mario Pontes"};
        }
        #endregion

        /// <summary>
        /// Used to get the employee attendance details
        /// </summary>
        /// <param name="year">Represents the year</param>
        /// <param name="month">Represents the month</param>
        public List<EmployeeDetails> GetEmployeeAttendanceDetails(int year, int month)
        {
            Random rnd = new();
            for (int i = 0; i < 30; i++)
            {
                EmployeeDetails details = new();
                details.Name = _employeeNames[i];
                details.Supervisor = _supervisor[rnd.Next(_supervisor.Length)];
                int numberOfDays = DateTime.DaysInMonth(year, month);
                for (int j = 0; j < numberOfDays; j++)
                {
                    DateTime date = new(year, month, j + 1);
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        details.Attendances.Add("WE");
                    else
                        details.Attendances.Add(_dayStatus[rnd.Next(_dayStatus.Length)]);
                }
                _employeeAttendanceList.Add(details);
            }
            return _employeeAttendanceList;
        }
    }
    #endregion
}
