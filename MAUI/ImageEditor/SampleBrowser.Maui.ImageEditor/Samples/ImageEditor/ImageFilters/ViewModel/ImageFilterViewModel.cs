namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    /// <summary>
    /// Represents the image filter view model for managing image filter settings in the UI.
    /// </summary>
    public class ImageFilterViewModel
    {
        #region Property

        /// <summary>
        /// Gets or sets the image filters.
        /// </summary>
        public List<ImageFilterModel>? ImageFilters { get; set; }

        #endregion

        #region Constructor

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

#if ANDROID || IOS
                if (effect is "Opacity" or "Sharpen" or "Blur")
                {
                    continue;
                }
#endif

                this.ImageFilters.Add(new ImageFilterModel() { Effect = effect });
            }
        }

        #endregion
    }
}