using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using WordStatistics.Core;

namespace WordStatistics
{
    public class LengthCategorizer : IWordCategorizer
    {
        public string Name { get; }
        public string? Description { get; }

        private CategorizerSetting _settings;
        private readonly JsonSerializerOptions _options;

        public LengthCategorizer()
        {
            Name = "Length Categorizer";
            _settings = new CategorizerSetting();

            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public IList<WordResult> Execute(IWordDictionary dictionary)
        {
            var result = new WordResult("Length");
            var nospaceResult = new WordResult("Length (without word including space)");
            var list = new List<WordResult>();

            if (_settings.IncludeSpace != "no")
            {
                list.Add(result);
            }

            if (_settings.IncludeSpace != "yes")
            {
                list.Add(nospaceResult);
            }

            foreach (var word in dictionary.Words)
            {
                var lengthStr = word.Length.ToString("0000");

                if (_settings.IncludeSpace != "no")
                {
                    result.CountUp(lengthStr);
                }

                if (_settings.IncludeSpace != "yes" && !word.Any(x => char.IsWhiteSpace(x)))
                {
                    nospaceResult.CountUp(lengthStr);
                }
            }

            return list;
        }

        public void SetSetting(CategorizerSetting settings)
        {
            _settings = settings;
        }
    }
}
