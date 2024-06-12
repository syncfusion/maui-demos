#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteMultiSelection.Model
{
    public class Applicationlist
    {
        private ImageSource? appimage;
        public ImageSource? AppImage
        {
            get { return appimage; }
            set
            {
                appimage = value;
            }
        }
        private string name = string.Empty;
        public string AppName
        {
            get { return name; }
            set { name = value; }
        }
        private string date = string.Empty;
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    } 
}