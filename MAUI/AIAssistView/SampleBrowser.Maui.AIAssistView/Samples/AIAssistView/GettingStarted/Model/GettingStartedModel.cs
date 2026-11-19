using System.ComponentModel;
using Syncfusion.Maui.AIAssistView;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class GettingStartedModel : AssistSuggestion, INotifyPropertyChanged
    {
        private string? image;
        private string? suggestionDescription;

        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string? SuggestionDescription
        {
            get { return suggestionDescription; }
            set
            {
                suggestionDescription = value;
                OnPropertyChanged("SuggestionDescription");
            }
        }

        // public event PropertyChangedEventHandler? PropertyChanged;

        // public void OnPropertyChanged(string name)
        // {
        //     if (this.PropertyChanged != null)
        //     {
        //         this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        //     }
        // } 
    }
}
