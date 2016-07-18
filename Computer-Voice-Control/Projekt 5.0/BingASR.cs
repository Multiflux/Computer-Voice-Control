using Microsoft.ProjectOxford.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_5._0
{
    public class BingASR
    {
        private MicrophoneRecognitionClient micClient;

        private SpeechRecognitionMode Mode;

        private string DefaultLocale;

        private string SubscriptionKey;

        private TextWriter _output;

        public BingASR(SpeechRecognitionMode mode, string locale, string subscriptionKey, TextWriter output)
        {
            Mode = mode;
            DefaultLocale = locale;
            SubscriptionKey = subscriptionKey;
            CreateMicrophoneRecoClient();
            CreateMicrophoneRecoClient();
            _output = output;
        }

        public BingASR(SpeechRecognitionMode mode, string locale, string subscriptionKey)
        {
            Mode = mode;
            DefaultLocale = locale;
            SubscriptionKey = subscriptionKey;
            CreateMicrophoneRecoClient();
            CreateMicrophoneRecoClient();
        }

        public void recognizeFromMicrophone()
        {
            micClient.StartMicAndRecognition();
        }

        private void CreateMicrophoneRecoClient()
        {
            this.micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                this.Mode,
                this.DefaultLocale,
                this.SubscriptionKey,
                this.SubscriptionKey);

            // Event handlers for speech recognition results
            this.micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
            this.micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            if (this.Mode == SpeechRecognitionMode.ShortPhrase)
            {
                this.micClient.OnResponseReceived += this.OnMicShortPhraseResponseReceivedHandler;
            }
            else if (this.Mode == SpeechRecognitionMode.LongDictation)
            {
                this.micClient.OnResponseReceived += this.OnMicDictationResponseReceivedHandler;
            }

            this.micClient.OnConversationError += this.OnConversationErrorHandler;
        }

        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            WriteLine("--- Microphone status change received by OnMicrophoneStatus() ---");
            WriteLine("********* Microphone status: {0} *********", e.Recording);
            if (e.Recording)
            {
                WriteLine("Please start speaking.");
            }

            WriteLine();
        }

        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            this.WriteLine("--- Partial result received by OnPartialResponseReceivedHandler() ---");
            this.WriteLine("{0}", e.PartialResult);
            this.WriteLine();
        }

        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {

            this.WriteLine("--- OnMicShortPhraseResponseReceivedHandler ---");

            // we got the final result, so it we can end the mic reco.  No need to do this
            // for dataReco, since we already called endAudio() on it as soon as we were done
            // sending all the data.
            this.micClient.EndMicAndRecognition();

            this.WriteResponseResult(e);

        }

        private void OnMicDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            this.WriteLine("--- OnMicDictationResponseReceivedHandler ---");
            if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.EndOfDictation ||
                e.PhraseResponse.RecognitionStatus == RecognitionStatus.DictationEndSilenceTimeout)
            {
                // we got the final result, so it we can end the mic reco.  No need to do this
                // for dataReco, since we already called endAudio() on it as soon as we were done
                // sending all the data.
                this.micClient.EndMicAndRecognition();
            }

            this.WriteResponseResult(e);
        }

        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            this.WriteLine("--- Error received by OnConversationErrorHandler() ---");
            this.WriteLine("Error code: {0}", e.SpeechErrorCode.ToString());
            this.WriteLine("Error text: {0}", e.SpeechErrorText);
            this.WriteLine();
        }

        private void WriteLine()
        {
            if(_output != null)
            {
                _output.WriteLine(string.Empty);
            }
            else
            {
                Console.WriteLine(string.Empty);
            }

        }
        private void WriteLine(string v)
        {
            if (_output != null)
            {
                _output.WriteLine(v);
            }
            else
            {
                Console.WriteLine(v);
            }
        }
        private void WriteLine(string format, params object[] args)
        {
            var formattedStr = string.Format(format, args);
            Trace.WriteLine(formattedStr);
            if (_output != null)
            {
                _output.WriteLine(formattedStr);
            }
            else
            {
                Console.WriteLine(formattedStr);
            }
            //_logText.ScrollToEnd();
        }
        private void WriteResponseResult(SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.Results.Length == 0)
            {
                this.WriteLine("No phrase response is available.");
            }
            else
            {
                this.WriteLine("********* Final n-BEST Results *********");
                for (int i = 0; i < e.PhraseResponse.Results.Length; i++)
                {
                    this.WriteLine(
                        "[{0}] Confidence={1}, Text=\"{2}\"",
                        i,
                        e.PhraseResponse.Results[i].Confidence,
                        e.PhraseResponse.Results[i].DisplayText);
                }

                this.WriteLine();
            }
        }
    }
}
