using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordStatistics.Core;

namespace WordStatistics
{
    public class DefaultDictionary : IWordDictionary
    {
        private readonly List<string> _words;

        public IEnumerable<string> Words
        {
            get
            {
                return _words;
            }
        }

        public DefaultDictionary()
        {
            _words = new List<string>();
        }

        public void Read(string data)
        {
            _words.Clear();
            _words.AddRange(data.Split(SPLIT_STRING_LIST, StringSplitOptions.RemoveEmptyEntries));
        }

        public void ReadFile(string path)
        {
            Read(File.ReadAllText(path));
        }

        private static readonly string[] SPLIT_STRING_LIST = new string[] { "\r\n", "\r", "\n" };
}
}
