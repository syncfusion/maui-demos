using Syncfusion.Maui.AIAssistView;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class CustomAssistViewChat : AssistViewChat
    {
        public CustomAssistViewChat(Syncfusion.Maui.AIAssistView.SfAIAssistView aIAssistView) : base(aIAssistView)
        {
            this.Editor.TextChanged += Editor_TextChanged;
        }

        private void Editor_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var viewmodel = this.BindingContext as ControlTemplateViewModel;
            if (viewmodel != null)
            {
                viewmodel.InputText = e.NewTextValue;
                viewmodel.EnableSendIcon = viewmodel.enableNewChatIcon && !string.IsNullOrEmpty(e.NewTextValue);
            }
        }
    }

    public class CustomAssistView : Syncfusion.Maui.AIAssistView.SfAIAssistView
    {
        public static readonly BindableProperty AssistChatViewProperty =
       BindableProperty.Create(nameof(AssistChatView), typeof(CustomAssistViewChat), typeof(CustomAssistView));

        public CustomAssistViewChat AssistChatView
        {
            get { return (CustomAssistViewChat)this.GetValue(AssistChatViewProperty); }
            set { this.SetValue(AssistChatViewProperty, value); }
        }
        protected override AssistViewChat CreateAssistChat()
        {
            AssistChatView = new CustomAssistViewChat(this);
            return AssistChatView;
        }
    }
}
