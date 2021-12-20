using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using SampleBrowser.Maui.Core;
using System;
using Microsoft.Maui.Controls;
using System.Globalization;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui
{
    public class ControlPageViewModel
    {
        #region fields

        private ObservableCollection<ControlModel> dataVisualizationControlsList;

        private ObservableCollection<ControlModel> layoutControlsList;

        private ObservableCollection<ControlModel> notificationControlsList;

        private ObservableCollection<ControlModel> miscellaneousControlsList;

        private ObservableCollection<ControlModel> navigationControlsList;

        private ObservableCollection<ControlModel> editorsControlsList;

        private ObservableCollection<ControlModel> slidersControlsList;

        private ObservableCollection<ControlModel> calendarControlsList;

        private ObservableCollection<ControlModel> fileFormatControlsList;

        private List<string> categoryList;

        private IEnumerable<SampleModel> samplesList;

        private AssemblyName controlName;

        #endregion

        #region constructor

        public ControlPageViewModel()
        {
            dataVisualizationControlsList = new ObservableCollection<ControlModel>();
            layoutControlsList = new ObservableCollection<ControlModel>();
            navigationControlsList = new ObservableCollection<ControlModel>();
            editorsControlsList = new ObservableCollection<ControlModel>();
            slidersControlsList = new ObservableCollection<ControlModel>();
            miscellaneousControlsList = new ObservableCollection<ControlModel>();
            calendarControlsList = new ObservableCollection<ControlModel>();
            fileFormatControlsList = new ObservableCollection<ControlModel>();
            notificationControlsList = new ObservableCollection<ControlModel>();


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

        public ObservableCollection<ControlModel> NavigationControlsList
        {
            get { return navigationControlsList; }
            set { navigationControlsList = value; }
        }

        public ObservableCollection<ControlModel> MiscellaneousControlsList
        {
            get { return miscellaneousControlsList; }
            set { miscellaneousControlsList = value; }
        }


        public ObservableCollection<ControlModel> CalendarControlsList
        {
            get { return calendarControlsList; }
            set { calendarControlsList = value; }
        }

        public ObservableCollection<ControlModel> EditorsControlsList
        {
            get { return editorsControlsList; }
            set { editorsControlsList = value; }
        }

        public ObservableCollection<ControlModel> SlidersControlsList
        {
            get { return slidersControlsList; }
            set { slidersControlsList = value; }
        }

        public ObservableCollection<ControlModel> NotificationControlsList
        {
            get { return notificationControlsList; }
            set { notificationControlsList = value; }
        }

        public ObservableCollection<ControlModel> FileFormatControlsList
        {
            get { return fileFormatControlsList; }
            set { fileFormatControlsList = value; }
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
            if (stream != null)
            {
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
                                controlModel.TagType = Maui.Core.SampleBrowser.GetDataFromXmlReader(xmlReader, "TagType");

                                if(controlModel.TagType == "New")
                                {
                                    controlModel.TagColor = Color.FromArgb("#608C1B");
                                }

                                currentControlTitle = controlModel.Title;
                                if (controlModel != null)
                                {
                                    AddControls(xmlReader, controlModel);
                                    if (controlModel.CategoryName != "" && !categoryList.Contains(controlModel.CategoryName))
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
            else if (control.CategoryName.Equals("Layout"))
            {
                LayoutControlsList.Add(control);
            }
            else if (control.CategoryName.Equals("Calendar"))
            {
                CalendarControlsList.Add(control);
            }
            else if (control.CategoryName.Equals("Editors"))
            {
                EditorsControlsList.Add(control);
            }
            else if (control.CategoryName.Equals("Notification"))
            {
                NotificationControlsList.Add(control);
            }
            else if (control.CategoryName.Equals("Navigation"))
            {
                NavigationControlsList.Add(control);
            }
            else if (control.CategoryName.Equals("Miscellaneous"))
            {
                MiscellaneousControlsList.Add(control);
            }
            else
            {
                FileFormatControlsList.Add(control);
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