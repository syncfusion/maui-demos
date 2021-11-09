using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.Core
{
    /// <summary>
    /// ViewModel class for getting Samples from control.
    /// </summary>
    public static class SampleBrowser
    {
        #region Properties

        public static bool IsIndividualSB;

        public static int DeviceAndroidVersion;

        /// <summary>
        /// Gets or sets Landscape mode.
        /// </summary>
        public static bool IsLandscapeMode;

        /// <summary>
        /// Gets or sets ScreenWidth of Application.
        /// </summary>
        public static double ScreenWidth;

        /// <summary>
        /// Gets or sets ScreenHeight of Application.
        /// </summary>
        public static double ScreenHeight;

        /// <summary>
        /// Gets or sets NavigationBar Height of Application.
        /// </summary>
        public static double NavigationBarHeight;

        /// <summary>
        /// Gets or sets StatusBar Height of Application.
        /// </summary>
        public static double StatusBarHeight;

        /// <summary>
        /// Gets or sets Screen Density of Application.
        /// </summary>
        public static double Density;

        #endregion

        #region methods

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBrowser"/> class.
        /// </summary>
        public static NavigationPage GetMainPage(object controlName, string assemblyName)
        {
            IsIndividualSB = true;
            return GetNavigationPage(controlName.ToString(), assemblyName, null);
        }


        public static NavigationPage GetMainPage(string controlName, string assemblyName, string title)
        {
            return GetNavigationPage(controlName, assemblyName, title);
        }

        static NavigationPage GetNavigationPage(string? controlName, string assemblyName, string? title)
        {
            ContentPage contentPage;
            ObservableCollection<SampleModel>? samples = null;

            if (!IsIndividualSB)
                samples = GetSamplesData("SampleBrowser.Maui.Samples." + title + ".SamplesList.SamplesList.xml", "SampleBrowser");
            else
                samples = GetSamplesData(assemblyName + ".SamplesList.SamplesList.xml", assemblyName);

            if (samples != null && !string.IsNullOrEmpty(assemblyName) && samples.Count() > 0)
                contentPage = new SamplePage(samples, controlName, samples[0], title);

            else
                contentPage = new ContentPage();

            return new NavigationPage(contentPage)
            {
                BarTextColor = Colors.White,
                BarBackgroundColor = Color.FromArgb("007DE6")
            };
        }

        /// <summary>
        /// Returns ContentPage based on SamplesCount in control.
        /// </summary>
        public static ContentPage GetSamplesPage(ObservableCollection<SampleModel> sampleList, string assemblyName, string controlName, string title)
        {
            if (sampleList != null && !string.IsNullOrEmpty(assemblyName) && sampleList.Count() > 0)
            {
                return new SamplePage(sampleList, controlName, sampleList[0], title);
            }
            else
                return new ContentPage();
        }

        public static Type? GetAssembliesType(string assemblyName, string sampleName)
        {
            Assembly assm = Assembly.Load(assemblyName + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            return assm.GetType(assemblyName + "." + sampleName);
        }

        public static Type? GetAssembliesType(string assemblyName, string controlName, string sampleName)
        {
            Assembly assm = Assembly.Load(assemblyName + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            return assm.GetType(assemblyName + "." + controlName + "." + sampleName);
        }

        public static Stream? GetSamplesList(string xmlFilePath, string assemblyName)
        {
            Assembly assm = Assembly.Load(assemblyName + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            return assm.GetManifestResourceStream(xmlFilePath);
        }

        /// <summary>
        /// Gets or sets Samples in the control.
        /// </summary>
        public static ObservableCollection<SampleModel> GetSamplesData(string xmlFileName, string assemblyName)
        {
            var samplesList = new ObservableCollection<SampleModel>();
            var controlName = assemblyName.Split('.').LastOrDefault();

            if (!IsIndividualSB)
                assemblyName = "SampleBrowser.Maui";

            var stream = GetSamplesList(xmlFileName, assemblyName);
            if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    var xmlReader = XmlReader.Create(reader);
                    xmlReader.Read();

                    string category = "";

                    while (!xmlReader.EOF)
                    {
                        if (xmlReader.Name == "SampleGroup" && xmlReader.IsStartElement())
                        {
                            var title = GetDataFromXmlReader(xmlReader, "Title");
                            category = title;                          
                        }
                        else if (xmlReader.Name == "SampleGroup" && !xmlReader.IsStartElement())
                            category = "None";

                        if (xmlReader.Name == "Sample" && xmlReader.IsStartElement())
                        {
                            var title = GetDataFromXmlReader(xmlReader, "Title");
                            var sample = new SampleModel
                            {
                                Title = title,
                                Description = GetDataFromXmlReader(xmlReader, "Description"),
                                Name = GetDataFromXmlReader(xmlReader, "Name"),
                                Icon = assemblyName + "." + GetDataFromXmlReader(xmlReader, "Icon"),
                                Category = category,
                                SearchTags = GetSampleSearchTags(xmlReader),
                                Control = controlName
                            };

                            if (sample != null)
                            {
                                AddSamples(xmlReader, samplesList, sample);
                            }
                        }

                        xmlReader.Read();
                    }
                }
            }

            return samplesList;
        }

        public static string GetDataFromXmlReader(XmlReader reader, string attribute)
        {
            reader.MoveToAttribute(attribute);
            return reader.Value;
        }

        private static string[]? GetSampleSearchTags(XmlReader xmlReader)
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
            else if (null != xmlReader.GetAttribute("IsNew"))
            {
                if (xmlReader.GetAttribute("SearchTags") != null)
                {
                    xmlReader.MoveToAttribute("SearchTags");
                    tags = ((xmlReader.Value as string) + ",IsNew,New").Split(',');
                    return (tags.Length > 0) ? tags : null;
                }
                else
                {
                    return new string[] { "IsNew,New" };
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

        static void AddSamples(XmlReader reader, ObservableCollection<SampleModel> samplesCollection, SampleModel sample)
        {
            string value = "All";
            if (reader.GetAttribute("Platforms") != null)
            {
                reader.MoveToAttribute("Platforms");
                value = reader.Value;
            }

            if (Device.RuntimePlatform == Device.Android && value.Contains("Android"))
                samplesCollection.Add(sample);
            else if (Device.RuntimePlatform == Device.iOS && value.Contains("iOS"))
                samplesCollection.Add(sample);
            else if (value.Contains("All"))
                samplesCollection.Add(sample);
        }

        #endregion
    }
}