#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure.AI.OpenAI;
using Azure;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class PreprocessingAzureAIService : AzureBaseService
    {
        public PreprocessingAzureAIService()
        {

        }

        public Task<ObservableCollection<DataPreprocessingModel>> GetCleanedData(ObservableCollection<DataPreprocessingModel> rawData)
        {
            ObservableCollection<DataPreprocessingModel> collection = new ObservableCollection<DataPreprocessingModel>();

            var prompt = $"Clean the following e-commerce website traffic data, resolve outliers and fill missing values:\n{string.Join("\n", rawData.Select(d => $"{d.DateTime:yyyy-MM-dd-HH-m-ss}: {d.Visitors}"))} and the output cleaned data should be in the yyyy-MM-dd-HH-m-ss:Value, not required explanations";
            if (ChatHistory != null)
            {
                ChatHistory.Clear();
                ChatHistory.AddUserMessage(prompt);
            }
            try
            {
                throw new NotImplementedException("");
                //if (ChatCompletions != null && ChatHistory != null)
                //{
                //var response = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
                //return GetCleanedData(response.ToString(), collection);
                //}
            }
            catch (Exception)
            {
                return Task.FromResult(GetDummyData(collection));
            }
        }

        ObservableCollection<DataPreprocessingModel> GetCleanedData(string json, ObservableCollection<DataPreprocessingModel> collection)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new ObservableCollection<DataPreprocessingModel>();
            }

            var lines = json.Split('\n');
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    var date = DateTime.ParseExact(parts[0].Trim(), "yyyy-MM-dd-HH-m-ss", CultureInfo.InvariantCulture);
                    var high = double.Parse(parts[1].Trim());

                    collection.Add(new DataPreprocessingModel { DateTime = date, Visitors = high });
                }
            }

            return collection;
        }

        private ObservableCollection<DataPreprocessingModel> GetDummyData(ObservableCollection<DataPreprocessingModel> collection)
        {
            return new ObservableCollection<DataPreprocessingModel>() {
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 00, 00, 00), Visitors = 150 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 01, 00, 00), Visitors = 160 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 02, 00, 00), Visitors = 155 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 03, 00, 00), Visitors = 162 }, // Missing data
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 04, 00, 00), Visitors = 170 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 05, 00, 00), Visitors = 175 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 06, 00, 00), Visitors = 145 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 07, 00, 00), Visitors = 180 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 08, 00, 00), Visitors = 182 }, // Missing data
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 09, 00, 00), Visitors = 185 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 10, 00, 00), Visitors = 200 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 11, 00, 00), Visitors = 207 }, // Missing data
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 12, 00, 00), Visitors = 220 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 13, 00, 00), Visitors = 230 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 14, 00, 00), Visitors = 237 }, // Missing data
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 15, 00, 00), Visitors = 250 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 16, 00, 00), Visitors = 260 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 17, 00, 00), Visitors = 270 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 18, 00, 00), Visitors = 277 }, // Missing data
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 19, 00, 00), Visitors = 280 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 20, 00, 00), Visitors = 250 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 21, 00, 00), Visitors = 290 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 22, 00, 00), Visitors = 300 },
                new DataPreprocessingModel{ DateTime = new DateTime(2024, 07, 01, 23, 00, 00), Visitors = 307 }, // Missing data
                };
        }
    }
}