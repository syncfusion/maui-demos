#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using System;
    using System.Reflection;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Kanban;
    using Syncfusion.Maui.Inputs;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the behavior for managing sorting cards in a sample view.
    /// </summary>
    public class SortingBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The sorting index combo box instance.
        /// </summary>
        private SfComboBox? sortByComboBox;

        /// <summary>
        /// The sorting order combo box instance.
        /// </summary>
        private SfComboBox? orderComboBox;

        /// <summary>
        /// The sorting mapping math combo box instance.
        /// </summary>
        private SfComboBox? mappingPathComboBox;

        /// <summary>
        /// The kanban control instance
        /// </summary>
        private SfKanban? kanban;

        /// <summary>
        /// To store initial Kanban SortingMappingPath value
        /// </summary>
        private string? sortingMappingPathValue;

        #endregion

        #region Override methods

        /// <summary>
        /// Invoked when behavior is attached to a view.
        /// </summary>
        /// <param name="sampleView">The sample view to which the behavior is attached.</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            this.kanban = sampleView.Content.FindByName<SfKanban>("kanban");
            this.sortByComboBox = sampleView.FindByName<SfComboBox>("sortByComboBox");
            this.orderComboBox = sampleView.FindByName<SfComboBox>("sortOrderComboBox");
            this.mappingPathComboBox = sampleView.FindByName<SfComboBox>("mappingPathComboBox");
            if (this.kanban != null)
            {
                this.sortingMappingPathValue = this.kanban.SortingMappingPath;
                this.kanban.DragEnd += this.OnCardDragEnd;
            }

            if (this.orderComboBox != null)
            {
                this.orderComboBox.SelectionChanged += this.OnSortOrderComboBoxSelectionChanged;
            }

            if (this.mappingPathComboBox != null)
            {
                this.mappingPathComboBox.SelectionChanged += this.OnSortingMappingPathComboBoxSelectionChanged;
            }

            if (this.sortByComboBox != null)
            {
                this.sortByComboBox.SelectionChanged += this.OnSortByComboBoxSelectionChanged;
                this.UpdateMappingPathEnabled();
            }
        }

        /// <summary>
        /// Invoked when behavior is detached from a view.
        /// </summary>
        /// <param name="sampleView">The sample view from which the behavior is detached.</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.kanban != null)
            {
                this.kanban.DragEnd -= this.OnCardDragEnd;
                this.kanban = null;
            }

            if (this.sortByComboBox != null)
            {
                this.sortByComboBox = null;
            }

            if (this.orderComboBox != null)
            {
                this.orderComboBox.SelectionChanged -= this.OnSortOrderComboBoxSelectionChanged;
                this.orderComboBox = null;
            }

            if (this.mappingPathComboBox != null)
            {
                this.mappingPathComboBox.SelectionChanged -= this.OnSortingMappingPathComboBoxSelectionChanged;
                this.mappingPathComboBox = null;
            }

            if (this.sortByComboBox != null)
            {
                this.sortByComboBox.SelectionChanged -= this.OnSortByComboBoxSelectionChanged;
                this.sortByComboBox = null;
            }
        }

        #endregion

        #region Property changed

        /// <summary>
        /// Occurs when the sortby field value is changed.
        /// </summary>
        /// /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnSortByComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            this.UpdateMappingPathEnabled();
            if (this.kanban == null || this.sortByComboBox == null || this.sortByComboBox.SelectedItem == null || sender is not SfComboBox)
            {
                return;
            }

            string selectedItem = this.sortByComboBox.SelectedItem.ToString() ?? string.Empty;
            switch (selectedItem)
            {
                case "Index":
                    this.kanban.SortingMappingPath = "Index";
                    break;

                case "Custom":
                    // Restore original value from XAML (fallback to "Index" if somehow null or empty)
                    var originalValue = string.IsNullOrEmpty(this.sortingMappingPathValue) ? "Index" : this.sortingMappingPathValue;
                    if (!string.Equals(this.kanban.SortingMappingPath, originalValue, StringComparison.Ordinal))
                    {
                        this.kanban.SortingMappingPath = originalValue;
                    }

                    break;

                case "ItemSource Order":
                    // Use item source order. So clearing SortingMappingPath value.
                    this.kanban.SortingMappingPath = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Occurs when the sorting order value is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnSortOrderComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as SfComboBox)?.SelectedItem?.ToString();
            if (this.kanban == null || selectedItem == null)
            {
                return;
            }

            if (Enum.TryParse<KanbanSortingOrder>(selectedItem.ToString(), out KanbanSortingOrder sortOrder))
            {
                this.kanban.SortingOrder = sortOrder;
            }
        }

        /// <summary>
        /// Occurs when the sorting mapping path value is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnSortingMappingPathComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as SfComboBox)?.SelectedItem?.ToString();
            if (this.kanban == null || string.IsNullOrEmpty(selectedItem))
            {
                return;
            }

            this.kanban.SortingMappingPath = selectedItem;
            this.sortingMappingPathValue = selectedItem;
        }

        /// <summary>
        /// Occurs when a card drag end event is completed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnCardDragEnd(object? sender, KanbanDragEndEventArgs e)
        {
            if (this.kanban == null || this.sortByComboBox == null || this.sortByComboBox.SelectedItem == null)
            {
                return;
            }

            // Update the card's progress when moving between specific columns
            this.UpdateProgressOnColumnChange(e);

            string selectedItem = this.sortByComboBox.SelectedItem.ToString() ?? string.Empty;
            if (selectedItem == "Index")
            {
                this.ApplySortingWithoutPositionChange(e);
            }
            else if (selectedItem == "Custom")
            {
                this.kanban.RefreshKanbanColumn();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Applies sorting without changing the position of the cards.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ApplySortingWithoutPositionChange(KanbanDragEndEventArgs eventArgs)
        {
            if (this.kanban == null || eventArgs.Data == null || eventArgs.TargetColumn?.Items == null || eventArgs.SourceColumn == null || (eventArgs.SourceColumn == eventArgs.TargetColumn && eventArgs.SourceIndex == eventArgs.TargetIndex))
            {
                return;
            }

            // Retrieve sorting configuration
            var sortMappingPath = kanban.SortingMappingPath;
            var sortingOrder = kanban.SortingOrder;

            // Extract and cast items from the target column
            var targetColumnItems = eventArgs.TargetColumn.Items is IList<object> items
                ? items.Cast<object>().ToList() : new List<object>();

            // Proceed only if sorting path is defined
            if (string.IsNullOrEmpty(sortMappingPath))
            {
                return;
            }

            // Sort items based on the sorting order
            if (targetColumnItems.Count > 0)
            {
                Func<object, object?> keySelector = item => this.GetPropertyInfo(item.GetType(), sortMappingPath);

                targetColumnItems = sortingOrder == KanbanSortingOrder.Ascending
                    ? targetColumnItems.OrderBy(item => keySelector(item) ?? 0).ToList()
                    : targetColumnItems.OrderByDescending(item => keySelector(item) ?? 0).ToList();
            }

            // Determine the index to insert the dragged card.
            int currentCardIndex = eventArgs.TargetIndex;
            if (currentCardIndex >= 0 && currentCardIndex <= targetColumnItems.Count)
            {
                targetColumnItems.Insert(currentCardIndex, eventArgs.Data);
            }
            else
            {
                targetColumnItems.Add(eventArgs.Data);
                currentCardIndex = targetColumnItems.Count - 1;
            }

            // Update index property of all items using smart positioning logic
            this.ApplySmartIndexUpdate(targetColumnItems, sortingOrder, currentCardIndex);
        }

        /// <summary>
        /// Method to get the property info for the specified property.
        /// </summary>
        /// <param name="type">The property type.</param>
        /// <param name="key">The property name.</param>
        /// <returns>The property info of the specified property.</returns>
        [UnconditionalSuppressMessage("Trimming", "IL2067", Justification = "Type is known to have a public parameterless constructor.")]
        private PropertyInfo? GetPropertyInfo(Type type, string key)
        {
            return this.GetPropertyInfoCustomType(type, key);
        }

        /// <summary>
        /// Method to get the property info for the specified property from the given type.
        /// </summary>
        /// <param name="type">The property type.</param>
        /// <param name="key">The property name.</param>
        /// <returns>The property info of the specified property.</returns>
        private PropertyInfo? GetPropertyInfoCustomType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type type, string key)
        {
            return type.GetProperty(key);
        }

        /// <summary>
        /// Updates index property with smart positioning based on sorting order.
        /// </summary>
        /// <param name="items">The list of items to update.</param>
        /// <param name="sortingOrder">The sorting order (Ascending/Descending).</param>
        /// <param name="currentCardIndex">The current position of the inserted card.</param>
        private void ApplySmartIndexUpdate(List<object> items, KanbanSortingOrder sortingOrder, int currentCardIndex)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            if (sortingOrder == KanbanSortingOrder.Ascending)
            {
                this.HandleAscendingIndexSorting(items, currentCardIndex);
                return;
            }

            this.HandleDescendingIndexSorting(items, currentCardIndex);
        }

        /// <summary>
        /// Method to handle ascending index sorting with smart positioning.
        /// </summary>
        /// <param name="items">The list of items to update.</param>
        /// <param name="currentCardIndex">The current card index.</param>
        private void HandleAscendingIndexSorting(List<object> items, int currentCardIndex)
        {
            int afterCardIndex = -1;
            int lastItemIndex = -1;

            // Get the index of the card after the insertion point
            if (currentCardIndex < items.Count - 1)
            {
                var afterCard = items[currentCardIndex + 1];
                afterCardIndex = GetCardIndex(afterCard) ?? -1;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    continue;
                }

                PropertyInfo? propertyInfo = this.GetPropertyInfo(item.GetType(), "Index");
                if (propertyInfo == null)
                {
                    continue;
                }

                int itemIndex = Convert.ToInt32(propertyInfo.GetValue(item) ?? 0);
                bool isFirstItem = i == 0;
                if (isFirstItem)
                {
                    // If the inserted card is at the beginning, assign a smart index
                    if (currentCardIndex == 0)
                    {
                        lastItemIndex = afterCardIndex > 1 ? afterCardIndex - 1 : 1;
                        propertyInfo.SetValue(item, lastItemIndex);
                    }
                    else
                    {
                        lastItemIndex = itemIndex;
                    }
                }
                else
                {
                    // Increment index for subsequent items
                    lastItemIndex++;
                    propertyInfo.SetValue(item, lastItemIndex);
                }
            }
        }

        /// <summary>
        /// Method to handle descending index sorting with smart positioning.
        /// </summary>
        /// <param name="items">The list of items to update.</param>
        /// <param name="currentCardIndex">The current card index.</param>
        private void HandleDescendingIndexSorting(List<object> items, int currentCardIndex)
        {
            int beforeCardIndex = -1;
            int lastItemIndex = -1;

            // Get the index of the card before the insertion point
            if (currentCardIndex > 0 && currentCardIndex < items.Count)
            {
                var cardBefore = items[currentCardIndex - 1];
                beforeCardIndex = GetCardIndex(cardBefore) ?? -1;
            }

            for (int i = items.Count - 1; i >= 0; i--)
            {
                var item = items[i];
                if (item == null)
                {
                    continue;
                }

                PropertyInfo? propertyInfo = this.GetPropertyInfo(item.GetType(), "Index");
                if (propertyInfo == null)
                {
                    continue;
                }

                int itemIndex = Convert.ToInt32(propertyInfo.GetValue(item) ?? 0);
                bool isLastItem = i == items.Count - 1;
                if (isLastItem)
                {
                    // If the inserted card is at the end, assign a smart index
                    if (currentCardIndex == items.Count - 1)
                    {
                        lastItemIndex = beforeCardIndex > 1 ? beforeCardIndex - 1 : 1;
                        propertyInfo.SetValue(item, lastItemIndex);
                    }
                    else
                    {
                        lastItemIndex = itemIndex;
                    }
                }
                else
                {
                    lastItemIndex++;
                    propertyInfo.SetValue(item, lastItemIndex);
                }
            }
        }

        /// <summary>
        /// Method to get the index property value from a card object.
        /// </summary>
        /// <param name="cardDetails">The card object.</param>
        /// <returns>The index value or null if not found.</returns>
        private int? GetCardIndex(object cardDetails)
        {
            if (cardDetails == null)
            {
                return null;
            }

            PropertyInfo? propertyInfo = this.GetPropertyInfo(cardDetails.GetType(), "Index");
            if (propertyInfo == null)
            {
                return null;
            }

            var indexValue = propertyInfo.GetValue(cardDetails);
            if (indexValue != null)
            {
                return Convert.ToInt32(indexValue);
            }

            return null;
        }

        /// <summary>
        /// Method to update card progress value on column changes.
        /// </summary>
        /// <param name="e">The drag end event args.</param>
        private void UpdateProgressOnColumnChange(KanbanDragEndEventArgs e)
        {
            if (e == null || e.Data is not CardDetails cardDetails || e.SourceColumn == null || e.TargetColumn == null)
            {
                return;
            }

            // Get source and target category from the column's categories
            string? sourceCategory = this.GetPrimaryCategoryValue(e.SourceColumn);
            string? targetCategory = this.GetPrimaryCategoryValue(e.TargetColumn);

            if (string.IsNullOrEmpty(sourceCategory) || string.IsNullOrEmpty(targetCategory) 
                || string.Equals(sourceCategory, targetCategory, StringComparison.Ordinal))
            {
                return;
            }

            if (string.Equals(sourceCategory, "Open", StringComparison.Ordinal) &&
                     string.Equals(targetCategory, "In Progress", StringComparison.Ordinal))
            {
                cardDetails.Progress = Math.Min(100, cardDetails.Progress + 30);
            }
            else if (string.Equals(sourceCategory, "Code Review", StringComparison.Ordinal) &&
                     string.Equals(targetCategory, "In Progress", StringComparison.Ordinal))
            {
                cardDetails.Progress = Math.Min(100, cardDetails.Progress - 25);
            }
            else if (targetCategory == "Code Review")
            {
                cardDetails.Progress = 90;
            }
            else if (targetCategory == "Done")
            {
                cardDetails.Progress = 100;
            }
            else if (targetCategory == "Open")
            {
                cardDetails.Progress = 0;
            }
        }

        /// <summary>
        /// Method to get the primary category value from a column.
        /// </summary>
        /// <param name="column">The kanban column.</param>
        /// <returns>The title value.</returns>
        private string? GetPrimaryCategoryValue(KanbanColumn column)
        {
            if (column?.Categories is IEnumerable<string> list)
            {
                return list.FirstOrDefault();
            }

            return column?.Title;
        }

        /// <summary>
        /// Method to enable or disable the mapping path combo box control.
        /// </summary>
        private void UpdateMappingPathEnabled()
        {
            if (this.orderComboBox == null || this.sortByComboBox == null || this.sortByComboBox.SelectedItem == null || this.mappingPathComboBox == null)
            {
                return;
            }

            var selectedItem = this.sortByComboBox.SelectedItem.ToString() ?? string.Empty;
            bool isIndex = string.Equals(selectedItem, "Index", StringComparison.Ordinal);
            bool isItemSourceOrder = string.Equals(selectedItem, "ItemSource Order", StringComparison.Ordinal);

            // Enable or disable the combo boxes based on the selected sort by option
            this.mappingPathComboBox.IsEnabled = !(isIndex || isItemSourceOrder);
            this.orderComboBox.IsEnabled = !isItemSourceOrder;
        }

        #endregion
    }
}