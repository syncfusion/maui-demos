#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public class PdfFileData 
{
    /// <summary>
    /// File name of the PDF with extension
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// The stream read from the PDF file.
    /// </summary>
    public Stream? Stream { get; }

    public PdfFileData(string fileName, Stream? stream)
    {
        FileName = fileName;
        Stream = stream;
    }
}