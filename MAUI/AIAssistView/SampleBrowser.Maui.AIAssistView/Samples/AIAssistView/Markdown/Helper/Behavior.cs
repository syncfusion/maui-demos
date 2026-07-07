using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.AIAssistView;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.MarkdownViewer;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class MarkdownBehavior : Behavior<SfMarkdownViewer>
    {
        public MarkdownViewModel? ViewModel { get; set; }
        protected override void OnAttachedTo(SfMarkdownViewer bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Loaded += Bindable_Loaded;        
        }

        private void Bindable_Loaded(object? sender, EventArgs e)
        {
            if (sender is SfMarkdownViewer markdownViewer)
            {
                // Set the height of the MarkdownViewer to 100% of the parent container
                markdownViewer.HeightRequest = 300;
#if WINDOWS
                markdownViewer.WidthRequest = 600;
#endif
            }
        }

        protected override void OnDetachingFrom(SfMarkdownViewer bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}



