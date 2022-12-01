using ClassExtensions;

namespace ClassExtensionsTest;

public class ClassExtensionsTest
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