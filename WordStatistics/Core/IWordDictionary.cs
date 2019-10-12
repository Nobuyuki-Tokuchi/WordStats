using System;
using System.Collections.Generic;
using System.Text;

namespace WordStatistics.Core
{
    public interface IWordDictionary
    {
        IEnumerable<string> Words { get; }

        void Read(string data);
        void ReadFile(string path);
    }
}
