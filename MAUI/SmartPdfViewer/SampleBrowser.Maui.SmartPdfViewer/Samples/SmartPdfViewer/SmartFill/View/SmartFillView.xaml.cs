using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf.Parsing;

namespace SampleBrowser.Maui.SmartPdfViewer.SfSmartPdfViewer;

public partial class SmartFillView : SampleView
{
    private bool tapped;
    Animation animation;
    public SmartFillView()
    {
        InitializeComponent();
        animation = new Animation();
        PdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
        Clipboard.ClipboardContentChanged += Clipboard_ClipboardContentChanged;
    }
    private async void Clipboard_ClipboardContentChanged(object? sender, EventArgs e)
    {
        string? copiedText = await Clipboard.GetTextAsync();
        StartBubbleAnimation();
        if (copiedText == viewModel.UserDetail1 || copiedText == viewModel.UserDetail2 || copiedText == viewModel.UserDetail3)
        {
            SubmitForm.IsEnabled = true;
        }
        else
        {
            SubmitForm.IsEnabled = false;
            StopBubbleAnimation();
        }
    }
    private async void OnSmartFillClicked(object? sender, EventArgs? e)
    {
        string? copiedText = await Clipboard.GetTextAsync();
        if(copiedText!=null)
        await PdfViewer.ApplySmartFillAsync(copiedText,new CancellationToken());
    }
    private void SavePDF(object? sender, EventArgs? e)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "SavedSample.pdf");

        var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        PdfViewer.SaveDocument(stream);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.Current?.MainPage?.DisplayAlertAsync("Success", $"Document saved successfully at:\n{filePath}", "OK");
#pragma warning restore CS0618 // Type or member is obsolete

    }
    private void PrintPDF(object? sender, EventArgs? e)
    {
        PdfViewer.PrintDocument();
    }

    private void PdfViewer_DocumentLoaded(object? sender, EventArgs? e)
    {
#if ANDROID || IOS
        MobileCopiedData.IsVisible = true;
#endif
    }

    private async void AddTextToClipBoard(object? sender, EventArgs? e)
    {
        if (sender is Button button)
        {
            button.Text = "\ue726";
            switch (button.AutomationId) // You can also use button.Id, button.AutomationId, etc.
            {
                case "CopiedButton1":
                    await Clipboard.SetTextAsync(InputData1.Text);
                    break;
                case "CopiedButton2":
                    // Logic for Button2
                    await Clipboard.SetTextAsync(InputData2.Text);
                    break;
                case "CopiedButton3":
                    // Logic for Button3
                    await Clipboard.SetTextAsync(InputData3.Text);
                    break;
            }
            await Task.Delay(3000);
            button.Text = "\ue737";
        }
    }

    private void FullViewForCopiedData(object? sender, EventArgs? e)
    {
        if (CopiedDataViewButton.Text == "\ue702")
        {
            MobileCopiedData.HeightRequest = 2 * MobileCopiedData.HeightRequest;
            CopiedDataViewButton.Text = "\ue703";
        }
        else
        {
            MobileCopiedData.HeightRequest = MobileCopiedData.HeightRequest / 2;
            CopiedDataViewButton.Text = "\ue702";
        }
    }
    private void StartBubbleAnimation()
    {
        if (!tapped)
        {
            var bubbleEffect = new Animation(v => SubmitForm.Scale = v, 1, 1.05, Easing.CubicInOut);
            var fadeEffect = new Animation(v => SubmitForm.Opacity = v, 1, 0.5, Easing.CubicInOut);

            animation.Add(0, 0.5, bubbleEffect);
            animation.Add(0, 0.5, fadeEffect);
            animation.Add(0.5, 1, new Animation(v => SubmitForm.Scale = v, 1.05, 1, Easing.CubicInOut));
            animation.Add(0.5, 1, new Animation(v => SubmitForm.Opacity = v, 1, 1, Easing.CubicInOut));

            animation.Commit(this, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);

        }
    }

    private void StopBubbleAnimation()
    {
        this.AbortAnimation("BubbleEffect");
        tapped = false;
    }
}