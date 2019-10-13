using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using WordStatistics.Core;

namespace WordStatistics
{
    public class ConcatCharacterCategorizer : IWordCategorizer
    {
        public string Name { get; }
        public string? Description { get; }

        private CategorizerSetting _settings;
        private readonly JsonSerializerOptions _options;

        
        public ConcatCharacterCategorizer()
        {
            Name = "Concat Character Categorizer";
            _settings = new CategorizerSetting();

            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }


        public IList<WordResult> Execute(IWordDictionary dictionary)
        {
            var length = 2;
            if (!(_settings.Length is null) && _settings.Length >= 2)
            {
                length = _settings.Length.Value;
            } 

            var result = new WordResult("Concat Character");
            var nospaceResult = new WordResult("Concat Character (without word including space)");
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
                var nospace = !word.Any(x => char.IsWhiteSpace(x));

                for (int i = 0; i < (word.Length - length); i++)
                {
                    var substr = word.Substring(i, length);
                    if (_settings.IncludeSpace != "no")
                    {
                        result.CountUp(substr);
                    }

                    if (_settings.IncludeSpace != "yes" && nospace)
                    {
                        nospaceResult.CountUp(substr);
                    }
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
