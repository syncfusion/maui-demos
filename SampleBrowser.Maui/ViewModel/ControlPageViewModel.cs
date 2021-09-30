#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui
{
    public class ControlPageViewModel 
    {
        #region fields
        
        private ObservableCollection<ControlModel> dataVisualizationControlsList;

        private ObservableCollection<ControlModel> layoutControlsList;

        private List<string> categoryList;

        private IEnumerable<SampleModel> samplesList;

        private AssemblyName controlName;
        
        #endregion

        #region ctor

        public ControlPageViewModel()
        {
            dataVisualizationControlsList = new ObservableCollection<ControlModel>();
            layoutControlsList = new ObservableCollection<ControlModel>();


            samplesList = new ObservableCollection<SampleModel>();

            categoryList = new List<string>();

            PopulateControlsList();
        }

        #endregion

        #region properties

        public ObservableCollection<ControlModel> DataVisualizationControlsList
        {
            get { return dataVisualizationControlsList; }
            set { dataVisualizationControlsList = value; }
        }

        public ObservableCollection<ControlModel> LayoutControlsList
        {
            get { return layoutControlsList; }
            set { layoutControlsList = value; }
        }

        public IEnumerable<SampleModel> SamplesList
        {
            get { return samplesList; }
            set { samplesList = value; }
        }

        public List<string> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; }
        }

        #endregion

        #region methods

        private void PopulateControlsList()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("SampleBrowser.Maui.ControlsList.ControlsList.xml");
            string currentControlTitle = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                var xmlReader = XmlReader.Create(reader);
                xmlReader.Read();

                while (!xmlReader.EOF)
                {
                    if (xmlReader.Name == "Group" && xmlReader.IsStartElement())
                    {
                        if (xmlReader.HasAttributes)
                        {
                            string displayName = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "Title").Substring(2);
                            var controlModel = new ControlModel
                            {
                                ImageId = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "ImageId"),
                                Title = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "Title"),
                                ControlName = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "ControlName"),
                                CategoryName = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "CategoryName"),
                            };
                            try
                            {
                                controlName = new AssemblyName("SampleBrowser.Maui" + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                                var samples = Maui.Core.SampleBrowser.GetSamplesData("SampleBrowser.Maui.Samples." + controlModel.ControlName + ".SamplesList.SamplesList.xml", "SampleBrowser.Maui." + controlModel.ControlName);

                                if (samples.Count > 0 && samples != null)
                                {
                                    controlModel.Samples = samples;
                                    controlModel.SamplesCount = samples.Count;
                                    samplesList = samplesList.Concat(samples);
                                }
                            }
                            catch
                            {
                            }

                            controlModel.Description = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "Description");
                            controlModel.Tags = GetControlSearchTags(xmlReader);
                            
                            currentControlTitle = controlModel.Title;
                            if (controlModel != null)
                            {
                                AddControls(xmlReader, controlModel);
                                if(controlModel.CategoryName !="" && !categoryList.Contains(controlModel.CategoryName))
                                {
                                    categoryList.Add(controlModel.CategoryName);
                                }
                            }
                        }
                    }

                    xmlReader.Read();
                }
            }
        }


        void AddControls(XmlReader reader, ControlModel control)
        {
            string value = "All";
            if (reader.GetAttribute("Platforms") != null)
            {
                reader.MoveToAttribute("Platforms");
                value = reader.Value;
            }
            if (control.CategoryName.Equals("Data Visualization"))
            {
                DataVisualizationControlsList.Add(control);
            }
            else
            {
                LayoutControlsList.Add(control);
            }
        }

        private string[] GetControlSearchTags(XmlReader xmlReader)
        {
            string[] tags;

            if (null != xmlReader.GetAttribute("IsUpdated"))
            {
                if (xmlReader.GetAttribute("SearchTags") != null)
                {
                    xmlReader.MoveToAttribute("SearchTags");
                    tags = ((xmlReader.Value as string) + ",IsUpdated,Updated").Split(',');
                    return (tags.Length > 0) ? tags : null;
                }
                else
                {
                    return new string[] { "IsUpdated,Updated" };
                }
            }
            else if (null != xmlReader.GetAttribute("IsPreview") || null != xmlReader.GetAttribute("IsNew"))
            {
                if (xmlReader.GetAttribute("SearchTags") != null)
                {
                    xmlReader.MoveToAttribute("SearchTags");
                    tags = ((xmlReader.Value as string) + ",IsPreview,IsNew,New,Preview").Split(',');
                    return (tags.Length > 0) ? tags : null;
                }
                else
                {
                    return new string[] { "IsPreview,IsNew,New,Preview" };
                }
            }
            else if (xmlReader.GetAttribute("SearchTags") != null)
            {
                xmlReader.MoveToAttribute("SearchTags");
                tags = xmlReader.Value.Split(',');
                return (tags.Length > 0) ? tags : null;
            }

            return null;
        }

        #endregion
    }
}