using NUnit.Framework;

using SharpEcho.Recruiting.SpellChecker.Contracts;
using SharpEcho.Recruiting.SpellChecker.Core;

namespace SharpEcho.Recruiting.SpellChecker.Tests
{
    [TestFixture]
    class MnemonicSpellCheckerIBeforeETests
    {
        private ISpellChecker SpellChecker;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            SpellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            // implement this test
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            // implement this test
        }      
    }
}
