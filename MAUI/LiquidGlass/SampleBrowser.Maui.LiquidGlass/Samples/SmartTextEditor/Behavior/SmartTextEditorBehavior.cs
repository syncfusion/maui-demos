using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.SmartComponents;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.LiquidGlass.SfSmartTextEditor
{
	public class SmartTextEditorBehavior : Behavior<SampleView>
	{
        /// <summary>
        /// The instance of SfSmartTextEditor.
        /// </summary>
        private Syncfusion.Maui.SmartComponents.SfSmartTextEditor? smartTextEditor;

        /// <summary>
        /// The instance of SfComboBox.
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.smartTextEditor = sampleView.Content.FindByName<Syncfusion.Maui.SmartComponents.SfSmartTextEditor>("smartTextEditor");
            this.comboBox = sampleView.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            this.comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

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
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged -= ComboBox_SelectionChanged;
            }
        }

        /// <summary>
        /// Initializes a new instance of the SmartTextEditorBehavior class.
        /// </summary>
        public SmartTextEditorBehavior()
        {
        }
	}
}