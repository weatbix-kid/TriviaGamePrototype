using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;


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
            words = File.ReadAllLines("words.txt");

            // Simple Menu
            Console.WriteLine("0- Definition by typed word \n1- Definition by random word \n2- Prototype Round: Phrase \n3- Prototype Round: Spellingbee \n4- Prototype Round: Opposite \n5- Prototype Round: Classic");
            int character = Convert.ToInt16(Console.ReadLine());

            switch (character)
            {
                case 0:
                    DefinitionByWord();
                    break;

                case 1:
                    DefinitionByRandomWord();
                    break;

                case 2:
                    RoundGuessFromPhrase();
                    break;

                case 3:
                    RoundSpellingBee();
                    break;

                case 4:
                    RoundGuessTheAnatonym();
                    break;

                case 5:
                    RoundClassic();
                    break;

                default:
                    Initiate();
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

        static void RoundGuessFromPhrase()
        {
            Console.WriteLine("");
            Console.WriteLine("Guess the word from the headword/phrase");

            string[] randomWord = new string[] { "stranger", "cat", "dinosaur", "person" };
            
            Random r = new Random();
            int randomNumber = r.Next(0, 3);
            
            GetPhraseByHeadwordRootObjectAsync(randomWord[randomNumber]).Wait();

            Console.WriteLine("");
            Console.WriteLine("Guess the word");
            string userInput = Console.ReadLine();

            if (userInput == randomWord[randomNumber])
                Console.WriteLine("Correct!");
            else
                Console.WriteLine("False :(");

            Console.ReadLine();
            Initiate();
        }

        static void RoundSpellingBee()
        {
            Console.WriteLine("");
            Console.WriteLine("Spell the word the word by sound");
            Console.ReadLine();
        }

        static void RoundGuessTheAnatonym()
        {
            Console.WriteLine("");
            Console.WriteLine("Guess the opposite");
            Console.ReadLine();
        }

        static void RoundClassic()
        {
            Console.WriteLine("");
            Console.WriteLine("Guess from top definition");
            Console.ReadLine();
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

        static async Task GetPhraseByHeadwordRootObjectAsync(string prWord)
        {
            try
            {
                HeadwordRootObj request = await ServiceClient.GetHeadwordRootObject(prWord);
                Console.WriteLine();

                StringBuilder censoredWord = new StringBuilder();
                foreach (char character in prWord)
                    censoredWord.Append("_ ");

                Console.WriteLine("Results: " + request.metadata.total);

                foreach (var result in request.results)
                {
                    StringBuilder phrase = new StringBuilder(result.word);
                    if (result.matchType == "headword")
                        phrase.Replace(prWord, Convert.ToString(censoredWord));
                    if (Convert.ToString(phrase) != prWord)
                        Console.WriteLine(phrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("");
            }
        }
    }
}
