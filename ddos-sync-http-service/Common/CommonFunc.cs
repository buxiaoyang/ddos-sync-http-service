using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetapp.Common
{
    public class CommonFunc
    {
        public static string escapePath(string input)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                input = input.Replace(c.ToString(), "");
            }
            return input;
        }
    }
}
