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
/// iOS Speech Recognition event handler following Syncfusion control standards.
/// Handles audio engine, recognition requests, and results from Apple's SFSpeechRecognizer.
/// </summary>
public class iOSSpeechRecognitionEventHandler
{
    /// <summary>
    /// Event raised when an error occurs during recognition.
    /// </summary>
    public event EventHandler<iOSSpeechRecognitionErrorEventArgs>? RecognitionErrorOccurred;

    /// <summary>
    /// Event raised when partial recognition results are available.
    /// </summary>
    public event EventHandler<iOSRecognitionResultEventArgs>? PartialResultsReceived;

    /// <summary>
    /// Event raised when final recognition results are available.
    /// </summary>
    public event EventHandler<iOSRecognitionResultEventArgs>? FinalResultsReceived;

    /// <summary>
    /// Gets or sets the recognition task state.
    /// </summary>
    public string RecognitionState { get; set; } = "Idle";

    protected virtual void OnRecognitionError(iOSSpeechRecognitionErrorEventArgs args)
    {
        RecognitionErrorOccurred?.Invoke(this, args);
    }

    protected virtual void OnPartialResults(iOSRecognitionResultEventArgs args)
    {
        PartialResultsReceived?.Invoke(this, args);
    }

    protected virtual void OnFinalResults(iOSRecognitionResultEventArgs args)
    {
        FinalResultsReceived?.Invoke(this, args);
    }

    public void HandleRecognitionResult(SFSpeechRecognitionResult? result, NSError? error, int currentIndex, ref int updatedIndex, IProgress<string>? progress)
    {
        if (error != null)
        {
            var errorArgs = new iOSSpeechRecognitionErrorEventArgs(error.LocalizedDescription);
            OnRecognitionError(errorArgs);
            return;
        }

        if (result?.Final == true)
        {
            updatedIndex = 0;
            var finalText = result.BestTranscription.FormattedString ?? string.Empty;
            OnFinalResults(new iOSRecognitionResultEventArgs(finalText, isPartial: false));
        }
        else if (result != null)
        {
            for (var i = currentIndex; i < result.BestTranscription.Segments.Length; i++)
            {
                var segmentText = result.BestTranscription.Segments[i].Substring ?? string.Empty;
                updatedIndex++;
                progress?.Report(segmentText);
                OnPartialResults(new iOSRecognitionResultEventArgs(segmentText, isPartial: true));
            }
        }
    }
}

/// <summary>
/// Event arguments for iOS speech recognition errors.
/// </summary>
public class iOSSpeechRecognitionErrorEventArgs : EventArgs
{
    public iOSSpeechRecognitionErrorEventArgs(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }
}

/// <summary>
/// Event arguments for iOS speech recognition results.
/// </summary>
public class iOSRecognitionResultEventArgs : EventArgs
{
    public iOSRecognitionResultEventArgs(string recognizedText, bool isPartial)
    {
        RecognizedText = recognizedText;
        IsPartial = isPartial;
    }

    public string RecognizedText { get; }
    public bool IsPartial { get; }
}

public class SfSpeechToText : ISfSpeechToText
{
    private AVAudioEngine? _audioEngine;
    private SFSpeechAudioBufferRecognitionRequest? _speechRequest;
    private SFSpeechRecognizer? _recognizer;
    private SFSpeechRecognitionTask? _recognitionTask;
    private iOSSpeechRecognitionEventHandler? _eventHandler;

    public async Task<string> StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        _recognizer = new SFSpeechRecognizer(NSLocale.FromLocaleIdentifier(culture.Name));

        if (_recognizer?.Available != true)
        {
            throw new ArgumentException("Speech recognizer is not available");
        }

        if (SFSpeechRecognizer.AuthorizationStatus != SFSpeechRecognizerAuthorizationStatus.Authorized)
        {
            throw new Exception("Permission denied");
        }

        _eventHandler = new iOSSpeechRecognitionEventHandler();
        _audioEngine = new AVAudioEngine();
        _speechRequest = new SFSpeechAudioBufferRecognitionRequest();

        var node = _audioEngine.InputNode;
        var recordingFormat = node.GetBusOutputFormat(new UIntPtr(0));
        node.InstallTapOnBus(new UIntPtr(0), 1024, recordingFormat, (buffer, _) =>
        {
            _speechRequest?.Append(buffer);
        });

        _audioEngine.Prepare();
        _audioEngine.StartAndReturnError(out var error);

        if (error is not null)
        {
            throw new ArgumentException("Error starting audio engine - " + error.LocalizedDescription);
        }

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

    public ValueTask DisposeAsync()
    {
        _audioEngine?.Dispose();
        _recognizer?.Dispose();
        _speechRequest?.Dispose();
        _recognitionTask?.Dispose();
        return ValueTask.CompletedTask;
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

    Task<string> ISfSpeechToText.StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        return StartRecognitionAsync(culture, recognitionResult, cancellationToken);
    }
}
