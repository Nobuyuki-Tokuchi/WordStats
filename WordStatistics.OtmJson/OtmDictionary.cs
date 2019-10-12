using System;
using System.Collections.Generic;
using System.Text;

namespace WordStatistics.OtmJson
{
    public class OtmDictionary
    {
        public string? Name { get; set; }
        public IList<OtmWord> Words { get; set; }

        public OtmDictionary()
        {
            Words = new List<OtmWord>();
        }
    }

    public class OtmWord
    {
        public OtmEntry Entry { get; set; }
        public IList<OtmTranslation> Translations { get; set; }
        public IList<string> Tags { get; set; }
        public IList<OtmContent> Contents { get; set; }
        public IList<OtmVariation> Variations { get; set; }
        public IList<OtmRelation> Relations { get; set; }

        public OtmWord()
        {
            Entry = new OtmEntry();
            Translations = new List<OtmTranslation>();
            Tags = new List<string>();
            Contents = new List<OtmContent>();
            Variations = new List<OtmVariation>();
            Relations = new List<OtmRelation>();
        }
    }

    public class OtmEntry
    {
        public int Id { get; set; }
        public string Form { get; set; }

        public OtmEntry()
        {
            Id = 0;
            Form = "";
        }
    }

    public class OtmTranslation
    {
        public string Title { get; set; }
        public IList<string> Forms { get; set; }

        public OtmTranslation()
        {
            Title = "";
            Forms = new List<string>();
        }
    }

    public class OtmContent
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public OtmContent()
        {
            Title = "";
            Text = "";
        }
    }

    public class OtmRelation
    {
        public string Title { get; set; }
        public OtmEntry? Entry { get; set; }

        public OtmRelation()
        {
            Title = "";
            Entry = new OtmEntry();
        }
    }

    public class OtmVariation
    {
        public string Title { get; set; }
        public string Form { get; set; }

        public OtmVariation()
        {
            Title = "";
            Form = "";
        }
    }
}
