using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using WordStatistics.Core;
using System.Linq;
using System.Xml.Linq;

namespace WordStatistics.PdicXml
{
    public class PdicXmlWordDictionary : IWordDictionary
    {
        private PdicDictionary? _pdic;

        public IEnumerable<string> Words {
            get
            {
                if (_pdic is null)
                {
                    return new List<string>();
                }
                else
                {
                    return _pdic.Records.Select(x => x.Word).ToList();
                }
            }
        }

        public void Read(string data)
        {
            XDocument document = XDocument.Parse(data);
            XElement pdic = document.Element("pdic");

            _pdic = new PdicDictionary
            {
                Records = pdic.Elements("record").Select(x => new PdicRecord
                {
                    Word = x.Element("word")?.Value ?? "",
                    Trans = x.Element("trans")?.Value ?? "",
                    Exp = x.Element("ex")?.Value ?? "",
                    Pron = x.Element("pron")?.Value ?? "",
                }).ToList(),
            };
        }
    }
}
