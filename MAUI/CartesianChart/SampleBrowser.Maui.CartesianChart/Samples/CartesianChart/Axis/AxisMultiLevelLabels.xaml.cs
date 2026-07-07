using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class AxisMultiLevelLabels : SampleView
    {
        public AxisMultiLevelLabels()
        {
            InitializeComponent();
            InitializePickerSelection();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            axisChart.Handler?.DisconnectHandler();
        }
        private void InitializePickerSelection()
        {
            BorderTypePicker.SelectedIndex = 0;
            BorderColorPicker.SelectedIndex = 0;
        }

        private double currentBorderWidth = 2;

        private void OnIncrementBorderWidthClicked(object sender, EventArgs e)
        {
            if (currentBorderWidth < 5)
            {
                currentBorderWidth += 1;
                BorderWidthLabel.Text = currentBorderWidth.ToString();
                UpdateBorderWidth();
            }
        }

        private void OnDecrementBorderWidthClicked(object sender, EventArgs e)
        {
            if (currentBorderWidth > 0)
            {
                currentBorderWidth -= 1;
                BorderWidthLabel.Text = currentBorderWidth.ToString();
                UpdateBorderWidth();
            }
        }

        private void UpdateBorderWidth()
        {
            if (XAxisLabelStyle != null)
                XAxisLabelStyle.StrokeThickness = currentBorderWidth;
        }

        private void OnBorderTypeChanged(object sender, EventArgs e)
        {
            int selectedIndex = BorderTypePicker.SelectedIndex;
            if (selectedIndex < 0) return;

            string borderTypeString = BorderTypePicker.Items[selectedIndex];
            ChartMultiLevelBorderType borderType = ConvertToBorderType(borderTypeString);

            if (XAxisLabelStyle != null)
                XAxisLabelStyle.BorderType = borderType;
        }
        private void OnBorderColorChanged(object sender, EventArgs e)
        {
            int selectedIndex = BorderColorPicker.SelectedIndex;

            string colorString = BorderColorPicker.Items[selectedIndex];
            Color borderColor = ConvertToColor(colorString);

            if (XAxisLabelStyle != null)
                XAxisLabelStyle.Stroke = borderColor;
        }

        private ChartMultiLevelBorderType ConvertToBorderType(string borderType)
        {
            return borderType switch
            {
                "Rectangle" => ChartMultiLevelBorderType.Rectangle,
                "WithoutTopAndBottom" => ChartMultiLevelBorderType.WithoutTopAndBottom,
                "SquareBrace" => ChartMultiLevelBorderType.SquareBrace,
                "CurlyBrace" => ChartMultiLevelBorderType.CurlyBrace,
                _ => ChartMultiLevelBorderType.Rectangle
            };
        }

        private Color ConvertToColor(string colorName)
        {
            return colorName switch
            {
                "Gray" => Colors.DarkGray,
                "Blue" => Colors.Blue,
                "Green" => Colors.Green,
                "Black" => Colors.Black,
                "Purple" => Colors.Purple,
                _ => Colors.Black
            };
        }
    }
}