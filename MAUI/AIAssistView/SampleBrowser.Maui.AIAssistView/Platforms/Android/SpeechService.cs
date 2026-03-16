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
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;

namespace SampleBrowser.Maui.AIAssistView.Platforms;

/// <summary>
/// Android Speech Recognition event handler following Syncfusion control standards.
/// Handles recognition events, errors, and partial/final results from Android's SpeechRecognizer.
/// </summary>
internal class AndroidSpeechRecognitionEventHandler : Java.Lang.Object, IRecognitionListener
{
    /// <summary>
    /// Event raised when an error occurs during recognition.
    /// </summary>
    public event EventHandler<AndroidSpeechRecognitionErrorEventArgs>? RecognitionErrorOccurred;

    /// <summary>
    /// Event raised when partial recognition results are available.
    /// </summary>
    public event EventHandler<AndroidRecognitionResultEventArgs>? PartialResultsReceived;

    /// <summary>
    /// Event raised when final recognition results are available.
    /// </summary>
    public event EventHandler<AndroidRecognitionResultEventArgs>? FinalResultsReceived;

    void IRecognitionListener.OnBeginningOfSpeech() => HandleRecognitionStarted();

    void IRecognitionListener.OnBufferReceived(byte[]? buffer) => HandleBufferReceived(buffer);

    void IRecognitionListener.OnEndOfSpeech() => HandleRecognitionEnd();

    void IRecognitionListener.OnError([GeneratedEnum] SpeechRecognizerError error) => HandleRecognitionError(error);

    void IRecognitionListener.OnEvent(int eventType, Bundle? @params) => HandleRecognitionEvent(eventType, @params);

    void IRecognitionListener.OnPartialResults(Bundle? partialResults) => HandlePartialResults(partialResults);

    void IRecognitionListener.OnReadyForSpeech(Bundle? @params) => HandleReadyForSpeech(@params);

    void IRecognitionListener.OnResults(Bundle? results) => HandleFinalResults(results);

    void IRecognitionListener.OnRmsChanged(float rmsdB) => HandleRmsChanged(rmsdB);

    private void HandleRecognitionStarted() { }

    private void HandleBufferReceived(byte[]? buffer) { }

    private void HandleRecognitionEnd() { }

    private void HandleRecognitionError(SpeechRecognizerError error)
    {
        RecognitionErrorOccurred?.Invoke(this, new AndroidSpeechRecognitionErrorEventArgs(error));
    }

    private void HandleRecognitionEvent(int eventType, Bundle? @params) { }

    private void HandlePartialResults(Bundle? partialResults)
    {
        ProcessRecognitionResults(partialResults, PartialResultsReceived);
    }

    private void HandleReadyForSpeech(Bundle? @params) { }

    private void HandleFinalResults(Bundle? results)
    {
        ProcessRecognitionResults(results, FinalResultsReceived);
    }

    private void HandleRmsChanged(float rmsdB) { }

    private static void ProcessRecognitionResults(Bundle? bundle, EventHandler<AndroidRecognitionResultEventArgs>? eventHandler)
    {
        if (bundle == null || eventHandler == null)
            return;

        var matches = bundle.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
        if (matches == null || matches.Count == 0)
            return;

        var recognizedText = matches.FirstOrDefault() ?? string.Empty;
        eventHandler?.Invoke(null, new AndroidRecognitionResultEventArgs(recognizedText));
    }
}

/// <summary>
/// Event arguments for Android speech recognition errors.
/// </summary>
internal class AndroidSpeechRecognitionErrorEventArgs : EventArgs
{
    public AndroidSpeechRecognitionErrorEventArgs(SpeechRecognizerError error)
    {
        Error = error;
        ErrorMessage = GetErrorMessage(error);
    }

    public SpeechRecognizerError Error { get; }
    public string ErrorMessage { get; }

    private static string GetErrorMessage(SpeechRecognizerError error) => error switch
    {
        SpeechRecognizerError.InsufficientPermissions => "Insufficient permissions",
        SpeechRecognizerError.NetworkTimeout => "Network timeout",
        SpeechRecognizerError.NoMatch => "No speech input detected",
        SpeechRecognizerError.RecognizerBusy => "Recognizer busy",
        SpeechRecognizerError.SpeechTimeout => "Speech input timeout",
        _ => $"Recognition error: {error}"
    };
}

/// <summary>
/// Event arguments for Android speech recognition results.
/// </summary>
internal class AndroidRecognitionResultEventArgs : EventArgs
{
    public AndroidRecognitionResultEventArgs(string recognizedText)
    {
        RecognizedText = recognizedText;
    }

    public string RecognizedText { get; }
}

public class SfSpeechToText : ISfSpeechToText
{
    private AndroidSpeechRecognitionEventHandler? _eventHandler;
    private SpeechRecognizer? _recognizer;

    public async Task<string> StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        var result = new TaskCompletionSource<string>();
        _eventHandler = new AndroidSpeechRecognitionEventHandler();

        _eventHandler.RecognitionErrorOccurred += (sender, args) =>
        {
            result.TrySetException(new Exception($"Speech recognition error: {args.ErrorMessage}"));
        };

        _eventHandler.PartialResultsReceived += (sender, args) =>
        {
            recognitionResult?.Report(args.RecognizedText);
        };

        _eventHandler.FinalResultsReceived += (sender, args) =>
        {
            result.TrySetResult(args.RecognizedText);
        };

        _recognizer = SpeechRecognizer.CreateSpeechRecognizer(Android.App.Application.Context) 
            ?? throw new InvalidOperationException("Speech recognizer is not available");

        _recognizer.SetRecognitionListener(_eventHandler);
        _recognizer.StartListening(BuildSpeechIntent(culture));

        await using (cancellationToken.Register(() =>
        {
            StopRecognition();
            result.TrySetCanceled();
        }))
        
        return await result.Task;
    }

    private void StopRecognition()
    {
        try
        {
            _recognizer?.StopListening();
            _recognizer?.Destroy();
        }
        catch
        {
        }
    }

    private static Intent BuildSpeechIntent(CultureInfo culture)
    {
        var speechIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
        var locale = Java.Util.Locale.ForLanguageTag(culture.Name);
        speechIntent.PutExtra(RecognizerIntent.ExtraLanguage, locale);
        speechIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
        speechIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, Android.App.Application.Context.PackageName);
        speechIntent.PutExtra(RecognizerIntent.ExtraPartialResults, true);

        return speechIntent;
    }

    public async Task<bool> RequestAudioPermissionAsync()
    {
        var status = await Permissions.RequestAsync<Permissions.Microphone>();
        var isAvailable = SpeechRecognizer.IsRecognitionAvailable(Android.App.Application.Context);
        return status == PermissionStatus.Granted && isAvailable;
    }

    public ValueTask DisposeAsync()
    {
        StopRecognition();
        _recognizer?.Dispose();
        _eventHandler?.Dispose();
        return ValueTask.CompletedTask;
    }
}
