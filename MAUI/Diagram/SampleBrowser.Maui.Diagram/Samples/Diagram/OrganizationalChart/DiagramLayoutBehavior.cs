using System.Diagnostics;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

/// <summary>
/// Runs <see cref="Syncfusion.Maui.Diagram.SfDiagram.DoLayoutAsync"/> when the diagram loads and
/// whenever the bound <see cref="LayoutRequestId"/> changes, so view models never touch the control.
/// </summary>
public sealed class DiagramLayoutBehavior : Behavior<Syncfusion.Maui.Diagram.SfDiagram>, IDisposable
{
    public static readonly BindableProperty LayoutRequestIdProperty =
        BindableProperty.Create(nameof(LayoutRequestId), typeof(int), typeof(DiagramLayoutBehavior), 0,
            propertyChanged: (b, _, _) => ((DiagramLayoutBehavior)b).RequestLayout());

    private readonly SemaphoreSlim semaphore = new(1, 1);
    private Syncfusion.Maui.Diagram.SfDiagram? diagram;
    private bool isLoaded;

    public int LayoutRequestId
    {
        get => (int)GetValue(LayoutRequestIdProperty);
        set => SetValue(LayoutRequestIdProperty, value);
    }

    protected override void OnAttachedTo(Syncfusion.Maui.Diagram.SfDiagram bindable)
    {
        ArgumentNullException.ThrowIfNull(bindable);
        base.OnAttachedTo(bindable);
        diagram = bindable;
        BindingContext = bindable.BindingContext;
        bindable.BindingContextChanged += OnBindingContextChanged;
        bindable.Created += OnDiagramCreated;
        bindable.Loaded += OnDiagramLoaded;
    }

    protected override void OnDetachingFrom(Syncfusion.Maui.Diagram.SfDiagram bindable)
    {
        ArgumentNullException.ThrowIfNull(bindable);
        bindable.BindingContextChanged -= OnBindingContextChanged;
        bindable.Created -= OnDiagramCreated;
        bindable.Loaded -= OnDiagramLoaded;
        diagram = null;
        base.OnDetachingFrom(bindable);
    }

    public void Dispose() => semaphore.Dispose();

    private void OnBindingContextChanged(object? sender, EventArgs e) =>
        BindingContext = (sender as BindableObject)?.BindingContext;

    private void OnDiagramCreated(object? sender, EventArgs e)
    {
        if (diagram != null)
            _ = diagram.FitToPage();
    }

    private void OnDiagramLoaded(object? sender, EventArgs e)
    {
        if (isLoaded)
            return;

        isLoaded = true;
        RequestLayout();
    }

    private void RequestLayout()
    {
        if (isLoaded)
            _ = RunLayoutAsync();
    }

    private async Task RunLayoutAsync()
    {
        await semaphore.WaitAsync();
        try
        {
            if (diagram != null)
            {
                await diagram.DoLayoutAsync();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Diagram layout failed: {ex}");
        }
        finally
        {
            semaphore.Release();
        }
    }
}
