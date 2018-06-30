using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryTestApp
{
    public class AntonymMetadata
    {
        public string provider { get; set; }
    }

    public class Antonym
    {
        public string id { get; set; }
        public string language { get; set; }
        public string text { get; set; }
    }

    public class AntonymExample
    {
        public string text { get; set; }
    }

    public class AntonymSubsens
    {
        public string id { get; set; }
        public List<string> registers { get; set; }
        public List<string> regions { get; set; }
    }

    public class AntonymSens
    {
        public List<Antonym> antonyms { get; set; }
        public List<AntonymExample> examples { get; set; }
        public string id { get; set; }
        public List<AntonymSubsens> subsenses { get; set; }
    }

    public class AntonymEntry
    {
        public string homographNumber { get; set; }
        public List<AntonymSens> senses { get; set; }
    }

    public class AntonymLexicalEntry
    {
        public List<AntonymEntry> entries { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public string text { get; set; }
    }

    public class AntonymResult
    {
        public string id { get; set; }
        public string language { get; set; }
        public List<AntonymLexicalEntry> lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class AntonymRootObj
    {
        public AntonymMetadata metadata { get; set; }
        public List<AntonymResult> results { get; set; }
    }
}
