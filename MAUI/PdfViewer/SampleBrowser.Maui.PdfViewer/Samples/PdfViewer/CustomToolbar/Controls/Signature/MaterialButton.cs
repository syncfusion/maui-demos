#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Core.Internals;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class MaterialButton : Button, ITouchListener
    {
        #region Variables
        bool IsMouseOver = false;
        Color m_normalBorderColor = Colors.Transparent;
        Color m_hoverBorderColor = Colors.Transparent;
        Color m_pressedBorderColor = Colors.Transparent;
        Color m_disabledBorderColor = Colors.Transparent;
        Color m_normalBackgroundColor = Colors.Transparent;
        Color m_hoverBackgroundColor = Color.FromRgba(73, 69, 79, 20);
        Color m_pressedBackgroundColor = Color.FromRgba(73, 69, 79, 31);
        Color m_disabledBackgroundColor = Colors.Transparent;
        Color m_normalTextColor = Color.FromRgb(103, 80, 164);
        Color m_hoverTextColor = Color.FromRgb(103, 80, 164);
        Color m_pressedTextColor = Color.FromRgb(103, 80, 164);
        Color m_disabledTextColor = Color.FromRgb(103, 80, 164);
        double m_normalBorderThickness = 0;
        double m_hoverBorderThickness = 0;
        double m_pressedBorderThickness = 0;
        double m_disabledBorderThickness = 0;
        double m_normalFontSize = 14;
        double m_cornerRadius = 20;
        #endregion

        #region Properties

        internal Color NormalBorderColor
        {
            get => m_normalBorderColor;
            set
            {
                m_normalBorderColor = value;
                if (this.IsEnabled)
                {
                    this.BorderColor = value;
                    this.Opacity = 1;
                }
            }
        }

        internal Color HoverBorderColor
        {
            get => m_hoverBorderColor;
            set
            {
                m_hoverBorderColor = value;
            }
        }

        internal Color PressedBorderColor
        {
            get => m_pressedBorderColor;
            set
            {
                m_pressedBorderColor = value;
            }
        }

        internal Color DisabledBorderColor
        {
            get => m_disabledBorderColor;
            set
            {
                m_disabledBorderColor = value;
                if (this.IsEnabled == false)
                {
                    this.BorderColor = value;
                    this.Opacity = 0.5;
                }
            }
        }

        internal Color NormalBackgroundColor
        {
            get => m_normalBackgroundColor;
            set
            {
                m_normalBackgroundColor = value;
                if (this.IsEnabled)
                {
                    this.BackgroundColor = value;
                    this.Opacity = 1;
                }
            }
        }

        internal Color HoverBackgroundColor
        {
            get => m_hoverBackgroundColor;
            set
            {
                m_hoverBackgroundColor = value;
            }
        }

        internal Color PressedBackgroundColor
        {
            get => m_pressedBackgroundColor;
            set
            {
                m_pressedBackgroundColor = value;
            }
        }

        internal Color DisabledBackgroundColor
        {
            get => m_disabledBackgroundColor;
            set
            {
                m_disabledBackgroundColor = value;
                if (this.IsEnabled == false)
                {
                    this.BackgroundColor = value;
                    this.Opacity = 0.5;
                }
            }
        }

        internal Color NormalTextColor
        {
            get => m_normalTextColor;
            set
            {
                m_normalTextColor = value;
                if (this.IsEnabled)
                {
                    this.TextColor = value;
                    this.Opacity = 1;
                }
            }
        }

        internal Color HoverTextColor
        {
            get => m_hoverTextColor;
            set
            {
                m_hoverTextColor = value;
            }
        }

        internal Color PressedTextColor
        {
            get => m_pressedTextColor;
            set
            {
                m_pressedTextColor = value;
            }
        }

        internal Color DisabledTextColor
        {
            get => m_disabledTextColor;
            set
            {
                m_disabledTextColor = value;
                if (this.IsEnabled == false)
                {
                    this.TextColor = value;
                    this.Opacity = 0.5;
                }
            }
        }

        internal double NormalBorderThickness
        {
            get => m_normalBorderThickness;
            set
            {
                m_normalBorderThickness = value;
                if (this.IsEnabled)
                    this.BorderWidth = value;
            }
        }

        internal double HoverBorderThickness
        {
            get => m_hoverBorderThickness;
            set
            {
                m_hoverBorderThickness = value;
            }
        }

        internal double PressedBorderThickness
        {
            get => m_pressedBorderThickness;
            set
            {
                m_pressedBorderThickness = value;
            }
        }

        internal double DisabledBorderThickness
        {
            get => m_disabledBorderThickness;
            set
            {
                m_disabledBorderThickness = value;
                if (this.IsEnabled == false)
                    this.BorderWidth = value;
            }
        }

        internal double NormalFontSize
        {
            get => m_normalFontSize;
            set
            {
                m_normalFontSize = value;
                this.FontSize = value;
            }
        }

        internal double NormalCornerRadius
        {
            get => m_cornerRadius;
            set
            {
                m_cornerRadius = value;
                this.CornerRadius = (int)value;
            }
        }
        #endregion

        internal MaterialButton()
        {
            this.BorderColor = NormalBorderColor;
            this.CornerRadius = (int)NormalCornerRadius;
            this.BorderWidth = NormalBorderThickness;
            this.FontSize = NormalFontSize;
            this.BackgroundColor = NormalBackgroundColor;
            this.TextColor = NormalTextColor;
#if !ANDROID
            //Mouse hover effect is not applicable for Android and with the below line the button click is not functioned properly in Android.
            this.AddTouchListener(this);
#endif            
            this.PropertyChanging += MaterialButton_PropertyChanging;
            this.Pressed += MaterialButton_Pressed;
        }
        /* Note: "MaterialButton_PropertyChanging" event is invoked only initially when icon button in outline view is pressed.
                  Thus to avoid repeated invoke of "ApplyPressedStyle" method due to "MaterialButton_PropertyChanging" and "MaterialButton_Pressed" event 
                  temporarily used this variable.*/
        bool IsPressedStyleApplied = false;
        private void MaterialButton_Pressed(object? sender, EventArgs e)
        {
            if (IsPressedStyleApplied == false)
                ApplyPressedStyle();
            else
                IsPressedStyleApplied = false;
        }

        /// <summary>
        /// Occurs before a property is changing.
        /// </summary>
        private void MaterialButton_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsEnabled":
                    if (IsEnabled == true)
                        ApplyDisabledStyle();
                    else
                        ApplyNormalStyle();
                    break;
                case "IsPressed":
                    if (IsPressed == false)
                    {
                        ApplyPressedStyle();
                        IsPressedStyleApplied = true;
                    }
                    else
                    {
                        if (IsMouseOver == true)
                            ApplyMouseOverStyle();
                        else
                            ApplyNormalStyle();
                    }
                    break;
                case "IsFocused":
                    if (IsFocused == true)
                        ApplyNormalStyle();
                    break;
            }
        }

        // Called when the parent of the button disappears even before the mouse pointer leaves the button.
        // In that case, the button will disappear in the pressed state. So, we need to reset the style.
        internal void ResetStyleOnDisappearing()
        {
            ApplyNormalStyle();
            IsMouseOver = false;
        }

        /// <summary>
        /// Resets the style to normal.
        /// </summary>
        void ApplyNormalStyle()
        {
            this.BackgroundColor = NormalBackgroundColor;
            this.TextColor = NormalTextColor;
            this.BorderColor = NormalBorderColor;
            this.BorderWidth = NormalBorderThickness;
            this.Opacity = 1;
        }

        void ApplyDisabledStyle()
        {
            this.BackgroundColor = DisabledBackgroundColor;
            this.TextColor = DisabledTextColor;
            this.BorderColor = DisabledBorderColor;
            this.BorderWidth = DisabledBorderThickness;
            this.Opacity = 0.5;
        }

        void ApplyPressedStyle()
        {
            this.BackgroundColor = PressedBackgroundColor;
            this.TextColor = PressedTextColor;
            this.BorderColor = PressedBorderColor;
            this.BorderWidth = PressedBorderThickness;
        }

        void ApplyMouseOverStyle()
        {
            this.BackgroundColor = HoverBackgroundColor;
            this.TextColor = HoverTextColor;
            this.BorderColor = HoverBorderColor;
            this.BorderWidth = HoverBorderThickness;
        }

        /// <summary>
        /// Wiring touch event for identify the Mouse is Over the button.
        /// </summary>
        public void OnTouch(Syncfusion.Maui.Core.Internals.PointerEventArgs e)
        {
            switch (e.Action)
            {
                case PointerActions.Exited:
                    ApplyNormalStyle();
                    IsMouseOver = false;
                    break;
                case PointerActions.Entered:
                    ApplyMouseOverStyle();
                    IsMouseOver = true;
                    break;
            }
        }
    }
}
