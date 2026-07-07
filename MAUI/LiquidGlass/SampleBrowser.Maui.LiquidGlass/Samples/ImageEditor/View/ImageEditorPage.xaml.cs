namespace SampleBrowser.Maui.LiquidGlass.SfImageEditor
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents a page that displays and manages an Image Editor within the application.
    /// </summary>
    public partial class ImageEditorPage : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEditorPage"/> class.
        /// </summary>
        public ImageEditorPage()
        {
            InitializeComponent();
#if IOS || MACCATALYST
            this.imageEditor.ToolbarSettings.Background = new SolidColorBrush(Colors.Transparent);
            this.imageEditor.ToolbarSettings.Stroke = new SolidColorBrush(Colors.Transparent);
#endif
        }
    }
}