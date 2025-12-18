#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using System.ComponentModel;

    /// <summary>
    /// Represents the image filter model that encapsulates the data and state for image filtering operations.
    /// </summary>
    public class ImageFilterModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Holds the effect name.
        /// </summary>
        private string? effect;

        /// <summary>
        /// Holds the image source.
        /// </summary>
        private ImageSource? imageSource;

        /// <summary>
        /// Gets or sets the value indicating whether the busy indicator is enabled.
        /// </summary>
        private bool isBusy = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the effect name.
        /// </summary>
        public string? Effect
        {
            get { return this.effect; }
            set
            {
                this.effect = value;
                this.RaiseOnPropertyChanged(nameof(Effect));
            }
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public ImageSource? ImageSource
        {
            get { return this.imageSource; }
            set
            {
                this.imageSource = value;
                this.RaiseOnPropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the busy indicator is enabled.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                this.RaiseOnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Property changed

        /// <summary>
        /// Invokes the event when the value of a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}