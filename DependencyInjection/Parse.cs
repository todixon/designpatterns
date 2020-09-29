using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class Parse : IParse
    {
        IDemoService _demo;
        IDemoService1 _demo1;

        public Parse(IDemoService demo, IDemoService1 demo1)
        {
            _demo = demo;
            _demo1 = demo1;
        }

        public string DoParse()
        {
            return _demo.DooWah();
        }

    }
}
