#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using SpeechRecognizer = Windows.Media.SpeechRecognition.SpeechRecognizer;

namespace SampleBrowser.Maui.AIAssistView.Platforms;

/// <summary>
/// Windows Speech Recognition event handler for online mode following Syncfusion control standards.
/// </summary>
public class WindowsSpeechRecognitionEventHandler
{
    /// <summary>
    /// Event raised when recognition result is generated.
    /// </summary>
    public event EventHandler<WindowsRecognitionResultEventArgs>? ResultGenerated;

    /// <summary>
    /// Event raised when recognition session is completed.
    /// </summary>
    public event EventHandler<WindowsRecognitionCompletedEventArgs>? RecognitionCompleted;

    /// <summary>
    /// Event raised when an error occurs.
    /// </summary>
    public event EventHandler<WindowsSpeechRecognitionErrorEventArgs>? RecognitionErrorOccurred;

    /// <summary>
    /// Gets or sets the accumulated recognition text.
    /// </summary>
    public string AccumulatedText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recognition state.
    /// </summary>
    public string RecognitionState { get; set; } = "Idle";

    protected virtual void OnResultGenerated(string text)
    {
        ResultGenerated?.Invoke(this, new WindowsRecognitionResultEventArgs(text));
    }

    protected virtual void OnRecognitionCompleted(SpeechRecognitionResultStatus status)
    {
        RecognitionCompleted?.Invoke(this, new WindowsRecognitionCompletedEventArgs(status, AccumulatedText));
    }

    protected virtual void OnRecognitionError(string errorMessage)
    {
        RecognitionErrorOccurred?.Invoke(this, new WindowsSpeechRecognitionErrorEventArgs(errorMessage));
    }

    public void AccumulateResults(string text)
    {
        AccumulatedText += text;
        OnResultGenerated(text);
    }

    public void CompleteRecognition(SpeechRecognitionResultStatus status)
    {
        OnRecognitionCompleted(status);
    }

    public void HandleError(string errorMessage)
    {
        OnRecognitionError(errorMessage);
    }
}

/// <summary>
/// Event arguments for Windows recognition results.
/// </summary>
public class WindowsRecognitionResultEventArgs : EventArgs
{
    public WindowsRecognitionResultEventArgs(string text)
    {
        Text = text;
    }

    public string Text { get; }
}

/// <summary>
/// Event arguments for Windows recognition completion.
/// </summary>
public class WindowsRecognitionCompletedEventArgs : EventArgs
{
    public WindowsRecognitionCompletedEventArgs(SpeechRecognitionResultStatus status, string accumulatedText)
    {
        Status = status;
        AccumulatedText = accumulatedText;
    }

    public SpeechRecognitionResultStatus Status { get; }
    public string AccumulatedText { get; }
}

/// <summary>
/// Event arguments for Windows speech recognition errors.
/// </summary>
public class WindowsSpeechRecognitionErrorEventArgs : EventArgs
{
    public WindowsSpeechRecognitionErrorEventArgs(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }
}

public class SfSpeechToText : ISfSpeechToText
{
    private SpeechRecognitionEngine? _engine;
    private SpeechRecognizer? _recognizer;
    private WindowsSpeechRecognitionEventHandler? _eventHandler;

    public Task<string> StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            return StartRecognitionOnlineAsync(culture, recognitionResult, cancellationToken);
        }

        return StartRecognitionOfflineAsync(culture, recognitionResult, cancellationToken);
    }

    private async Task<string> StartRecognitionOnlineAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        _eventHandler = new WindowsSpeechRecognitionEventHandler();
        _recognizer = new SpeechRecognizer(new Language(culture.IetfLanguageTag));
        await _recognizer.CompileConstraintsAsync();

        var result = new TaskCompletionSource<string>();
        _recognizer.ContinuousRecognitionSession.ResultGenerated += (s, e) =>
        {
            var text = e.Result?.Text ?? string.Empty;
            _eventHandler.AccumulateResults(text);
            recognitionResult?.Report(text);
        };

        _recognizer.ContinuousRecognitionSession.Completed += (s, e) =>
        {
            _eventHandler.CompleteRecognition(e.Status);
            switch (e.Status)
            {
                case SpeechRecognitionResultStatus.Success:
                    result.TrySetResult(_eventHandler.AccumulatedText);
                    break;
                case SpeechRecognitionResultStatus.UserCanceled:
                    result.TrySetCanceled();
                    break;
                default:
                    result.TrySetException(new Exception($"Speech recognition error: {e.Status}"));
                    break;
            }
        };

        await _recognizer.ContinuousRecognitionSession.StartAsync();

        await using (cancellationToken.Register(async () =>
        {
            await StopRecognitionAsync();
            result.TrySetCanceled();
        }))

        {
            return await result.Task;
        }
    }

    private async Task<string> StartRecognitionOfflineAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        _engine = new SpeechRecognitionEngine(culture);
        _engine.LoadGrammarAsync(new DictationGrammar());
        var accumulatedText = new StringBuilder();

        EventHandler<SpeechRecognizedEventArgs>? speechRecognizedHandler = null;
        EventHandler<RecognizeCompletedEventArgs>? recognizeCompletedHandler = null;

        speechRecognizedHandler = (s, e) =>
        {
            accumulatedText.Append(e.Result.Text + " ");
            recognitionResult?.Report(e.Result.Text);
        };

        recognizeCompletedHandler = (s, e) =>
        {
            if (e.Result != null)
            {
                accumulatedText.Append(e.Result.Text);
            }
        };

        _engine.SpeechRecognized += speechRecognizedHandler;
        _engine.RecognizeCompleted += recognizeCompletedHandler;

        _engine.SetInputToDefaultAudioDevice();
        _engine.RecognizeAsync(RecognizeMode.Multiple);

        var result = new TaskCompletionSource<string>();

        using (cancellationToken.Register(() =>
        {
            StopRecognitionOffline();
            // Unsubscribe handlers
            if (speechRecognizedHandler != null)
                _engine.SpeechRecognized -= speechRecognizedHandler;
            if (recognizeCompletedHandler != null)
                _engine.RecognizeCompleted -= recognizeCompletedHandler;
            result.TrySetCanceled();
        }))
            
        return await result.Task;
        
    }

    private async Task StopRecognitionAsync()
    {
        try
        {
            await _recognizer?.ContinuousRecognitionSession.StopAsync();
        }
        catch
        {
        }
    }

    private void StopRecognitionOffline()
    {
        try
        {
            _engine?.RecognizeAsyncCancel();
        }
        catch
        {
        }
    }

    private async Task CleanupOfflineEngine()
    {
        if (_engine != null)
        {
            try
            {
                StopRecognitionOffline();
                await Task.Delay(100);
            }
            catch
            {
            }
            finally
            {
                _engine?.Dispose();
                _engine = null;
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        await StopRecognitionAsync();
        await CleanupOfflineEngine();
        _recognizer?.Dispose();
    }

    public Task<bool> RequestAudioPermissionAsync()
    {
        return Task.FromResult(true);
    }

    Task<string> ISfSpeechToText.StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        return StartRecognitionAsync(culture, recognitionResult, cancellationToken);
    }
}
