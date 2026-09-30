using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using DiagramConnector = Syncfusion.Maui.Diagram.Connector;
using DiagramSfDiagram = Syncfusion.Maui.Diagram.SfDiagram;
using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

public partial class Connectors : SampleView
{
	private ObservableCollection<string> connectorTypes = new();
	private ObservableCollection<string> decoratorShapes = new();
	private ObservableCollection<string> colors = new();
	private ObservableCollection<string> dashStyles = new();
	private DiagramSfDiagram? diagram;

    public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();
    public ObservableCollection<Connector> ConnectorCollection { get; } = new ObservableCollection<Connector>();

    public Connectors()
	{
        BindingContext = this;
		Nodes = new ObservableCollection<Node>();
		ConnectorCollection = new ObservableCollection<Connector>();
		InitializeComponent();
        CreateDiagram();
    }
    public void OnCreated(object? sender, EventArgs e)
    {
        _ = Diagram.FitToPage();
    }
    private void CreateDiagram()
	{
		diagram = Diagram;
		InitializeOptions();
		CreateSampleDiagram();
	}

	private void InitializeOptions()
	{
		// Connector Types
		connectorTypes.Add("Orthogonal");
		connectorTypes.Add("Straight");
		connectorTypeComboBox.ItemsSource = connectorTypes;
		connectorTypeComboBox.SelectedIndex = 0;
		connectorTypeComboBox.SelectionChanged += OnConnectorTypeChanged;

		// Decorator Shapes
		decoratorShapes.Add("None");
		decoratorShapes.Add("Arrow");
		decoratorShapes.Add("Circle");
		decoratorShapes.Add("Diamond");
		decoratorShapes.Add("OpenArrow");
		sourceDecoratorComboBox.ItemsSource = decoratorShapes;
		sourceDecoratorComboBox.SelectedIndex = 0;
		sourceDecoratorComboBox.SelectionChanged += OnSourceDecoratorChanged;

		targetDecoratorComboBox.ItemsSource = new ObservableCollection<string>(decoratorShapes);
		targetDecoratorComboBox.SelectedIndex = 1;
		targetDecoratorComboBox.SelectionChanged += OnTargetDecoratorChanged;

		// Colors
		colors.Add("Blue");
		colors.Add("Red");
		colors.Add("Green");
		colors.Add("Purple");
		colors.Add("Orange");
		lineColorComboBox.ItemsSource = colors;
		lineColorComboBox.SelectedIndex = 0;
		lineColorComboBox.SelectionChanged += OnLineColorChanged;

		// Dash Styles
		dashStyles.Add("Solid");
		dashStyles.Add("Dashed");
		dashStyles.Add("Dotted");
		dashStyles.Add("DashDot");
		dashStyleComboBox.ItemsSource = dashStyles;
		dashStyleComboBox.SelectedIndex = 0;
		dashStyleComboBox.SelectionChanged += OnDashStyleChanged;

		// Decorator Size
		decoratorSizeEntry.Value = 12;
		decoratorSizeEntry.ValueChanged += OnDecoratorSizeChanged;

		// Stroke Width
		strokeWidthEntry.Value = 2;
		strokeWidthEntry.ValueChanged += OnStrokeWidthChanged;
	}

	private void CreateSampleDiagram()
	{
		if (diagram != null)
		{
			// Create flowchart nodes
			var customerIssueNode = CreateNode("customerIssue", 300, 50, 150, 70, NodeBasicShapes.Rectangle, "#2196F3", "Customer Issue");
			var issueReviewNode = CreateNode("issueReview", 300, 170, 150, 70, NodeBasicShapes.Rectangle, "#1976D2", "Issue Review");
			var resolvedNode = CreateNode("resolved", 150, 310, 120, 70, NodeBasicShapes.Rectangle, "#4CAF50", "Resolved");
			var escalateNode = CreateNode("escalate", 450, 310, 120, 70, NodeBasicShapes.Rectangle, "#FF9800", "Escalate");
			var engineeringNode = CreateNode("engineering", 450, 450, 150, 70, NodeBasicShapes.Rectangle, "#F44336", "Engineering");

			Nodes.Add(customerIssueNode);
			Nodes.Add(issueReviewNode);
			Nodes.Add(resolvedNode);
			Nodes.Add(escalateNode);
			Nodes.Add(engineeringNode);

			// Create connectors for flowchart
			var connector1 = CreateConnector("customerIssue", "issueReview", ConnectorSegmentType.Orthogonal, DecoratorShape.None, DecoratorShape.Arrow);
			ConnectorCollection.Add(connector1);

			var connector2 = CreateConnector("issueReview", "resolved", ConnectorSegmentType.Orthogonal, DecoratorShape.None, DecoratorShape.Arrow);
			ConnectorCollection.Add(connector2);

			var connector3 = CreateConnector("issueReview", "escalate", ConnectorSegmentType.Orthogonal, DecoratorShape.None, DecoratorShape.Arrow);
			ConnectorCollection.Add(connector3);

			var connector4 = CreateConnector("escalate", "engineering", ConnectorSegmentType.Orthogonal, DecoratorShape.None, DecoratorShape.Arrow);
			ConnectorCollection.Add(connector4);

			_ = diagram.FitToPage();
		}
	}

	private static Node CreateNode(string id, double offsetX, double offsetY, double width, double height, NodeBasicShapes shape, string fill, string label)
	{
		var node = new Node
		{
			Id = id,
			OffsetX = offsetX,
			OffsetY = offsetY,
			Width = width,
			Height = height,
			Shape = new BasicShape { Shape = shape },
		};

		node.Style.Fill = Color.FromArgb(fill);
		node.Style.StrokeColor = Color.FromArgb("#000000");
		node.Style.StrokeWidth = 1;

		var annotation = new ShapeAnnotation(label);
		annotation.Style.Color = Colors.White;
		annotation.Style.FontSize = 17;
		node.Annotations.Add(annotation);

		return node;
	}

	private static DiagramConnector CreateConnector(string sourceId, string targetId, ConnectorSegmentType type, DecoratorShape sourceShape, DecoratorShape targetShape)
	{
		var connector = new DiagramConnector
		{
			SourceID = sourceId,
			TargetID = targetId,
			Type = type,
		};

		connector.Style.StrokeColor = Color.FromArgb("#1565C0");
		connector.Style.StrokeWidth = 2;

		connector.SourceDecorator = new DecoratorSettings { Shape = sourceShape, Width = 12, Height = 12 };
		connector.TargetDecorator = new DecoratorSettings { Shape = targetShape, Width = 12, Height = 12 };

		return connector;
	}

	private void OnConnectorTypeChanged(object? sender, EventArgs e)
	{
		if (diagram != null && connectorTypeComboBox.SelectedIndex >= 0)
		{
			var selectedType = connectorTypeComboBox.SelectedIndex switch
			{
				0 => ConnectorSegmentType.Orthogonal,
				1 => ConnectorSegmentType.Straight,
				_ => ConnectorSegmentType.Orthogonal
			};

			foreach (var connector in diagram.Connectors.Take(3))
			{
				connector.Type = selectedType;
			}
		}
	}

	private void OnSourceDecoratorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && sourceDecoratorComboBox.SelectedIndex >= 0)
		{
			var selectedShape = sourceDecoratorComboBox.SelectedIndex switch
			{
				0 => DecoratorShape.None,
				1 => DecoratorShape.Arrow,
				2 => DecoratorShape.Circle,
				3 => DecoratorShape.Diamond,
				4 => DecoratorShape.OpenArrow,
				_ => DecoratorShape.None
			};

			var size = (int)(decoratorSizeEntry.Value ?? 12);

			foreach (var connector in diagram.Connectors)
			{
				if (connector.SourceDecorator == null)
					connector.SourceDecorator = new DecoratorSettings();
				connector.SourceDecorator.Shape = selectedShape;
				connector.SourceDecorator.Width = size;
				connector.SourceDecorator.Height = size;
			}
		}
	}

	private void OnTargetDecoratorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && targetDecoratorComboBox.SelectedIndex >= 0)
		{
			var selectedShape = targetDecoratorComboBox.SelectedIndex switch
			{
				0 => DecoratorShape.None,
				1 => DecoratorShape.Arrow,
				2 => DecoratorShape.Circle,
				3 => DecoratorShape.Diamond,
				4 => DecoratorShape.OpenArrow,
				_ => DecoratorShape.Arrow
			};

			var size = (int)(decoratorSizeEntry.Value ?? 12);

			foreach (var connector in diagram.Connectors)
			{
				if (connector.TargetDecorator == null)
					connector.TargetDecorator = new DecoratorSettings();
				connector.TargetDecorator.Shape = selectedShape;
				connector.TargetDecorator.Width = size;
				connector.TargetDecorator.Height = size;
			}
		}
	}

	private void OnDecoratorSizeChanged(object? sender, EventArgs e)
	{
		if (diagram != null)
		{
			var size = (int)(decoratorSizeEntry.Value ?? 12);

			foreach (var connector in diagram.Connectors)
			{
				if (connector.SourceDecorator != null)
				{
					connector.SourceDecorator.Width = size;
					connector.SourceDecorator.Height = size;
				}

				if (connector.TargetDecorator != null)
				{
					connector.TargetDecorator.Width = size;
					connector.TargetDecorator.Height = size;
				}
			}
		}
	}

	private void OnStrokeWidthChanged(object? sender, EventArgs e)
	{
		if (diagram != null)
		{
			var width = (double)(strokeWidthEntry.Value ?? 2);

			foreach (var connector in diagram.Connectors)
			{
				connector.Style.StrokeWidth = width;
			}
		}
	}

	private void OnLineColorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && lineColorComboBox.SelectedIndex >= 0)
		{
			var color = lineColorComboBox.SelectedIndex switch
			{
				0 => Color.FromArgb("#1565C0"),
				1 => Color.FromArgb("#D32F2F"),
				2 => Color.FromArgb("#388E3C"),
				3 => Color.FromArgb("#7B1FA2"),
				4 => Color.FromArgb("#F57C00"),
				_ => Color.FromArgb("#1565C0")
			};

			foreach (var connector in diagram.Connectors)
			{
				connector.Style.StrokeColor = color;
			}
		}
	}

	private void OnDashStyleChanged(object? sender, EventArgs e)
	{
		if (diagram != null && dashStyleComboBox.SelectedIndex >= 0)
		{
			var dashArray = dashStyleComboBox.SelectedIndex switch
			{
				0 => null, // Solid
				1 => "5,5", // Dashed
				2 => "2,2", // Dotted
				3 => "5,5,2,5", // DashDot
				_ => null
			};

			foreach (var connector in diagram.Connectors)
			{
				connector.Style.StrokeDashArray = dashArray;
			}
		}
	}
}