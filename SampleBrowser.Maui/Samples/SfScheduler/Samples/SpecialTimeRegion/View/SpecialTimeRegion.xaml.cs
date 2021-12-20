using SampleBrowser.Maui.Core;
using System;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// Interaction logic for SpecialTimeRegion.xaml
    /// </summary>
    public partial class SpecialTimeRegion : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialTimeRegion" /> class.
        /// </summary>
        public SpecialTimeRegion()
        {
            InitializeComponent();
            Scheduler.SelectableDayPredicate = (date) =>
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return false;
                }

                return true;
            };
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Scheduler.Handler?.DisconnectHandler();
        }
    }
}