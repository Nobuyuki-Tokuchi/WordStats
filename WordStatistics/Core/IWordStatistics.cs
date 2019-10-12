using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordStatistics.Core
{
    public interface IWordStatistics
    {
        public IWordDictionary? Dictionary { get; set; }
        public SortOrder Order { get; set; }

        public void Execute(IEnumerable<IWordCategorizer> categorizers);
        public void Execute(params IWordCategorizer[] categorizers)
        {
            Execute(categorizers.ToList());
        }
    }
}
