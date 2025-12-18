#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
