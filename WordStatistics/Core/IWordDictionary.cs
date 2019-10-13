using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordStatistics.Core
{
    public interface IWordDictionary
    {
        IEnumerable<string> Words { get; }

        void Read(string data);

        public void ReadFile(string path)
        {
            Read(File.ReadAllText(path));
        }
    }
}
