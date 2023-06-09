﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        private static string defaultDirectory = "..\\..\\..\\dict\\";
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
           
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "help")
                {
                    Console.WriteLine("quit - quits the application");
                    Console.WriteLine("help - displays this help screen");
                    Console.WriteLine("load - loads dictionary from a given file or default if not given");
                    Console.WriteLine("list - displays the whole dictionary");
                    Console.WriteLine("new - add a new glossary");
                    Console.WriteLine("delete - delets a glossary");
                    Console.WriteLine("translate - translates a given word");
                }
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        loadGloss(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        loadGloss(defaultFile);
                    }
                }
                else if (command == "list")
                {
                    foreach (SweEngGloss gloss in dictionary) //FIXME check that a list excists
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}"); 
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        try
                        {
                            dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Can not add new glossary when no dictionary is loaded");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string sweedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWord = Console.ReadLine();
                        try
                        {
                            dictionary.Add(new SweEngGloss(sweedishWord, englishWord)); //FIXME check words are not null
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Can not add new glossary when no dictionary is loaded");
                        }

                    } else
                    {
                        Console.WriteLine("Wrong number of arguments");
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        DeleteGloss(argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedishWord = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string englishWord = Console.ReadLine();
                        DeleteGloss(swedishWord, englishWord);
                    }
                    else
                    {
                        Console.WriteLine("Wrong number of arguments");
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        TranslateWord(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string word = Console.ReadLine();
                        TranslateWord(word);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void TranslateWord(string argument)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == argument)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == argument)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void DeleteGloss(string swedishWord, string englishWord)
        {
            int index = -1;
            try
            {
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == swedishWord && gloss.word_eng == englishWord)
                        index = i;
                }
            } catch (NullReferenceException)
            {
                Console.WriteLine("Can not delete when no list is loaded");
                return;
            }

            if (index != -1)
            {
                dictionary.RemoveAt(index);
            } else
            {
                Console.WriteLine("Could not delete " + swedishWord + ", " + englishWord);
            }
           
        }

        private static void loadGloss(string argument)
        {
            try
            {
                using (StreamReader sr = new StreamReader(defaultDirectory + argument))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
    }
}