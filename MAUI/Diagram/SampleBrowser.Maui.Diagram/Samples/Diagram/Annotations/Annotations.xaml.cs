using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using DiagramNode = Syncfusion.Maui.Diagram.Node;
using DiagramSfDiagram = Syncfusion.Maui.Diagram.SfDiagram;
using DiagramConnector = Syncfusion.Maui.Diagram.Connector;
using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

public partial class Annotations : SampleView
{
	private ObservableCollection<string> textColors = new();
	private ObservableCollection<string> fontFamilies = new();
	private DiagramSfDiagram? diagram;
	private List<ShapeAnnotation> nodeAnnotations = new();
	private List<PathAnnotation> connectorAnnotations = new();
	private Button? activeAlignmentButton;
	private bool isBoldActive = true;
	private bool isItalicActive;
	private bool isUnderlineActive;

    public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();
    public ObservableCollection<Connector> Connectors { get; } = new ObservableCollection<Connector>();

    public Annotations()
	{
        BindingContext = this;
		Nodes = new ObservableCollection<Node>();
		Connectors = new ObservableCollection<Connector>();
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
		// Text Colors
		textColors.Add("Black");
		textColors.Add("Blue");
		textColors.Add("Red");
		textColors.Add("Green");
		textColors.Add("Purple");
		textColorComboBox.ItemsSource = textColors;
		textColorComboBox.SelectedIndex = 0;
		textColorComboBox.SelectionChanged += OnTextColorChanged;

		// Font Families
		fontFamilies.Add("Arial");
		fontFamilies.Add("Courier");
		fontFamilies.Add("Times New Roman");
		fontFamilies.Add("Verdana");
		fontFamilies.Add("Georgia");
		fontFamilyComboBox.ItemsSource = fontFamilies;
		fontFamilyComboBox.SelectedIndex = 0;
		fontFamilyComboBox.SelectionChanged += OnFontFamilyChanged;

		// Font Size
		fontSizeEntry.Value = 12;

		// Set initial alignment button
		activeAlignmentButton = centerBtn;
		UpdateAlignmentButtonStyles();
	}

	private void CreateSampleDiagram()
	{
		if (diagram != null)
		{
			// Create flowchart nodes for Request Approval Workflow
			var newRequestNode = CreateNode("newRequest", "New Request", 300, 50, 140, 70, "#E3F2FD", "#1976D2", NodeBasicShapes.Rectangle);
			var reviewBoardNode = CreateNode("reviewBoard", "Review Board", 300, 220, 140, 70, "#FFF3E0", "#FF9800", NodeBasicShapes.Diamond);
			var approvedNode = CreateNode("approved", "Approved", 120, 380, 120, 70, "#C8E6C9", "#2E7D32", NodeBasicShapes.Ellipse);
			var rejectedNode = CreateNode("rejected", "Rejected", 480, 380, 120, 70, "#FFCDD2", "#C62828", NodeBasicShapes.Ellipse);

			Nodes.Add(newRequestNode);
			Nodes.Add(reviewBoardNode);
			Nodes.Add(approvedNode);
			Nodes.Add(rejectedNode);

			// Create connectors with annotations
			var connections = new (string source, string target, string label, string strokeColor)[]
			{
				("newRequest", "reviewBoard", "Submit Request", "#1976D2"),
				("reviewBoard", "approved", "Approved", "#2E7D32"),
				("reviewBoard", "rejected", "Rejected", "#C62828"),
			};

			foreach (var (source, target, label, strokeColor) in connections)
			{
				var connector = new DiagramConnector
				{
					SourceID = source,
					TargetID = target,
					Type = ConnectorSegmentType.Orthogonal,
				};

				connector.Style.StrokeColor = Color.FromArgb(strokeColor);
				connector.Style.StrokeWidth = 2;

				if (!string.IsNullOrEmpty(label))
				{
					var annotation = new PathAnnotation(label);
					annotation.Style.Color = Color.FromArgb(strokeColor);
					annotation.Style.FontSize = 12;
					annotation.Style.Bold = true;
					connector.Annotations.Add(annotation);
					connectorAnnotations.Add(annotation);
				}

				Connectors.Add(connector);
			}

			_ = diagram.FitToPage();
		}
	}

	private DiagramNode CreateNode(string id, string label, double offsetX, double offsetY, double width, double height, string fill, string strokeColor, NodeBasicShapes basicShape = NodeBasicShapes.Rectangle)
	{
		var node = new DiagramNode
		{
			Id = id,
			OffsetX = offsetX,
			OffsetY = offsetY,
			Width = width,
			Height = height,
			Shape = new BasicShape { Shape = basicShape },
		};

		node.Style.Fill = Color.FromArgb(fill);
		node.Style.StrokeColor = Color.FromArgb(strokeColor);
		node.Style.StrokeWidth = 2;

		var annotation = new ShapeAnnotation(label);
		annotation.Style.Color = Color.FromArgb(strokeColor);
		annotation.Style.FontSize = 17;
		annotation.Style.Bold = true;
		node.Annotations.Add(annotation);
		nodeAnnotations.Add(annotation);

		return node;
	}

	private void ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment hAlign, Syncfusion.Maui.Diagram.VerticalAlignment vAlign, double offsetX, double offsetY, Button clickedButton)
	{
		if (diagram == null || nodeAnnotations.Count == 0)
			return;

		activeAlignmentButton = clickedButton;
		UpdateAlignmentButtonStyles();

		foreach (var annotation in nodeAnnotations)
		{
			annotation.Offset = new DiagramPoint(offsetX, offsetY);
			annotation.HorizontalAlignment = hAlign;
			annotation.VerticalAlignment = vAlign;
		}
	}

	private void OnTopLeftClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Left, Syncfusion.Maui.Diagram.VerticalAlignment.Top, 0.0, 0.0, topLeftBtn);
	private void OnTopCenterClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Center, Syncfusion.Maui.Diagram.VerticalAlignment.Top, 0.5, 0.0, topCenterBtn);
	private void OnTopRightClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Right, Syncfusion.Maui.Diagram.VerticalAlignment.Top, 1.0, 0.0, topRightBtn);
	private void OnLeftClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Left, Syncfusion.Maui.Diagram.VerticalAlignment.Center, 0.0, 0.5, leftBtn);
	private void OnCenterClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Center, Syncfusion.Maui.Diagram.VerticalAlignment.Center, 0.5, 0.5, centerBtn);
	private void OnRightClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Right, Syncfusion.Maui.Diagram.VerticalAlignment.Center, 1.0, 0.5, rightBtn);
	private void OnBottomLeftClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Left, Syncfusion.Maui.Diagram.VerticalAlignment.Bottom, 0.0, 1.0, bottomLeftBtn);
	private void OnBottomCenterClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Center, Syncfusion.Maui.Diagram.VerticalAlignment.Bottom, 0.5, 1.0, bottomCenterBtn);
	private void OnBottomRightClicked(object? sender, EventArgs e) => ApplyAlignment(Syncfusion.Maui.Diagram.HorizontalAlignment.Right, Syncfusion.Maui.Diagram.VerticalAlignment.Bottom, 1.0, 1.0, bottomRightBtn);

	private void UpdateAlignmentButtonStyles()
	{
		var allButtons = new[] { topLeftBtn, topCenterBtn, topRightBtn, leftBtn, centerBtn, rightBtn, bottomLeftBtn, bottomCenterBtn, bottomRightBtn, boldBtn, italicBtn };
		foreach (var btn in allButtons)
		{
			btn.BackgroundColor = btn == activeAlignmentButton ? Colors.LightGray : Colors.LightBlue;
		}
	}

	private void OnBoldClicked(object? sender, EventArgs e)
	{
		if (diagram == null || nodeAnnotations.Count == 0)
			return;

		isBoldActive = !isBoldActive;
		UpdateFontAttributes();
		boldBtn.BackgroundColor = isBoldActive ? Colors.LightGray : Colors.LightBlue;
	}

	private void OnItalicClicked(object? sender, EventArgs e)
	{
		if (diagram == null || nodeAnnotations.Count == 0)
			return;

		isItalicActive = !isItalicActive;
		UpdateFontAttributes();
		italicBtn.BackgroundColor = isItalicActive ? Colors.LightGray : Colors.LightBlue;
	}

	private void OnUnderlineClicked(object? sender, EventArgs e)
	{
		if (diagram == null || nodeAnnotations.Count == 0)
			return;

		isUnderlineActive = !isUnderlineActive;
		UpdateFontAttributes();
	}

	private void UpdateFontAttributes()
	{
		foreach (var annotation in nodeAnnotations)
		{
			annotation.Style.Bold = isBoldActive;
			annotation.Style.Italic = isItalicActive;
		}
	}

	private void OnFontSizeChanged(object? sender, EventArgs e)
	{
		if (diagram != null)
		{
			var fontSize = (double)(fontSizeEntry.Value ?? 12);

			foreach (var annotation in nodeAnnotations)
			{
				annotation.Style.FontSize = fontSize;
			}
		}
	}

	private void OnTextColorChanged(object? sender, EventArgs e)
	{
		if (diagram != null && textColorComboBox.SelectedIndex >= 0 && nodeAnnotations.Count > 0)
		{
			var selectedColor = textColorComboBox.SelectedIndex switch
			{
				0 => Colors.Black,
				1 => Color.FromArgb("#1976D2"),
				2 => Color.FromArgb("#D32F2F"),
				3 => Color.FromArgb("#388E3C"),
				4 => Color.FromArgb("#7B1FA2"),
				_ => Colors.Black
			};

			foreach (var annotation in nodeAnnotations)
			{
				annotation.Style.Color = selectedColor;
			}
		}
	}

	private void OnFontFamilyChanged(object? sender, EventArgs e)
	{
		if (diagram != null && fontFamilyComboBox.SelectedIndex >= 0 && nodeAnnotations.Count > 0)
		{
			var selectedFamily = fontFamilyComboBox.SelectedIndex switch
			{
				0 => "Arial",
				1 => "Courier",
				2 => "Times New Roman",
				3 => "Verdana",
				4 => "Georgia",
				_ => "Arial"
			};

			foreach (var annotation in nodeAnnotations)
			{
				annotation.Style.FontFamily = selectedFamily;
			}
		}
	}


}