using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Sorting : SampleView
{
	public Sorting()
	{
		InitializeComponent();
    }


    /// <summary>
    /// Performs the undo operation programmatically using the DataGrid UndoRedoController.
    /// This method is triggered when the undo button is released.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments associated with the touch event.</param>
    private void undoButton_TouchUp(object? sender, EventArgs e)
    {
        dataGrid.UndoRedoController.Undo();
    }

    /// <summary>
    /// Performs the redo operation programmatically using the DataGrid UndoRedoController.
    /// This method is triggered when the redo button is released.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Event arguments associated with the touch event.</param>
    private void redoButton_TouchUp(object? sender, EventArgs e)
    {
        dataGrid.UndoRedoController.Redo();
    }
}