using ClassExtensions;

namespace ClassExtensionsTest;

public class StringExtensionsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("ABCDEF")]
    [TestCase("Abcdef")]
    [TestCase("aBcDeF")]
    public void ToRandomCaseTest(string text)
    {
        Assert.AreNotEqual(text.ToRandomCase(), text.ToRandomCase());
    }
}