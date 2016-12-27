using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Mocks
{
    class MockTextDisplay : ITextDisplay
    {
        public string Text
        {
            get;
            private set;
        }

        public MockTextDisplay()
        {
            Text = string.Empty;
        }

        public void DisplayText()
        {
            DisplayText(string.Empty);
        }

        public void DisplayText(string text)
        {
            Text += text;
        }
    }
}
