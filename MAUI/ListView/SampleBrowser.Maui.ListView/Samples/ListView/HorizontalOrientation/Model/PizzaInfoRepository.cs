#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class PizzaInfoRepository
    {
        #region Constructor

        public PizzaInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<PizzaInfo> GetPizzaInfo()
        {
            var categoryInfo = new ObservableCollection<PizzaInfo>();
            for (int i = 0; i < PizzaNames.Length; i++)
            {
                var info = new PizzaInfo() { PizzaName = PizzaNames[i] };

                if (i == 9)
                    info.PizzaImage = "pizza3.jpg";
                else
                    info.PizzaImage = "pizza" + i + ".jpg";

                categoryInfo.Add(info);
            }
            return categoryInfo;
        }

        internal ObservableCollection<PizzaInfo> GetPizzaInfo1()
        {
            var categoryInfo = new ObservableCollection<PizzaInfo>();

            for (int i = 0; i < PizzaNames1.Length; i++)
            {
                var info = new PizzaInfo() { PizzaName = PizzaNames1[i] };

                if (i == 9)
                    info.PizzaImage = "pizza12.jpg";
                else
                    info.PizzaImage = "pizza" + (i + 9) + ".jpg";

                categoryInfo.Add(info);
            }
            return categoryInfo;
        }

        #endregion

        #region CategoryInfo

        readonly string[] PizzaNames = new string[]
        {
            "Supreme",
            "GodFather",
            "Ciao-ciao",
            "Frutti di mare",
            "Kebabpizza",
            "Napolitana",
            "Double Cheese",
            "Gourmet",
            "Mr Wedge",
            "Vegorama",
        };
        readonly string[] PizzaNames1 = new string[]
        {
            "Margherita",
            "Funghi",
            "Capriciosa",
            "Stagioni",
            "Vegetariana",
            "Formaggi",
            "Marinara",
            "Peperoni",
            "apolitana",
            "Hawaii"
        };

        #endregion
    }
}
