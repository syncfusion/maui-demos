using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using System.Windows.Input;

namespace SampleBrowser.Maui.DataGrid
{
    public class ContextMenuBehavior : Behavior<SampleView>
    {
        #region Fields

        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        #endregion

        #region Commands

        public ICommand? SortAscendingCommand { get; set; }
        public ICommand? SortDescendingCommand { get; set; }
        public ICommand? ClearSortingCommand { get; set; }
        public ICommand? CopyContentCommand { get; set; }
        public ICommand? PasteContentCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }

        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");

            InitializeCommands();

            InitializeContextMenus();

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            datagrid = null;

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Private Methods

        void InitializeCommands()
        {
            SortAscendingCommand = new Command(SortAscending);
            SortDescendingCommand = new Command(SortDescending);
            ClearSortingCommand = new Command(ClearSorting);
            CopyContentCommand = new Command(CopyCellContent);
            PasteContentCommand = new Command(PasteCellContent);
            DeleteCommand = new Command(ExecuteDelete);
        }

        void InitializeContextMenus()
        {
            if (datagrid != null)
            {
                var headerContextMenu = new Syncfusion.Maui.DataGrid.MenuItemCollection
                {
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Sort Ascending",
                        Command = SortAscendingCommand,
                        Icon = new Label
                        {
                            Text = "\ue710",
                            HeightRequest = 25,
                            FontSize = 14,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    },
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Sort Descending",
                        Command = SortDescendingCommand,
                        Icon = new Label
                        {
                            Text = "\ue711",
                            HeightRequest = 25,
                            FontSize = 14,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    },
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Clear Sorting",
                        Command = ClearSortingCommand,
                        Icon = new Label
                        {
                            Text = "\ue71A",
                            HeightRequest = 25,
                            FontSize = 14,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    }
                };

                datagrid.HeaderContextMenu = headerContextMenu;

                var recordContextMenu = new Syncfusion.Maui.DataGrid.MenuItemCollection
                {
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Copy",
                        Command = CopyContentCommand,
                        Icon = new Label
                        {
                            Text = "\ue737",
                            HeightRequest = 25,
                            FontSize = 14,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    },
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Paste",
                        Command = PasteContentCommand,
                        Icon = new Label
                        {
                            Text = "\ue7ED",
                            HeightRequest = 25,
                            FontSize = 14,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    },
                    new Syncfusion.Maui.DataGrid.MenuItem
                    {
                        Text = "Delete",
                        Command = DeleteCommand,
                        Icon = new Label
                        {
                            Text = "\ue73C",
                            HeightRequest = 25,
                            FontSize = 18,
                            FontFamily = "MauiSampleFontIcon",
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center
                        }
                    }
                };

                datagrid.RecordContextMenu = recordContextMenu;
            }
        }

        private void SortAscending(object obj)
        {
            if (obj is HeaderContextInfo context && context.Column != null && context.DataGrid != null)
            {
                context.DataGrid.SortColumnDescriptions.Clear();
                context.DataGrid.SortColumnDescriptions.Add(new SortColumnDescription { ColumnName = context.Column.MappingName });
            }
        }

        private void SortDescending(object obj)
        {
            if (obj is HeaderContextInfo context && context.Column != null && context.DataGrid != null)
            {
                context.DataGrid.SortColumnDescriptions.Clear();
                context.DataGrid.SortColumnDescriptions.Add(new SortColumnDescription
                {
                    ColumnName = context.Column.MappingName,
                    SortDirection = System.ComponentModel.ListSortDirection.Descending
                });
            }
        }

        private void ClearSorting(object obj)
        {
            if (obj is HeaderContextInfo context && context.DataGrid != null)
            {
                context.DataGrid.SortColumnDescriptions.Clear();
            }
        }

        private void CopyCellContent(object obj)
        {
            if (obj is RowContextMenuInfo context && context.RowIndex >= 0)
            {
                context.DataGrid?.CopyPasteController.Copy();
            }
        }

        private void PasteCellContent(object obj)
        {
            if (obj is RowContextMenuInfo context && context.RowIndex >= 0)
            {
                context.DataGrid?.CopyPasteController.Paste();
            }
        }

        private void ExecuteDelete(object obj)
        {
            if (obj is RowContextMenuInfo context && context.RowIndex >= 0)
            {
                if (context.DataGrid?.SelectedRow != null && context.DataGrid.ItemsSource is System.Collections.IList list)
                {
                    list.Remove(context.DataGrid.SelectedRow);
                }
            }
        }

        #endregion
    }
}
