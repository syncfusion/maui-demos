#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class StampView : ContentView
    {
        public Syncfusion.Maui.PdfViewer.SfPdfViewer? StampHelper { get; set; }
        public event EventHandler? StampSelected;

        internal StampType stampType { get; set; }

        internal bool StampMode { get; set; } = false;

        public VerticalStackLayout? CustomStampListLayout { get; set; }

        public StampView()
        {
            Shadow = new Shadow
            {
                Offset = new Point(-1, 0),
                Brush = Color.FromRgba("#000000"),
#if IOS
                Radius = 1,
#else
                Radius = 15,
#endif
                Opacity = 0.5f
            };
        }

        public void OnStampSelected()
        {
            StampSelected?.Invoke(this, EventArgs.Empty);
        }

        internal StampType GetStampType(string stampType)
        {
            if (stampType == "APPROVED")
                return StampType.Approved;
            else if (stampType == "AS IS")
                return StampType.AsIs;
            else if (stampType == "COMPLETED")
                return StampType.Completed;
            else if (stampType == "CONFIDENTIAL")
                return StampType.Confidential;
            else if (stampType == "DEPARTMENTAL")
                return StampType.Departmental;
            else if (stampType == "DRAFT")
                return StampType.Draft;
            else if (stampType == "EXPERIMENTAL")
                return StampType.Experimental;
            else if (stampType == "EXPIRED")
                return StampType.Expired;
            else if (stampType == "FINAL")
                return StampType.Final;
            else if (stampType == "FOR COMMENT")
                return StampType.ForComment;
            else if (stampType == "FOR PUBLIC RELEASE")
                return StampType.ForPublicRelease;
            else if (stampType == "INFORMATION ONLY")
                return StampType.InformationOnly;
            else if (stampType == "NOT APPROVED")
                return StampType.NotApproved;
            else if (stampType == "NOT FOR PUBLIC RELEASE")
                return StampType.NotForPublicRelease;
            else if (stampType == "VOID")
                return StampType.Void;
            else if (stampType == "PRELIMINARY RESULTS")
                return StampType.PreliminaryResults;
            return StampType.Departmental;
        }

        internal Task<Stream>? GetStreamFromImageSourceAsync(StreamImageSource streamImage, CancellationToken cancellationToken)
        {
            if (streamImage.Stream != null)
            {
                return streamImage.Stream(cancellationToken);
            }
            return null;
        }
    }
}
