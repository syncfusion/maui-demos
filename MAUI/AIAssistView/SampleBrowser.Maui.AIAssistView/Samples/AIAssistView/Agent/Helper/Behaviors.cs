using System;
using System.Collections.ObjectModel;
using System.Text;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.AIAssistView;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class SfAIAssistViewAgentBehavior : Behavior<SampleView>
    {
        #region Fields
        internal GettingStartedViewModel? viewModel;
        private SampleView? sampleView;
        private Syncfusion.Maui.AIAssistView.SfAIAssistView? assistView;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            sampleView = bindable;
#if WINDOWS || MACCATALYST

            var samplename = bindable.GetType();
            
#endif
            this.assistView = bindable.FindByName<Syncfusion.Maui.AIAssistView.SfAIAssistView>("sfAIAssistView");
            if (this.assistView != null)
            {
                this.assistView.PropertyChanged += OnCustomAssistViewPropertyChanged;
                this.assistView.SuggestionItemSelected += OnSuggestionItemSelected;
            }
            viewModel = bindable.BindingContext as GettingStartedViewModel;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (this.assistView != null)
            {
                this.assistView.PropertyChanged -= OnCustomAssistViewPropertyChanged;
                this.assistView.SuggestionItemSelected -= OnSuggestionItemSelected;
            }

            this.assistView = null;
            viewModel = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBacks

        private void OnCustomAssistViewPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Syncfusion.Maui.AIAssistView.SfAIAssistView.SelectedAgent) && this.viewModel != null && this.assistView != null)
            {
                if (this.assistView.SelectedAgent is AssistAgent assistAgent)
                {
                    this.assistView.ToolbarTitle = assistAgent.Name;
                    if (this.assistView.AssistItems != null && this.assistView.AssistItems.Count == 0)
                    {
                        switch (assistAgent.Name)
                        {
                            case "Writing Assistant":
                                this.assistView.Suggestions = this.viewModel.WritingAgentSuggestions;
                                break;
                            case "Art Assistant":
                                this.assistView.Suggestions = this.viewModel.DataAgentSuggestions;
                                break;
                        }
                    }
                }
                else
                {
                    this.assistView.ToolbarTitle = string.Empty;
                    this.assistView.Suggestions = new ObservableCollection<ISuggestion>();
                }
            }
        }


        private void OnSuggestionItemSelected(object? sender, SuggestionItemSelectedEventArgs e)
        {
            e.CancelRequest = true;
            if(this.assistView != null)
            {
                this.assistView.FooterSuggestions = new ObservableCollection<ISuggestion>();
            }
        }

        #endregion
    }
}
