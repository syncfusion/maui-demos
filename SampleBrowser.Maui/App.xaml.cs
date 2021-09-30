using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;

namespace SampleBrowser.Maui
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new ControlsHomePage()) { BarBackgroundColor = Colors.White, BarTextColor = Colors.Black };
		}
	}
}