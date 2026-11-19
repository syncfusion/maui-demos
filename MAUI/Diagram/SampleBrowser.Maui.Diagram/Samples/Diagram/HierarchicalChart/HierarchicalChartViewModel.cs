using Syncfusion.Maui.Diagram;

namespace SampleBrowser.Maui.Diagram.SfDiagram;

/// <summary>Hierarchical tree sample view model; uses its own node set but shares all layout/option logic.</summary>
public class HierarchicalChartViewModel : OrganizationalChartViewModel
{
    protected override DiagramLayoutType LayoutType => DiagramLayoutType.HierarchicalTree;
    protected override double DefaultHorizontalSpacing => 40;

    protected override IEnumerable<OrganizationalEmployee> CreateEmployees()
    {
        const string color = "#6B98E4";

        return new OrganizationalEmployee[]
        {
            new("diagram", "Diagram", "", null, color),
            new("layout", "Layout", "", "diagram", color),
            new("tree_layout", "Tree Layout", "", "layout", color),
            new("org_chart", "Organizational Chart", "", "layout", color),
            new("hier_tree", "Hierarchical Tree", "", "tree_layout", color),
            new("radial_tree", "Radial Tree", "", "tree_layout", color),
            new("management", "Management", "", "org_chart", color),
            new("mind_map", "Mind Map", "", "hier_tree", color),
            new("family_tree", "Family Tree", "", "hier_tree", color),
            new("hr", "Human Resources", "", "management", color),
            new("university", "University", "", "management", color),
            new("business", "Business", "", "management", color),
        };
    }
}
