using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Maps
{

    public class AustraliaModel
    {
        public AustraliaModel(string state, int size)
        {
            State = state;
            Size = size;
        }

        public string State
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }
    }
}
