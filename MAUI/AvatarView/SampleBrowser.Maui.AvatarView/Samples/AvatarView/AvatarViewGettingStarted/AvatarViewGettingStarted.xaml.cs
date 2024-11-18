#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using System.Collections.ObjectModel;

#if ANDROID
using Android.Content.Res;
#endif


namespace SampleBrowser.Maui.AvatarView.SfAvatarView;
public partial class AvatarViewGettingStarted : SampleView
{

   private bool usedoublecharacter = false;
    public bool UseDoubleCharacter
    {
        get
        {
            return usedoublecharacter;
        }
        set
        {
            usedoublecharacter = value;
            if (value)
            {
                usedoublecharacter = true;
                InitialsType = InitialsType.DoubleCharacter;
                //SetAvatarName();
            }
            else
            {
                usedoublecharacter = false;
                InitialsType = InitialsType.SingleCharacter;
                //SetAvatarName();

            }

            this.OnPropertyChanged();
        }
    }

    private InitialsType initials;

    public InitialsType InitialsType
    {
        get
        {
            return initials;

        }
        set
        {
            initials = value;
            this.OnPropertyChanged();
        }
    }

    private GradientBrush? gradientBrush;

    public GradientBrush? GradientBrush
    {
        get
        {
            return gradientBrush;
        }
        set
        {
            gradientBrush = value;
            this.OnPropertyChanged();
        }
    }

    private bool useCustomAvatar = false;

    public bool UseCustomAvatar
    {
        get
        {
            return useCustomAvatar;
        }
        set
        {
            useCustomAvatar = value;

            if (value)
            {
                UseInitialAvatar = false;
                ContentType = ContentType.Custom;
            }
            else
            {
                ContentType = ContentType.Initials;
                UseInitialAvatar = true;
            }

            this.OnPropertyChanged();
        }
    }

    private ContentType contentType = ContentType.Initials;

    public ContentType ContentType
    {
        get
        {
            return contentType;
        }
        set
        {
            contentType = value;
            this.OnPropertyChanged();
        }
    }

    private bool editionIsVisible = true;

    public bool EditionIsVisible
    {
        get
        {
            return editionIsVisible;
        }
        set
        {
            editionIsVisible = value;
            this.OnPropertyChanged();
        }
    }

    private bool useInitialAvatar = true;

    public bool UseInitialAvatar
    {
        get
        {
            return useInitialAvatar;
        }
        set
        {
            useInitialAvatar = value;
            if (value)
                ColorPickerOpacity = 1;
            else
                ColorPickerOpacity = 0.3;
            this.OnPropertyChanged();
        }
    }

    private bool useGradients;

    public bool UseGradients
    {
        get
        {
            return useGradients;
        }
        set
        {
            useGradients = value;
            if (useGradients)
            {
                SetGradients();
                SetColorToAvatar();
            }
            else
            {
                SetGradients();
                PopulateColorCollection();
                SetColorToAvatar();

            }
            this.OnPropertyChanged();
        }
    }

    private String firstName = "Ellana";

    public String FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            firstName = value;
            UserName = FirstName + " " + LastName;
            this.OnPropertyChanged();
        }
    }

    private String? lastName;

    public String? LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            lastName = value;
            UserName = FirstName + " " + LastName;
            this.OnPropertyChanged();
        }
    }

    private String? userName;

    public String? UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
            TitleText = value;
            this.OnPropertyChanged();
        }
    }

    private String? titleText;

    public String? TitleText
    {
        get
        {
            if (UserName == String.Empty || UserName == " ")
                return String.Empty;
            return "Hi " + titleText;
        }
        set
        {
            titleText = value;
            this.OnPropertyChanged();
        }
    }

    private Color? profileColor;

    public Color? ProfileColor
    {
        get
        {
            return profileColor;
        }
        set
        {
            profileColor = value;
            this.OnPropertyChanged();
        }
    }

    private Color? textColor;

    public Color? TextColor
    {
        get
        {
            return textColor;
        }
        set
        {
            textColor = value;
            this.OnPropertyChanged();
        }
    }

    private double colorPickerOpacity = 1;

    public double ColorPickerOpacity
    {
        get
        {
            return colorPickerOpacity;
        }
        set
        {
            colorPickerOpacity = value;
            this.OnPropertyChanged();
        }
    }


    private ObservableCollection<ColorBackgroundAvatar> colorItemCollection = new ObservableCollection<ColorBackgroundAvatar>();

    public ObservableCollection<ColorBackgroundAvatar> ColorItemCollection
    {
        get
        {
            return colorItemCollection;
        }
        set
        {
            colorItemCollection = value;
            this.OnPropertyChanged();
        }
    }

    public AvatarViewGettingStarted()
    {
        InitializeComponent();

        PopulateColorCollection();

        tappedAvatar = ColorItemCollection[0];

        UseGradients = true;
        this.BindingContext = this;
    }

    private void StatusIndicatorCheck_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e) 
    {
        if (this.StatusIndicatorCheck.IsChecked == true) {
            this.StatusBadge.BadgeSettings!.Icon = BadgeIcon.Available;
        }
        else {
            this.StatusBadge.BadgeSettings!.Icon = BadgeIcon.None;
        }
    }
    private void SetAvatarName()
    {
        if (tappedAvatar == null)
        { return; }
        if (InitialsType == InitialsType.DoubleCharacter)
        {
            if (UserName != null)
            {
                tappedAvatar.AvatarName = UserName;
                tappedAvatar.InitialsType = InitialsType;
            }
        }
        else
        {
            if (UserName != null)
            {
                tappedAvatar.AvatarName = UserName;
                tappedAvatar.InitialsType = InitialsType;
            }

        }
        UserName = tappedAvatar.AvatarName;
        InitialsType = tappedAvatar.InitialsType;
    }

    private void PopulateColorCollection()
    {
        ColorItemCollection.Clear();

        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#0061FF"), Color.FromArgb("#30A7FF"), Color.FromArgb("#60EFFF")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#F3696E"), Color.FromArgb("#F68937"), Color.FromArgb("#F8A902")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#6EEE87"), Color.FromArgb("#67DA5B"), Color.FromArgb("#5FC52E")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#239EAB"), Color.FromArgb("#4BBECC"), Color.FromArgb("#74DEEE")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#4E54C8"), Color.FromArgb("#6E74E1"), Color.FromArgb("#8F94FB")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#FFF6B7"), Color.FromArgb("#FBAD99"), Color.FromArgb("#F6416C")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#DA22FF"), Color.FromArgb("#BA2AF7"), Color.FromArgb("#9733EE")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#AFFCAF"), Color.FromArgb("#67EFCE"), Color.FromArgb("#12DFF3")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#F6D5F7"), Color.FromArgb("#F8DEE8"), Color.FromArgb("#FBE9D7")));
        ColorItemCollection.Add(GetColorPickerItem(Color.FromArgb("#CAD0FF"), Color.FromArgb("#D1D6F7"), Color.FromArgb("#E3E3E3")));

    }

    private ColorBackgroundAvatar GetColorPickerItem(Color backgroundColor, Color startcolor, Color stopcolor)
    {
        ColorBackgroundAvatar colorAvatar = new ColorBackgroundAvatar();
        colorAvatar.BackgroundColor = backgroundColor;
        colorAvatar.Stroke = Color.FromArgb("#9E9E9E");
        colorAvatar.InitialsColor = Colors.Transparent;
        colorAvatar.AvatarShape = AvatarShape.Circle;
        colorAvatar.AvatarSize = AvatarSize.Small;
        colorAvatar.VerticalOptions = LayoutOptions.Center;
        colorAvatar.HorizontalOptions = LayoutOptions.Center;
        colorAvatar.AvatarName = "";
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += ColorTapGestureRecognizer_Tapped;
        colorAvatar.GestureRecognizers.Add(tapGestureRecognizer);
        colorAvatar.StartColor = startcolor;
        colorAvatar.StopColor = stopcolor;
        return colorAvatar;
    }

    private void SetGradients()
    {
        foreach (var item in ColorItemCollection)
        {
            if (this.UseGradients)
                if (ColorItemCollection.IndexOf(item) < 5)
                    item.Background = GetGradients(item.StartColor!, item.StopColor!);
                else
                    item.Background = GetGradients(item.StartColor!, item.StopColor!);
        }
    }

    private LinearGradientBrush GetGradients(Color startColor, Color endColor)
    {
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
        linearGradientBrush.GradientStops = new GradientStopCollection()
            {
                new GradientStop(){Color = startColor, Offset=0.0f},
                new GradientStop(){Color = endColor, Offset=1.0f},
            };

        return linearGradientBrush;
    }

    ColorBackgroundAvatar tappedAvatar;

    private void ColorTapGestureRecognizer_Tapped(object? sender, EventArgs e)
    {
        if (this.UseCustomAvatar)
            return;
        
        var colorBackgroundAvatar = sender as ColorBackgroundAvatar;
        if (colorBackgroundAvatar != null)
        {
            tappedAvatar = colorBackgroundAvatar;
        }
        SetColorToAvatar();
    }

    private void SetColorToAvatar()
    {
        if (tappedAvatar == null)
            return;

        foreach (var item in ColorItemCollection)
        {
            item.InitialsColor = Colors.Transparent;
            item.Stroke = Colors.Transparent;
        }

        tappedAvatar.Stroke = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        tappedAvatar.StrokeThickness = 2;

        if (ColorItemCollection.IndexOf(tappedAvatar) < 5)
        {
            tappedAvatar.InitialsColor = Colors.White;
        }
        else
        {
            tappedAvatar.InitialsColor = Colors.Black;
        }
        ProfileColor = tappedAvatar.BackgroundColor;
        TextColor = tappedAvatar.InitialsColor;
        if (UseGradients)
        {
            GradientBrush = (GradientBrush)tappedAvatar.Background;
        }
        else
        {
            GradientBrush = null;
        }
    }

    public class ColorBackgroundAvatar : Syncfusion.Maui.Core.SfAvatarView
    {
        private Color? startColor;

        public Color? StartColor
        {
            get
            {
                return startColor;
            }
            set
            {
                startColor = value;
                this.OnPropertyChanged();
            }
        }

        private Color? stopcolor;

        public Color? StopColor
        {
            get
            {
                return stopcolor;
            }
            set
            {
                stopcolor = value;
                this.OnPropertyChanged();
            }
        }
    }

  
}

public class SfEntry : Entry {
    public SfEntry() {

#if ANDROID
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            if (v is SfEntry) {
                h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
            }
        });
#endif
#if WINDOWS
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is SfEntry)
                {
                    h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                    h.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(8, 0, 0, 0);
                    h.PlatformView.Resources["TextControlBorderThemeThicknessFocused"] = new Microsoft.UI.Xaml.Thickness(0);
                }
            });
#endif
#if MACCATALYST
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is SfEntry)
                {
                    h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                }
            });
#endif
    }

}

