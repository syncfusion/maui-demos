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

namespace SampleBrowser.Maui.AIAssistView
{
    /// <summary>
    /// Defines the contract for platform-specific speech-to-text recognition services.
    /// </summary>
    public interface ISfSpeechToText : IAsyncDisposable
    {
        /// <summary>
        /// Requests microphone permissions from the platform.
        /// </summary>
        /// <returns>True if permission granted, false otherwise.</returns>
        Task<bool> RequestAudioPermissionAsync();

        /// <summary>
        /// Starts listening for speech input.
        /// </summary>
        /// <param name="culture">The culture for recognition.</param>
        /// <param name="recognitionResult">Progress reporter for partial results.</param>
        /// <param name="cancellationToken">Cancellation token for stopping recognition.</param>
        /// <returns>The final recognized text.</returns>
        Task<string> StartRecognitionAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken);
    }
}