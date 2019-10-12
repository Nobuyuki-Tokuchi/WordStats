using System;
using System.Collections.Generic;
using System.Text;

namespace WordStatistics.Core
{
    public interface IWordCategorizer
    {
        public string Name { get; }
        public string? Description { get; }

        public IList<WordResult> Execute(IWordDictionary dictionary);
        public void ReadSettingFile(string path);
    }
}
