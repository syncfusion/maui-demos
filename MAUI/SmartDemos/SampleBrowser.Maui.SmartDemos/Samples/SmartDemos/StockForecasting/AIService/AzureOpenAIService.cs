#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Extensions.AI;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    internal class AzureOpenAIService : AzureBaseService
    {

        public AzureOpenAIService()
        {

        }

        /// <summary>
        /// Initialize local embeddings for the provided text chunks
        /// </summary>
        /// <param name="chunks"></param>
        /// <returns></returns>
        public Task Initialize(string[] chunks)
        {
            return Task.CompletedTask;
        }

        public async Task<ObservableCollection<CompaniesModel>> GetAnswerFromGPT(string userPrompt, CompanyInfoRepo viewModel, int index)
        {
            try
            {
                if (IsCredentialValid && Client != null)
                {
                    ChatHistory = string.Empty;
                    // Add the system message and user message to the options
                    ChatHistory = ChatHistory + userPrompt;
                    var response = await Client.GetResponseAsync(ChatHistory);
                    return this.ConvertToCompaniesModelCollection(response.ToString());
                }
            }
            catch (Exception)
            {
                return viewModel.GenerateDataSource();
            }

            return viewModel.GenerateDataSource();
        }

        public ObservableCollection<CompaniesModel> ConvertToCompaniesModelCollection(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new ObservableCollection<CompaniesModel>();
            }

            var stockData = new ObservableCollection<CompaniesModel>();

            var lines = data.Split('\n');
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(':');
                if (parts.Length == 5)
                {
                    var date = DateTime.ParseExact(parts[0].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var high = double.Parse(parts[1].Trim());
                    var low = double.Parse(parts[2].Trim());
                    var open = double.Parse(parts[3].Trim());
                    var close = double.Parse(parts[4].Trim());

                    stockData.Add(new CompaniesModel(date, high, low, open, close));
                }
            }

            if (stockData.Count == 0)
                throw new Exception();

            return stockData;
        }

        internal string GeneratePrompt(List<CompaniesModel> historicalData)
        {
            var items = new List<CompaniesModel>();

            for (int i = historicalData.Count() - 1; i >= 0; i--)
            {
                items.Add(historicalData[i]);
            }

            var prompt = "Your job is to predicted OHLC values for the next 45 time step(s) for the following data:\n";
            foreach (var data in items)
            {
                prompt += $"{data.Date:yyyy-MM-dd}: {data.High}, {data.Low}, {data.Open}, {data.Close}\n";
            }
            prompt += "it can be random values based on the input data, and the prediction output data should be in the yyyy-MM-dd:High:Low:Open:Close, no other explaination required\n";
            return prompt;
        }
    }
}
