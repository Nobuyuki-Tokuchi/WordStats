using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordStatistics.Core;

namespace WordStatistics
{
    public class DefaultStatistics : IWordStatistics
    {
        public IWordDictionary? Dictionary { get; set; }
        public SortOrder Order { get; set; }

        public DefaultStatistics() {
            Order = SortOrder.KEY;
        }

        public void Execute(IEnumerable<IWordCategorizer> categorizers)
        {
            if(Dictionary is null)
            {
                Console.WriteLine("Not found Dictionary");
                return;
            }

            Console.WriteLine("Result:");
            foreach (var category in categorizers)
            {
                Console.WriteLine("Categorizer Name: \"{0}\"", category.Name);

                IList<WordResult> results = category.Execute(Dictionary);
                foreach (var result in results)
                {
                    Console.WriteLine("{0}: (All Count: {1})", result.Name, result.AllCount);

                    var values = result.Values.ToList();
                    switch (Order)
                    {
                        case SortOrder.KEY:
                            values.Sort((x, y) => x.Key.CompareTo(y.Key));
                            break;
                        case SortOrder.VALUE:
                            values.Sort((x, y) => x.Value.CompareTo(y.Value));
                            break;
                        default:
                            values.Sort((x, y) => x.Key.CompareTo(y.Key));
                            break;
                    }

                    foreach (var value in values)
                    {
                        Console.WriteLine("\"{0}\" : {1}", value.Key, value.Value);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
