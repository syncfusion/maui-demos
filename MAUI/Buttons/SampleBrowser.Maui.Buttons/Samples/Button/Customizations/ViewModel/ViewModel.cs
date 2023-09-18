#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SampleBrowser.Maui.Base.Converters;
using System.Reflection;

namespace SampleBrowser.Maui.Buttons.Button;

public class ViewModel : INotifyPropertyChanged
{

    #region Fields
    /// <summary>
    /// The background image of button
    /// </summary>
    public ImageSource? backgroundImage;

    /// <summary>
    /// The border color of button.
    /// </summary>
    private Color borderColor = Colors.Black;

    /// <summary>
    /// The background color of button.
    /// </summary>

    private Color backgroundColor = Colors.DarkRed;
    /// <summary>
    /// Represents the text color
    /// </summary>
    private Color textColor = Colors.White;

    /// <summary>
    /// Represents the visibility of image
    /// </summary>
    private bool showImage = true;

    /// <summary>
    /// The corner radius slider.
    /// </summary>
#if ANDROID || IOS
    private int rightSlider = 8;
#elif WINDOWS || MACCATALYST
    private int rightSlider = 8;
#endif

    /// <summary>
    /// The corner radius of button.
    /// </summary>
#if ANDROID || IOS
    private CornerRadius cornerRadius = 8;
#elif WINDOWS || MACCATALYST
    private CornerRadius cornerRadius = 8;
#endif

    /// <summary>
    /// The default corner radius.
    /// </summary>
#if ANDROID || IOS
    private int leftSlider = 8;
#elif WINDOWS || MACCATALYST
    private int leftSlider = 8;
#endif

    /// <summary>
    /// Represents the border width
    /// </summary>
    private int borderWidth = 1;

    /// <summary>
    /// The text of button.
    /// </summary>
    private string text = "ADD TO CART";

    /// <summary>
    /// The can show background image
    /// </summary>
    private bool canShowBackgroundImage = false;

    /// <summary>
    /// Represents the enable or disable the shadow
    /// </summary>
    private bool enableShadow;
    #endregion

    #region Property


    /// <summary>
    /// Gets or sets the color of the border of button.
    /// </summary>
    /// <value>The color of the border of button.</value>
    public Color BorderColor
    {
        get
        {
            return borderColor;
        }

        set
        {
            borderColor = value;
            OnPropertyChanged("BorderColor");
        }
    }

    /// <summary>
    /// Gets or Sets the text color
    /// </summary>
    public Color TextColor
    {
        get
        {
            return textColor;
        }

        set
        {
            textColor = value;
            OnPropertyChanged("TextColor");
        }
    }

    /// <summary>
    /// Gets or sets the background color of button.
    /// </summary>
    /// <value>The background color of button</value>
    [Obsolete]
    public Color BackgroundColor
    {
        get
        {
            return backgroundColor;
        }
        set
        {
            backgroundColor = value;
            OnPropertyChanged("BackgroundColor");
        }
    }

    /// <summary>
    /// Gets or sets the slider value.
    /// </summary>
    /// <value>The slider value.</value>
#if ANDROID || IOS
    public int RightCornerRadius
    {
        get
        {
            return rightSlider;
        }
        set
        {
            rightSlider = value;
            CornerRadius = new CornerRadius(cornerRadius.TopLeft, value, value, cornerRadius.BottomRight);
            OnPropertyChanged("RightCornerRadius");
        }
    }
#elif WINDOWS || MACCATALYST
    public int RightCornerRadius
    {
        get
        {
            return rightSlider;
        }
        set
        {
            rightSlider = value;
            CornerRadius = new CornerRadius(cornerRadius.TopLeft, value, value, cornerRadius.BottomRight);
            OnPropertyChanged("RightCornerRadius");
        }
    }
#endif
    /// <summary>
    /// Gets or sets the slider value.
    /// </summary>
    /// <value>The slider value.</value>
#if ANDROID || IOS
    public int LeftCornerRadius
    {
        get
        {
            return leftSlider;
        }
        set
        {
            leftSlider = value;
            CornerRadius = new CornerRadius(value, cornerRadius.TopRight, cornerRadius.BottomLeft, value);
            OnPropertyChanged("LeftCornerRadius");
        }
    }
#elif MACCATALYST || WINDOWS
    public int LeftCornerRadius
    {
        get
        {
            return leftSlider;
        }
        set
        {
            leftSlider = value;
            CornerRadius = new CornerRadius(value, cornerRadius.TopRight, cornerRadius.BottomLeft, value);
            OnPropertyChanged("LeftCornerRadius");
        }
    }
#endif

    /// <summary>
    /// Gets or sets the border width.
    /// </summary>
    public int BorderWidth
    {
        get
        {
            return borderWidth;
        }
        set
        {
            borderWidth = value;
            OnPropertyChanged("BorderWidth");
        }
    }


    /// <summary>
    /// Gets or sets the corner radius.
    /// </summary>
    /// <value>The corner radius.</value>
#if ANDROID || IOS
    public CornerRadius CornerRadius
    {
        get
        {
            return cornerRadius;
        }
        set
        {

            cornerRadius = value;
            OnPropertyChanged("CornerRadius");
        }
    }
#elif WINDOWS || MACCATALYST
    public CornerRadius CornerRadius
    {
        get
        {
            return cornerRadius;
        }
        set
        {

            cornerRadius = value;
            OnPropertyChanged("CornerRadius");
        }
    }
#endif


    /// <summary>
    /// Gets or Sets the image visibility
    /// </summary>
    public bool ShowImage
    {
        get
        {
            return showImage;
        }
        set
        {
            showImage = value;

            OnPropertyChanged("ShowImage");
        }
    }


    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>The text.</value>
    public string Text
    {
        get
        {
            return text;
        }
        set
        {
            text = value;
            OnPropertyChanged("Text");
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:button.buttonViewModel"/> is enable stack.
    /// </summary>
    /// <value><c>true</c> if is enable stack; otherwise, <c>false</c>.</value>
    public bool CanShowBackgroundImage
    {
        get
        {
            return canShowBackgroundImage;
        }
        set
        {
            canShowBackgroundImage = value;
            if (value)
            {
                var ResourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;
                BackgroundImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.april.png", ResourceAssembly);
            }
            else
            {
                BackgroundImage = null;
            }
            OnPropertyChanged("CanShowBackgroundImage");
            OnPropertyChanged("BackgroundImage");
        }
    }

    /// <summary>
    /// Source for transparent background
    /// </summary>
    public ImageSource? BackgroundImage
    {
        get
        {

            return backgroundImage;
        }
        set
        {
            backgroundImage=value;
            OnPropertyChanged("BackgroundImage");
        }
    }

    /// <summary>
    /// Gets or sets whether shadow enable or disable
    /// </summary>
    public bool EnableShadow
    {
        get
        {
            return enableShadow;
        }
        set
        {
            enableShadow = value;
            OnPropertyChanged("EnableShadow");
        }
    }

    #endregion

    #region Property changed method

    /// <summary>
    /// Occurs when property changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;


    public void OnPropertyChanged([CallerMemberName] string? name = null) =>
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    #endregion
}
