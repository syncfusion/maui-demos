#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using System.ComponentModel;

    /// <summary>
    /// Represents the image filter model.
    /// </summary>
    internal class ImageFilterModel : INotifyPropertyChanged
    {
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

        /// <summary>
        /// Gets or sets the effect name.
        /// </summary>
        public string? Effect
        {
            get { return effect; }
            set
            {
                effect = value;
                this.RaiseOnPropertyChanged(nameof(Effect));
            }
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public ImageSource? ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
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

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes on property changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}