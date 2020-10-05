using System;
using System.Collections.Generic;
using System.Text;

namespace LoadFromSpreadsheet
{
    class ParseApplication
    {
        private readonly IParser _parser;
        public ParseApplication(IParser parser)
        {
            _parser = parser;
        }

        public void Run()
        {
            _parser.Parse();
        }
    }
}
