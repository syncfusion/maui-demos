using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCircularChart
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Brush> CustomBrushes { get; set; }
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

            CustomBrushes = new ObservableCollection<Brush>()
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

        public static object InvokeInternalMethod(Type type, object obj, string methodName, params object[] args)
        {
            var method = type.GetTypeInfo().GetDeclaredMethod(methodName);
            return method != null ? method.Invoke(obj, args) : null;
        }

        public static object GetInternalProperty(Type type, object obj, string propertyName)
        {
            var property = type.GetTypeInfo().GetDeclaredProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(obj);
            }

            return null;
        }
    }
}
