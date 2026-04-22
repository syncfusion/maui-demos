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
using AVFoundation;
using Foundation;
using Speech;

namespace SampleBrowser.Maui.AIAssistView.Platforms;

/// <summary>
/// macOS/Catalyst Speech Recognition event handler following Syncfusion control standards.
/// Handles audio engine, recognition requests, and results from Apple's SFSpeechRecognizer.
/// </summary>
internal class MacCatalystSpeechRecognitionEventHandler
{
    /// <summary>
    /// Event raised when an error occurs during recognition.
    /// </summary>
    public event EventHandler<MacCatalystSpeechRecognitionErrorEventArgs>? RecognitionErrorOccurred;

    /// <summary>
    /// Event raised when partial recognition results are available.
    /// </summary>
    public event EventHandler<MacCatalystRecognitionResultEventArgs>? PartialResultsReceived;

    /// <summary>
    /// Event raised when final recognition results are available.
    /// </summary>
    public event EventHandler<MacCatalystRecognitionResultEventArgs>? FinalResultsReceived;

    protected virtual void OnRecognitionError(MacCatalystSpeechRecognitionErrorEventArgs args)
    {
        RecognitionErrorOccurred?.Invoke(this, args);
    }

    protected virtual void OnPartialResults(MacCatalystRecognitionResultEventArgs args)
    {
        PartialResultsReceived?.Invoke(this, args);
    }

    protected virtual void OnFinalResults(MacCatalystRecognitionResultEventArgs args)
    {
        FinalResultsReceived?.Invoke(this, args);
    }

    public void HandleRecognitionResult(SFSpeechRecognitionResult? result, NSError? error, int currentIndex, ref int updatedIndex, IProgress<string>? progress)
    {
        if (error != null)
        {
            OnRecognitionError(new MacCatalystSpeechRecognitionErrorEventArgs(error.LocalizedDescription));
            return;
        }

        if (result?.Final == true)
        {
            updatedIndex = 0;
            var finalText = result.BestTranscription.FormattedString ?? string.Empty;
            OnFinalResults(new MacCatalystRecognitionResultEventArgs(finalText));
        }
        else if (result != null)
        {
            for (var i = currentIndex; i < result.BestTranscription.Segments.Length; i++)
            {
                var segmentText = result.BestTranscription.Segments[i].Substring ?? string.Empty;
                updatedIndex++;
                progress?.Report(segmentText);
                OnPartialResults(new MacCatalystRecognitionResultEventArgs(segmentText));
            }
        }
    }
}

/// <summary>
/// Event arguments for macOS/Catalyst speech recognition errors.
/// </summary>
internal class MacCatalystSpeechRecognitionErrorEventArgs : EventArgs
{
    public MacCatalystSpeechRecognitionErrorEventArgs(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }
}

/// <summary>
/// Event arguments for macOS/Catalyst speech recognition results.
/// </summary>
internal class MacCatalystRecognitionResultEventArgs : EventArgs
{
    public MacCatalystRecognitionResultEventArgs(string recognizedText)
    {
        RecognizedText = recognizedText;
    }

    public string RecognizedText { get; }
}

public class SfSpeechToText : ISfSpeechToText
{
    private AVAudioEngine? _audioEngine;
    private SFSpeechAudioBufferRecognitionRequest? _speechRequest;
    private SFSpeechRecognizer? _recognizer;
    private SFSpeechRecognitionTask? _recognitionTask;
    private MacCatalystSpeechRecognitionEventHandler? _eventHandler;

    public async Task<string> StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        _recognizer = new SFSpeechRecognizer(NSLocale.FromLocaleIdentifier(culture.Name));

        if (_recognizer?.Available != true)
            throw new InvalidOperationException("Speech recognizer is not available");

        if (SFSpeechRecognizer.AuthorizationStatus != SFSpeechRecognizerAuthorizationStatus.Authorized)
            throw new UnauthorizedAccessException("Microphone permission denied");

        _eventHandler = new MacCatalystSpeechRecognitionEventHandler();
        _audioEngine = new AVAudioEngine();
        _speechRequest = new SFSpeechAudioBufferRecognitionRequest();

        var audioSession = AVAudioSession.SharedInstance();
        audioSession.SetCategory(AVAudioSessionCategory.Record, AVAudioSessionCategoryOptions.DefaultToSpeaker);

        var mode = audioSession.AvailableModes.FirstOrDefault() ?? "AVAudioSessionModeMeasurement";
        audioSession.SetMode(new NSString(mode), out var audioSessionError);
        if (audioSessionError != null)
            throw new InvalidOperationException(audioSessionError.LocalizedDescription);

        audioSession.SetActive(true, AVAudioSessionSetActiveOptions.NotifyOthersOnDeactivation, out audioSessionError);
        if (audioSessionError is not null)
            throw new InvalidOperationException(audioSessionError.LocalizedDescription);

        var node = _audioEngine.InputNode;
        var recordingFormat = node.GetBusOutputFormat(new UIntPtr(0));
        node.InstallTapOnBus(new UIntPtr(0), 1024, recordingFormat, (buffer, _) =>
        {
            _speechRequest?.Append(buffer);
        });

        _audioEngine.Prepare();
        _audioEngine.StartAndReturnError(out var error);

        if (error is not null)
            throw new InvalidOperationException($"Error starting audio engine: {error.LocalizedDescription}");

        var currentIndex = 0;
        var result = new TaskCompletionSource<string>();

        _eventHandler.RecognitionErrorOccurred += (sender, args) =>
        {
            StopRecognition();
            result.TrySetException(new Exception($"Speech recognition error: {args.ErrorMessage}"));
        };

        _eventHandler.FinalResultsReceived += (sender, args) =>
        {
            StopRecognition();
            result.TrySetResult(args.RecognizedText);
        };

        _recognitionTask = _recognizer.GetRecognitionTask(_speechRequest, (speechResult, err) =>
        {
            var updatedIndex = currentIndex;
            _eventHandler.HandleRecognitionResult(speechResult, err, currentIndex, ref updatedIndex, recognitionResult);
            currentIndex = updatedIndex;
        });

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
            _audioEngine?.InputNode.RemoveTapOnBus(new UIntPtr(0));
            _audioEngine?.Stop();
            _speechRequest?.EndAudio();
            _recognitionTask?.Cancel();
        }
        catch
        {
        }
    }

    public Task<bool> RequestAudioPermissionAsync()
    {
        var result = new TaskCompletionSource<bool>();
        SFSpeechRecognizer.RequestAuthorization(status =>
        {
            result.SetResult(status == SFSpeechRecognizerAuthorizationStatus.Authorized);
        });
        return result.Task;
    }

    public async ValueTask DisposeAsync()
    {
        StopRecognition();
        _audioEngine?.Dispose();
        _recognizer?.Dispose();
        _speechRequest?.Dispose();
        _recognitionTask?.Dispose();
        await ValueTask.CompletedTask;
    }
}
