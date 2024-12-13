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

public partial class DefaultKanban : SampleView
{
    public DefaultKanban()
    {
        InitializeComponent();

        List<KanbanWorkflow> flows = new List<KanbanWorkflow>();
        flows.Add(new KanbanWorkflow("Open", new List<object>() { "In Progress"}));
        flows.Add(new KanbanWorkflow("In Progress", new List<object>() { "Open", "Code Review" }));
        flows.Add(new KanbanWorkflow("Code Review", new List<object>() { "Open" , "In Progress", "Closed" }));
        flows.Add(new KanbanWorkflow("Closed", new List<object>() { "Open" }));

        kanban.Workflows = flows;
    }

}