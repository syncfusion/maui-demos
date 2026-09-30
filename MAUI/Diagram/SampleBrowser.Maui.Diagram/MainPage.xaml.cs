namespace SampleBrowser.Maui.Diagram;

/// <summary>The placeholder page shown before the sample browser's main view takes over.</summary>
public partial class MainPage : ContentPage
{
	int count;

	/// <summary>Loads the page's XAML content.</summary>
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
