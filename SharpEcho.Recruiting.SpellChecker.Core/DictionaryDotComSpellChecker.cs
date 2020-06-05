using SharpEcho.Recruiting.SpellChecker.Contracts;

using System;
//using System.IO;
using System.Net;
using System.Web;

namespace SharpEcho.Recruiting.SpellChecker.Core
{
    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked
    /// 
    /// Example: http://dictionary.reference.com/browse/SharpEcho would lookup the word SharpEcho
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not
    /// </summary>
    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        
        const string API_URL = "https://www.dictionary.com/browse/";
        const int SUCCESS = 200;
        const int REDIRECT = 301;
        string[] punctuation = { "!", ".", ",", ";" };
        // RFC_RESERVED_CHARACTERS according to https://en.wikipedia.org/wiki/Percent-encoding
        string[] RFC_RESERVED_CHARACTERS = new string[] { 
            "!", "*",
            "'", "(",
            ")", ";",
            ":", "@",
            "&", "=", 
            "+",  ",",
            "/", "?",
            "#",  "[",
            "]",
            };
        //string[] RFC_ENCODED_SEQUENCES = new string[] { "%21", "%2A", "%27", "%28", "%29", "%3B", "%40", "%3A", "%26", "%3D", "%2B", "%2C", "%2F", "%3F", "%23", "%5B", "%5D" };
        public DictionaryDotComSpellChecker()
        {
            // This trusts all certificates. This should NEVER be done in production.
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }
        
        public bool Check(string word)
        {

            //Console.WriteLine("checking word: " + word);
            // attempting to place invalid characters in the url
            // causes a web exception
            foreach (string punct in this.punctuation)
            {
                // if the word ends with the punctuation, 
                // then remove it, because it is most likely,
                // the end of the sentence.
                if (word.EndsWith(punct))
                {
                   word = word.Substring(0, word.Length - 1);
                }
                //While it is possible to EncodeUri the basic punctuation,
                // any word that contains punctuation in the middle of the
                // word is misspelled anyways. If the program was allowed
                // to send a request that contains those characters, 
                // the request could fail
                if (word.Contains(punct))
                { 
                    return false;
                }



            }
            // The SpellChecker should have checked for all valid punctuation at this point. 
            // if this spellchecker encounters any reserved characters, or unreserved characters
            // that could cause the request to fail, then the spellchecker should return false
            foreach (string reservedCharacter in RFC_RESERVED_CHARACTERS)
            {
                if (word.Contains(reservedCharacter))
                {
                    return false;
                }
            }
            WebRequest dictionaryCheck = WebRequest.Create(API_URL + word);
            //HttpsWebResponse response = (HttpsWebResponse)dictionaryCheck.GetResponse();
            // this type cast could fail, even if unlikely.
            try
            {

            HttpWebResponse response = (HttpWebResponse)dictionaryCheck.GetResponse();
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    response.Close();
                    return true;
                }
                else if ((response.StatusCode.Equals(HttpStatusCode.Moved) && response.Headers.Get("location").Contains("misspelling"))
                          || response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    response.Close();
                    return false;
                }
                else
                {
                    Console.WriteLine("got status code " + response.StatusCode.ToString());
                    response.Close();
                    return false;

                }

            }
            /* Console error statements would probably be best kept behind a debug flag,
             * or environment variable during production. catching them will allow the program
             * to continue even in case of a failure
             */
            catch (InvalidCastException)
            {
                Console.Error.WriteLine("An InvalidCastException occurred while trying to check word");
                return false;
            }
            catch (WebException webExc)
            {
                // return false if any exception occurred;
                if(webExc.Status.Equals(HttpStatusCode.NotFound))
                {
                    Console.WriteLine("Word not found on dictionary.com");
                }
                
                return false;
            }
            finally
            {
                dictionaryCheck.Abort();
            }
            

            //throw new System.NotImplementedException();

        }
        //public string EncodeUri(string theString)
        //{
        //    // blindly replace puncuation and reserved characters
        //    // with the rfc escape codes.
        //    // this function assumes that the programmer has already cleaned up the
        //    // data other than encoding the URI.
        //    for(short reservedCharPairIndex = 0; reservedCharPairIndex < RFC_RESERVED_CHARACTERS.GetLength(0); reservedCharPairIndex++){
        //        theString.Replace(RFC_RESERVED_CHARACTERS[reservedCharPairIndex,0], RFC_RESERVED_CHARACTERS[reservedCharPairIndex, 1]);
        //    }
        //    return theString;
        //}
    }
}
