#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.PdfViewer.Samples.PdfViewer.ShowAndHideFormField.View;
using Syncfusion.Maui.PdfViewer;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class ShowAndHideFormField : SampleView
{
    ShowAndHideFormFieldViewModel viewModel;
    public ObservableCollection<ListItem> ListItems { get; set; }
    private ListItem selectedItem;
    private Color originalBackgroundColor;
    private Color originalBorderColor;
    public ShowAndHideFormField()
    {
        InitializeComponent();
        viewModel = new ShowAndHideFormFieldViewModel(PdfViewer);
        PdfViewer.FormFieldValueChanged += PdfViewer_FormFieldValueChanged;
       
        viewModel.IsEnableSave = false;
        ListItems = new ObservableCollection<ListItem>
        {
            new ListItem("Andrew Fuller", "andrew@mycompany.com", "dropdown_icon.png", Colors.Red, true),
            new ListItem("Anne Dodsworth", "anne@mycompany.com", "dropdown_icon.png", Colors.Green, false),
        };
        selectedItem = ListItems[0];
        myListView.ItemsSource = ListItems;
        myListView.SelectedItem = ListItems[0];
        myListView.ItemSelected += MyListView_ItemSelected;
#if WINDOWS || MACCATALYST
        this.SizeChanged += InvisibleSignature_SizeChanged;
#endif
        BindingContext = viewModel;
        originalBackgroundColor = Color.FromRgba(0.854901969, 0.917647064, 0.968627453, 1);
        originalBorderColor = Color.FromRgba(1, 1, 1, 1);
    }

    private void MyListView_ItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ListItem selectedItem)
        {
            ToggleFormFieldsVisibility(selectedItem.Name);
        }
        viewModel.IsListVisible = false;
    }

    private void ToggleFormFieldsVisibility(string selectedName)
    {
        if (selectedName == "Andrew Fuller")
        {
            var semiBrightRed =  Color.FromRgba(255, 102, 102, 30);
            var borderRed = Color.FromRgb(255, 0, 0);
            var fieldsToColor = new string[]
            {
                "StartDate", "EndDate", "LeaseAmount", "TenantName", "TenantDate", "TenantSignature"
            };

            SetFieldColors(fieldsToColor, semiBrightRed, borderRed);
            if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordSignature") is SignatureFormField signature2)
            {
                signature2.IsHidden = true;
                signature2.ReadOnly = false;
            }

            if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordName") is TextFormField nameTextBox8)
            {
                nameTextBox8.IsHidden = true;
                nameTextBox8.Text = string.Empty;
            }

            if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordDate") is TextFormField nameTextBox9)
            {
                nameTextBox9.IsHidden = true;
                nameTextBox9.Text = string.Empty;
            }
        }
        else if (selectedName == "Anne Dodsworth")
        {
            var otherFields = PdfViewer.FormFields
            .Where(x => x.Name != "LandlordName" && x.Name != "LandlordDate" && x.Name != "LandlordSignature")
            .ToList();
            bool allOtherFieldsHaveValues = otherFields.Count == 6 && otherFields.All(field =>
            {
                if (field is TextFormField textField)
                {
                    return !string.IsNullOrEmpty(textField.Text);
                }
                else if (field is SignatureFormField signatureField)
                {
                    return signatureField.Signature != null;  
                }
                return false;
            });
            if (allOtherFieldsHaveValues)
            {
                if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LeaseAmount") is TextFormField leaseAmountField && !IsTextNumeric(leaseAmountField.Text))
                {
                    viewModel.ShowDialog("Invalid Lease Amount", "Please enter valid lease amount");
                    leaseAmountField.Text = string.Empty;
                    myListView.SelectedItem = ListItems[0];
                    return;
                }
                var fieldsToColor = new string[]
                {
                   "StartDate", "EndDate", "LeaseAmount", "TenantName", "TenantDate", "TenantSignature"
                };
                SetFieldColors(fieldsToColor, originalBackgroundColor, originalBorderColor);
                foreach (var form in otherFields)
                {                    
                    form.ReadOnly = true;
                }
                var semiGreenBackground = Color.FromRgba(102, 255, 102, 10); // Semi-transparent green
                var originalGreenBorder = Colors.Green;
                
                if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordSignature") is SignatureFormField signature2)
                {
                    signature2.IsHidden = false;
                    signature2.ReadOnly = false;
                }

                if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordName") is TextFormField nameTextBox5)
                {
                    nameTextBox5.IsHidden = false; 
                    nameTextBox5.Text = string.Empty;
                }

                if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == "LandlordDate") is TextFormField nameTextBox6)
                {
                    nameTextBox6.IsHidden = false; 
                    nameTextBox6.Text = string.Empty;
                }
                var fieldsToGreenColor = new string[]
                {
                    "LandlordSignature", "LandlordName", "LandlordDate"
                };
               SetFieldColors(fieldsToGreenColor, semiGreenBackground, originalGreenBorder);
            }
            else
            {
                myListView.SelectedItem = ListItems[0];
                var nonFilledFields = new List<string>();

                foreach (var field in otherFields)
                {
                    if (field is TextFormField textField && string.IsNullOrEmpty(textField.Text))
                    {
                        nonFilledFields.Add(textField.Name);
                    }
                    else if (field is SignatureFormField signatureField && signatureField.Signature == null)
                    {
                        nonFilledFields.Add(signatureField.Name);
                    }
                }
                string missingFields = string.Join(", ", nonFilledFields);
                viewModel.ShowDialog("Required Field(s)", $"{missingFields}");
            }

        }
        
    }

    private void SetFieldColors(string[] fieldNames, Color backgroundColor, Color borderColor)
    {
        foreach (var fieldName in fieldNames)
        {
            if (PdfViewer.FormFields.FirstOrDefault(x => x.Name == fieldName) is var formField && formField is not null)
            {
                foreach (var item in formField.Widgets)
                {
                    item.BackgroundColor = backgroundColor;
                    item.BorderColor= borderColor;
                }
            }
        }
    }

    private bool IsTextNumeric(string text)
    {
        return double.TryParse(text, out _);
    }

   

    void FitToHeight()
    {
        double toolbarHeight = 48;
        double documentPageHeight = 596;
        // Zoom the document to fit to page to view the signature field in the view.
        PdfViewer.ZoomFactor = (this.Height - toolbarHeight) / (documentPageHeight * DeviceDisplay.MainDisplayInfo.Density);
    }

    private void InvisibleSignature_SizeChanged(object? sender, EventArgs e)
    {
       FitToHeight();
    }

    private void PdfViewer_FormFieldValueChanged(object? sender, Syncfusion.Maui.PdfViewer.FormFieldValueChangedEventArgs? e)
    {

        bool allOtherFieldsHaveValues = PdfViewer.FormFields.All(field =>
        {
            if (field is TextFormField textField)
            {
                return !string.IsNullOrEmpty(textField.Text);
            }
            else if (field is SignatureFormField signatureField)
            {
                return signatureField.Signature != null; 
            }
            return false;
        });
        if (allOtherFieldsHaveValues)
        {
            viewModel.IsCompleteSigningEnable = true;
            PdfViewer.AnnotationSettings.IsLocked = true;
        }
    }

    private void Save_Clicked(object? sender, EventArgs e)
    {
        viewModel?.SaveDocument();
    }

    private  void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        viewModel.IsCompleteSigningEnable = false;
        ToggleFormFieldsVisibility(selectedItem.Name);
#if WINDOWS || MACCATALYST
       FitToHeight();
#endif
    }
    ///// <summary>
    ///// Handles when leaving the current page
    ///// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    private void dropdownButton_Clicked_1(object sender, EventArgs e)
    {
        viewModel.IsListVisible = !viewModel.IsListVisible;
        gridOfPdfViewer.InvalidateMeasure();
    }

    private void PdfViewer_Tapped(object sender, GestureEventArgs e)
    {
        viewModel.IsListVisible = false;
    }

    private void FormField_FocusedChange(object sender, FormFieldFocusChangedEvenArgs e)
    {
        viewModel.IsListVisible = false;
    }
}
