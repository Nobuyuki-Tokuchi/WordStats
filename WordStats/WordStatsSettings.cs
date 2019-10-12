using System;
using System.Collections.Generic;
using System.Text;

namespace WordStats
{
    public class WordStatsSettings
    {
        public UsingClassInfo? Dictionary { get; set; }
        public IList<UsingClassInfo>? Categorizers { get; set; }
        public UsingClassInfo? Statistics { get; set; }
        public string? Order { get; set; }
    }

    public class UsingClassInfo
    {
        public string? DllPath { get; set; }
        public string? ClassName { get; set; }
        public string? SettingPath { get; set; }
    }
}
