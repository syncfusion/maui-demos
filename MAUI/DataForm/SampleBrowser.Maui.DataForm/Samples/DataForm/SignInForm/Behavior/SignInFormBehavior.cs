#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.DataForm;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SignInFormBehavior : Behavior<SampleView>
    {

        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the sign in button instance.
        /// </summary>
        private Button? signInButton;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("signInForm");
            if (dataForm != null)
            {
                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
            }

            this.signInButton = bindable.Content.FindByName<Button>("signInButton");

            if (this.signInButton != null)
            {
                this.signInButton.Clicked += OnSignInButtonCliked;
            }
        }

        /// <summary>
        /// Invokes on each data form item generation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnGenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null && e.DataFormItem.FieldName == nameof(SignInFormModel.UserName) && e.DataFormItem is DataFormTextEditorItem textItem)
            {
                textItem.Keyboard = Keyboard.Email;
            }
        }

        /// <summary>
        /// Invokes on sign in button click.
        /// </summary>
        /// <param name="sender">The sign in button.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnSignInButtonCliked(object? sender, EventArgs e)
        {
            if(this.dataForm != null && App.Current?.MainPage != null)
            {
                if(this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Signed in successfully", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.signInButton != null)
            {
                this.signInButton.Clicked -= OnSignInButtonCliked;  
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
            }
        }
    }
}
