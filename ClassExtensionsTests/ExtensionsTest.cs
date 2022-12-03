using ClassExtensions;
using TestHelper;

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

    [Test]
    public void EqualJsonCheckTest_NoException()
    {
        var a = new { Amount = 108, Message = "Hello" };
        var b = new { Amount = 108, Message = "Hello" };

        Assert.DoesNotThrow(() => a.EqualJsonCheck(b));
    }

    [Test]
    public void EqualJsonCheckTest_NoException_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MaxValue;

        Assert.DoesNotThrow(() => a.EqualJsonCheck(b));
    }

    [Test]
    public void EqualJsonCheckTest_ExceptionThrown()
    {
        var a = new { Amount = 108, Message = "Hella" };
        var b = new { Amount = 108, Message = "Hello" };
        var expected = @"{""Amount"":108,""Message"":""Hella""} not equal to {""Amount"":108,""Message"":""Hello""}";

        var exception = Assert.Throws<Exception>(() => a.EqualJsonCheck(b));
        StringAssert.Contains(expected, exception!.Message);
    }

    [Test]
    public void EqualJsonCheckTest_ExceptionThrown_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MinValue;

        var expected = @"""9999-12-31T23:59:59.9999999"" not equal to ""0001-01-01T00:00:00""";

        var exception = Assert.Throws<Exception>(() => a.EqualJsonCheck(b));
        StringAssert.Contains(expected, exception!.Message);
    }

    [Test]
    [TestCase("Os")]
    [TestCase("Username")]
    [TestCase("Temp")]
    public void ExpandEnvTest(string name)
    {
        Assert.AreEqual(name.ExpandEnv(), Environment.ExpandEnvironmentVariables(name));
    }

    [Test]
    public void IsEmptyTest_ReturnsTrue()
    {
        Assert.IsTrue("".IsEmpty(), "'' was not empty");
    }

    [Test]
    public void IsEmptyTest_ReturnsFalse()
    {
        var text = Helper.GenerateRandomString();
        Assert.IsFalse(text.IsEmpty(), $"'{text}' was not empty");
    }

    [Test]
    public void IsNotEmptyTest_ReturnsTrue()
    {
        Assert.IsFalse("".IsNotEmpty(), "'' was not empty");
    }

    [Test]
    public void IsNotEmptyTest_ReturnsFalse()
    {
        var text = Helper.GenerateRandomString();
        Assert.IsTrue(text.IsNotEmpty(), $"'{text}' was not empty");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void IsNullOrEmptyTest_ReturnsTrue(string value)
    {
        Assert.IsTrue(value.IsNullOrEmpty(), $"'{value}' was not null or empty");
    }

    [Test]
    public void IsNullOrEmptyTest_ReturnsFalse()
    {
        var text = Helper.GenerateRandomString();
        Assert.IsFalse(text.IsNullOrEmpty(), "'{text}' was null or empty");
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void IsNotNullOrEmptyTest_ReturnsTrue(string value)
    {
        Assert.IsFalse(value.IsNotNullOrEmpty(), $"'{value}' was null or empty");
    }

    [Test]
    public void IsNotNullOrEmptyTest_ReturnsFalse()
    {
        var text = Helper.GenerateRandomString();
        Assert.IsTrue(text.IsNotNullOrEmpty(), "'{text}' was not null or empty");
    }

}