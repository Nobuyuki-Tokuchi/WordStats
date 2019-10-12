using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using WordStatistics;
using WordStatistics.Core;
using WordStatistics.OtmJson;

namespace WordStats
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("WordStats (options) file");
            }
            else if (args.Any(x => x == "--help" || x == "-h"))
            {
                Console.WriteLine("WordStats");
                Console.WriteLine("  WordStats (options) file");
                Console.WriteLine();
                Console.WriteLine("  options:");
                Console.WriteLine("    --help, --h: this message");
                Console.WriteLine("    @(fileName): setting file name");
            }
            else
            {
                string? filePath = null;
                string? settingFilePath = null;

                foreach (var item in args)
                {
                    if(item.StartsWith("@"))
                    {
                        settingFilePath = item.Substring(1);
                    }
                    else
                    {
                        filePath = item;
                    }
                }

                if(string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentException($"invalid dictionary name: {filePath}");
                }

                WordStatsSettings settings = GetSettings(settingFilePath);
                IWordDictionary dictionary = GetDictionary(settings.Dictionary);
                dictionary.ReadFile(filePath);

                IList<IWordCategorizer> categorizers = GetCategorizers(settings.Categorizers);
                IWordStatistics statistics = GetStatistics(settings.Statistics, settings.Order);
                statistics.Dictionary = dictionary;
                statistics.Execute(categorizers);
            }
        }

        private static WordStatsSettings GetSettings(string? settingFilePath)
        {
            WordStatsSettings settings;

            if (string.IsNullOrWhiteSpace(settingFilePath))
            {
                settings = new WordStatsSettings
                {
                    Dictionary = new UsingClassInfo
                    {
                        DllPath = "WordStatistics.dll",
                        ClassName = typeof(DefaultDictionary).FullName,
                    },
                    Categorizers = new List<UsingClassInfo>
                    {
                        new UsingClassInfo
                        {
                            DllPath = "WordStatistics.dll",
                            ClassName = typeof(LengthCategorizer).FullName,
                        },
                        new UsingClassInfo
                        {
                            DllPath = "WordStatistics.dll",
                            ClassName = typeof(UsingCharactersCategorizer).FullName,
                        },
                    },
                    Statistics = new UsingClassInfo
                    {
                        DllPath = "WordStatistics.dll",
                        ClassName = typeof(DefaultStatistics).FullName,
                    },
                };
            }
            else
            {
                var option = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                settings = JsonSerializer.Deserialize<WordStatsSettings>(File.ReadAllText(settingFilePath), option);
            }

            return settings;
        }

        private static IWordDictionary GetDictionary(UsingClassInfo? dictionaryInfo)
        {
            if (dictionaryInfo is null)
            {
                return new DefaultDictionary();
            }
            else
            {
                return CreateInstance<IWordDictionary>(dictionaryInfo);
            }
        }

        private static IList<IWordCategorizer> GetCategorizers(IList<UsingClassInfo>? categorizersInfo)
        {
            if (categorizersInfo is null)
            {
                return new List<IWordCategorizer> {
                    new LengthCategorizer(),
                    new UsingCharactersCategorizer(),
                };
            }
            else
            {
                List<IWordCategorizer> list = new List<IWordCategorizer>();
                foreach (var info in categorizersInfo)
                {
                    var instance = CreateInstance<IWordCategorizer>(info);

                    if (!(info.SettingPath is null))
                    {
                        instance.ReadSettingFile(info.SettingPath);
                    }

                    list.Add(instance);
                }

                return list;
            }
        }

        private static IWordStatistics GetStatistics(UsingClassInfo? statisticsInfo, string? order)
        {
            IWordStatistics statistics;
            if (statisticsInfo is null)
            {
                statistics = new DefaultStatistics();
            }
            else
            {
                statistics = CreateInstance<IWordStatistics>(statisticsInfo);
            }

            if (order is null || order.ToLower() != "value")
            {
                order = "key";
            }

            statistics.Order = Enum.Parse<SortOrder>(order, true);
            return statistics;
        }

        private static T CreateInstance<T>(UsingClassInfo classInfo)
        {
            if (classInfo.DllPath is null)
            {
                throw new ArgumentNullException($"invalid option: (DllPath: {classInfo.DllPath}");
            }

            if (classInfo.ClassName is null)
            {
                throw new ArgumentNullException($"invalid option: (ClassName: {classInfo.ClassName}");
            }

            Assembly assembly = Assembly.LoadFrom(classInfo.DllPath);
            Type? type = assembly.GetType(classInfo.ClassName);

            if (type is null)
            {
                throw new ArgumentNullException($"invalid option: (ClassName: {classInfo.ClassName}");
            }

            object? instance = Activator.CreateInstance(type);
            if (instance is T)
            {
                return (T)instance;
            }
            else
            {
                throw new ArgumentNullException($"invalid option: (ClassName: {classInfo.ClassName}");
            }
        }
    }
}
