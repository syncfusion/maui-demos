using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using DiagramNode = Syncfusion.Maui.Diagram.Node;
using DiagramSfDiagram = Syncfusion.Maui.Diagram.SfDiagram;
using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

public partial class Nodes : SampleView
{
	private ObservableCollection<string> basicShapes = new();
	private ObservableCollection<string> flowShapes = new();
	private ObservableCollection<string> fillColors = new();
	private ObservableCollection<string> textColors = new();
	private ObservableCollection<string> strokeColors = new();
	private ObservableCollection<string> dashStyles = new();
	private Dictionary<string, string> shapeNames = new();
	private DiagramSfDiagram? diagram;
	private DiagramNode? sampleNode;

    public ObservableCollection<Node> NodeCollection { get; } = new ObservableCollection<Node>();

    public Nodes()
	{
        BindingContext = this;
        NodeCollection = new ObservableCollection<Node>();
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
		// Basic Shapes - All available basic shape types
		basicShapes.Add("Rectangle");
		basicShapes.Add("Ellipse");
		basicShapes.Add("Diamond");
		basicShapes.Add("Triangle");
		basicShapes.Add("Hexagon");
		basicShapes.Add("Parallelogram");
		basicShapes.Add("Plus");
		basicShapes.Add("Minus");
		basicShapes.Add("Polygon");
		basicShapeComboBox.ItemsSource = basicShapes;
		basicShapeComboBox.SelectedIndex = 4; // Set to Hexagon as default
		basicShapeComboBox.SelectionChanged += OnBasicShapeChanged;

		// Initialize shape names dictionary
		shapeNames.Add("Rectangle", "Rectangle");
		shapeNames.Add("Ellipse", "Ellipse");
		shapeNames.Add("Diamond", "Diamond");
		shapeNames.Add("Triangle", "Triangle");
		shapeNames.Add("Hexagon", "Hexagon");
		shapeNames.Add("Parallelogram", "Parallelogram");
		shapeNames.Add("Plus", "Plus");
		shapeNames.Add("Minus", "Minus");
		shapeNames.Add("Polygon", "Polygon");

		// Flow Shapes - Common flowchart shapes
		flowShapes.Add("Process");         // Maps to Rectangle
		flowShapes.Add("Decision");        // Maps to Diamond
		flowShapes.Add("Start/End");       // Maps to Ellipse
		flowShapes.Add("Input/Output");    // Maps to Parallelogram
		flowShapes.Add("Data");            // Maps to Parallelogram
		flowShapes.Add("Terminator");      // Maps to Plus
		flowShapes.Add("Connector");       // Maps to Triangle
		flowShapeComboBox.ItemsSource = flowShapes;
		flowShapeComboBox.SelectedIndex = 0; // Set to Process as default
		flowShapeComboBox.SelectionChanged += OnFlowShapeChanged;

		// Fill Colors
		fillColors.Add("Teal");
		fillColors.Add("Blue");
		fillColors.Add("Green");
		fillColors.Add("Red");
		fillColors.Add("Purple");
		fillColorComboBox.ItemsSource = fillColors;
		fillColorComboBox.SelectedIndex = 1; // Set to Blue as default
		fillColorComboBox.SelectionChanged += OnFillColorChanged;

		// Text Colors
		textColors.Add("White");
		textColors.Add("Black");
		textColors.Add("Yellow");
		textColorComboBox.ItemsSource = textColors;
		textColorComboBox.SelectedIndex = 0;
		textColorComboBox.SelectionChanged += OnTextColorChanged;

		// Stroke Colors
		strokeColors.Add("Black");
		strokeColors.Add("Gray");
		strokeColors.Add("Red");
		strokeColors.Add("Blue");
		strokeColors.Add("Green");
		strokeColorComboBox.ItemsSource = strokeColors;
		strokeColorComboBox.SelectedIndex = 0;
		strokeColorComboBox.SelectionChanged += OnStrokeColorChanged;

		// Dash Styles
		dashStyles.Add("Solid");
		dashStyles.Add("Dashed");
		dashStyles.Add("Dotted");
		dashStyleComboBox.ItemsSource = dashStyles;
		dashStyleComboBox.SelectedIndex = 0;
		dashStyleComboBox.SelectionChanged += OnDashStyleChanged;

		// Stroke Width
		strokeWidthEntry.Value = 2;
		strokeWidthEntry.ValueChanged += OnStrokeWidthChanged;

		// Node Width
		nodeWidthEntry.Value = 120;
		nodeWidthEntry.ValueChanged += OnNodeWidthChanged;

		// Node Height
		nodeHeightEntry.Value = 120;
		nodeHeightEntry.ValueChanged += OnNodeHeightChanged;
	}

	private void CreateSampleDiagram()
	{
		if (diagram != null)
		{
			// Create a sample node for demonstration with shape name as text
			sampleNode = CreateNode("sample", 300, 250, 120, 120, NodeBasicShapes.Hexagon, "#1976D2", "Hexagon");
			NodeCollection.Add(sampleNode);

			_ = diagram.FitToPage();
		}
	}

	private static DiagramNode CreateNode(string id, double offsetX, double offsetY, double width, double height, NodeBasicShapes shape, string fill, string label)
	{
		var node = new DiagramNode
		{
			Id = id,
			OffsetX = offsetX,
			OffsetY = offsetY,
			Width = width,
			Height = height,
			Shape = new BasicShape { Shape = shape },
		};

		node.Style.Fill = Color.FromArgb(fill);
		node.Style.StrokeColor = Color.FromArgb("#2C5F5D");
		node.Style.StrokeWidth = 2;

		var annotation = new ShapeAnnotation(label);
		annotation.Style.Color = Colors.White;
		annotation.Style.FontSize = 17;
		node.Annotations.Add(annotation);

		return node;
	}

	private void OnBasicShapeChanged(object? sender, EventArgs e)
	{
		if (diagram != null && basicShapeComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			var selectedShape = basicShapeComboBox.SelectedIndex switch
			{
				0 => NodeBasicShapes.Rectangle,
				1 => NodeBasicShapes.Ellipse,
				2 => NodeBasicShapes.Diamond,
				3 => NodeBasicShapes.Triangle,
				4 => NodeBasicShapes.Hexagon,
				5 => NodeBasicShapes.Parallelogram,
				6 => NodeBasicShapes.Plus,
				7 => NodeBasicShapes.Star,
				8 => NodeBasicShapes.Pentagon,
				_ => NodeBasicShapes.Rectangle
			};

			sampleNode.Shape = new BasicShape { Shape = selectedShape };

			// Update the node text to display the shape name
			if (sampleNode.Annotations.Count > 0)
			{
				var shapeName = basicShapeComboBox.SelectedItem?.ToString() ?? "Rectangle";
				sampleNode.Annotations[0].Content = shapeName;
			}
		}
	}

	private void OnFlowShapeChanged(object? sender, EventArgs e)
	{
		if (diagram != null && flowShapeComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			// Flow shapes mapping to available basic shapes
			var selectedShape = flowShapeComboBox.SelectedIndex switch
			{
				0 => NodeBasicShapes.Rectangle,      // Process
				1 => NodeBasicShapes.Diamond,        // Decision
				2 => NodeBasicShapes.Ellipse,        // Start/End
				3 => NodeBasicShapes.Parallelogram,  // Input/Output
				4 => NodeBasicShapes.Parallelogram,  // Data
				5 => NodeBasicShapes.Plus,           // Terminator
				6 => NodeBasicShapes.Triangle,       // Connector
				_ => NodeBasicShapes.Rectangle
			};

			sampleNode.Shape = new BasicShape { Shape = selectedShape };

			// Update the node text to display the flow shape name
			if (sampleNode.Annotations.Count > 0)
			{
				var flowShapeName = flowShapeComboBox.SelectedItem?.ToString() ?? "Process";
				sampleNode.Annotations[0].Content = flowShapeName;
			}
		}
	}

	private void OnFillColorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && fillColorComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			var selectedColor = fillColorComboBox.SelectedIndex switch
			{
				0 => "#4A8F8C", // Teal
				1 => "#1976D2", // Blue
				2 => "#388E3C", // Green
				3 => "#D32F2F", // Red
				4 => "#7B1FA2", // Purple
				_ => "#4A8F8C"
			};

			sampleNode.Style.Fill = Color.FromArgb(selectedColor);
		}
	}

	private void OnTextColorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && textColorComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			var selectedColor = textColorComboBox.SelectedIndex switch
			{
				0 => Colors.White,
				1 => Colors.Black,
				2 => Colors.Yellow,
				_ => Colors.White
			};

			if (sampleNode.Annotations.Count > 0)
			{
				sampleNode.Annotations[0].Style.Color = selectedColor;
			}
		}
	}

	private void OnStrokeWidthChanged(object? sender, EventArgs e)
	{
		if (diagram != null && sampleNode != null)
		{
			var strokeWidth = (double)(strokeWidthEntry.Value ?? 2);
			sampleNode.Style.StrokeWidth = strokeWidth;
		}
	}

	private void OnStrokeColorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && strokeColorComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			var selectedColor = strokeColorComboBox.SelectedIndex switch
			{
				0 => "#000000", // Black
				1 => "#808080", // Gray
				2 => "#FF0000", // Red
				3 => "#0000FF", // Blue
				4 => "#008000", // Green
				_ => "#000000"
			};

			sampleNode.Style.StrokeColor = Color.FromArgb(selectedColor);
		}
	}

	private void OnDashStyleChanged(object? sender, EventArgs e)
	{
		if (diagram != null && dashStyleComboBox.SelectedIndex >= 0 && sampleNode != null)
		{
			var dashPattern = dashStyleComboBox.SelectedIndex switch
			{
				0 => null,      // Solid (null or empty)
				1 => "5,5",     // Dashed
				2 => "2,2",     // Dotted
				_ => null
			};

			sampleNode.Style.StrokeDashArray = dashPattern;
		}
	}

	private void OnNodeWidthChanged(object? sender, EventArgs e)
	{
		if (diagram != null && sampleNode != null)
		{
			var width = (double)(nodeWidthEntry.Value ?? 120);
			sampleNode.Width = width;
		}
	}

	private void OnNodeHeightChanged(object? sender, EventArgs e)
	{
		if (diagram != null && sampleNode != null)
		{
			var height = (double)(nodeHeightEntry.Value ?? 120);
			sampleNode.Height = height;
		}
	}
}