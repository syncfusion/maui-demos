#if __ANDROID__ || __IOS__ || __MACCATALYST__
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Syncfusion.Maui.ListView;
using SelectionMode = Syncfusion.Maui.ListView.SelectionMode;
using System.Drawing;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfListView
{
    public class SfListViewGridLayoutBehavior : Behavior<SampleView>
    {
        #region Fields

        private Syncfusion.Maui.ListView.SfListView listView;
        private GridLayoutViewModel viewModel;

        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            bindable.PropertyChanged += View_PropertyChanged;
            listView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            viewModel = bindable.FindByName<GridLayoutViewModel>("viewModel");
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            bindable.PropertyChanged -= View_PropertyChanged;
            listView = null;
            viewModel = null;
            base.OnDetachingFrom(bindable);
        }


        #endregion

        #region Events

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width")
            {
                double spanCount = (this.listView.ItemsLayout as Syncfusion.Maui.ListView.GridLayout).SpanCount;
                //Below calulation is to find the individual imageWidth
                this.viewModel.ImageHeightRequest = ((sender as SampleView).Width - (spanCount * this.listView.ItemSpacing.Left) - (spanCount * this.listView.ItemSpacing.Right)) / spanCount;
            }
        }
        #endregion

    }
}

#endif