#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Inputs;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class ComboBoxCustomFilter : IComboBoxFilterBehavior
    {
        private readonly ComboBoxAzureAIService _azureAIService;
        public ObservableCollection<ComboBoxModel> Items { get; set; }
        public ObservableCollection<ComboBoxModel> FilteredItems { get; set; } = new ObservableCollection<ComboBoxModel>();
        private CancellationTokenSource? _cancellationTokenSource;
        private SoundexAndLevensteinAlgorithm soundexAndLevensteinAlgorithm;

        public ComboBoxCustomFilter()
        {
            _azureAIService = new ComboBoxAzureAIService();
            Items = new ObservableCollection<ComboBoxModel>();
            _cancellationTokenSource = new CancellationTokenSource();
            soundexAndLevensteinAlgorithm = new SoundexAndLevensteinAlgorithm();
        }

        public async Task<object?> GetMatchingIndexes(SfComboBox source, ComboBoxFilterInfo filterInfo)
        {
            Items = (ObservableCollection<ComboBoxModel>)source.ItemsSource;

            if (string.IsNullOrEmpty(filterInfo.Text))
            {
                _cancellationTokenSource?.Cancel();
                return await Task.FromResult(Items);
            }
            
            // If the Azure API credential is invalid, perform an offline search using Soundex and Levenshtein algorithms.
            if (!AzureBaseService.IsCredentialValid)
            {
                foreach (ComboBoxModel item in Items)
                {
                    soundexAndLevensteinAlgorithm.FilterItemsBySoundexAndLevenshtein(filterInfo.Text, item.Name!);
                }
                var filteredItemsInOrder = soundexAndLevensteinAlgorithm.GetOrder();              
                return await Task.FromResult(filteredItemsInOrder);
            }           

            string listItems = string.Join(", ", Items!.Select(c => c.Name));

            // Join the first five items with newline characters for demo output template for AI.           
            string outputTemplate = string.Join("\n", Items.Take(5).Select(c => c.Name));

            //The cancellationToken was used for cancelling the API request if user types continuously.       
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            //Passing the User Input, ItemsSource, Reference output and CancellationToken
            var filteredItems = await FilterItemsUsingAzureAI(filterInfo.Text, listItems, outputTemplate, cancellationToken);
          
            return await Task.FromResult(filteredItems);
        }

        public async Task<ObservableCollection<ComboBoxModel>> FilterItemsUsingAzureAI(string userInput, string itemsList, string outputTemplate, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                var prompt = $"Filter the list items based on the user input using character Starting with and Phonetic algorithms like Soundex or Damerau-Levenshtein Distance. " +
                           $"The filter should ignore spelling mistakes and be case insensitive. " +
                           $"Return only the filtered items with each item in new line without any additional content like explanations, Hyphen, Numberings and - Minus sign. Ignore the content 'Here are the filtered items or similar things' " +
                           $"Only return items that are present in the List Items. " +
                           $"Ensure that each filtered item is returned in its entirety without missing any part of its content. " +
                           $"Arrange the filtered items that starting with the user input's first letter are at the first index, followed by other matches. " +
                           $"Examples of filtering behavior: " +
                           $" userInput: a, filter the items starting with A " +
                           $" userInput: b, filter items starting with B " +
                           $" userInput: c, filter items starting with C " +
                           $" userInput: d, filter items starting with D " +
                           $" userInput: e, filter items starting with E " +
                           $" userInput: f, filter items starting with F " +
                           $" userInput: i, filter items starting with I " +
                           $" userInput: z, filter items starting with Z " +
                           $" userInput: l, filter items starting with L " +
                           $" userInput: q, filter items starting with Q " +
                           $" userInput: o, filter items starting with O " +
                           $" userInput: in, filter items starting with In " +
                           $" userInput: pa, filter items starting with Pa " +
                           $" userInput: em, filter items starting with Em " +
                           $"The example data are for reference, dont provide these as output. Filter the item from list items properly" +
                           $"Here is the User input: {userInput}, " +
                           $"List of Items: {itemsList}" +
                           $"If no items found, return \"Empty\" " +
                           $"Dont use 'Here are the filtered items:' in the output. Check this demo output template, you should return output like this: {outputTemplate} ";

                var completion = await _azureAIService.GetCompletion(prompt, cancellationToken);

                var filteredItems = completion.Split('\n').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();

                if (FilteredItems.Count > 0)
                    FilteredItems.Clear();
                FilteredItems.AddRange(
                        Items
                        .Where(i => filteredItems.Any(item => i.Name!.StartsWith(item))));

                cancellationToken.ThrowIfCancellationRequested();
            }
            return FilteredItems;
        }
    }
}
