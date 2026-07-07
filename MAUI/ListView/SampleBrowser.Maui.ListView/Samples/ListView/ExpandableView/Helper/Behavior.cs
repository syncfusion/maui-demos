using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    class GridBehavior : Behavior<Grid>
    {
        public Syncfusion.Maui.ListView.SfListView ListView { get; set; }
        private TapGestureRecognizer tapGestureRecognizer;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnItemTapped;
            (bindable as Grid).GestureRecognizers.Add(tapGestureRecognizer);
#if ANDROID
            // Todo Breaking in https://github.com/essential-studio/maui-listview/pull/358/files
            // ListView.RefreshView();
#endif
            base.OnAttachedTo(bindable);
        }

        private void OnItemTapped(object sender, EventArgs e)
        {
            var dataItem = (sender as Grid).BindingContext as ListViewFoodCategory;
            var currentIndex = ListView.DataSource.DisplayItems.IndexOf(dataItem);

            if (dataItem.IsExpanded)
            {
                dataItem.IsExpanded = false;
            }
            else
            {
                dataItem.IsExpanded = true;
            }
            ListView.RefreshItem(currentIndex, currentIndex, false);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {            
            base.OnDetachingFrom(bindable);            
            ListView = null;
            tapGestureRecognizer.Tapped -= OnItemTapped;
        }
    }

    // Toggles selection for FoodMenu item on Grid tap (no command needed).
    class ItemSelectionBehavior : Behavior<Grid>
    {
        private TapGestureRecognizer tapGestureRecognizer;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapped;
            (bindable as Grid).GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            var item = (sender as Grid)?.BindingContext as FoodMenu;
            if (item != null)
            {
                item.IsSelected = !item.IsSelected;
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            if (tapGestureRecognizer != null)
            {
                tapGestureRecognizer.Tapped -= OnTapped;
                (bindable as Grid).GestureRecognizers.Remove(tapGestureRecognizer);
                tapGestureRecognizer = null;
            }
        }
    }
}
