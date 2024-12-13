#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Kanban;

namespace SampleBrowser.Maui.Kanban.SfKanban;

public partial class KanbanCustomizationSample : SampleView
{
    public KanbanCustomizationSample()
    {
        InitializeComponent();

        column1.Categories = new List<object>() { "Menu" };
        column2.Categories = new List<object>() { "Dining" };
        column3.Categories = new List<object>() { "Ready to Serve" };
        column4.Categories = new List<object>() { "Door Delivery" };

        KanbanErrorBarSettings errorBar = new KanbanErrorBarSettings();
        errorBar.Height = 2;
        errorBar.Fill = Color.FromRgb(179.0f / 255.0f, 71.0f / 255.0f, 36.0f / 255.0f);
        errorBar.MaxValidationFill = Color.FromRgb(211.0f / 255.0f, 51.0f / 255.0f, 54.0f / 255.0f);
        errorBar.MinValidationFill = Color.FromRgb(67.0f / 255.0f, 188.0f / 255.0f, 131.0f / 255.0f);

        column1.ErrorBarSettings = column2.ErrorBarSettings = column3.ErrorBarSettings
            = column4.ErrorBarSettings = errorBar;
        column1.AllowDrop = false;
        column3.AllowDrag = column4.AllowDrag = false;

        List<KanbanWorkflow> flows = new List<KanbanWorkflow>();
        flows.Add(new KanbanWorkflow("Menu", new List<object>() { "Dining", "Delivery" }));
        flows.Add(new KanbanWorkflow("Dining", new List<object>() { "Ready to Serve" }));
        flows.Add(new KanbanWorkflow("Delivery", new List<object>() { "Door Delivery" }));
        flows.Add(new KanbanWorkflow("Ready to Serve", new List<object>() { }));
        flows.Add(new KanbanWorkflow("Door Delivery", new List<object>() { }));

        kanban.Workflows = flows;
    }
}

public class KanbanTemplateSelector : DataTemplateSelector
{
    private readonly DataTemplate menuTemplate;

    private readonly DataTemplate orderTemplate;

    private readonly DataTemplate readyToServeTemplate;

    private readonly DataTemplate deliveryTemplate;


    public KanbanTemplateSelector()
    {
        menuTemplate = new DataTemplate(typeof(MenuTemplate));
        orderTemplate = new DataTemplate(typeof(OrderTemplate));
        readyToServeTemplate = new DataTemplate(typeof(ReadyToServeTemplate));
        deliveryTemplate = new DataTemplate(typeof(DeliveryTemplate));
    }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        var data = item as CustomKanbanModel;
        if (data == null)
            return null;

        string? category = data.Category?.ToString();

        if (category == null)
            return null;

        return category.Equals("Menu") ? menuTemplate :
                       category.Equals("Dining") || category.Equals("Delivery") ? orderTemplate :
                       category.Equals("Ready to Serve") ? readyToServeTemplate : deliveryTemplate;
    }
}