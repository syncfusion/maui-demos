#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart;

public class TrendlineViewModel : INotifyPropertyChanged
{
    public const string LinearType = "Linear";
    public const string ExponentialType = "Exponential";
    public const string PowerType = "Power";
    public const string PolynomialType = "Polynomial";
    public const string MovingAverageType = "MovingAvg";
    public const string LogarithmicType = "Logarithmic";

    private const int PolyMin = 2, PolyMax = 6;
    private const int PeriodMin = 2, PeriodMax = 6;

    private const int ForecastMin = 0, ForecastMax = 6;

    public ObservableCollection<QuarterRevenue> QuarterlyRevenue { get; } = new()
    {
        new QuarterRevenue(2018, 640),
        new QuarterRevenue(2019, 705),
        new QuarterRevenue(2020, 665),
        new QuarterRevenue(2021, 740),
        new QuarterRevenue(2022, 710),
        new QuarterRevenue(2023, 785),
        new QuarterRevenue(2024, 760),
        new QuarterRevenue(2025, 845)
    };

    public IList<string> TrendlineTypes { get; } =
        new List<string> { LinearType, ExponentialType, PowerType, PolynomialType, MovingAverageType, LogarithmicType };

    bool isTrendlineVisible = true;
    public bool IsTrendlineVisible
    {
        get => isTrendlineVisible;
        set => SetField(ref isTrendlineVisible, value);
    }

    bool showMarkers = true;
    public bool ShowMarkers
    {
        get => showMarkers;
        set => SetField(ref showMarkers, value);
    }

    bool enableTrendlineTooltip = true;
    public bool EnableTrendlineTooltip
    {
        get => enableTrendlineTooltip;
        set => SetField(ref enableTrendlineTooltip, value);
    }

    string selectedTrendlineType = LinearType;
    public string SelectedTrendlineType
    {
        get => selectedTrendlineType;
        set
        {
            if (SetField(ref selectedTrendlineType, value))
            {
                OnPropertyChanged(nameof(IsPolynomialVisible));
                OnPropertyChanged(nameof(IsMovingAverageVisible));
            }
        }
    }

    public bool IsPolynomialVisible => SelectedTrendlineType == PolynomialType;
    public bool IsMovingAverageVisible => SelectedTrendlineType == MovingAverageType;

    int polynomialOrder = 3;
    public int PolynomialOrder
    {
        get => polynomialOrder;
        set
        {
            if (SetField(ref polynomialOrder, Clamp(value, PolyMin, PolyMax)))
                RaiseStepperCanExecutes();
        }
    }

    int movingAveragePeriod = 4;
    public int MovingAveragePeriod
    {
        get => movingAveragePeriod;
        set
        {
            if (SetField(ref movingAveragePeriod, Clamp(value, PeriodMin, PeriodMax)))
                RaiseStepperCanExecutes();
        }
    }

    int forwardForecast;
    public int ForwardForecast
    {
        get => forwardForecast;
        set
        {
            if (SetField(ref forwardForecast, Clamp(value, ForecastMin, ForecastMax)))
                RaiseStepperCanExecutes();
        }
    }

    int backwardForecast;
    public int BackwardForecast
    {
        get => backwardForecast;
        set
        {
            if (SetField(ref backwardForecast, Clamp(value, ForecastMin, ForecastMax)))
                RaiseStepperCanExecutes();
        }
    }

    public ICommand IncrementForwardCommand { get; }
    public ICommand DecrementForwardCommand { get; }
    public ICommand IncrementBackwardCommand { get; }
    public ICommand DecrementBackwardCommand { get; }

    public ICommand IncrementPolynomialOrderCommand => incrementPolynomialOrderCommand;
    public ICommand DecrementPolynomialOrderCommand => decrementPolynomialOrderCommand;

    public ICommand IncrementMAPeriodCommand => incrementMAPeriodCommand;
    public ICommand DecrementMAPeriodCommand => decrementMAPeriodCommand;

    private readonly RelayCommand incrementPolynomialOrderCommand;
    private readonly RelayCommand decrementPolynomialOrderCommand;

    private readonly RelayCommand incrementMAPeriodCommand;
    private readonly RelayCommand decrementMAPeriodCommand;

    public TrendlineViewModel()
    {
        IncrementForwardCommand = new RelayCommand(() => ForwardForecast = Math.Min(ForwardForecast + 1, ForecastMax));
        DecrementForwardCommand = new RelayCommand(() => ForwardForecast = Math.Max(ForwardForecast - 1, ForecastMin));
        IncrementBackwardCommand = new RelayCommand(() => BackwardForecast = Math.Min(BackwardForecast + 1, ForecastMax));
        DecrementBackwardCommand = new RelayCommand(() => BackwardForecast = Math.Max(BackwardForecast - 1, ForecastMin));

        incrementPolynomialOrderCommand = new RelayCommand(
            () => PolynomialOrder += 1,
            () => PolynomialOrder < PolyMax);

        decrementPolynomialOrderCommand = new RelayCommand(
            () => PolynomialOrder -= 1,
            () => PolynomialOrder > PolyMin);

        incrementMAPeriodCommand = new RelayCommand(
            () => MovingAveragePeriod += 1,
            () => MovingAveragePeriod < PeriodMax);

        decrementMAPeriodCommand = new RelayCommand(
            () => MovingAveragePeriod -= 1,
            () => MovingAveragePeriod > PeriodMin);
    }

    private static int Clamp(int value, int min, int max) =>
        Math.Max(min, Math.Min(max, value));

    private static double Clamp(double value, double min, double max) =>
        Math.Max(min, Math.Min(max, value));

    private void RaiseStepperCanExecutes()
    {
        incrementPolynomialOrderCommand?.RaiseCanExecuteChanged();
        decrementPolynomialOrderCommand?.RaiseCanExecuteChanged();

        incrementMAPeriodCommand?.RaiseCanExecuteChanged();
        decrementMAPeriodCommand?.RaiseCanExecuteChanged();

    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    bool SetField<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public record QuarterRevenue(double Year, double Revenue);

public sealed class RelayCommand : ICommand
{
    readonly Action execute;
    readonly Func<bool>? canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => execute();

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
