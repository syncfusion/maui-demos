#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using Newtonsoft.Json;
using Syncfusion.Maui.Buttons;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public class AnamolyDetectionBehaviour : Behavior<SampleView>
{
    private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
#pragma warning disable CS0618 // Type or member is obsolete
    private Frame? frame;
#pragma warning restore CS0618 // Type or member is obsolete
    private ActivityIndicator? activityIndicator;
    private OpenAi openAi;
    private Syncfusion.Maui.Buttons.SfButton? button;
    private bool isButtonClicked = false;
    private Animation? animation;
    public AnamolyDetectionBehaviour()
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
                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                {
                    this.activityIndicator.Color = Color.FromArgb("#FEFAF6");

                }
                else
                {
                    this.activityIndicator.Color = Color.FromArgb("#1E201E");
                }
                this.activityIndicator.IsRunning = true;
            }
            GridReport gridReport = new GridReport()
            {
                DataSource = (this.datagrid?.BindingContext as MachineDataRepository)!.MachineDataCollection,
            };
            var gridReportJson = GetSerializedGridReport(gridReport);
            string userInput = ValidateAndGeneratePrompt(gridReportJson);
            var result = await openAi.GetResponseFromOpenAI(userInput);

            // If result is null, need to process predefined AI response.
            if (result == null)
            {
                result = openAi.GetAnamolyDetectionResponse();
            }

            result = result.Replace("```json", "").Replace("```", "").Trim();
            GridReport deserializeResult;
            try
            {
                deserializeResult = DeserializeResult(result);
            }
            catch (Exception)
            {
                result = openAi.GetAnamolyDetectionResponse();
                result = result.Replace("```json", "").Replace("```", "").Trim();
                deserializeResult = DeserializeResult(result);
            }

            if (deserializeResult?.DataSource != null)
            {
                string[] anamoly = deserializeResult.DataSource.Select(x => x.AnomalyDescription).ToArray();
                string[] generateDataAlone = deserializeResult.DataSource.Select(x => x.MachineID).ToArray();
                AnamolyDetetcionConverter colorConverter = new AnamolyDetetcionConverter();
                colorConverter.GetString(anamoly);
                var anomalyDescriptionColumn = new DataGridTextColumn() { HeaderText = "Anomaly Description", MappingName = "AnomalyDescription", ColumnWidthMode = ColumnWidthMode.Auto };
                int index = -1;
                this.datagrid?.Columns.Add(anomalyDescriptionColumn);

                if (gridReport.DataSource != null)
                {
                    foreach (var item in gridReport.DataSource)
                    {
                        if (generateDataAlone.Contains(item.MachineID))
                        {
                            index++;
                            item.AnomalyDescription = deserializeResult.DataSource[index].AnomalyDescription;
                        }
                    }
                }
            }

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                if (datagrid != null)
                {
                    await datagrid.ScrollToColumnIndex(6);
                }
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
    private string GetSerializedGridReport(GridReport report)
    {
        return JsonConvert.SerializeObject(report);
    }
    private GridReport DeserializeResult(string result)
    {
        return JsonConvert.DeserializeObject<GridReport>(result)!;
    }

    private static string ValidateAndGeneratePrompt(string data)
    {
        return $"Given the following datasource are bounded in the Grid table\n\n{data}.\n Return the anomaly data rows (ie. pick the ir-relevant datas mentioned in the corresponding table) present in the table mentioned above as like in the same JSON format (it should referred as DataSource) provided do not change the format. Example: Watch out the production rate count and the factors that is used to acheive the mentioned production rate(Temprature, Pressure, Motor Speed) If the production rate is not relevant to the concern factors mark it as anomaly Data. If it is anomaly data then due to which column data it is marked as anomaly that particular column name should be updated in the AnomalyFieldName. Also Update the AnomalyDescription stating that due to which reason it is marked as anomaly a short description. Example if the data is marked as anomaly due to the Temperature column, Since the mentioned temperature is too high than expected, it is marked as anomaly data.\n\nGenerate an output in JSON format only and Should not include any additional information or contents in response";
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
        this.animation = null;
        this.frame = null;
        this.datagrid = null;
        this.activityIndicator = null;
    }
}
