#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.LiquidGlass.SfStepProgressBar
{
    using Syncfusion.Maui.ProgressBar;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// Represents a ViewModel that designed to manage a collection of step progress bar items.
    /// </summary>
    public class StepProgressBarViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The order formatted string.
        /// </summary>
        private FormattedString orderFormattedString;

        /// <summary>
        /// The shipped formatted string.
        /// </summary>
        private FormattedString shippedFormattedString;

        /// <summary>
        /// The out of delivery formatted string.
        /// </summary>
        private FormattedString outForDeliveryFormattedString;

        /// <summary>
        /// The delivery primary formatted string.
        /// </summary>
        private FormattedString deliveryPrimaryFormattedString;

#if WINDOWS || MACCATALYST
        /// <summary>
        /// The delivery secondary formatted string.
        /// </summary>
        private FormattedString deliverySecondaryFormattedString;
#endif

        /// <summary>
        /// The shipment collection
        /// </summary>
        private ObservableCollection<StepProgressBarItem> shipmentInfoCollection;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="StepProgressBarItem"/> objects representing shipment details.
        /// </summary>
        public ObservableCollection<StepProgressBarItem> ShipmentInfoCollection
        {
            get { return this.shipmentInfoCollection; }
            set
            {
                if (this.shipmentInfoCollection != value)
                {
                    this.shipmentInfoCollection = value;
                    this.OnPropertyChanged(nameof(ShipmentInfoCollection));
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StepProgressBarViewModel"/> class.
        /// </summary>
        public StepProgressBarViewModel()
        {
#if WINDOWS || MACCATALYST
            var currentDate = DateTime.Now.AddDays(-2);
            orderFormattedString = new FormattedString();
            orderFormattedString.Spans.Add(new Span { Text = "Your purchase has been confirmed.", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            orderFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            shippedFormattedString = new FormattedString();
            shippedFormattedString.Spans.Add(new Span { Text = "Your order is on its way.", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            shippedFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddHours(5).AddMinutes(30)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            outForDeliveryFormattedString = new FormattedString();
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "Your order is out for delivery.", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddDays(2).AddHours(-2)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            deliverySecondaryFormattedString = new FormattedString();
            deliverySecondaryFormattedString.Spans.Add(new Span { Text = "Delivery Expected By ", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            deliverySecondaryFormattedString.Spans.Add(new Span { Text = "\n" + DateTime.Now.ToString($"ddd, d'{this.GetDaySuffix(DateTime.Now)}' MMM"), FontSize = 14 });

            deliveryPrimaryFormattedString = new FormattedString();
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "Item yet to be delivered.", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "\nExpected by " + DateTime.Now.ToString($"ddd, d'{this.GetDaySuffix(DateTime.Now)}' MMM") + " at " + DateTime.Now.AddHours(1).ToString("h:mm tt"), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            shipmentInfoCollection = new ObservableCollection<StepProgressBarItem>();
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryText = "Ordered Confirmed", SecondaryFormattedText = orderFormattedString, ToolTipText = "Ordered" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { SecondaryText = "Shipped", PrimaryFormattedText = shippedFormattedString, ToolTipText = "Shipped" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryText = "Out For Delivery", SecondaryFormattedText = outForDeliveryFormattedString, ToolTipText = "Out for delivery" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryFormattedText = deliveryPrimaryFormattedString, SecondaryFormattedText = deliverySecondaryFormattedString, ToolTipText = "Delivery in progress" });
#else
            var currentDate = DateTime.Now.AddDays(-2);
            orderFormattedString = new FormattedString();
            orderFormattedString.Spans.Add(new Span { Text = "Order Confirmed", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Medium" });
            orderFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8 });
            orderFormattedString.Spans.Add(new Span { Text = "\nYour purchase has been confirmed.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            orderFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            orderFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8, });
            orderFormattedString.Spans.Add(new Span { Text = "\nYour order has been picked up by courier partner.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            orderFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddHours(5)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            shippedFormattedString = new FormattedString();
            shippedFormattedString.Spans.Add(new Span { Text = "Shipped", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Colors.White, FontFamily = "Roboto-Medium" });
            shippedFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8 });
            shippedFormattedString.Spans.Add(new Span { Text = "\nYour order is on its way.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            shippedFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddHours(5).AddMinutes(30)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            shippedFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8, });
            shippedFormattedString.Spans.Add(new Span { Text = "\nYour order has been received in the hub nearest to you.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            shippedFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddHours(7).AddMinutes(30)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });

            outForDeliveryFormattedString = new FormattedString();
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "Out For Delivery", FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Colors.White, FontFamily = "Roboto-Medium" });
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8 });
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "\nYour order is out for delivery.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#1C1B1F") : Color.FromRgba("#E6E1E5"), FontFamily = "Roboto-Regular" });
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "\n" + GetFormattedDateText(currentDate.AddDays(2).AddHours(-2)), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            outForDeliveryFormattedString.Spans.Add(new Span { Text = "\n", FontSize = 5 });

            deliveryPrimaryFormattedString = new FormattedString();
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "Delivery Expected By " + DateTime.Now.ToString($"ddd, d'{this.GetDaySuffix(DateTime.Now)}' MMM"), FontSize = 14, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Medium" });
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "\n ", FontSize = 8 });
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "\nItem yet to be delivered.", FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "\nExpected by " + DateTime.Now.ToString($"ddd, d'{this.GetDaySuffix(DateTime.Now)}' MMM") + " at " + DateTime.Now.AddHours(1).ToString("h:mm tt"), FontSize = 12, TextColor = isLightTheme ? Color.FromRgba("#49454F") : Color.FromRgba("#CAC4D0"), FontFamily = "Roboto-Regular" });
            deliveryPrimaryFormattedString.Spans.Add(new Span { Text = "\n", FontSize = 5 });

            shipmentInfoCollection = new ObservableCollection<StepProgressBarItem>();
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryFormattedText = orderFormattedString, ToolTipText = "Ordered" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryFormattedText = shippedFormattedString, ToolTipText = "Shipped" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryFormattedText = outForDeliveryFormattedString, ToolTipText = "Out for delivery" });
            shipmentInfoCollection.Add(new StepProgressBarItem() { PrimaryFormattedText = deliveryPrimaryFormattedString, ToolTipText = "Delivery in progress" });
#endif
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method used to convert the specified <see cref="DateTime"/> into a formatted string.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <returns></returns>
        private string GetFormattedDateText(DateTime dateTime)
        {
            string formattedDate = dateTime.ToString("ddd, d MMM \\'yy 'at' h:mm tt");
            return formattedDate;
        }

        /// <summary>
        /// Method to determine appropriate ordinal suffix for the day component of the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <returns></returns>
        private string GetDaySuffix(DateTime dateTime)
        {
            // Adding 'st', 'nd', 'rd', or 'th' to the day
            string daySuffix;
            switch (dateTime.Day % 10)
            {
                case 1 when dateTime.Day != 11:
                    daySuffix = "st";
                    break;
                case 2 when dateTime.Day != 12:
                    daySuffix = "nd";
                    break;
                case 3 when dateTime.Day != 13:
                    daySuffix = "rd";
                    break;
                default:
                    daySuffix = "th";
                    break;
            }

            return daySuffix;
        }

        #endregion

        #region Event

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Property changed

        /// <summary>
        /// Invoked when the step progress bar item collection changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}