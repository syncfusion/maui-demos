#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class SoundexAndLevensteinAlgorithm
    {
        ObservableCollection<string> processedItems = new ObservableCollection<string>();
        ObservableCollection<SearchData> filteredSearchResults = new ObservableCollection<SearchData>();

        List<string> soundexTerms = new List<string>() { };

        public SoundexAndLevensteinAlgorithm()
        {
            soundexTerms.Add("aeiouhyw");
            soundexTerms.Add("bfpv");
            soundexTerms.Add("cgikqsxz");
            soundexTerms.Add("dt");
            soundexTerms.Add("l");
            soundexTerms.Add("mn");
            soundexTerms.Add("r");
        }

        /// <summary>
        /// Validate the Soundex and Levenshtein Distance for the user input against each source item.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public bool FilterItemsBySoundexAndLevenshtein(object inputValue, object itemValue)
        {
            if (inputValue != null && itemValue != null)
            {
                var inputString = inputValue.ToString()!.ToLower();
                var itemString = itemValue.ToString()!.ToLower();
                if (inputString.Length > 0 && itemString.Length > 0)
                    if (inputString[0] != itemString[0])
                        return false;
                var trimmedInputString = string.Empty;
                var trimmedItemString = string.Empty;

                if (inputString.Length < itemString.Length)
                {
                    trimmedItemString = itemString.Remove(inputString.Length);
                    trimmedInputString = inputString;
                }

                if (itemString.Length < inputString.Length)
                {
                    return false;
                }

                if (itemString.Length == inputString.Length)
                {
                    trimmedInputString = inputString;
                    trimmedItemString = itemString;
                }
                bool IsMatchSoundex = ProcessOnSoundexAlgorithmn(trimmedInputString) == ProcessOnSoundexAlgorithmn(trimmedItemString);
                int Distance = GetDamerauLevenshteinDistance(trimmedInputString, trimmedItemString);

                if (IsMatchSoundex || Distance <= 4)
                {
                    var searchData = new SearchData() { Item = itemValue.ToString(), Distance = Distance };
                    if (!processedItems.Contains(itemValue.ToString()!))
                    {
                        filteredSearchResults.Add(searchData);
                        processedItems.Add(itemValue.ToString()!);
                    }
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Computes the Soundex value for the given input string.
        /// </summary>
        /// <returns>The on soundex algorithmn.</returns>
        /// <param name="input">Input.</param>
        /// <param name="moreAccuracy">If set to <c>true</c> more accuracy.</param>
        public string ProcessOnSoundexAlgorithmn(string input, bool moreAccuracy = true)
        {
            string stringValue = string.Empty;
            foreach (var item in input.ToLower())
            {
                for (int i = 0; i < soundexTerms.Count; i++)
                {
                    if (soundexTerms[i].Contains(item.ToString()))
                    {
                        stringValue += i.ToString();
                        continue;
                    }
                }
            }
            if (stringValue.Length > 0)
            {
                if (moreAccuracy)
                {
                    stringValue = stringValue.Insert(0, input[0].ToString());
                    stringValue = stringValue.Replace("0", "");
                }
            }
            return stringValue;
        }

        /// <summary>
        /// Computes the Damerau-Levenshtein distance between two strings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int GetDamerauLevenshteinDistance(string source, string target)
        {
            var bounds = new { Height = source.Length + 1, Width = target.Length + 1 };

            int[,] matrix = new int[bounds.Height, bounds.Width];

            for (int height = 0; height < bounds.Height; height++) { matrix[height, 0] = height; };
            for (int width = 0; width < bounds.Width; width++) { matrix[0, width] = width; };

            for (int height = 1; height < bounds.Height; height++)
            {
                for (int width = 1; width < bounds.Width; width++)
                {
                    int cost = (source[height - 1] == target[width - 1]) ? 0 : 1;
                    int insertion = matrix[height, width - 1] + 1;
                    int deletion = matrix[height - 1, width] + 1;
                    int substitution = matrix[height - 1, width - 1] + cost;

                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));

                    if (height > 1 && width > 1 && source[height - 1] == target[width - 2] && source[height - 2] == target[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }

                    matrix[height, width] = distance;
                }
            }
            return matrix[bounds.Height - 1, bounds.Width - 1];
        }

        /// <summary>
        /// Arrange and return filtered items based on their Levenshtein distance
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ComboBoxModel> GetOrder()
        {
            ObservableCollection<ComboBoxModel> orderedSource = new ObservableCollection<ComboBoxModel>();
            for (int i = 0; i < 10; i++)
            {
                int count = 0;
                for (int c = 0; c < filteredSearchResults.Count; c++)
                {
                    count++;
                    if (filteredSearchResults[c] != null && filteredSearchResults[c].Distance == i)
                    {
                        orderedSource.Add(new ComboBoxModel() { Name = filteredSearchResults[c].Item! });
                    }
                }
            }
            processedItems.Clear();
            filteredSearchResults.Clear();
            return orderedSource;
        }
    }
}
