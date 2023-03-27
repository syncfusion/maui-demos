#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using System.Text.RegularExpressions;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Inputs;

    /// <summary>
    /// Represents the custom numeric editor class.
    /// </summary>
    public class NumericEditor : IDataFormEditor
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private Syncfusion.Maui.DataForm.SfDataForm dataForm;

        /// <summary>
        /// Holds the DataForm item object.
        /// </summary>
        private DataFormCustomItem? dataFormCustomItem;

#if ANDROID
        /// <summary>
        /// Hold the bottom border draw view for entry in android.
        /// </summary>
        private EntryBorder? borderLine;

        /// <summary>
        /// Hold the bottom border graphics view for entry in android.
        /// </summary>
        private GraphicsView? borderGraphicsView;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericEditor"/> class.
        /// </summary>
        /// <param name="dataForm">The data form instance.</param>
        public NumericEditor(Syncfusion.Maui.DataForm.SfDataForm dataForm)
        {
            this.dataForm = dataForm;
        }

        /// <inheritdoc/>
        public View CreateEditorView(DataFormItem dataFormItem)
        {
            Entry inputView = new Entry();
            if (dataFormItem.FieldName == "CVV")
            {
                inputView.IsPassword = true;
                inputView.MaxLength = 3;
            }
            else
            {
                inputView.MaxLength = 16;
            }

#if IOS || MACCATALYST
            inputView.PlaceholderColor = dataFormItem.PlaceholderColor;
#endif
            if (this.dataForm.LayoutType == DataFormLayoutType.Default)
            {
                inputView.Placeholder = dataFormItem.PlaceholderText;
            }

            inputView.Keyboard = Keyboard.Numeric;
            DataFormTextStyle textStyle = dataForm.EditorTextStyle;
            inputView.TextColor = textStyle.TextColor;
            inputView.FontSize = textStyle.FontSize;
            inputView.FontFamily = textStyle.FontFamily;
            inputView.FontAttributes = textStyle.FontAttributes;
            inputView.TextChanged += this.OnViewTextChanged;
            this.dataFormCustomItem = (DataFormCustomItem)dataFormItem;
            this.dataFormCustomItem.EditorValue = string.Empty;

#if ANDROID
            if (this.dataForm.LayoutType == DataFormLayoutType.Default)
            {
                //// TODO: There is no border for android entry in .net 6.	
                VerticalStackLayout stack = new VerticalStackLayout();
                this.borderLine = new EntryBorder();
                this.borderGraphicsView = new GraphicsView()
                {
                    Drawable = this.borderLine,
                    HeightRequest = 2,
                    Margin = new Thickness(0, -7, 0, 0),
                };

                inputView.Focused += this.OnInputViewFocused;
                inputView.Unfocused += this.OnInputViewUnfocused;
                inputView.HandlerChanged += this.OnInputViewHandlerChanged;
                stack.Add(inputView);
                stack.Add(borderGraphicsView);
                return stack;
            }
#endif

            return inputView;
        }

        /// <inheritdoc/>
        public void CommitValue(DataFormItem dataFormItem, View view)
        {
            if (view is InputView numericText)
            {
                if (dataFormItem.FieldName == nameof(PaymentFormModel.CVV))
                {
                    dataFormItem.SetValue(numericText.Text);
                }
                else
                {
                    double numericValue;
                    double.TryParse(numericText.Text, out numericValue);
                    dataFormItem.SetValue(numericValue);
                }
            }
        }

        /// <summary>
        /// Method validates the view
        /// </summary>
        /// <param name="dataFormItem">The data form item.</param>
        private void ValidateValue(DataFormItem dataFormItem)
        {
            dataForm.Validate(new List<string>() { dataFormItem.FieldName });
        }

        /// <summary>
        /// Invokes on view text changes.
        /// </summary>
        /// <param name="sender">The entry field.</param>
        /// <param name="e">The event arguments.</param>
        private void OnViewTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is not InputView numericEntry || dataFormCustomItem == null)
            {
                return;
            }

            string? numericText = Regex.Replace(numericEntry.Text, "[^0-9]+", string.Empty);
            if (numericText != numericEntry.Text)
            {
                numericEntry.Text = numericText;
                return;
            }

            dataFormCustomItem.EditorValue = numericText;
            this.ValidateValue(dataFormCustomItem);
            this.CommitValue(dataFormCustomItem, numericEntry);
        }

        public void UpdateReadyOnly(DataFormItem dataFormItem)
        {
        }

#if ANDROID

        /// <summary>
        /// Invokes on view is unfocused.
        /// </summary>
        /// <param name="sender">The entry.</param>
        /// <param name="e">The focus event arguments.</param>
        private void OnInputViewUnfocused(object? sender, FocusEventArgs e)
        {
            if (this.borderLine != null)
            {
                this.borderLine.IsFocused = false;
            }

            if (this.borderGraphicsView != null)
            {
                this.borderGraphicsView.Invalidate();
            }
        }

        /// <summary>
        /// Invokes on view is focused.
        /// </summary>
        /// <param name="sender">The entry.</param>
        /// <param name="e">The focus event arguments.</param>
        private void OnInputViewFocused(object? sender, FocusEventArgs e)
        {
            if (this.borderLine != null)
            {
                this.borderLine.IsFocused = true;
            }

            if (this.borderGraphicsView != null)
            {
                this.borderGraphicsView.Invalidate();
            }
        }

        /// <summary>
        /// Invokes on view handler is changed.
        /// </summary>
        /// <param name="sender">The entry.</param>
        /// <param name="e">The event arguments.</param>
        private void OnInputViewHandlerChanged(object? sender, EventArgs e)
        {
            if (sender is InputView entry && entry.Handler != null && entry.Handler.PlatformView is Android.Widget.EditText androidView)
            {
                androidView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                if (this.borderLine != null && androidView.Context != null)
                {
                    int colorCode;
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                    {
                        colorCode = Android.Resource.Attribute.ColorAccent;
                    }
                    else
                    {
                        colorCode = Android.Resource.Color.BackgroundDark;
                    }

                    Android.Util.TypedValue outValue = new();
                    androidView.Context.Theme?.ResolveAttribute(colorCode, outValue, true);
                    Android.Graphics.Color color = new(outValue.Data);
                    this.borderLine.FocusedColor = new Color(color.R, color.G, color.B, color.A);
                }
            }
        }

#endif
    }

#if ANDROID

    /// <summary>
    /// Represents the draw view class for entry border.
    /// </summary>
    public class EntryBorder : IDrawable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the field is focused or not.
        /// </summary>
        public bool IsFocused { get; set; }

        /// <summary>
        /// Gets or sets the focused entry border color.
        /// </summary>
        public Color FocusedColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryBorder"/> class.
        /// </summary>
        public EntryBorder()
        {
            this.FocusedColor = Colors.Blue;
        }

        /// <summary>
        /// Method to draw the border using canvas.
        /// </summary>
        /// <param name="canvas">The draw canvas.</param>
        /// <param name="dirtyRect">The available size.</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            canvas.StrokeColor = this.IsFocused ? this.FocusedColor : Color.FromRgba(100, 100, 100, 255);
            canvas.StrokeSize = this.IsFocused ? 2 : 1;
            canvas.DrawLine(new PointF(0, -1), new PointF(dirtyRect.Width, -1));
            canvas.RestoreState();
        }
    }
#endif
}