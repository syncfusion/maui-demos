#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class GenerateGridReport
    {
        public ObservableCollection<GenerateModel>? GenerateDataSource { get; set; }
    }
    public class GenerateDataCollection
    {
        public ObservableCollection<GenerateModel> _predictivedata;
        public ObservableCollection<GenerateModel> Predictivedatas { get { return _predictivedata; } set { _predictivedata = value; } }
        public GenerateDataCollection()
        {
            _predictivedata = new ObservableCollection<GenerateModel>();
            _predictivedata.Add(new GenerateModel(2001, "John Smith", 4.7, 4.1, 5.0));
            _predictivedata.Add(new GenerateModel(2002, "Emily Davis", 3.3, 3.5, 3.7));
            _predictivedata.Add(new GenerateModel(2003, "Micheal Lee", 3.9, 3.8, 3.9));
            _predictivedata.Add(new GenerateModel(2004, "Sarah Brown", 2.0, 2.7, 2.5));
            _predictivedata.Add(new GenerateModel(2005, "James Wilson", 3.0, 3.5, 3.2));
            _predictivedata.Add(new GenerateModel(2006, "Sarah Jane", 3.7, 3.0, 4.3));
            _predictivedata.Add(new GenerateModel(2007, "Emily Rose", 5.0, 4.9, 4.8));
            _predictivedata.Add(new GenerateModel(2008, "John Michael", 4.0, 4.1, 4.2));
            _predictivedata.Add(new GenerateModel(2009, "David James", 1.5, 2.2, 2.3));
            _predictivedata.Add(new GenerateModel(2010, "Mary Ann", 2.7, 2.1, 3.0));
            _predictivedata.Add(new GenerateModel(2011, "Robert Paul", 2.9, 3.7, 3.9));
            _predictivedata.Add(new GenerateModel(2012, "Laura Grace", 4.0, 3.1, 3.7));
            _predictivedata.Add(new GenerateModel(2013, "William Henry", 4.0, 4.1, 4.2));
            _predictivedata.Add(new GenerateModel(2014, "Anna Marie", 4.0, 4.0, 4.1));
            _predictivedata.Add(new GenerateModel(2015, "Charles Thomas", 4.7, 4.8, 4.9));
            _predictivedata.Add(new GenerateModel(2016, "Jennifer Lynn", 3.0, 3.1, 3.2));
            _predictivedata.Add(new GenerateModel(2017, "Christopher Lee", 3.9, 3.9, 4.2));
            _predictivedata.Add(new GenerateModel(2018, "Elizabeth Claire", 2.0, 2.1, 2.2));
            _predictivedata.Add(new GenerateModel(2019, "Daniel Scott", 4.0, 4.1, 3.3));
            _predictivedata.Add(new GenerateModel(2020, "Megan Louise", 3.0, 3.5, 3.5));
            
        }

    }
}
