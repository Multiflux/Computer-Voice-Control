using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt_5._0
{
    public class TextBoxStreamWriter : TextWriter
    {

        RichTextBox _output = null;

        public TextBoxStreamWriter(RichTextBox output)
        {
            _output = output;
            _output.SelectionStart = _output.Text.Length;
            _output.ScrollToCaret();
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString()); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
