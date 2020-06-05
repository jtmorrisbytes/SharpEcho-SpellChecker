# SharpEcho SpellChecker
 Sharp Echo Recruiting Challenge

## Instructions
1. Ensure that you have an active connection to the internet
2. Install Visual Studio, Ensure .Net is selected before installing
3. Clone this repository, and open the .sln file in Visual Studio
4. Unless you are running tests, there should not be any external dependencies.
   Click on the start button to compile and the app in debug mode
5. When the application starts up, It will ask you to enter a sentence. The application makes best effort to be case-insensitive,
   search dictionary.com to check for misspellings, and check the english -IE- grammar rule.
6. The application will print "No misspellings found" on success and list "misspelled words: '<word1>', '<word2>'..." when
   it finds misspelled words.
### Notes
*  If the application fails to look up dictionary.com, the application will assume that the word is spelled
   incorrectly instead of crashing to ensure that it will always try every word.
*  The application will attempt to filter out "!", "," ,".", ";" at the end of each word.
*  The application will assume the words are spelled incorrectly if they contain the above punctuation
   or most special characters at the beginning and middle of each word. 
