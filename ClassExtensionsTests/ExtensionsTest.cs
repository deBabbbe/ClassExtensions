using ClassExtensions;

namespace ClassExtensionsTest;

public class ExtensionsTest
{
    private static object[] _isNullCases =
    {
        null!,
        (null as string)!,
        (null as bool?)!,
        (null as DateTime?)!,
        (null as int?)!
    };

    private static object[] _isNotNullCases =
    {
        123,
        "ABC",
        true,
        DateTime.MinValue,
        Guid.Empty
    };

    [Test]
    [TestCase("ABCDEF")]
    [TestCase("Abcdef")]
    [TestCase("aBcDeF")]
    public void ToRandomCaseTest(string text)
    {
        Assert.AreNotEqual(text.ToRandomCase(), text.ToRandomCase());
    }

    [Test]
    [TestCaseSource(nameof(_isNullCases))]
    public void IsNullTest_ReturnsTrue(object @object)
    {
        Assert.IsTrue(@object.IsNull(), $"{@object} was not null");
    }

    [Test]
    [TestCaseSource(nameof(_isNotNullCases))]
    public void IsNullTest_ReturnsFalse(object @object)
    {
        Assert.IsFalse(@object.IsNull(), $"{@object} was null");
    }

    [Test]
    [TestCaseSource(nameof(_isNotNullCases))]
    public void IsNotNullTest_ReturnsTrue(object @object)
    {
        Assert.IsTrue(@object.IsNotNull(), $"{@object} was null");
    }

    [Test]
    [TestCaseSource(nameof(_isNullCases))]
    public void IsNotNullTest_ReturnsFalse(object @object)
    {
        Assert.IsFalse(@object.IsNotNull(), $"{@object} was not null");
    }

    [Test]
    public void ForAllTest()
    {
        var list = new List<string> { "A", "b", "C", "d" };
        var expectedString = "!A!!b!!C!!d!";
        var stringToCheck = "";
        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.AreEqual(expectedString, stringToCheck);
    }

    [Test]
    public void ForAllTest_DifferentType()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        var expectedString = "!1!!2!!3!!4!!5!";
        var stringToCheck = "";
        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.AreEqual(expectedString, stringToCheck);
    }


    [Test]
    public void ForAllTest_EnumerationNull_NoException()
    {
        IEnumerable<object> list = null!;
        Assert.DoesNotThrow(() => list.ForAll(entry => "".ToArray()));
    }

    [Test]
    public void ForAllTest_ActionNull_NoException()
    {
        var list = new List<string> { "A", "b", "C", "d" };
        Assert.DoesNotThrow(() => list.ForAll(null!));
    }

    [Test]
    public void TimesTest()
    {
        var resultString = "";
        var expectedString = "0,1,2,3,4,5,6,7,8,9,";

        10.Times(t => resultString += $"{t},");

        Assert.AreEqual(expectedString, resultString);
    }

    [Test]
    public void TimesTest_ActionNull_DoesNotThrowException()
    {
        Assert.DoesNotThrow(() => 10.Times(null!));
    }
}