using SampleBrowser.Maui.Base.CustomView;
using Syncfusion.Maui.BottomSheet;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
namespace SampleBrowser.Maui.BottomSheet.SfBottomSheet
{
    public class BottomSheetBehavior : Behavior<SampleView>
    {
        CollectionView? _collectionView;
        ItemViewModel? _itemViewModel;
        Syncfusion.Maui.BottomSheet.SfBottomSheet? _bottomSheet;
        SfEffectsViewAdv1? _decreaseQuantity;
        SfEffectsViewAdv1? _increaseQuantity;
        SfEffectsViewAdv1? _closeIcon;
        SfCheckBox? _extraOne;
        SfCheckBox? _extraTwo;
        Grid? grid;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindable">SampleView type parameter named as bindable.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            _collectionView = bindable.FindByName<CollectionView>("CollectionView");
            _collectionView.SelectionChanged += _collectionView_SelectionChanged;
            _itemViewModel = new ItemViewModel();
            bindable.BindingContext = _itemViewModel;
            _bottomSheet = bindable.FindByName<Syncfusion.Maui.BottomSheet.SfBottomSheet>("BottomSheet");
            _bottomSheet.StateChanged += OnStateChanged;
            _decreaseQuantity = bindable.FindByName<SfEffectsViewAdv1>("DecreaseQuantity");
            _increaseQuantity = bindable.FindByName<SfEffectsViewAdv1>("IncreaseQuantity");
            _closeIcon = bindable.FindByName<SfEffectsViewAdv1>("CloseIcon");
            _extraOne = bindable.FindByName<SfCheckBox>("ExtraCheese");
            _extraTwo = bindable.FindByName<SfCheckBox>("ExtraDoubleCheese");
            grid = bindable.FindByName<Grid>("Grid");

            TapGestureRecognizer decreaseTapped = new TapGestureRecognizer();
            decreaseTapped.Tapped += OnDecreaseTapped;
            _decreaseQuantity.GestureRecognizers.Add(decreaseTapped);

            TapGestureRecognizer increaseTapped = new TapGestureRecognizer();
            increaseTapped.Tapped += OnIncreaseTapped;
            _increaseQuantity.GestureRecognizers.Add(increaseTapped);

            TapGestureRecognizer closeIconTapped = new TapGestureRecognizer();
            closeIconTapped.Tapped += OnCloseIconTapped;
            _closeIcon.GestureRecognizers.Add(closeIconTapped);

            _extraOne.StateChanged += ExtraOne_StateChanged;
            _extraTwo.StateChanged += ExtraTwo_StateChanged;

            base.OnAttachedTo(bindable);
        }

        private void _collectionView_SelectionChanged(object? sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Item selectedItem)
            {
                if (_bottomSheet is not null && grid is not null)
                {
                    grid.BindingContext = selectedItem;
                    if (_bottomSheet.IsOpen)
                    {
                        _bottomSheet.State = BottomSheetState.HalfExpanded;
                    }

                    _bottomSheet.Show();
                }
            }
        }

        private void ExtraTwo_StateChanged(object? sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            if (_extraTwo is not null && _bottomSheet is not null && grid is not null)
            {
                if (_extraTwo.IsChecked == true && _extraOne is not null)
                {
                    _extraOne.IsChecked = false;
                    var item = (Item)grid.BindingContext;
                    item.TotalPrice = (item.Price + 4) * item.Quantity;
                }
                else
                {
                    var item = (Item)grid.BindingContext;
                    item.TotalPrice = item.Price * item.Quantity;
                }
            }
        }

        private void ExtraOne_StateChanged(object? sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            if (_extraOne is not null && _bottomSheet is not null && grid is not null)
            {
                if (_extraOne.IsChecked == true && _extraTwo is not null)
                {
                    _extraTwo.IsChecked = false;
                    var item = (Item)grid.BindingContext;
                    item.TotalPrice = (item.Price + 2) * item.Quantity;
                }
                else
                {
                    var item = (Item)grid.BindingContext;
                    item.TotalPrice = item.Price * item.Quantity;
                }
            }
        }

        private void OnStateChanged(object? sender, Syncfusion.Maui.BottomSheet.StateChangedEventArgs e)
        {
            if (_bottomSheet is not null && _bottomSheet.State is BottomSheetState.Hidden && grid is not null)
            {
                grid.BindingContext = null;
            }
        }

        private void OnDecreaseTapped(object? sender, TappedEventArgs e)
        {
            if (_bottomSheet is not null && grid is not null)
            {
                var temp = (Item)grid.BindingContext;
                if (temp.Quantity > 1)
                {
                    temp.Quantity--;
                }
            }
        }

        private void OnIncreaseTapped(object? sender, TappedEventArgs e)
        {
            if (_bottomSheet is not null && grid is not null)
            {
                var temp = (Item)grid.BindingContext;
                temp.Quantity++;
            }
        }

        private void OnCloseIconTapped(object? sender, TappedEventArgs e)
        {
            if (_bottomSheet is not null)
            {
                _bottomSheet.Close();
            }
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindable">SampleView type parameter named as bindable</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            _collectionView = null;
            _itemViewModel = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
