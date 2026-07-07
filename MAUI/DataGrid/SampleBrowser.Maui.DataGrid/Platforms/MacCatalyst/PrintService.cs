using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Services
{
    public class PrintService : IPrintService
    {
        public async Task PrintAsync(string filePath)
        {
            try
            {
                var printController = UIPrintInteractionController.SharedPrintController;

                if (printController == null)
                    throw new Exception("Print controller not available");

                // Configure print info
                var printInfo = UIPrintInfo.PrintInfo;
                printInfo.OutputType = UIPrintInfoOutputType.General;
                printInfo.JobName = "DataGrid Export";
                printInfo.Duplex = UIPrintInfoDuplex.None;

                printController.PrintInfo = printInfo;

                // Set print item (PDF file)
                var url = NSUrl.FromFilename(filePath);
                printController.PrintingItem = url;

                // Present print dialog
                var tcs = new TaskCompletionSource<bool>();

                var viewController = GetCurrentViewController();
                if (viewController == null)
                    throw new Exception("Unable to get current view controller");

                printController.Present(true, (printInteractionController, completed, error) =>
                {
                    if (error != null)
                        tcs.SetException(new Exception(error.LocalizedDescription));
                    else
                        tcs.SetResult(completed);
                });

                await tcs.Task;
            }
            catch (Exception ex)
            {
                throw new Exception($"Print failed: {ex.Message}");
            }
        }

        private UIViewController? GetCurrentViewController()
        {
            var windowScene = UIApplication.SharedApplication
                .ConnectedScenes
                .OfType<UIWindowScene>()
                .FirstOrDefault();

            var window = windowScene?
                .Windows
                .FirstOrDefault(w => w.IsKeyWindow);

            var viewController = window?.RootViewController;

            while (viewController?.PresentedViewController != null)
            {
                viewController = viewController.PresentedViewController;
            }

            return viewController;
        }
    }
}