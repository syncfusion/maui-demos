using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class FoodMenu : INotifyPropertyChanged
    {
        #region Field        
        
        private string foodName;
        private bool isSelected;

        #endregion

        #region Properties

        public string FoodName
        {
            get
            {
                return foodName;
            }
            set
            {
                foodName = value;
                RaisedOnPropertyChanged("FoodName");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                RaisedOnPropertyChanged("IsSelected");
            }
        }
        
        #endregion

        public FoodMenu()
        {
        }

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion

    }
}
