#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfPicker
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Picker;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view.
        /// </summary>
        private SfPicker? picker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Switch? showHeaderSwitch, showFooterSwitch;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

#if IOS || MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = Color.FromArgb("#E6E6E6");
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker1");
#else
            Frame frame = sampleView.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
            frame.BorderColor = Color.FromArgb("#E6E6E6");
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker");
#endif

            this.picker.TextStyle = new PickerTextStyle()
            {
                TextColor = Color.FromRgba(0, 0, 0, 128),
            };

            this.picker.SelectedTextStyle = new PickerTextStyle()
            {
                TextColor = Colors.Black,
            };

            this.showHeaderSwitch = sampleView.Content.FindByName<Switch>("showHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Switch>("showFooterSwitch");

            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.Toggled += ShowHeaderSwitch_Toggled;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.Toggled += ShowFooterSwitch_Toggled;
            }
        }

        /// <summary>
        /// Method for show header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowHeaderSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.picker != null)
            {
                if (e.Value == true)
                {
                    this.picker.HeaderView = new PickerHeaderView()
                    {
                        Height = 50,
                        Text = "Select a Country",
                        Background = Color.FromArgb("#6750A4"),
                        TextStyle = new PickerTextStyle()
                        {
                            TextColor = Colors.White,
                            FontSize = 15,
                        },
                    };
                }
                else if (e.Value == false)
                {
                    this.picker.HeaderView = new PickerHeaderView()
                    {
                        Height = 0,
                    };
                }
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.picker != null)
            {
                this.picker.ColumnHeaderView.Height = e.Value == true ? 40 : 0;
            }
        }

        /// <summary>
        /// Method for show footer switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowFooterSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.picker != null)
            {
                if (e.Value == true)
                {
                    this.picker.FooterView = new PickerFooterView()
                    {
                        Height = 40,
                        TextStyle = new PickerTextStyle()
                        {
                            TextColor = Color.FromArgb("#6750A4"),
                            FontSize = 15,
                        },
                    };
                }
                else if (e.Value == false)
                {
                    this.picker.FooterView = new PickerFooterView()
                    {
                        Height = 0,
                    };
                }
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.Toggled -= ShowHeaderSwitch_Toggled;
                this.showHeaderSwitch = null;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.Toggled -= ShowFooterSwitch_Toggled;
                this.showFooterSwitch = null;
            }
        }

        public GettingStartedBehavior()
        {
        }
    }
}