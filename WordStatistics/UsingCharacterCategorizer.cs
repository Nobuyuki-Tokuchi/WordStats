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
    public class UsingCharactersCategorizer : IWordCategorizer
    {
        public string Name { get; }
        public string? Description { get; }

        private DefaultCategorizerSettings _settings;
        private readonly JsonSerializerOptions _options;

        public UsingCharactersCategorizer()
        {
            Name = "Using Character Categorizer";
            _settings = new DefaultCategorizerSettings();

            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        public IList<WordResult> Execute(IWordDictionary dictionary)
        {
            var result = new WordResult("Using Character");
            var nospaceResult = new WordResult("Using Character (without word including space)");
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
                var pairs = word.Aggregate(new Dictionary<char, int>(), (acc, x) =>
                {
                    if(acc.ContainsKey(x))
                    {
                        acc[x]++;
                    }
                    else
                    {
                        acc[x] = 1;
                    }
                    return acc;
                });

                var nospace = !word.Any(x => char.IsWhiteSpace(x));
                foreach (var pair in pairs)
                {
                    var key = pair.Key.ToString();
                    if (_settings.IncludeSpace != "no")
                    {
                        result.AddCount(key, pair.Value);
                    }

                    if (_settings.IncludeSpace != "yes" && nospace)
                    {
                        nospaceResult.AddCount(key, pair.Value);
                    }
                }
            }

            return list;
        }

        public void ReadSettingFile(string path)
        {
            _settings = JsonSerializer.Deserialize<DefaultCategorizerSettings>(File.ReadAllText(path), _options);
        }
    }
}
