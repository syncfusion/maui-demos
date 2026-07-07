using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Printing;
using System.Diagnostics;
using System.Globalization;
using Windows.Data.Pdf;
using Windows.Globalization;
using Windows.Graphics.Printing;
using Windows.Storage.Streams;
using Color = Windows.UI.Color;
using HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment;
using Image = Microsoft.UI.Xaml.Controls.Image;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;
using Thickness = Microsoft.UI.Xaml.Thickness;
using VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment;
using Window = Microsoft.UI.Xaml.Window;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maui.Devices;
using Microsoft.Maui;

namespace SampleBrowser.Maui.Services
{
    public class PrintService : IPrintService
    {
        private readonly Dictionary<int, UIElement> printPreviewPages = new();
        private readonly Canvas pdfDocumentPanel = new();
        private PrintManager? printMan;
        private PrintTask? printTask;
        private PdfDocument? sourcePdfDocument;
        private PrintDocument? printDocument;
        private IPrintDocumentSource? printDocumentSource;
        private Window? window;
        private string? fileName;
        private double marginWidth;
        private double marginHeight;
        private int pageCount;
        private nint hWnd;

        public async Task PrintAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath is null or empty", nameof(filePath));

            var mauiWindow = Microsoft.Maui.Controls.Application.Current?.Windows.FirstOrDefault();
            if (mauiWindow?.Handler?.PlatformView is not Microsoft.UI.Xaml.Window nativeWindow)
                throw new InvalidOperationException("Native Window not available.");

            this.fileName = Path.GetFileName(filePath);
            this.window = nativeWindow;
            this.hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            using var fs = File.OpenRead(filePath);

            // Read into memory stream like the original helper
            using var ms = new MemoryStream();
            await fs.CopyToAsync(ms);
            ms.Position = 0;

            var ras = await ConvertToRandomAccessStream(ms);

            this.sourcePdfDocument = await PdfDocument.LoadFromStreamAsync(ras);

            this.pageCount = (int)this.sourcePdfDocument.PageCount;

            try
            {
                await PrintWithCanvas();
            }
            catch
            {
                Debug.WriteLine("No UI thread available, printing without Canvas.");

                await PrintWithoutCanvas();
            }
        }

        private async Task PrintWithCanvas()
        {
            await this.IncludeCanvas();

            this.RegisterForPrint();

            await PrintManagerInterop.ShowPrintUIForWindowAsync(hWnd);
        }

        private async Task PrintWithoutCanvas()
        {
            this.RegisterForPrint();

            await PrintManagerInterop.ShowPrintUIForWindowAsync(hWnd);
        }

        private void RegisterForPrint()
        {
            // Clean up any previous print state to ensure a fresh registration.
            // This prevents stale handlers and ensures the dialog can open on subsequent attempts.
            if (printDocument != null)
            {
                printDocument.Paginate -= PrintDoc_Paginate;
                printDocument.GetPreviewPage -= PrintDoc_GetPreviewPage;
                printDocument.AddPages -= AddPrintPages;
            }
            if (printMan != null)
            {
                printMan.PrintTaskRequested -= PrintTask_Requested;
            }

            printDocument = new PrintDocument();
            printDocumentSource = printDocument.DocumentSource;
            printDocument.Paginate += PrintDoc_Paginate;
            printDocument.GetPreviewPage += PrintDoc_GetPreviewPage;
            printDocument.AddPages += AddPrintPages;

            printMan = PrintManagerInterop.GetForWindow(hWnd);
            printMan.PrintTaskRequested += PrintTask_Requested;
        }

        private async void AddPrintPages(object sender, AddPagesEventArgs e)
        {
            try
            {
                await this.PrepareForPrint(0, pageCount);

                ((PrintDocument)sender).AddPagesComplete();
            }
            catch
            {
                ((PrintDocument)sender).InvalidatePreview();
            }

            if (printDocument != null)
            {
                printDocument.Paginate -= PrintDoc_Paginate;
                printDocument.GetPreviewPage -= PrintDoc_GetPreviewPage;
                printDocument.AddPages -= AddPrintPages;
            }
        }


        private async Task PrepareForPrint(int p, int count)
        {
            if (sourcePdfDocument != null)
            {
                for (var i = p; i < count; i++)
                {
                    ApplicationLanguages.PrimaryLanguageOverride = CultureInfo.InvariantCulture.TwoLetterISOLanguageName;

                    using var pdfPage = sourcePdfDocument.GetPage(uint.Parse(i.ToString()));

                    using var ras = new InMemoryRandomAccessStream();

                    await pdfPage.RenderToStreamAsync(ras, new PdfPageRenderOptions
                    {
                        DestinationWidth = (uint)(pdfPage.Size.Width * pdfPage.PreferredZoom),
                        DestinationHeight = (uint)(pdfPage.Size.Height * pdfPage.PreferredZoom)
                    });

                    var imageCtrl = new Image();
                    var src = new BitmapImage();
                    ras.Seek(0);
                    src.SetSource(ras);
                    imageCtrl.Source = src;

                    imageCtrl.Height = src.PixelHeight / DeviceDisplay.Current.MainDisplayInfo.Density;
                    imageCtrl.Width = src.PixelWidth / DeviceDisplay.Current.MainDisplayInfo.Density;

                    if (printDocument != null)
                    {
                        printDocument.AddPage(imageCtrl);
                    }
                }
            }
        }

        private async Task IncludeCanvas()
        {
            pdfDocumentPanel.Children.Clear();
            printPreviewPages.Clear();

            if (this.sourcePdfDocument != null)
            {
                for (var i = 0; i < pageCount; i++)
                {
                    using var pdfPage = this.sourcePdfDocument.GetPage(uint.Parse(i.ToString()));

                    using var ras = new InMemoryRandomAccessStream();

                    await pdfPage.RenderToStreamAsync(ras, new PdfPageRenderOptions
                    {
                        DestinationWidth = (uint)(pdfPage.Size.Width * pdfPage.PreferredZoom),
                        DestinationHeight = (uint)(pdfPage.Size.Height * pdfPage.PreferredZoom)
                    });

                    var imageCtrl = new Image();
                    var src = new BitmapImage();
                    ras.Seek(0);
                    src.SetSource(ras);
                    imageCtrl.Source = src;

                    imageCtrl.Height = src.PixelHeight / DeviceDisplay.Current.MainDisplayInfo.Density;
                    imageCtrl.Width = src.PixelWidth / DeviceDisplay.Current.MainDisplayInfo.Density;

                    var pageCanvas = new Canvas
                    {
                        Width = pdfPage.Size.Width,
                        Height = pdfPage.Size.Height,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                        Margin = new Thickness(0, 0, 0, 0)
                    };

                    pageCanvas.Children.Add(imageCtrl);

                    pdfDocumentPanel.Children.Add(pageCanvas);
                }
            }
        }

        #region Event handlers

        private void PrintTask_Requested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            printTask = e.Request.CreatePrintTask(fileName, sourceRequested => sourceRequested.SetSource(printDocumentSource));
            printTask.Completed += PrintTask_Completed;
        }

        private void PrintTask_Completed(PrintTask sender, PrintTaskCompletedEventArgs args)
        {
            if (printTask != null)
            {
                printTask.Completed -= PrintTask_Completed;
                printTask = null;
            }

            if (printMan != null)
            {
                printMan.PrintTaskRequested -= PrintTask_Requested;
                printMan = null;
            }
        }

        private void PrintDoc_Paginate(object sender, PaginateEventArgs e)
        {
            var pageDescription = e.PrintTaskOptions.GetPageDescription((uint)e.CurrentPreviewPageNumber);

            marginWidth = pageDescription.PageSize.Width;
            marginHeight = pageDescription.PageSize.Height;

            for (var i = pdfDocumentPanel.Children.Count - 1; i >= 0; i--)
            {
                var print = pdfDocumentPanel.Children[i] as Canvas;

                if (print == null)
                    continue;

                print.Width = marginWidth;
                print.Height = marginHeight;

                this.printPreviewPages.TryAdd(i, print);
            }

            ((PrintDocument)sender).SetPreviewPageCount(pageCount, PreviewPageCountType.Final);
        }

        private void PrintDoc_GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            pdfDocumentPanel.Children.Remove(this.printPreviewPages[e.PageNumber - 1]);
            ((PrintDocument)sender).SetPreviewPage(e.PageNumber, this.printPreviewPages[e.PageNumber - 1]);
        }

        #endregion

        #region Static Helpers

        private static async Task<IRandomAccessStream> ConvertToRandomAccessStream(Stream memoryStream)
        {
            var randomAccessStream = new InMemoryRandomAccessStream();

            using var contentStream = new MemoryStream();

            await memoryStream.CopyToAsync(contentStream);

            using var outputStream = randomAccessStream.GetOutputStreamAt(0);
            using var dw = new DataWriter(outputStream);

            dw.WriteBytes(contentStream.ToArray());

            await dw.StoreAsync();
            await outputStream.FlushAsync();
            await dw.FlushAsync();

            outputStream.Dispose();
            dw.DetachStream();

            return randomAccessStream;
        }

        #endregion
    }
}
