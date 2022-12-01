using TestHelper;

namespace TestHelperTest;

public class HelperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GenerateRandomIntTest()
    {
        var result = Helper.GenerateRandomInt();

        var toCompare = Helper.GenerateRandomInt();
        if (toCompare == result) toCompare = Helper.GenerateRandomInt();

        Assert.IsTrue(result.IsInBetween(1, 9));
        Assert.AreNotEqual(toCompare, result);
    }

    [Test]
    public void GenerateRandomIntTest_WithRange()
    {
        var min = 100;
        var max = 100000;
        var results = Helper.GenerateRandomList(() => Helper.GenerateRandomInt(min, max), 100);
        results.ForEach(result =>
        {
            Assert.IsTrue(result.IsInBetween(min, max));
            Assert.AreNotEqual(Helper.GenerateRandomInt(), result);
        });
    }

    [Test]
    public void GenerateRandomStringTest_DefaultValueIsUsed()
    {
        var result = Helper.GenerateRandomString();
        StringAssert.IsMatch("[A-z]{10}", result);
    }

    [Test]
    public void GenerateRandomStringTest_SingleChar()
    {
        var count = 1;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_HundertChars()
    {
        var count = 100;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_RandomCount()
    {
        var count = Helper.GenerateRandomInt(1000, 5000);
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
        StringAssert.IsMatch("[a-z]", result);
    }

    [Test]
    public void GenerateRandomListTest_SingleEntry()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<string> { "Test" };
        var result = Helper.GenerateRandomList(() => "Test", 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_SingleEntry_DifferentType()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<int> { 1 };
        var result = Helper.GenerateRandomList(() => 1, 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_FiveEntries()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<string> { "ABC", "ABC", "ABC", "ABC", "ABC" };
        var result = Helper.GenerateRandomList(() => "ABC", 5);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_MultipleEntriesWithDifferentValues()
    {
        var count = 0;
        string GetAlphabet() => ((char)('A' + count++)).ToString();
        var expected = new List<string> { "A", "B", "C", "D", "E" };

        var result = Helper.GenerateRandomList(GetAlphabet, 5);

        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomBoolTest()
    {
        var randomBools = Helper.GenerateRandomList(Helper.GenerateRandomBool, 100);
        Assert.AreEqual(2, randomBools.Distinct().Count());
    }

    [Test]
    public void GetGenerateRandomDateTimeTest()
    {
        var result = Helper.GetGenerateRandomDateTime();

        Assert.AreNotEqual(DateTime.MinValue, result);
        Assert.IsTrue(result.Year.IsInBetween(1870, 2300));
        Assert.IsTrue(result.Month.IsInBetween(1, 12));
        Assert.IsTrue(result.Day.IsInBetween(1, 26));
        Assert.IsTrue(result.Hour.IsInBetween(0, 23));
        Assert.IsTrue(result.Minute.IsInBetween(0, 59));
        Assert.IsTrue(result.Second.IsInBetween(0, 59));
    }

    [Test]
    public void GenerateRandomStringGuidWithPrefixTest()
    {
        var prefix = Helper.GenerateRandomString();
        var result = Helper.GenerateRandomStringGuidWithPrefix(prefix);
        StringAssert.StartsWith(prefix, result);
        var isParsable = Guid.TryParse(result.Replace(prefix, ""), out var parsedGuid);
        Assert.IsTrue(isParsable, "Guid not parsable");
        Assert.AreNotEqual(Guid.Empty, parsedGuid);
    }
}