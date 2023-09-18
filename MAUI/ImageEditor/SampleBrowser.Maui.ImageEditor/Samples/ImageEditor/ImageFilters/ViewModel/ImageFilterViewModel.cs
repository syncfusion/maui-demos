#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    /// <summary>
    /// Represents the image filter view model.
    /// </summary>
    internal class ImageFilterViewModel
    {
        /// <summary>
        /// Gets or sets the image filters.
        /// </summary>
        public List<ImageFilterModel>? ImageFilters { get; set; }

        /// <summary>
        /// Initializes the new instance of <see cref="ImageFilterViewModel"/> class.
        /// </summary>
        public ImageFilterViewModel()
        {
            var list = Enum.GetValues(typeof(Syncfusion.Maui.ImageEditor.ImageEffect));
            this.ImageFilters ??= new List<ImageFilterModel>();
            foreach (var item in list)
            {
                var effect = item.ToString();
                if (effect == "None")
                {
                    continue;
                }
                else if (effect == "Saturation")
                {
                    effect = "Grayscale";
                }

#if ANDROID
                if (effect is "Opacity" or "Sharpen" or "Blur")
                {
                    continue;
                }
#endif

                this.ImageFilters.Add(new ImageFilterModel() { Effect = effect });
            }
        }
    }
}
