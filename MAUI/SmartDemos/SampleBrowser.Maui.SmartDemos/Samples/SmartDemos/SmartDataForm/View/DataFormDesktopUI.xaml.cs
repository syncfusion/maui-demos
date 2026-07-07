namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    public partial class DataFormDesktopUI : ContentView
    {
        public DataFormDesktopUI()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
        {
            var templateItem = (sender as Border)?.BindingContext as TemplateItem;
            var bindingContext = this.ParentGrid.BindingContext as DataFormGeneratorModel;

            if (templateItem != null && bindingContext != null)
            {
                entry.Text = templateItem.Description;
                bindingContext.FormTitle = templateItem.Title;
            }
        }
    }
}
