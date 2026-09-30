using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Diagram;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

public partial class GettingStarted : SampleView
{
	public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();
	public ObservableCollection<Connector> Connectors { get; } = new ObservableCollection<Connector>();

    public GettingStarted()
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
		// Create nodes for the payment processing flowchart
		var placeOrder = CreateNode("placeOrder", 315, 40, 120, 60, NodeBasicShapes.Ellipse, "#1976D2", "#0D47A1", "Place Order");
		var startTransaction = CreateNode("startTransaction", 315, 140, 140, 60, NodeBasicShapes.Rectangle, "#1565C0", "#0D47A1", "Start Transaction");
		var verification = CreateNode("verification", 315, 260, 140, 60, NodeBasicShapes.Rectangle, "#42A5F5", "#1565C0", "Verification");
		var cardValid = CreateNode("cardValid", 315, 400, 120, 100, NodeBasicShapes.Diamond, "#2196F3", "#0D47A1", "Credit card invalid?");
		var enterPayment = CreateNode("enterPayment", 560, 400, 140, 80, NodeBasicShapes.Rectangle, "#7E57C2", "#512DA8", "Enter payment inmethod");
		var fundsAvailable = CreateNode("fundsAvailable", 315, 580, 120, 100, NodeBasicShapes.Diamond, "#42A5F5", "#1565C0", "Funds inavailable?");
		var sendEmail = CreateNode("sendEmail", 120, 710, 140, 60, NodeBasicShapes.Parallelogram, "#29B6F6", "#0277BD", "Send e-mail");
		var completeTransaction = CreateNode("completeTransaction", 315, 710, 160, 60, NodeBasicShapes.Rectangle, "#4DB6AC", "#00796B", "Complete inTransaction");
		var customerDb = CreateNode("customerDb", 560, 710, 140, 80, NodeBasicShapes.Ellipse, "#AB47BC", "#6A1B9A", "Customer inDatabase");
		var logTransaction = CreateNode("logTransaction", 315, 850, 140, 60, NodeBasicShapes.Ellipse, "#1976D2", "#0D47A1", "Log transaction");
		var reconcile = CreateNode("reconcile", 560, 850, 160, 60, NodeBasicShapes.Rectangle, "#FBC02D", "#F57F17", "Reconcile the inentries");

		// Add ports to decision nodes for better connector routing
		var cardValidNoPort = new PointPort { Id = "cardValidNo", Offset = new DiagramPoint(1.0, 0.5) };   // right edge
		var cardValidYesPort = new PointPort { Id = "cardValidYes", Offset = new DiagramPoint(0.5, 1.0) };  // bottom edge
		cardValid.Ports.Add(cardValidNoPort);
		cardValid.Ports.Add(cardValidYesPort);

		var fundsNoPort = new PointPort { Id = "fundsNo", Offset = new DiagramPoint(1.0, 0.5) };   // right edge
		var fundsYesPort = new PointPort { Id = "fundsYes", Offset = new DiagramPoint(0.5, 1.0) };  // bottom edge
		fundsAvailable.Ports.Add(fundsNoPort);
		fundsAvailable.Ports.Add(fundsYesPort);

		// Add all nodes to the diagram
		Nodes.Add(placeOrder);
		Nodes.Add(startTransaction);
		Nodes.Add(verification);
		Nodes.Add(cardValid);
		Nodes.Add(enterPayment);
		Nodes.Add(fundsAvailable);
		Nodes.Add(sendEmail);
		Nodes.Add(completeTransaction);
		Nodes.Add(customerDb);
		Nodes.Add(logTransaction);
		Nodes.Add(reconcile);

		// Create connectors - main flow
		Connectors.Add(CreateConnector("conn1", "placeOrder", "startTransaction", ConnectorSegmentType.Orthogonal));
		Connectors.Add(CreateConnector("conn2", "startTransaction", "verification", ConnectorSegmentType.Orthogonal));
		Connectors.Add(CreateConnector("conn3", "verification", "cardValid", ConnectorSegmentType.Orthogonal));

		// Card Valid - NO branch (to Enter Payment Method)
		var cardNoConnector = CreateConnector("conn4", "cardValid", "enterPayment", ConnectorSegmentType.Straight);
		cardNoConnector.SourcePortID = "cardValidNo";
		cardNoConnector.Annotations.Add(new PathAnnotation("No"));
		Connectors.Add(cardNoConnector);

		// Card Valid - YES branch (to Funds Available)
		var cardYesConnector = CreateConnector("conn5", "cardValid", "fundsAvailable", ConnectorSegmentType.Straight);
		cardYesConnector.SourcePortID = "cardValidYes";
		cardYesConnector.Annotations.Add(new PathAnnotation("Yes"));
		Connectors.Add(cardYesConnector);

		// Enter Payment Method loops back to verification
		Connectors.Add(CreateConnector("conn6", "enterPayment", "verification", ConnectorSegmentType.Orthogonal));

		// Funds Available - NO branch (to Enter Payment Method)
		var fundsNoConnector = CreateConnector("conn7", "fundsAvailable", "enterPayment", ConnectorSegmentType.Orthogonal);
		fundsNoConnector.SourcePortID = "fundsNo";
		fundsNoConnector.Annotations.Add(new PathAnnotation("No"));
		Connectors.Add(fundsNoConnector);

		// Funds Available - YES branch (to Complete Transaction)
		var fundsYesConnector = CreateConnector("conn8", "fundsAvailable", "completeTransaction", ConnectorSegmentType.Straight);
		fundsYesConnector.SourcePortID = "fundsYes";
		fundsYesConnector.Annotations.Add(new PathAnnotation("Yes"));
		Connectors.Add(fundsYesConnector);

		// Complete Transaction branches
		Connectors.Add(CreateConnector("conn9", "completeTransaction", "sendEmail", ConnectorSegmentType.Orthogonal));
		Connectors.Add(CreateConnector("conn10", "completeTransaction", "customerDb", ConnectorSegmentType.Orthogonal));

		// Bottom connectors
		Connectors.Add(CreateConnector("conn11", "sendEmail", "logTransaction", ConnectorSegmentType.Orthogonal));
		Connectors.Add(CreateConnector("conn12", "logTransaction", "reconcile", ConnectorSegmentType.Orthogonal));

		_ = Diagram.FitToPage();
	}

	private static Node CreateNode(
		string id, double offsetX, double offsetY, double width, double height,
		NodeBasicShapes shape, string fill, string stroke, string label)
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
		node.Style.StrokeColor = Color.FromArgb(stroke);
		node.Style.StrokeWidth = 2;

		var annotation = new ShapeAnnotation(label);
		annotation.Style.Color = Colors.White;
		annotation.Style.FontSize = 17;
		node.Annotations.Add(annotation);

		return node;
	}

	private static Connector CreateConnector(string id, string sourceId, string targetId, ConnectorSegmentType type)
	{
		var connector = new Connector
		{
			Id = id,
			SourceID = sourceId,
			TargetID = targetId,
			Type = type,
		};

		connector.Style.StrokeColor = Color.FromArgb("#1565C0");
		connector.Style.StrokeWidth = 2;
		return connector;
	}
}