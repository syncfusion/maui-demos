#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class StampsData
    {
        public List<StandardStamps> StandardStampItems { get; set; }

        public StampsData()
        {
            StandardStampItems = new List<StandardStamps>
            {

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#ecf3e5"), LabelText = "APPROVED",
                LabelTextColor = Color.FromArgb("#416a1c"),BorderHeight = 36,BorderColor = Color.FromArgb("#416a1c"),
                #if ANDROID || IOS
                    BorderWidth = 90
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 103
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#ecf3e5"), LabelText = "FINAL",
                LabelTextColor = Color.FromArgb("#416a1c"),BorderHeight = 36,BorderColor = Color.FromArgb("#416a1c"),
                #if ANDROID || IOS
                    BorderWidth = 55
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 71
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "AS IS",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                 #if ANDROID || IOS
                    BorderWidth = 55
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 60
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#ecf3e5"), LabelText = "COMPLETED",
                LabelTextColor = Color.FromArgb("#416a1c"),BorderHeight = 36,BorderColor = Color.FromArgb("#416a1c"),
                #if ANDROID || IOS
                    BorderWidth = 98
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 114
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#fce2e0"), LabelText = "NOT APPROVED",
                LabelTextColor = Color.FromArgb("#910f05"),BorderHeight = 36,BorderColor = Color.FromArgb("#910f05"),
                #if ANDROID || IOS
                    BorderWidth = 120
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 139
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "CONFIDENTIAL",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                #if ANDROID || IOS
                    BorderWidth = 112
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 132
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "DEPARTMENTAL",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"), 
                #if ANDROID || IOS
                    BorderWidth = 128
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 143
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "DRAFT",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                #if ANDROID || IOS
                    BorderWidth = 60
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 71
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "EXPIRED",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                 #if ANDROID || IOS
                    BorderWidth = 75
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 85
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "FOR COMMENT",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564" ),
                #if ANDROID || IOS
                    BorderWidth = 118
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 134
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "FOR PUBLIC RELEASE",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                #if ANDROID || IOS
                    BorderWidth = 155
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 181
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "INFORMATION ONLY",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                #if ANDROID || IOS
                    BorderWidth = 148
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 171
                #endif
            },

            new StandardStamps 
            { 
                BorderBackground = Color.FromArgb("#fce2e0"), LabelText = "VOID",
                LabelTextColor = Color.FromArgb("#910f05"),BorderHeight = 36,BorderColor = Color.FromArgb("#910f05"),
                #if ANDROID || IOS
                    BorderWidth = 56
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 70
                #endif
            },

            new StandardStamps
            {
                BorderBackground = Color.FromArgb("#dde5f2"), LabelText = "PRELIMINARY RESULTS",
                LabelTextColor = Color.FromArgb("#182564"),BorderHeight = 36,BorderColor = Color.FromArgb("#182564"),
                #if ANDROID || IOS
                    BorderWidth = 160
                #elif WINDOWS || MACCATALYST
                    BorderWidth = 216
                #endif
            },

            };

        }

    }
    public class StandardStamps
    {
        public Color? BorderBackground { get; set; }
        public Color? BorderColor { get; set; }
        public string? LabelText { get; set; }
        public Color? LabelTextColor { get; set; }
        public double? BorderHeight { get; set; }
        public double? BorderWidth { get; set; }
    }


}
