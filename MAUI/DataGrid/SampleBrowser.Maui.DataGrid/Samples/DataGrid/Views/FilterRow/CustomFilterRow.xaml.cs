using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Inputs;
using System.Collections.ObjectModel;
using System.Collections;


namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class CustomFilterRow : SampleView
{
    public CustomFilterRow()
    {
        InitializeComponent();

        dataGrid.FilterRowCellRenderers.Add("IntervalRenderer", new NumericIntervalFilterRowRenderer());
        dataGrid.FilterRowCellRenderers.Add("RangeRenderer", new StartsWithRangeFilterRowRenderer());
        dataGrid.FilterRowCellRenderers.Add("DateRenderer", new DateRelativeFilterRowRenderer());
        dataGrid.FilterRowCellRenderers.Add("TextOnlyRenderer", new TextOnlyFilterRowRenderer());
    }
}

public class TextOnlyFilterRowRenderer : DataGridFilterRowTextBoxRenderer
{
    protected override void OnInitializeDisplayView(DataColumnBase dataColumn, SfDataGridContentView? view)
    {
        if (dataColumn?.ColumnElement is not DataGridFilterRowCell cellElement || view == null)
            return;

        var label = new SfDataGridFilterRowLabel();
        var textBinding = new Binding
        {
            Path = "FilterRowText",
            Source = dataColumn.DataGridColumn,
            Mode = BindingMode.Default
        };
        label.SetBinding(SfDataGridFilterRowLabel.TextProperty, textBinding);
        label.SetBinding(SfDataGridFilterRowLabel.PaddingProperty, new Binding { Path = "CellPadding", Source = dataColumn.DataGridColumn });
        label.SetBinding(SfDataGridFilterRowLabel.HorizontalTextAlignmentProperty, new Binding { Path = "CellTextAlignment", Source = dataColumn.DataGridColumn });
        label.LineBreakMode = LineBreakMode.NoWrap;

        UpdateViewCellStyle(dataColumn, label);

        var container = new Grid();
        container.Children.Add(label);
        Grid.SetRow(label, 0);
        Grid.SetColumn(label, 0);
        view.Content = container;
    }

    public override void OnInitializeEditView(DataColumnBase dataColumn, Syncfusion.Maui.DataGrid.SfDataGridContentView? view)
    {
        base.OnInitializeEditView(dataColumn, view);

        if (dataColumn?.ColumnElement is not DataGridFilterRowCell cellElement)
            return;

        if (view?.Content is Layout layout && layout.Children.Count > 1)
        {
            while (layout.Children.Count > 1)
                layout.Children.RemoveAt(1);
        }

        dataColumn?.DataGridColumn?.SetValue(DataGridColumn.ImmediateUpdateColumnFilterProperty, true);
        if (dataColumn?.DataGridColumn != null && dataColumn.DataGridColumn.FilterRowCondition == FilterRowCondition.Null)
            dataColumn.DataGridColumn.FilterRowCondition = FilterRowCondition.Contains;
    }

    public override void ProcessSingleFilter(object? filterValue)
    {
        if (DataGrid == null || FilterRowCell?.DataColumn == null)
            return;

        var column = FilterRowCell.DataColumn.DataGridColumn;
        if (column == null)
            return;

        var text = filterValue?.ToString() ?? string.Empty;
        var predicates = new List<Syncfusion.Maui.Data.FilterPredicate>();

        if (!string.IsNullOrEmpty(text))
        {
            predicates.Add(new Syncfusion.Maui.Data.FilterPredicate
            {
                FilterBehavior = FilterBehavior.StringTyped,
                FilterType = FilterType.Contains,
                IsCaseSensitive = column.IsCaseSensitiveFilterRow,
                PredicateType = PredicateType.And,
                FilterMode = ColumnFilter.Value,
                FilterValue = text
            });
        }
        ApplyFilters(predicates, text);
    }
}

public class NumericIntervalFilterRowRenderer : DataGridFilterRowMultiSelectRenderer
{
    private static readonly ObservableCollection<object> IntervalItems = new()
    {
        "Between 1001 and 3000",
        "Between 3001 and 5000",
        "Between 5001 and 7000",
        ">7001",
    };

    protected override void PopulateComboBoxItems(SfComboBox comboBox, DataGridColumn column)
    {
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.IsEditable = false;

        comboBox.DropdownWidth = 200;
        comboBox.ItemsSource = new ObservableCollection<object>(IntervalItems);
        comboBox.IsClearButtonVisible = true;
    }

    protected override object? FindMatchingItemInSource(IEnumerable itemsSource, object filterValue, DataGridColumn column)
    {
        if (filterValue is string s)
            return itemsSource.Cast<object>().FirstOrDefault(i => Equals(i, s));

        if (filterValue is IConvertible)
        {
            var val = Convert.ToInt32(filterValue);
            if (val == 7001)
                return itemsSource.Cast<object>().FirstOrDefault(i => Equals(i, ">7001"));
        }
        return base.FindMatchingItemInSource(itemsSource, filterValue, column);
    }

    protected override void SetSelectedItemsFromFilterPredicates(SfComboBox comboBox, List<FilterPredicate> filterPredicates, DataGridColumn column)
    {
        if (comboBox == null || filterPredicates == null || filterPredicates.Count == 0)
            return;

        var chips = ResolveChips(filterPredicates);
        if (chips.Count == 0)
            return;

        var items = (comboBox.ItemsSource as IEnumerable)?.Cast<object>()?.ToList() ?? new List<object>();
        if (items.Count == 0)
            items = new List<object>(IntervalItems);

        var selected = new List<object>();
        foreach (var chip in chips)
        {
            var match = items.FirstOrDefault(i => Equals(i, chip)) ?? chip;
            selected.Add(match);
        }
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.SelectedItems = new ObservableCollection<object>(selected);
    }

    private static List<string> ResolveChips(List<FilterPredicate> predicates)
    {
        var chips = new List<string>();

        static void TryAddRangeChip(List<string> list, object? lower, object? upper)
        {
            if (lower is IConvertible && upper is IConvertible)
            {
                int min = Convert.ToInt32(lower);
                int max = Convert.ToInt32(upper);
                if (min == 1001 && max == 3000) list.Add("Between 1001 and 3000");
                else if (min == 3001 && max == 5000) list.Add("Between 3001 and 5000");
                else if (min == 5001 && max == 7000) list.Add("Between 5001 and 7000");
            }
        }

        for (int i = 0; i < predicates.Count; i++)
        {
            var p = predicates[i];
            if (p.FilterType == FilterType.GreaterThan && i + 1 < predicates.Count && predicates[i + 1].FilterType == FilterType.LessThan)
            {
                var next = predicates[i + 1];
                TryAddRangeChip(chips, p.FilterValue, next.FilterValue);
                i++;
            }
            else if (p.FilterType == FilterType.GreaterThan && p.FilterValue is IConvertible)
            {
                int val = Convert.ToInt32(p.FilterValue);
                if (val == 7001 && !chips.Contains(">7001")) chips.Add(">7001");
            }
        }
        return chips;
    }

    public override List<FilterPredicate> GetFilterPredicates(object? filterValue)
    {
        var column = FilterRowCell?.DataColumn?.DataGridColumn;
        var predicates = new List<FilterPredicate>();
        if (column == null) return predicates;

        var behavior = column.GetFilterBehavior();
        var selected = (filterValue as IEnumerable<object>)?.Cast<string>()?.ToList();
        if (selected == null || selected.Count == 0) return predicates;

        void AddRange(int minExclusive, int maxExclusive, bool firstGroup)
        {
            predicates.Add(new FilterPredicate
            {
                FilterBehavior = behavior,
                FilterType = FilterType.GreaterThan,
                PredicateType = firstGroup ? PredicateType.And : PredicateType.Or,
                FilterValue = minExclusive,
                IsCaseSensitive = column.IsCaseSensitiveFilterRow
            });
            predicates.Add(new FilterPredicate
            {
                FilterBehavior = behavior,
                FilterType = FilterType.LessThan,
                PredicateType = PredicateType.And,
                FilterValue = maxExclusive,
                IsCaseSensitive = column.IsCaseSensitiveFilterRow
            });
        }

        void AddGreaterThan(int threshold, bool firstGroup)
        {
            predicates.Add(new FilterPredicate
            {
                FilterBehavior = behavior,
                FilterType = FilterType.GreaterThan,
                PredicateType = firstGroup ? PredicateType.And : PredicateType.Or,
                FilterValue = threshold,
                IsCaseSensitive = column.IsCaseSensitiveFilterRow
            });
        }

        var first = true;
        foreach (var chip in selected)
        {
            switch (chip)
            {
                case "Between 1001 and 3000":
                    AddRange(1001, 3000, first);
                    break;
                case "Between 3001 and 5000":
                    AddRange(3001, 5000, first);
                    break;
                case "Between 5001 and 7000":
                    AddRange(5001, 7000, first);
                    break;
                case ">7001":
                    AddGreaterThan(7001, first);
                    break;
            }
            first = false;
        }

        return predicates;
    }

    public override string GetFilterText(List<FilterPredicate> filterPredicates)
    {
        var value = GetControlValue();
        var chips = (value as IEnumerable<object>)?.Cast<string>()?.ToList() ?? new List<string>();
        return chips.Count > 0 ? string.Join(" - ", chips) : string.Empty;
    }

    protected override void UpdateDisplayText(SfDataGridFilterRowLabel label, DataGridColumn column)
    {
        if (label == null || column == null)
            return;

        var predicates = column.FilterPredicates?.ToList();
        if (predicates == null || predicates.Count == 0)
        {
            label.Text = string.Empty;
            return;
        }

        var chips = ResolveChips(predicates);
        label.Text = chips.Count > 0 ? string.Join("/", chips) : string.Empty;
    }
}

public class StartsWithRangeFilterRowRenderer : DataGridFilterRowMultiSelectRenderer
{
    private static readonly (string Label, char From, char To)[] Buckets =
    {
        ("A to E", 'A','E'), ("F to J",'F','J'), ("K to O",'K','O'), ("P to T",'P','T'), ("U to Z",'U','Z')
    };

    private static readonly ObservableCollection<object> Items = new(Buckets.Select(b => (object)b.Label).ToList());

    protected override void PopulateComboBoxItems(SfComboBox comboBox, DataGridColumn column)
    {
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.IsEditable = false;
        comboBox.ItemsSource = Items;
    }

    protected override void InitializeEditBinding(SfComboBox comboBox, DataColumnBase dataColumn)
    {
        base.InitializeEditBinding(comboBox, dataColumn);
        ApplyBucketChipsFromPredicates(comboBox, dataColumn?.DataGridColumn);
    }

    protected override void SetSelectedItemsFromFilterPredicates(SfComboBox comboBox, List<FilterPredicate> filterPredicates, DataGridColumn column)
    {
        ApplyBucketChipsFromPredicates(comboBox, column);
    }

    private static void ApplyBucketChipsFromPredicates(SfComboBox comboBox, DataGridColumn? column)
    {
        var preds = column?.FilterPredicates?.ToList();
        if (preds == null || preds.Count == 0)
            return;

        var letters = new HashSet<char>();
        foreach (var p in preds)
        {
            if (p.FilterType == FilterType.StartsWith && p.FilterValue is string s && s.Length > 0)
                letters.Add(char.ToUpperInvariant(s[0]));
        }

        var selectedLabels = new List<string>();
        foreach (var b in Buckets)
        {
            for (char c = b.From; c <= b.To; c++)
            {
                if (letters.Contains(c))
                {
                    selectedLabels.Add(b.Label);
                    break;
                }
            }
        }

        if (selectedLabels.Count == 0)
            return;

        var items = (comboBox.ItemsSource as IEnumerable)?.Cast<object>()?.ToList() ?? new List<object>();
        var selectedInstances = new List<object>();
        foreach (var lab in selectedLabels)
        {
            var match = items.FirstOrDefault(i => Equals(i, lab)) ?? lab;
            selectedInstances.Add(match);
        }
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.SelectedItems = new ObservableCollection<object>(selectedInstances);
    }

    public override List<FilterPredicate> GetFilterPredicates(object? filterValue)
    {
        var column = FilterRowCell?.DataColumn?.DataGridColumn;
        var preds = new List<FilterPredicate>();
        if (column == null) return preds;

        var selected = (filterValue as IEnumerable<object>)?.Cast<string>()?.ToList();
        if (selected == null || selected.Count == 0) return preds;

        var buckets = Buckets;

        bool first = true;
        foreach (var label in selected)
        {
            var b = buckets.FirstOrDefault(x => x.Label == label);
            if (string.IsNullOrEmpty(b.Label)) continue;

            for (char c = b.From; c <= b.To; c++)
            {
                preds.Add(new FilterPredicate
                {
                    FilterBehavior = FilterBehavior.StringTyped,
                    FilterType = FilterType.StartsWith,
                    PredicateType = first ? PredicateType.And : PredicateType.Or,
                    FilterValue = c.ToString(),
                    IsCaseSensitive = false,
                });
                first = false;
            }
        }
        return preds;
    }

    protected override void UpdateDisplayText(SfDataGridFilterRowLabel label, DataGridColumn column)
    {
        if (label == null || column == null)
            return;

        var preds = column.FilterPredicates?.ToList();
        if (preds == null || preds.Count == 0)
        {
            label.Text = string.Empty;
            return;
        }

        var letters = new HashSet<char>();
        foreach (var p in preds)
        {
            if (p.FilterType == FilterType.StartsWith && p.FilterValue is string s && s.Length > 0)
                letters.Add(char.ToUpperInvariant(s[0]));
        }

        var labels = new List<string>();
        foreach (var b in Buckets)
        {
            for (char c = b.From; c <= b.To; c++)
            {
                if (letters.Contains(c))
                {
                    labels.Add(b.Label);
                    break;
                }
            }
        }

        label.Text = labels.Count > 0 ? string.Join("/", labels) : string.Empty;
    }
}
public class DateRelativeFilterRowRenderer : DataGridFilterRowMultiSelectRenderer
{
    protected override void PopulateComboBoxItems(SfComboBox comboBox, DataGridColumn column)
    {
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.IsEditable = false;
        comboBox.MaxDropDownHeight = 320;
        comboBox.ItemsSource = BuildRelativeDayItems();
    }

    protected override void InitializeEditBinding(SfComboBox comboBox, DataColumnBase dataColumn)
    {
        base.InitializeEditBinding(comboBox, dataColumn);
        ApplyDateChipsFromPredicates(comboBox, dataColumn?.DataGridColumn);
    }

    protected override void SetSelectedItemsFromFilterPredicates(SfComboBox comboBox, List<FilterPredicate> filterPredicates, DataGridColumn column)
    {
        ApplyDateChipsFromPredicates(comboBox, column);
    }

    private static void ApplyDateChipsFromPredicates(SfComboBox comboBox, DataGridColumn? column)
    {
        var preds = column?.FilterPredicates?.ToList();
        if (preds == null || preds.Count == 0)
            return;

        var today = DateTime.Today;
        var week = GetCurrentWeekDates(today);
        var startThisWeek = week[DayOfWeek.Monday];
        var firstDayThisMonth = new DateTime(today.Year, today.Month, 1);
        var firstDayLastMonth = firstDayThisMonth.AddMonths(-1);

        var labels = new List<string>();
        for (int i = 0; i < preds.Count; i++)
        {
            var p = preds[i];
            if (p.FilterType == FilterType.GreaterThanOrEqual && i + 1 < preds.Count && preds[i + 1].FilterType == FilterType.LessThan)
            {
                var start = ConvertToDate(preds[i].FilterValue);
                var end = ConvertToDate(preds[i + 1].FilterValue);
                if (start == today.AddDays(-1) && end == today) labels.Add("Yesterday");
                else if (start == today && end == today.AddDays(1)) labels.Add("Today");
                else if (start == startThisWeek.AddDays(-7) && end == startThisWeek) labels.Add("LastWeek");
                else if (start == firstDayLastMonth && end == firstDayThisMonth) labels.Add("LastMonth");
                i++;
            }
            else if (p.FilterType == FilterType.LessThan)
            {
                var end = ConvertToDate(p.FilterValue);
                if (end == firstDayLastMonth) labels.Add("Older");
            }
        }

        if (labels.Count == 0)
            return;

        var items = (comboBox.ItemsSource as IEnumerable)?.Cast<object>()?.ToList() ?? new List<object>();
        var selectedInstances = labels.Distinct().Select(l => items.FirstOrDefault(i => Equals(i, l)) ?? l).ToList();
        comboBox.SelectionMode = ComboBoxSelectionMode.Multiple;
        comboBox.SelectedItems = new ObservableCollection<object>(selectedInstances);
    }

    private static DateTime? ConvertToDate(object? value)
    {
        if (value is DateTime dt) return dt;
        if (value is string s && DateTime.TryParse(s, out var p)) return p;
        return null;
    }

    public override List<FilterPredicate> GetFilterPredicates(object? filterValue)
    {
        var predicates = new List<FilterPredicate>();
        var selected = (filterValue as IEnumerable<object>)?.Cast<string>()?.ToList();
        if (selected == null || selected.Count == 0)
            return predicates;

        var today = DateTime.Today;
        var week = GetCurrentWeekDates(today);
        var startThisWeek = week[DayOfWeek.Monday];
        var firstDayThisMonth = new DateTime(today.Year, today.Month, 1);
        var firstDayLastMonth = firstDayThisMonth.AddMonths(-1);

        bool first = true;
        foreach (var label in selected)
        {
            if (label == "Today")
            {
                AddDateRange(predicates, today, today.AddDays(1), first);
            }
            else if (label == "Yesterday")
            {
                AddDateRange(predicates, today.AddDays(-1), today, first);
            }
            else if (label == "LastWeek")
            {
                AddDateRange(predicates, startThisWeek.AddDays(-7), startThisWeek, first);
            }
            else if (label == "LastMonth")
            {
                AddDateRange(predicates, firstDayLastMonth, firstDayThisMonth, first);
            }
            else if (label == "Older")
            {
                predicates.Add(new FilterPredicate
                {
                    FilterType = FilterType.LessThan,
                    PredicateType = first ? PredicateType.And : PredicateType.Or,
                    FilterValue = firstDayLastMonth
                });
            }
            else
            {
                continue;
            }
            first = false;
        }

        return predicates;
    }

    public override string GetFilterText(List<FilterPredicate> filterPredicates)
    {
        var value = GetControlValue();
        var chips = (value as IEnumerable<object>)?.Cast<string>()?.ToList() ?? new List<string>();
        return chips.Count > 0 ? string.Join(" - ", chips) : string.Empty;
    }

    protected override void UpdateDisplayText(SfDataGridFilterRowLabel label, DataGridColumn column)
    {
        if (label == null || column == null)
            return;

        var items = (GetControlValue() as IEnumerable<object>)?.Cast<string>()?.ToList();
        if (items != null && items.Count > 0)
        {
            label.Text = string.Join("/", items);
            return;
        }

        var list = column.FilterPredicates?.ToList();
        if (list == null || list.Count == 0)
        {
            label.Text = string.Empty;
            return;
        }

        var tmpCombo = new SfComboBox { ItemsSource = BuildRelativeDayItems() };
        ApplyDateChipsFromPredicates(tmpCombo, column);
        var sel = tmpCombo.SelectedItems?.Cast<object>()?.Select(o => o.ToString()).Where(s => !string.IsNullOrEmpty(s)).ToList() ?? [];
        label.Text = sel.Count > 0 ? string.Join("/", sel) : string.Empty;
    }

    static void AddDateRange(List<FilterPredicate> list, DateTime start, DateTime end, bool first)
    {
        list.Add(new FilterPredicate
        {
            FilterType = FilterType.GreaterThanOrEqual,
            PredicateType = first ? PredicateType.And : PredicateType.Or,
            FilterValue = start
        });
        list.Add(new FilterPredicate
        {
            FilterType = FilterType.LessThan,
            PredicateType = PredicateType.And,
            FilterValue = end
        });
    }

    static Dictionary<DayOfWeek, DateTime> GetCurrentWeekDates(DateTime today)
    {
        int delta = ((int)today.DayOfWeek + 6) % 7;
        var monday = today.Date.AddDays(-delta);

        var dict = new Dictionary<DayOfWeek, DateTime>();
        for (int i = 0; i < 7; i++)
        {
            var d = monday.AddDays(i);
            dict[d.DayOfWeek] = d;
        }
        return dict;
    }

    static ObservableCollection<object> BuildRelativeDayItems()
    {
        // Only show the required chips: Today, Yesterday, LastWeek, LastMonth, Older
        var items = new List<string>
        {
            "Today",
            "Yesterday",
            "LastWeek",
            "LastMonth",
            "Older"
        };
        return new ObservableCollection<object>(items.Cast<object>().ToList());
    }
}