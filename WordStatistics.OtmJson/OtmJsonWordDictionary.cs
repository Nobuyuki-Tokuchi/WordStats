using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using WordStatistics.Core;
using System.Linq;

namespace WordStatistics.OtmJson
{
    public class OtmJsonWordDictionary : IWordDictionary
    {
        private OtmDictionary? _otm;
        private readonly JsonSerializerOptions _options;

        public IEnumerable<string> Words
        {
            get
            {
                if (_otm is null)
                {
                    return new List<string>();
                }
                else
                {
                    return _otm.Words.Select(x => x.Entry.Form).ToList();
                }
            }
        }

        public OtmJsonWordDictionary()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public void Read(string data)
        {
            _otm = JsonSerializer.Deserialize<OtmDictionary>(data, _options);
        }

        public void Read(ReadOnlySpan<byte> data)
        {
            _otm = JsonSerializer.Deserialize<OtmDictionary>(data, _options);
        }

        public void ReadFile(string path)
        {
            Read(File.ReadAllText(path));
        }
    }
}
