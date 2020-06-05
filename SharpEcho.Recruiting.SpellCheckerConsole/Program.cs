using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;

namespace SharpEcho.Recruiting.SpellCheckerConsole
{
    /// <summary>
    /// Thank you for your interest in a position at SharpEcho.  The following are the "requirements" for this project:
    /// 
    /// 1. Implent Main() below so that a user can input a sentence.  Each word in that
    ///    sentence will be evaluated with the SpellChecker, which returns true for a word
    ///    that is spelled correctly and false for a word that is spelled incorrectly.  Display
    ///    out each *distnict* word that is misspelled.  That is, if a user uses the same misspelled
    ///    word more than once, simply output that word one time.
    ///    
    ///    Example:
    ///    Please enter a sentence: Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.
    ///    Misspelled words: Salley seashellss
    ///    
    /// 2. The concrete implementation of SpellChecker depends on two other implementations of ISpellChecker, DictionaryDotComSpellChecker
    ///    and MnemonicSpellCheckerIBeforeE.  You will need to implement those classes.  See those classes for details.
    ///    
    /// 3. There are covering unit tests in the SharpEcho.Recruiting.SpellChecker.Tests library that should be implemented as well.
    /// </summary>
    class Program
    {
        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Please enter a sentence: ");
            var sentence = Console.ReadLine();

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled

            // use this spellChecker to evaluate the words
            var spellChecker = new SpellChecker.Core.SpellChecker
                (
                    new ISpellChecker[]
                    {
                        //new MnemonicSpellCheckerIBeforeE(),
                        new DictionaryDotComSpellChecker(),
                    }
                );

            var words = FilterInput(sentence);
            var mispelledWords = new List<String>();
            foreach (string word in words)
            {

                if (spellChecker.Check(word) == false)
                {
                    mispelledWords.Add(word);
                }
            }

            // replace puntuaction with empty string when string ends with punctuation , to prevent false negatives
            // when spell checking

            if(mispelledWords.Count() > 0)
            {
                Console.Write("mispelled words: ");
               foreach (string misspelledWord in mispelledWords)
               {
                   Console.Write("'" + misspelledWord + "' ");
               }
                Console.Write("\r\n");
            }
            else
            {
                Console.WriteLine("No misspelled words.");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        static List<String> FilterInput(string input)
        {
            // trim the start and end of the sentence, since we are splitting by spaces 
            input = input.Trim();
            /* It was not implied that sentence structure was important here. It seems
             * that only the words were important.
             * in this case, we should remove any unneccessary characters that may induce false 
             * negatives ex: "hello." == "hello" is false, but is spelled correctly.
             */

            // since 'words' are logically seperated by spaces, split the sentence by spaces
            Dictionary<String, bool> seenWords = new Dictionary<string, bool>();
            var words = input.Split(' ').ToList();
            // filter the words that are strictly equal to each other
            for (var wordIndex = 0; wordIndex < words.Count; wordIndex++)
            {
                // compare them in lowercase since they are essentially the same word
                var seen = false;
                var word = words[wordIndex].ToLower();
                //Console.WriteLine("filtering words: word'" + word + "'");
                seenWords.TryGetValue(word, out seen);
                if (seen)
                {
                    words.RemoveAt(wordIndex);
                }
                else
                {
                    seenWords.Add(word, true);
                    // we dont care if the word is uppercase or lower case,
                    // since it will have the same meaning
                    words[wordIndex] = word;
                }
            }
            return words;
        }
    }
}
