#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Syncfusion.XlsIO;

namespace SampleBrowser.Maui.XlsIO
{
    #region Hyperlink Class

    public class Hyperlink : IHyperLink
    {
        public IApplication Application { get; private set; }
        public object Parent { get; private set; }
        public string Address { get; set; }
        public string Name { get; private set; }
        public IRange Range { get; private set; }
        public string ScreenTip { get; set; }
        public string SubAddress { get; set; }
        public string TextToDisplay { get; set; }
        public ExcelHyperLinkType Type { get; set; }
        public IShape Shape { get; private set; }
        public ExcelHyperlinkAttachedType AttachedType { get; private set; }
        public byte[] Image { get; set; }

        public Hyperlink(string address, string subAddress, string screenTip, string textToDisplay, ExcelHyperLinkType type, byte[] image)
        {
            Address = address;
            ScreenTip = screenTip;
            SubAddress = subAddress;
            TextToDisplay = textToDisplay;
            Type = type;
            Image = image;
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public Hyperlink Link { get; set; }
    }

    #endregion
    /// <summary>
    /// A simple helper class
    /// </summary>
    public class Customers 
    {

        #region Members
        private string m_salesPerson;
        private int m_salesJanJune;
        private int m_salesJulyDec;
        private int m_change;
        private byte[] m_image;
        private Hyperlink m_hyperlink;
        private string m_imageText;
        #endregion

        #region Properties
        public string SalesPerson
        {
            get
            {
                return m_salesPerson;
            }
            set
            {
                m_salesPerson = value;
            }
        }

        public int SalesJanJune
        {
            get
            {
                return m_salesJanJune;
            }
            set
            {
                m_salesJanJune = value;
            }
        }
        public int SalesJulyDec
        {
            get
            {
                return m_salesJulyDec;
            }
            set
            {
                m_salesJulyDec = value;
            }

        }
        public int Change
        {
            get
            {
                return m_change;
            }
            set
            {
                m_change = value;
            }

        }
        public string ImageText
        {
            get
            {
                return m_imageText;
            }
            set
            {
                m_imageText = value;
            }
        }

        public Hyperlink Hyperlink
        {
            get
            {
                return m_hyperlink;
            }
            set
            {
                m_hyperlink = value;
            }

        }
        public byte[] Image
        {
            get
            {
                return m_image;
            }
            set
            {
                m_image = value;
            }
        }
        #endregion

        #region Intialization
        public Customers()
        {
        }
        public Customers(string name, int juneToJuly, int julyToDec, int change, string imageText, byte[] image)
        {
            this.m_salesPerson = name;
            this.m_salesJanJune = juneToJuly;
            this.m_salesJulyDec = julyToDec;
            this.m_change = change;
            this.m_image = image;
            this.m_imageText = imageText;
        }
        #endregion
    }
}
