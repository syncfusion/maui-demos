using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

public partial class OrganizationalChart : SampleView
{
    public OrganizationalChart()
    {
        InitializeComponent();
    }

    private void Switch_Toggled(object? sender, ToggledEventArgs e)
    {
        if (sfDiagram != null && sfDiagram.NodeDefaults is NodeDefaults nodeDefaults)
        {
           if (e.Value)
            {
                nodeDefaults.ExpandIcon.Shape = IconShapeModel.Minus;
                nodeDefaults.CollapseIcon.Shape = IconShapeModel.Plus;
                _ = sfDiagram.DoLayoutAsync();
            }
            else
            {
                nodeDefaults.ExpandIcon.Shape = IconShapeModel.None;
                nodeDefaults.CollapseIcon.Shape = IconShapeModel.None;
            }
        }
    }
}
