using System;
using System.Collections.Generic;
using System.Text;

namespace WordStatistics.Core
{
    public class WordResult
    {
        public string Name { get; }
        public long AllCount { get; private set; }
        public IDictionary<string, long> Values { get; }

        public WordResult(string name)
        {
            this.Name = name;
            this.AllCount = 0;
            this.Values = new Dictionary<string, long>();
        }

        public void CountUp(string result)
        {
            if (this.Values.ContainsKey(result))
            {
                this.Values[result]++;
            }
            else
            {
                this.Values[result] = 1;
            }
            this.AllCount++;
        }

        public void AddCount(string result, long count)
        {
            if (this.Values.ContainsKey(result))
            {
                this.Values[result] += count;
            }
            else
            {
                this.Values[result] = count;
            }
            this.AllCount += count;
        }
    }
}
