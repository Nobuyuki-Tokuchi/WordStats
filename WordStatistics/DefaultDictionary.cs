using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WordStatistics.Core;

namespace WordStatistics
{
    public class DefaultDictionary : IWordDictionary
    {
        public IEnumerable<string> Words { get; set; }

        public DefaultDictionary()
        {
            Words = new List<string>();
        }

        public void Read(string data)
        {
            Words = new List<string>(data.Split(SPLIT_STRING_LIST, StringSplitOptions.RemoveEmptyEntries));
        }

        public void ReadFile(string path)
        {
            Read(File.ReadAllText(path));
        }

        private static readonly string[] SPLIT_STRING_LIST = new string[] { "\r\n", "\r", "\n" };
}
}
