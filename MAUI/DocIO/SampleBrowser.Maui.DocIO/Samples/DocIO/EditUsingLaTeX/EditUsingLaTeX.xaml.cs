#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
#if DOCIOSB
using SampleBrowser.Maui.DocIO.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class EditUsingLaTeX : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EditUsingLaTeX()
        {
            InitializeComponent();
            assembly = typeof(EditUsingLaTeX).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates a Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {

            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditEquationLaTeXInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditEquationLaTeXInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);

            //Find all the equations in the Word document.
            List<Entity> entities = document.FindAllItemsByProperty(EntityType.Math, null, null);

            //Iterate through each equation in the Word document.
            foreach (Entity entity in entities)
            {
                WMath math = (WMath)entity;
                //Get the laTeX code of equation.
                string laTeX = math.MathParagraph.LaTeX;

                //Modify the laTeX code of derivative equation.
                if (laTeX.StartsWith("\\frac"))
                    laTeX = laTeX.Replace("n", "k");
                //Modify the laTeX code of expansion of the sum equation.
                else if (laTeX.StartsWith("{\\left"))
                    laTeX = laTeX.Replace("n", "k");
                //Modify the laTeX code of quadratic equation.
                else if (laTeX.StartsWith("x=\\frac"))
                    laTeX = laTeX.Replace("x", "y");

                //Update the modified laTeX code to the equation.
                math.MathParagraph.LaTeX = laTeX;
            }


            using MemoryStream ms = new();
            //Saves the word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditEquationLaTeX.docx", "application/msword", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditEquationLaTeXInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditEquationLaTeXInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditEquationLaTeXInput.docx", "application/msword", ms);
        }
        #endregion
    }
}