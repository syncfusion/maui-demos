using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class ShapeAnnotationViewModel : INotifyPropertyChanged
    {
        private bool _isDragDropEnabled = false;
        private Color _selectedColor = Colors.Black;

        public bool IsDragDropEnabled
        {
            get => _isDragDropEnabled;
            set
            {
                if (_isDragDropEnabled != value)
                {
                    _isDragDropEnabled = value;
                    OnPropertyChanged();
                    DragDropEnabledChanged?.Invoke(this, value);
                }
            }
        }

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event EventHandler<bool>? DragDropEnabledChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
