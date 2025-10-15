#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using Syncfusion.Maui.Kanban;

    public class HeaderTemplateSelector : DataTemplateSelector
    {

        public DataTemplate? MenuHeaderTemplate { get; set; }
        public DataTemplate? OrderHeaderTemplate { get; set; }
        public DataTemplate? ReadyToServeHeaderTemplate { get; set; }
        public DataTemplate? DoorDeliveryHeaderTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            string? header = (item as KanbanColumn)?.Title;

            if (string.IsNullOrEmpty(header))
                return new DataTemplate();

            var headerTemplate = header.Equals("Menu", StringComparison.OrdinalIgnoreCase) ? MenuHeaderTemplate :
                header.Equals("Order", StringComparison.OrdinalIgnoreCase) ? OrderHeaderTemplate :
                header.Equals("Ready to Serve", StringComparison.OrdinalIgnoreCase) ? ReadyToServeHeaderTemplate : DoorDeliveryHeaderTemplate;
            return headerTemplate ?? new DataTemplate();
        }

    }
}
