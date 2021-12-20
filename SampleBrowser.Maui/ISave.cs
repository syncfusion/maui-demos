#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.IO;
using System.Threading.Tasks;
public interface ISave
{
    //Method to save document as a file and view the saved document
	Task SaveAndView(string filename, string contentType, MemoryStream stream);
}