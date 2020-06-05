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
            Assert.IsTrue(SpellChecker.Check("priest"));
            Assert.IsTrue(SpellChecker.Check("fierce"));

            Assert.IsTrue(SpellChecker.Check("conceive"));
            Assert.IsTrue(SpellChecker.Check("deceit"));
        }
        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            // implement this test
            // assert true when spelled correctly

            Assert.IsFalse(SpellChecker.Check("concieve"));
            Assert.IsFalse(SpellChecker.Check("deciet"));
        }      
    }
}
