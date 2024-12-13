#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Buttons;
using Newtonsoft.Json;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public class DataPredictionBehaviour : Behavior<SampleView>
{
    private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
#pragma warning disable CS0618 // Type or member is obsolete
    private Frame? frame;
#pragma warning restore CS0618 // Type or member is obsolete
    private ActivityIndicator? activityIndicator;
    private OpenAi openAi;
    private bool isButtonClicked = false;
    private Syncfusion.Maui.Buttons.SfButton? button;
    private Animation animation;
    public DataPredictionBehaviour()
    {
        openAi = new OpenAi();
        animation = new Animation();
    }
    protected override void OnAttachedTo(SampleView bindable)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        this.frame = bindable.FindByName<Frame>("frame");
#pragma warning restore CS0618 // Type or member is obsolete
        this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
        this.activityIndicator = bindable.FindByName<ActivityIndicator>("Indicator");
        this.button = bindable.FindByName<Syncfusion.Maui.Buttons.SfButton>("button");
        if (this.button != null)
        {
            this.button.Clicked += TapGestureRecognizer_Clicked;
        }

        this.StartAnimation();
    }

    async private void TapGestureRecognizer_Clicked(object? sender, EventArgs e)
    {
        StopAnimation();

        if (!isButtonClicked)
        {
            isButtonClicked = true;
            if (this.activityIndicator != null)
            {
                this.activityIndicator.IsRunning = true;
                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                {
                    this.activityIndicator.Color = Color.FromArgb("#FEFAF6");

                }
                else
                {
                    this.activityIndicator.Color = Color.FromArgb("#1E201E");

                }

            }
            string prompt = "Final year GPA column should updated based on GPA of FirstYearGPA, SecondYearGPA and ThirdYearGPA columns. Total GPA should update based on average of all years GPA. Total Grade update based on total GPA. Updated the grade based on following details, 0 - 2.5 = F, 2.6 - 2.9 = C, 3.0 - 3.4 = B, 3.5 - 3.9 = B+, 4.0 - 4.4 = A, 4.5 - 5 = A+. average value decimal should not exceed 1 digit";
            GenerateGridReport gridReport = new GenerateGridReport()
            {
                GenerateDataSource = (this.datagrid?.BindingContext as GenerateDataCollection)!.Predictivedatas,
            };
            var gridReportJson = GetSerializedGridReport(gridReport);
            string userInput = ValidateAndGeneratePrompt(gridReportJson, prompt);
            var result = await openAi.GetResponseFromOpenAI(userInput);

            if (result == null)
            {
                result = openAi.GetDataPredictionResponse();
            }

            GenerateGridReport deserializeResult = new GenerateGridReport();

            result = result.Replace("```json", "").Replace("```", "").Trim();
            try
            {
                deserializeResult = DeserializeResult(result);
            }
            catch(Exception)
            {
                result = openAi.GetDataPredictionResponse();
                result = result.Replace("```json", "").Replace("```", "").Trim();
                deserializeResult = DeserializeResult(result);
            }
            var FinalYearGPA = new DataGridTextColumn() { HeaderText = "Final Year GPA", MappingName = "FinalYearGPA", MinimumWidth = 150 };
            var TotalCgpa = new DataGridTextColumn() { HeaderText = "CGPA", MappingName = "TotalGPA", MinimumWidth = 150 };
            var TotalGrade = new DataGridTextColumn() { HeaderText = "Total Grade", MappingName = "TotalGrade", MinimumWidth = 150 };
            this.datagrid.Columns.Add(FinalYearGPA);
            this.datagrid.Columns.Add(TotalCgpa);
            this.datagrid.Columns.Add(TotalGrade);
            int index = 0;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                if (datagrid != null)
                {
                    await datagrid.ScrollToColumnIndex(5);
                }
            }
            foreach (var item in gridReport.GenerateDataSource)
            {
                if (item != null)
                {
                    if (item.StudentID == gridReport.GenerateDataSource[index].StudentID)
                    {
                        if (deserializeResult != null && deserializeResult.GenerateDataSource != null)
                        {
                            gridReport.GenerateDataSource[index].FinalYearGPA = deserializeResult.GenerateDataSource[index].FinalYearGPA;
                            gridReport.GenerateDataSource[index].TotalGrade = deserializeResult.GenerateDataSource[index].TotalGrade;
                            gridReport.GenerateDataSource[index].TotalGPA = deserializeResult.GenerateDataSource[index].TotalGPA;
                        }
                    }
                }
                index++;
                datagrid!.Refresh();
                await Task.Delay(250);
            }


            if (this.activityIndicator != null)
            {
                this.activityIndicator.IsRunning = false;
            }
        }
    }

    private async void StartAnimation()
    {
        if (this.button != null && this.animation != null)
        {
            var bubbleEffect = new Animation(v => this.button.Scale = v, 1, 1.15, Easing.CubicInOut);
            var fadeEffect = new Animation(v => this.button.Opacity = v, 1, 0.5, Easing.CubicInOut);

            animation.Add(0, 0.5, bubbleEffect);
            animation.Add(0, 0.5, fadeEffect);
            animation.Add(0.5, 1, new Animation(v => this.button.Scale = v, 1.15, 1, Easing.CubicInOut));
            animation.Add(0.5, 1, new Animation(v => this.button.Opacity = v, 0.5, 1, Easing.CubicInOut));
            await Task.Delay(250);

            animation.Commit(this.button, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);
        }
    }
    private void StopAnimation()
    {
        if (this.animation != null)
        {
            animation.Dispose();
        }
    }
    private string GetSerializedGridReport(GenerateGridReport report)
    {
        return JsonConvert.SerializeObject(report);
    }
    private GenerateGridReport DeserializeResult(string result)
    {
        return JsonConvert.DeserializeObject<GenerateGridReport>(result)!;
    }

    private static string ValidateAndGeneratePrompt(string data, string userInput)
    {
        return $"Given the following datasource are bounded in the Grid table\n\n{data}.\n Return the newly prepared datasource based on following user query:  {userInput}\n\nGenerate an output in JSON format only (it should be refered as GenerateDataSource) and Should not include any additional information or contents in response";
    }

    protected override void OnDetachingFrom(SampleView bindable)
    {

        if (this.button != null)
        {
            this.button.Clicked -= TapGestureRecognizer_Clicked;
            if (this.button.AnimationIsRunning("BubbleEffect"))
            {
                this.button.AbortAnimation("BubbleEffect");
            }
        }

        this.StopAnimation();
        this.frame = null;
        this.datagrid = null;
        this.activityIndicator = null;
    }
}

