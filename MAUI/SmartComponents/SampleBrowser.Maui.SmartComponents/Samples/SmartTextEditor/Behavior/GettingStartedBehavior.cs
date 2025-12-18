#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartTextEditor
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.SmartComponents;
    using System.Collections.ObjectModel;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// The instance of SfSmartTextEditor.
        /// </summary>
        private SfSmartTextEditor? smartTextEditor;

        /// <summary>
        /// The instance of SfComboBox.
        /// </summary>
        private SfComboBox? modeComboBox, comboBox;

        /// <summary>
        /// The instance of SfNumericEntry.
        /// </summary>
        private SfNumericEntry? maxLengthEntry;

#if WINDOWS
        /// <summary>
        /// The instance of Grid.
        /// </summary>
        private Grid? grid;
#endif
        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
#if WINDOWS
            this.grid = sampleView.Content.FindByName<Grid>("grid");
            this.grid.SizeChanged += Grid_SizeChanged;
#endif
            this.smartTextEditor = sampleView.Content.FindByName<SfSmartTextEditor>("smartTextEditor");
            this.comboBox = sampleView.Content.FindByName<SfComboBox>("comboBox");
            this.modeComboBox = sampleView.Content.FindByName<SfComboBox>("modeComboBox");
            this.maxLengthEntry = sampleView.Content.FindByName<SfNumericEntry>("maxLengthEntry");
            this.modeComboBox.ItemsSource = new ObservableCollection<object>()
            {
                "Inline",
                "Popup",
            };

            this.modeComboBox.SelectedIndex = 0;
            this.modeComboBox.SelectionChanged += SuggestionModeComboBox_SelectionChanged;
            this.maxLengthEntry.ValueChanged += MaxLengthEntry_ValueChanged;
            this.comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }
#if WINDOWS
        /// <summary>
        /// Method to handle SizeChanged event of Grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Grid_SizeChanged(object? sender, EventArgs e)
        {
            if (Application.Current != null && Application.Current.Windows.Count > 0)
            {
                var width = Application.Current.Windows[0].Width;
                if (this.comboBox != null && this.comboBox.CustomView is Label label)
                {
                    label.WidthRequest = width <= 1250 ? 290 : 550;
                }
            }
        }
#endif
        /// <summary>
        /// Method to handle SelectionChanged event of ComboBox.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">Event Args.</param>
        private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.smartTextEditor == null || e.AddedItems == null || this.comboBox == null)
            {
                return;
            }
            
            this.smartTextEditor.Text = string.Empty;
        }

        /// <summary>
        /// Method to handle SelectionChanged event of SuggestionModeComboBox.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">Event Args.</param>
        private void SuggestionModeComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.smartTextEditor == null || e.AddedItems == null || this.modeComboBox == null)
            {
                return;
            }

            string? mode = e.AddedItems[0].ToString();
            switch (mode)
            {
                case "Inline":
                    this.smartTextEditor.SuggestionDisplayMode = SuggestionDisplayMode.Inline;
                    break;
                case "Popup":
                    this.smartTextEditor.SuggestionDisplayMode = SuggestionDisplayMode.Popup;
                    break;
            }
        }

        /// <summary>
        /// Method to handle ValueChanged event of MaxLengthEntry.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">Event Args.</param>
        private void MaxLengthEntry_ValueChanged(object? sender, NumericEntryValueChangedEventArgs e)
        {
            if (this.smartTextEditor == null || e.NewValue == null)
            {
                return;
            }

            int newValue = (int)e.NewValue;
            this.smartTextEditor.MaxLength = newValue;
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.modeComboBox != null)
            {
                this.modeComboBox.SelectionChanged -= SuggestionModeComboBox_SelectionChanged;
            }

            if (this.maxLengthEntry != null)
            {
                this.maxLengthEntry.ValueChanged -= MaxLengthEntry_ValueChanged;
            }

            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged -= ComboBox_SelectionChanged;
            }
#if WINDOWS
            if (this.grid != null)
            {
                this.grid.SizeChanged -= Grid_SizeChanged;
            }
#endif
        }

        /// <summary>
        /// Initializes a new instance of the GettingStartedBehavior class.
        /// </summary>
        public GettingStartedBehavior()
        {
        }
    }
}