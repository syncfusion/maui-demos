using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.Core
{
    public class ScrollViewExt : ScrollView
    {

        public ScrollViewExt()
        {
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (this.Parent is SampleView)
            {
                ((SampleView)this.Parent).ScrollView = this;
            }
        }

    }
}
