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
        private static string[] words;

        static void Main(string[] args)
        {
            Initiate();
        }

        static void Initiate()
        {
            // Retrieve word list
            words = File.ReadAllLines(@"C:\Users\WILLIES\Desktop\words.txt");

            // Simple Menu
            Console.WriteLine("0- Definition by typed word \n1- Definition by random word");
            int character = Convert.ToInt16(Console.ReadLine());

            switch (character)
            {
                case 0:
                    DefinitionByWord();
                    break;

                case 1:
                    DefinitionByRandomWord();
                    break;
            }
        }

        static void DefinitionByWord()
        {
            Console.WriteLine("");
            Console.WriteLine("Type in a word to define");
            string wordToDefine = Console.ReadLine();
            GetDefinitionByRootObjectAsync(wordToDefine).Wait();
            Initiate();
        }

        static void DefinitionByRandomWord()
        {
            Random r = new Random();
            int randomNumber = r.Next(0, 400000);

            Console.WriteLine("");
            Console.WriteLine(string.Format("Definition of {0}:", words[randomNumber]));
            GetDefinitionByRootObjectAsync(words[randomNumber]).Wait();
            Initiate();
        }

        static async Task GetDefinitionByRootObjectAsync(string prWord)
        {
            try
            {
                RootObject currentResult = await ServiceClient.GetRootObject(prWord);
                Console.WriteLine();
                Console.WriteLine("Word: " + currentResult.results[0].word);
                Console.WriteLine("Number of Definitions: " + currentResult.results[0].lexicalEntries[0].entries[0].senses.Count);
                for (int i = 0; i < currentResult.results[0].lexicalEntries[0].entries[0].senses.Count; i++)
                {
                    Console.WriteLine(string.Format("- {0}", currentResult.results[0].lexicalEntries[0].entries[0].senses[i].definitions[0]));
                }
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
            }
        }
    }
}
