#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban
{
    using System.Reflection;
    using SampleBrowser.Maui.Base;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var appInfo = typeof(App).GetTypeInfo().Assembly;
            BaseConfig.IsIndividualSB = true;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            var mainPage = BaseConfig.MainPageInit(assembly);
            return new Window(mainPage);
        }
    }
}