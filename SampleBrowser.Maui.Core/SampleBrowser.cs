#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;

namespace SampleBrowser.Maui.Core
{
    /// <summary>
    /// ViewModel class for getting Samples from control.
    /// </summary>
    public static class SampleBrowser
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static bool IsIndividualSB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static int DeviceAndroidVersion { get; set; }

        /// <summary>
        /// Gets or sets Landscape mode.
        /// </summary>
        public static bool IsLandscapeMode { get; set; }

        /// <summary>
        /// Gets or sets ScreenWidth of Application.
        /// </summary>
        public static double ScreenWidth { get; set; }

        /// <summary>
        /// Gets or sets ScreenHeight of Application.
        /// </summary>
        public static double ScreenHeight { get; set; }

        /// <summary>
        /// Gets or sets NavigationBar Height of Application.
        /// </summary>
        public static double NavigationBarHeight { get; set; }

        /// <summary>
        /// Gets or sets StatusBar Height of Application.
        /// </summary>
        public static double StatusBarHeight { get; set; }

        /// <summary>
        /// Gets or sets Screen Density of Application.
        /// </summary>
        public static double Density { get; set; }

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
            bool isUpdated = false;
            ObservableCollection<SampleModel>? samples;
            if (!IsIndividualSB)
                samples = GetSamplesData("SampleBrowser.Maui.Samples." + title + ".SamplesList.SamplesList.xml", "SampleBrowser", ref isUpdated);
            else
                samples = GetSamplesData(assemblyName + ".SamplesList.SamplesList.xml", assemblyName, ref isUpdated);

            if (samples != null && !string.IsNullOrEmpty(assemblyName) && samples.Count > 0)
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
            if (sampleList != null && !string.IsNullOrEmpty(assemblyName) && sampleList.Count > 0)
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
        public static ObservableCollection<SampleModel> GetSamplesData(string xmlFileName, string assemblyName, ref bool isUpdated)
        {
            isUpdated = false;
            var samplesList = new ObservableCollection<SampleModel>();
            var controlName = assemblyName.Split('.').LastOrDefault();

            if (!IsIndividualSB)
                assemblyName = "SampleBrowser.Maui";

            var stream = GetSamplesList(xmlFileName, assemblyName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                var xmlReader = XmlReader.Create(reader);
                xmlReader.Read();

                string category = "";
                string updateGroupType = "";
                while (!xmlReader.EOF)
                {
                    if (xmlReader.Name == "SampleGroup" && xmlReader.IsStartElement())
                    {
                        var tag = GetDataFromXmlReader(xmlReader, "UpdateType");
                        updateGroupType = tag;
                        var title = GetDataFromXmlReader(xmlReader, "Title");
                        category = title;
                    }
                    else if (xmlReader.Name == "SampleGroup" && !xmlReader.IsStartElement())
                    {
                        updateGroupType = "";
                        var tag = GetDataFromXmlReader(xmlReader, "UpdateType");
                        updateGroupType = tag;
                        category = "None";
                    }

                    if (xmlReader.Name == "Sample" && xmlReader.IsStartElement())
                    {
                        var title = GetDataFromXmlReader(xmlReader, "Title");
                        var sample = new SampleModel
                        {
                            Title = title,
                            Description = GetDataFromXmlReader(xmlReader, "Description"),
                            Name = GetDataFromXmlReader(xmlReader, "Name"),
                            Icon = assemblyName + "." + GetDataFromXmlReader(xmlReader, "Icon"),
                            Category = category == "" ? "None" : category,
                            SearchTags = GetSampleSearchTags(xmlReader),
                            Control = controlName,
                            UpdateGroupType = updateGroupType,
                        };
// Need to remove following hard coded condiation added for List View Platform specific requirement 
                        var listTempVar = false;
#if WINDOWS
if(controlName == "SfListView")
{
listTempVar = true;
}
#endif

                        if (null != xmlReader.GetAttribute("IsUpdated") && GetDataFromXmlReader(xmlReader, "IsUpdated").ToLower() == "true")
                        {
                            sample.UpdateType = GetUpdateType(GetDataFromXmlReader(xmlReader, "IsUpdated"), "IsUpdated");
                            isUpdated = true;
                        }
                        else if (null != xmlReader.GetAttribute("IsNew") && GetDataFromXmlReader(xmlReader, "IsNew").ToLower() == "true")
                        {

                            if (!listTempVar)
                            {
                                sample.UpdateType = GetUpdateType(GetDataFromXmlReader(xmlReader, "IsNew"), "IsNew");
                                isUpdated = true;
                            }

                    }

                    if (sample != null)
                        {
                            AddSamples(xmlReader, samplesList, sample);
                        }
                    }

                    xmlReader.Read();
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

        private static string GetUpdateType(string value, string type)
        {
            string updateTag = string.Empty;
            string updateTypeValue = (value as string).ToLower();
            if (updateTypeValue == "true" && type == "IsPreview")
                updateTag = "preview";

            if (updateTypeValue == "true" && type == "IsNew")
                updateTag = "new";

            if (updateTypeValue == "true" && type == "IsUpdated")
                updateTag = "updated";

            return updateTag;
        }

        static void AddSamples(XmlReader reader, ObservableCollection<SampleModel> samplesCollection, SampleModel sample)
        {
            string value = "All";
            if (reader.GetAttribute("Platforms") != null)
            {
                reader.MoveToAttribute("Platforms");
                value = reader.Value;
            }

            if (DeviceInfo.Platform == DevicePlatform.Android && value.Contains("Android"))
                samplesCollection.Add(sample);
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                if (RunTimeDevice.IsMobileDevice() && value.Contains("iOS"))
                    samplesCollection.Add(sample);
                else if (!RunTimeDevice.IsMobileDevice() && value.Contains("MacCatalyst"))
                    samplesCollection.Add(sample);
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI && value.Contains("Windows"))
                samplesCollection.Add(sample);
            if (value.Contains("All"))
                samplesCollection.Add(sample);
        }

#endregion
    }
}
