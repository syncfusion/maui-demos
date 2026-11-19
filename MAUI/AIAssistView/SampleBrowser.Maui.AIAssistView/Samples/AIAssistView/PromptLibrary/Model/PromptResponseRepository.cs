using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    internal static class PromptResponseRepository
    {
        private static readonly Regex WhitespaceRegex = new("\\s+", RegexOptions.Compiled);

        private static string Normalize(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            // collapse whitespace and normalize case
            var collapsed = WhitespaceRegex.Replace(s.Trim(), " ");
            return collapsed.ToLowerInvariant();
        }

        // Build a mapping from normalized prompt content (and title) -> response markdown.
        public static IReadOnlyDictionary<string, string> BuildMapping(PromptLibraryInfoRepository repo, string[] promptResponseHtmlForMarkdown)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            if (promptResponseHtmlForMarkdown == null) throw new ArgumentNullException(nameof(promptResponseHtmlForMarkdown));

            var map = new Dictionary<string, string>(StringComparer.Ordinal);

            var items = repo.PromptLibraryInfo.ToList();

            // If the arrays are the same length we assume index -> index mapping.
            // Otherwise, we still try to map by normalized PromptContent or Title when possible.
            if (items.Count == promptResponseHtmlForMarkdown.Length)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var contentKey = Normalize(items[i].PromptContent);
                    var titleKey = Normalize(items[i].Title);
                    var response = promptResponseHtmlForMarkdown[i] ?? string.Empty;

                    if (!string.IsNullOrEmpty(contentKey) && !map.ContainsKey(contentKey))
                        map[contentKey] = response;

                    if (!string.IsNullOrEmpty(titleKey) && !map.ContainsKey(titleKey))
                        map[titleKey] = response;
                }

                return map;
            }

            // Fallback: try matching each response to an item by exact normalized prefix of the response
            // This is a best-effort attempt; prefer explicit same-length arrays when available.
            int min = Math.Min(items.Count, promptResponseHtmlForMarkdown.Length);
            for (int i = 0; i < min; i++)
            {
                var contentKey = Normalize(items[i].PromptContent);
                var titleKey = Normalize(items[i].Title);
                var response = promptResponseHtmlForMarkdown[i] ?? string.Empty;

                if (!string.IsNullOrEmpty(contentKey) && !map.ContainsKey(contentKey))
                    map[contentKey] = response;

                if (!string.IsNullOrEmpty(titleKey) && !map.ContainsKey(titleKey))
                    map[titleKey] = response;
            }

            return map;
        }

        // Try exact normalized match, then title match, then contains-based fallback.
        public static bool TryGetResponse(IReadOnlyDictionary<string, string> map, string? promptContent, out string response)
        {
            response = string.Empty;
            if (map == null) return false;
            if (string.IsNullOrWhiteSpace(promptContent)) return false;

            var key = Normalize(promptContent);

            // Exact normalized match
            if (map.TryGetValue(key, out response!))
                return true;

            // Try fuzzy contains: find the first map key that contains the normalized input or vice versa
            foreach (var kv in map)
            {
                if (string.IsNullOrEmpty(kv.Key))
                    continue;

                if (kv.Key.Contains(key, StringComparison.Ordinal) || key.Contains(kv.Key, StringComparison.Ordinal))
                {
                    response = kv.Value;
                    return true;
                }
            }

            return false;
        }

        // Convenience: find by predicate over original stored keys
        public static string? FindResponseByPredicate(IReadOnlyDictionary<string, string> map, Func<string, bool> predicate)
        {
            if (map == null || predicate == null) return null;

            foreach (var kv in map)
            {
                if (predicate(kv.Key))
                    return kv.Value;
            }

            return null;
        }
    }
}
