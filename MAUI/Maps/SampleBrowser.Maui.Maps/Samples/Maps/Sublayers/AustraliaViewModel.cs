using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Maps
{
    public class AustraliaViewModel
    {
        public ObservableCollection<AustraliaModel> Data { get; set; }

        public AustraliaViewModel()
        {
            Data = new ObservableCollection<AustraliaModel>()
            {
                new AustraliaModel("New South Wales",5),
                new AustraliaModel("Queensland",23),
                new AustraliaModel("Northern Territory",56),
                new AustraliaModel("Victoria",16),
                new AustraliaModel("Western Australia",43),
                new AustraliaModel("South Australia",26)
           };    
        }
    }

}
