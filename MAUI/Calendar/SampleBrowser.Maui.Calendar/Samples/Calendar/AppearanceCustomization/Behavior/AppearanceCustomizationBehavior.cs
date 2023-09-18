#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Calendar;

    /// <summary>
    /// Represents a class which contains appearance customization.
    /// </summary>
    internal class AppearanceCustomizationBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Calendar view 
        /// </summary>
        private SfCalendar? calendar;

        /// <summary>
        /// The combo box that allows users to choose to whether to select date or a range.
        /// </summary>
        private SfComboBox? comboBox;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
#if IOS || MACCATALYST
            Border border = bindable.Content.FindByName<Border>("border");
            border.IsVisible = true;
#if MACCATALYST
            border.Stroke = Color.FromArgb("#E6E6E6");
#else
            border.Stroke = Colors.Transparent;
#endif
            this.calendar = bindable.Content.FindByName<SfCalendar>("iOSCircleCalendar");
#else
            Frame frame = bindable.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
#if ANDROID
            frame.BorderColor = Colors.Transparent;
#else
            frame.BorderColor = Colors.Transparent;
#endif
            this.calendar = bindable.Content.FindByName<SfCalendar>("circleCalendar");
#endif
            this.calendar.SelectionBackground = Color.FromRgba("#0A3A74").WithAlpha(0.5f);
            this.calendar.SelectionShape = CalendarSelectionShape.Circle;
            this.comboBox = bindable.Content.FindByName<SfComboBox>("comboBox");
            this.comboBox.ItemsSource = new List<string>() { "Circle", "Rectangle" };
            this.comboBox.SelectedIndex = 0;
            this.comboBox.SelectionChanged += this.ComboBox_SelectionChanged;
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged -= this.ComboBox_SelectionChanged;
                this.comboBox = null;
            }
        }

        /// <summary>
        /// Method for Combo box selection type changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.calendar != null && e.CurrentSelection != null)
            {
                string? selectionShape = e.CurrentSelection[0].ToString();
                if (this.calendar.BindingContext is AppearanceViewModel)
                {
                    AppearanceViewModel appearanceViewModel = (AppearanceViewModel)this.calendar.BindingContext;
                    bool isCircleShape = selectionShape == "Circle";
                    appearanceViewModel.UpdateSelectionShape(isCircleShape);
                    if (isCircleShape)
                    {
                        this.calendar.SelectionShape = CalendarSelectionShape.Circle;
                    }
                    else
                    {
                        this.calendar.SelectionShape = CalendarSelectionShape.Rectangle;
                    }
                }
            }
        }
    }
}