#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// Interaction logic for RecursiveAppointments.xaml
    /// </summary>
    public partial class RecursiveAppointments : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecursiveAppointments" /> class.
        /// </summary>
        public RecursiveAppointments()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Scheduler.Handler?.DisconnectHandler();
        }
    }
}