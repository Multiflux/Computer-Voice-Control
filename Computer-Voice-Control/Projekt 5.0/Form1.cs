using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using iPlugin;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.ProjectOxford.SpeechRecognition;
using System.Diagnostics;

namespace Projekt_5._0
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            foreach (var e in Settings.Instance.tupl)
            {
                Lbox_befehle.Items.Add(e.Item2);
            }

            // Instantiate the writer
            _writer = new TextBoxStreamWriter(richTextBox1);
            // Redirect the out Console stream
            Console.SetOut(_writer);

            Console.WriteLine("Now redirecting output to the text box");
            this.WindowState = FormWindowState.Minimized;
            CreateMicrophoneRecoClient();

            sps = new Sprachsteuerung(this, micClient);
        }

        TextWriter _writer = null;

        Sprachsteuerung sps = null;


        private void ausToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.spracherkennung_ein == true)
            {
                try
                {
                    sps.spracherkennung.RecognizeAsyncCancel(); //Spracherkennung Pausieren
                    Settings.Instance.spracherkennung_ein = false; //Variable auf false setzten
                    this.ausToolStripMenuItem.Enabled = false;
                    this.anToolStripMenuItem.Enabled = true;
                }
                catch
                {

                }
            }
        }


        private void anToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sps.spracherkennung_init();
            this.ausToolStripMenuItem.Enabled = true;
            this.anToolStripMenuItem.Enabled = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Visible = false;
            }

        }

        private void NotifyIcon_Sps_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = 0;
        }

        public void onRecognitionForm(string utterance)
        {
            this.richTextBox1.Text = utterance;
        }
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }

        private MicrophoneRecognitionClient micClient;

        private SpeechRecognitionMode Mode = SpeechRecognitionMode.ShortPhrase;

        private string DefaultLocale = "en-US";

        private string SubscriptionKey = "f4f6a3fd6d784810a4e37b4803719928";

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

        public delegate void MyDelegate();
        

        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                WriteLine("--- Microphone status change received by OnMicrophoneStatus() ---");
                WriteLine("********* Microphone status: {0} *********", e.Recording);
                if (e.Recording)
                {
                    WriteLine("Please start speaking.");
                }
                WriteLine();
            }));
        }

        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.WriteLine("--- Partial result received by OnPartialResponseReceivedHandler() ---");
                this.WriteLine("{0}", e.PartialResult);
                this.WriteLine();
            }));
        }

        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.WriteLine("--- OnMicShortPhraseResponseReceivedHandler ---");

                // we got the final result, so it we can end the mic reco.  No need to do this
                // for dataReco, since we already called endAudio() on it as soon as we were done
                // sending all the data.
                this.micClient.EndMicAndRecognition();

                this.WriteResponseResult(e);
            }));

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
                this.Invoke(new Action(() =>
                {
                    this.micClient.EndMicAndRecognition();
                }));
            }

            this.WriteResponseResult(e);
        }

        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.WriteLine("--- Error received by OnConversationErrorHandler() ---");
                this.WriteLine("Error code: {0}", e.SpeechErrorCode.ToString());
                this.WriteLine("Error text: {0}", e.SpeechErrorText);
                this.WriteLine();
                Settings.Instance.spracherkennung_ein = true;
            }));
        }

        private void WriteLine()
        {
            this.richTextBox1.AppendText(string.Empty + "\n");
        }
        //private void WriteLine(string v)
        //{
        //    this.richTextBox1.AppendText(v);
        //}
        private void WriteLine(string format, params object[] args)
        {
            var formattedStr = string.Format(format, args);
            Trace.WriteLine(formattedStr);
            this.Invoke(new Action(() =>
            {
                this.richTextBox1.AppendText(formattedStr + "\n");
            }));
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
            Settings.Instance.spracherkennung_ein = true;
        }
    }
}
