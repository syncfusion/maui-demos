using Android.Content;
using Android.Print;
using Android.OS;
using Java.IO;
using AndroidFile = Java.IO.File;
using SystemFile = System.IO.File;

namespace SampleBrowser.Maui.Services

{
    public class PrintService : IPrintService
    {
        public async Task PrintAsync(string filePath)
        {
            try
            {
                var context = Platform.CurrentActivity ?? throw new InvalidOperationException("Current Activity is null");

                // Get the PrintManager service
                var printManager = (PrintManager?)context.GetSystemService(Context.PrintService);
                if (printManager == null)
                    throw new Exception("PrintManager not available");

                // Create print job name
                string jobName = $"{context.PackageName} Document";

                // Create PrintDocumentAdapter
                var printAdapter = new PdfDocumentAdapter(context, filePath);

                // Print the document
                printManager.Print(jobName, printAdapter, null);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception($"Print failed: {ex.Message}");
            }
        }
    }

    // Custom PDF Document Adapter
    public class PdfDocumentAdapter : PrintDocumentAdapter
    {
        private readonly Context _context;
        private readonly string _filePath;

        public PdfDocumentAdapter(Context context, string filePath)
        {
            _context = context;
            _filePath = filePath;
        }

        public override void OnLayout(PrintAttributes? oldAttributes, PrintAttributes? newAttributes,
            CancellationSignal? cancellationSignal, LayoutResultCallback? callback, Bundle? extras)
        {
            if (cancellationSignal?.IsCanceled == true)
            {
                callback?.OnLayoutCancelled();
                return;
            }

            var builder = new PrintDocumentInfo.Builder("document.pdf")
                .SetContentType(PrintContentType.Document)
                .SetPageCount(PrintDocumentInfo.PageCountUnknown);

            var info = builder.Build();
            callback?.OnLayoutFinished(info, newAttributes?.Equals(oldAttributes) != false);
        }

        public override void OnWrite(PageRange[]? pages, ParcelFileDescriptor? destination,
            CancellationSignal? cancellationSignal, WriteResultCallback? callback)
        {
            try
            {
                if (destination == null)
                {
                    callback?.OnWriteFailed("Destination is null");
                    return;
                }

                using var input = new FileInputStream(new AndroidFile(_filePath));
                using var output = new FileOutputStream(destination.FileDescriptor);

                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = input.Read(buffer)) != -1)
                {
                    if (cancellationSignal?.IsCanceled == true)
                    {
                        callback?.OnWriteCancelled();
                        return;
                    }
                    output.Write(buffer, 0, bytesRead);
                }

                //PageRange[] resultPages = new[] { PageRange.AllPages };
                //callback?.OnWriteFinished(resultPages);


                PageRange[] resultPages = new PageRange[]
                {
                    PageRange.AllPages!
                };

                callback?.OnWriteFinished(resultPages);

            }
            catch (Exception ex)
            {
                callback?.OnWriteFailed(ex.Message);
            }
        }
    }
}