using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class DemoService : IDemoService
    {
        public string DooWah()
        {
            return "Doowah";
        }
    }
}
