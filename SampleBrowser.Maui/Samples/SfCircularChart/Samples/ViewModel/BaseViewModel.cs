#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.SfCircularChart
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Brush> PaletteBrushes { get; set; }
        public ObservableCollection<Brush> SelectionBrushes { get; set; }

        public ObservableCollection<Brush> CustomColor2 { get; set; }
        public ObservableCollection<Brush> AlterColor1 { get; set; }

        private bool enableAnimation = true;
        public bool EnableAnimation
        {
            get { return enableAnimation; }
            set
            {
                enableAnimation = value;
                OnPropertyChanged("EnableAnimation");
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public BaseViewModel()
        {
            if (!RunTimeDevice.IsMobileDevice())
            {
                EnableAnimation = false;
            }

            PaletteBrushes = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Color.FromArgb("#314A6E")),
                 new SolidColorBrush(Color.FromArgb("#48988B")),
                 new SolidColorBrush(Color.FromArgb("#5E498C")),
                 new SolidColorBrush(Color.FromArgb("#74BD6F")),
                 new SolidColorBrush(Color.FromArgb("#597FCA"))
            };

            SelectionBrushes = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#40314A6E")),
                new SolidColorBrush(Color.FromArgb("#4048988B")),
                new SolidColorBrush(Color.FromArgb("#405E498C")),
                new SolidColorBrush(Color.FromArgb("#4074BD6F")),
                new SolidColorBrush(Color.FromArgb("#40597FCA"))
            };

            CustomColor2 = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#519085")),
                new SolidColorBrush(Color.FromArgb("#F06C64")),
                new SolidColorBrush(Color.FromArgb("#FDD056")),
                new SolidColorBrush(Color.FromArgb("#81B589")),
                new SolidColorBrush(Color.FromArgb("#88CED2"))
            };

            AlterColor1 = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#346bf5")),
                new SolidColorBrush(Color.FromArgb("#ff9d00")),
            };
        }
    }
}
