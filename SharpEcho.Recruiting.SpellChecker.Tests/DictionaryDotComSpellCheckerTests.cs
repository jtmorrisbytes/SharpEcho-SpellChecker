using NUnit.Framework;

using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    class DictionaryDotComSpellCheckerTests
    {
        private ISpellChecker SpellChecker;

        [TestFixtureSetUp]
        public void TestFixureSetUp()
        {
            SpellChecker = new DictionaryDotComSpellChecker();
        }

        [Test]
        public void Check_That_SharpEcho_Is_Misspelled()
        {
            // implement this test
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            // implement this test
        }
    }
}
