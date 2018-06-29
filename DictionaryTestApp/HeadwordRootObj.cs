using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTestApp
{
    public class HeadwordResult
    {
        public string matchString { get; set; }
        public string id { get; set; }
        public string word { get; set; }
        public double score { get; set; }
        public string matchType { get; set; }
        public string inflection_id { get; set; }
        public string region { get; set; }
    }

    public class HeadwordMetadata
    {
        public int limit { get; set; }
        public string sourceLanguage { get; set; }
        public int total { get; set; }
        public string provider { get; set; }
        public int offset { get; set; }
    }

    public class HeadwordRootObj
    {
        public List<HeadwordResult> results { get; set; }
        public HeadwordMetadata metadata { get; set; }
    }
}
