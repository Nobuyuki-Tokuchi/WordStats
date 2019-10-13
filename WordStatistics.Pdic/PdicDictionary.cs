using System;
using System.Collections.Generic;

namespace WordStatistics.PdicXml
{
    public class PdicDictionary
    {
        public string Name { get; set; }
        public IList<PdicRecord> Records { get; set; }

        public PdicDictionary()
        {
            Name = "";
            Records = new List<PdicRecord>();
        }
    }

    public class PdicRecord
    {
        public string Word { get; set; }
        public string Trans { get; set; }
        public string Exp { get; set; }
        public string Level { get; set; }
        public string Memory { get; set; }
        public string Modify { get; set; }
        public string Pron { get; set; }
        public string FileLink { get; set; }

        public PdicRecord()
        {
            Word = "";
            Trans = "";
            Pron = "";
            Exp = "";
            Level = "";
            Memory = "";
            Modify = "";
            FileLink = "";
        }
    }
}
