using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace DictionaryTestApp
{
    class Program
    {
        static void Main(string[] args)

        {
            Initiate();
        }

        static void Initiate()
        {
            Console.WriteLine("Type in a word to define");
            string wordToDefine = Console.ReadLine();
            GetDefinitionByRootObjectAsync(wordToDefine).Wait();
        }

        static async Task GetDefinitionByRootObjectAsync(string prWord)
        {
            try
            {
                RootObject currentResult = await TestServiceClient.GetRootObject(prWord);
                Console.WriteLine();
                Console.WriteLine("Word: " + currentResult.results[0].word);
                Console.WriteLine("Number of Definitions: " + currentResult.results[0].lexicalEntries[0].entries[0].senses.Count);
                for (int i = 0; i < currentResult.results[0].lexicalEntries[0].entries[0].senses.Count; i++)
                {
                    Console.WriteLine(string.Format("- {0}", currentResult.results[0].lexicalEntries[0].entries[0].senses[i].definitions[0]));
                }
                Console.WriteLine("");
                Initiate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
                Initiate();
            }
        }
    }
}
