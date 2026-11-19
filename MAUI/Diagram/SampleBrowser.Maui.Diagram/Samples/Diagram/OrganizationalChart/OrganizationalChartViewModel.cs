using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

/// <summary>Employee record used as the diagram data source.</summary>
public class OrganizationalEmployee : INotifyPropertyChanged
{
    private string name;
    private string title;
    private string color;

    public OrganizationalEmployee(string id, string name, string title, string? managerId, string color,
        OrganizationalRole role = OrganizationalRole.Normal)
    {
        Id = id;
        this.name = name;
        this.title = title;
        this.color = color;
        ManagerId = managerId;
        Role = role;
    }

    public string Id { get; }
    public string? ManagerId { get; }
    public OrganizationalRole Role { get; }
    public bool IsExpanded { get; set; } = true;
    private SubTreeOrientation? subTreeOrientation;
    private SubTreeAlignments? subTreeAlignment;
    public SubTreeOrientation? SubTreeOrientation { get => subTreeOrientation; set => Set(ref subTreeOrientation, value); }
    public SubTreeAlignments? SubTreeAlignment { get => subTreeAlignment; set => Set(ref subTreeAlignment, value); }

    public string Name { get => name; set => Set(ref name, value); }
    public string Title { get => title; set => Set(ref title, value); }
    public string Color { get => color; set => Set(ref color, value); }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class OrganizationalChartViewModel : INotifyPropertyChanged
{
    private static readonly Dictionary<string, (SubTreeOrientation? Orientation, SubTreeAlignments? Alignment)> SubtreeAlignments = new()
    {
        ["Vertical Left"] = (SubTreeOrientation.Vertical, SubTreeAlignments.Left),
        ["Vertical Right"] = (SubTreeOrientation.Vertical, SubTreeAlignments.Right),
        ["Vertical Alternate"] = (SubTreeOrientation.Vertical, SubTreeAlignments.Alternate),
        ["Horizontal Left"] = (SubTreeOrientation.Horizontal, SubTreeAlignments.Left),
        ["Horizontal Right"] = (SubTreeOrientation.Horizontal, SubTreeAlignments.Right),
        ["Horizontal Center"] = (SubTreeOrientation.Horizontal, SubTreeAlignments.Center),
        ["Horizontal Balanced"] = (SubTreeOrientation.Horizontal, SubTreeAlignments.Balanced),
    };

    private string subtreeAlignment = "Vertical Left";
    private string orientation;
    private double horizontalSpacing;
    private double verticalSpacing;
    private int layoutRequestId;
    private DiagramLayout autoLayout = null!;

    public OrganizationalChartViewModel()
    {
        Employees = new ObservableCollection<OrganizationalEmployee>(CreateEmployees());

        horizontalSpacing = DefaultHorizontalSpacing;
        verticalSpacing = DefaultVerticalSpacing;
        OrientationOptions = Enum.GetNames<LayoutOrientation>();
        orientation = OrientationOptions[0];

        // These are set up once; the diagram control binds to them from XAML.
        DataSourceSettings = CreateDataSourceSettings();
        NodeDefaults = CreateNodeDefaults();
        ConnectorDefaults = CreateConnectorDefaults();
        ApplySubtreeAlignment();
        autoLayout = CreateAutoLayout();
    }

    protected virtual IEnumerable<OrganizationalEmployee> CreateEmployees()
    {
        const string green = "#6BA81A";
        const string blue = "#1859B7";
        const string light = "#2E94DB";

        return new OrganizationalEmployee[]
        {
            new("board", "Board", "", null, green),
            new("gm", "General Manager", "", "board", green),
            new("agm", "Assistant General Manager", "", "gm", green, OrganizationalRole.Assistant),
            new("hr", "Human Resource Manager", "", "gm", blue),
            new("design", "Design Manager", "", "gm", blue),
            new("ops", "Operations Manager", "", "gm", blue),
            new("marketing", "Marketing Manager", "", "gm", blue),
            new("trainers", "Trainers", "", "hr", light),
            new("recruiting", "Recruiting Team", "", "hr", light),
            new("finance", "Finance Asst. Manager", "", "hr", light),
            new("design_sup", "Design Supervisor", "", "design", light),
            new("dev_sup", "Development Supervisor", "", "design", light),
            new("draft_sup", "Drafting Supervisor", "", "design", light),
            new("stats", "Statistics Department", "", "ops", light),
            new("logistics", "Logistics Department", "", "ops", light),
            new("overseas", "Overseas Sales Manager", "", "marketing", light),
            new("petroleum", "Petroleum Manager", "", "marketing", light),
            new("service", "Service Department Manager", "", "marketing", light),
            new("quality", "Quality Control Department", "", "marketing", light),
        };
    }
    protected virtual double DefaultHorizontalSpacing => 30;
    protected virtual double DefaultVerticalSpacing => 30;

    public ObservableCollection<OrganizationalEmployee> Employees { get; }
    public IList<string> OrientationOptions { get; }
    public IList<string> SubtreeAlignmentOptions { get; } = SubtreeAlignments.Keys.ToList();

    public string SubtreeAlignment
    {
        get => subtreeAlignment;
        set
        {
            if (string.IsNullOrEmpty(value) || !SubtreeAlignments.ContainsKey(value))
                return;

            if (Set(ref subtreeAlignment, value))
            {
                ApplySubtreeAlignment();
                RefreshLayout();
            }
        }
    }

    private void ApplySubtreeAlignment()
    {
        var (subOrientation, alignment) = SubtreeAlignments[subtreeAlignment];
        // Sub-tree arrangement applies to managers whose reports are all leaf nodes (e.g. department heads).
        var parents = Employees.Where(e => e.ManagerId != null).Select(e => e.ManagerId!).ToHashSet();

        foreach (var employee in Employees)
        {
            var children = Employees.Where(e => e.ManagerId == employee.Id && e.Role == OrganizationalRole.Normal).ToList();
            bool applies = children.Count > 0 && children.All(c => !parents.Contains(c.Id));
            employee.SubTreeOrientation = applies ? subOrientation : null;
            employee.SubTreeAlignment = applies ? alignment : null;
        }
    }
    public DataSourceSettings DataSourceSettings { get; }
    public NodeDefaults NodeDefaults { get; }
    public ConnectorDefaults ConnectorDefaults { get; }

    public DiagramLayout AutoLayout { get => autoLayout; private set => Set(ref autoLayout, value); }
    /// <summary>Incremented whenever the diagram must be laid out again; bound to <see cref="DiagramLayoutBehavior"/>.</summary>
    public int LayoutRequestId { get => layoutRequestId; private set => Set(ref layoutRequestId, value); }

    public string Orientation
    {
        get => orientation;
        set
        {
            // The combo box writes null back when the page is torn down or its items reset; ignore that.
            if (string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(LayoutOrientation), value))
                return;

            if (Set(ref orientation, value))
                RefreshLayout();
        }
    }

    public double HorizontalSpacing
    {
        get => horizontalSpacing;
        set { if (Set(ref horizontalSpacing, value)) RefreshLayout(); }
    }

    public double VerticalSpacing
    {
        get => verticalSpacing;
        set { if (Set(ref verticalSpacing, value)) RefreshLayout(); }
    }

    protected virtual DiagramLayoutType LayoutType => DiagramLayoutType.OrganizationalChart;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RefreshLayout()
    {
        AutoLayout = CreateAutoLayout();
        LayoutRequestId++;
    }

    private DataSourceSettings CreateDataSourceSettings()
    {
        var settings = new Syncfusion.Maui.Diagram.DataSourceSettings
        {
            DataSource = Employees,
            Id = nameof(OrganizationalEmployee.Id),
            ParentId = nameof(OrganizationalEmployee.ManagerId),
            IsExpandedPath = nameof(OrganizationalEmployee.IsExpanded),
            WriteBackExpandState = true,
            AdditionalDataPaths = new ObservableCollection<string>
            {
                nameof(OrganizationalEmployee.Name),
                nameof(OrganizationalEmployee.Title),
                nameof(OrganizationalEmployee.Color),
            },
        };

        if (LayoutType == DiagramLayoutType.OrganizationalChart)
        {
            settings.LayoutInfoMapping = new LayoutInfoMapping
            {
                Role = nameof(OrganizationalEmployee.Role),
                SubTreeOrientation = nameof(OrganizationalEmployee.SubTreeOrientation),
                SubTreeAlignment = nameof(OrganizationalEmployee.SubTreeAlignment),
            };
        }

        return settings;
    }

    private static NodeDefaults CreateNodeDefaults() => new()
    {
        Width = 110,
        Height = 40,
        Shape = NodeBasicShapes.Rectangle,
        CornerRadius = 0,
        Fill = Color.FromArgb("#2E94DB"),
        Stroke = Color.FromArgb("#2E94DB"),
        StrokeThickness = 1,
        FillPath = nameof(OrganizationalEmployee.Color),
        Annotations = new ObservableCollection<AnnotationMapping>
        {
            new()
            {
                TextPath = nameof(OrganizationalEmployee.Name),
                Offset = new Point(0.5, 0.5),
                TextColor = Colors.White,
                FontSize = 9,
                FontAttributes = FontAttributes.Bold,
            },
        },
    };
    private static ConnectorDefaults CreateConnectorDefaults() => new()
    {
        Type = ConnectorSegmentType.Orthogonal,
        CornerRadius = 0,
        Stroke = Colors.Black,
        StrokeThickness = 1,
        ShowTargetArrow = false,
    };

    private DiagramLayout CreateAutoLayout() => new()
    {
        Type = LayoutType,
        Root = Employees[0].Id,
        Orientation = Enum.TryParse<LayoutOrientation>(orientation, out var parsed) ? parsed : default,
        HorizontalSpacing = horizontalSpacing,
        VerticalSpacing = verticalSpacing,
        Margin = new Thickness(50),
    };

    private bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}