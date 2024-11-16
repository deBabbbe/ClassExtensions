using System.Collections;
using ClassExtensions;
using TestHelper;
using static System.Text.Json.JsonSerializer;

namespace ClassExtensionsTest;

public class ExtensionsTest
{
    private StringWriter _stringWriter;
    
    public ExtensionsTest() => _stringWriter = new StringWriter();

    [OneTimeSetUp]
    public void SetUp() => Console.SetOut(_stringWriter);

    [OneTimeTearDown]
    public void TearDown() => _stringWriter.Dispose();

    private static object[] _nullCases =
    {
        null!,
        (null as string)!,
        (null as bool?)!,
        (null as DateTime?)!,
        (null as int?)!
    };

    private static object[] _notNullCases =
    {
        Helper.GenerateRandomInt(),
        Helper.GenerateRandomString(),
        Helper.GenerateRandomBool(),
        Helper.GenerateRandomDateTime(),
        Guid.NewGuid()
    };

    private static object[] _nullOrEmptyValues =
    {
        null!,
        (null as string)!,
        (null as bool?)!,
        (null as DateTime?)!,
        (null as int?)!,
        new List<string>(),
        new int[]{},
        new List<bool>(),
    };

    private static object[] _emptyValues =
    {
        new List<string>(),
        new int[]{},
        new List<bool>(),
    };

    private static object[] _notNullOrEmptyValues =
    {
        Helper.GenerateRandomString(),
        Enumerable.Range(1,5)
    };

    [TestCaseSource(nameof(_nullCases))]
    public void IsNullTest_ReturnsTrue(object @object) =>
        Assert.That(@object.IsNull(), Is.True, $"{@object} was not null");

    [TestCaseSource(nameof(_notNullCases))]
    public void IsNullTest_ReturnsFalse(object @object) =>
        Assert.That(@object.IsNull(), Is.False, $"{@object} was null");

    [TestCaseSource(nameof(_notNullCases))]
    public void IsNotNullTest_ReturnsTrue(object @object) =>
        Assert.That(@object.IsNotNull(), Is.True, $"{@object} was null");

    [TestCaseSource(nameof(_nullCases))]
    public void IsNotNullTest_ReturnsFalse(object @object) =>
        Assert.That(@object.IsNotNull(), Is.False, $"{@object} was not null");

    [Test]
    public void ForAllTest()
    {
        var list = new List<string> { "A", "b", "C", "d" };
        const string expectedString = "!A!!b!!C!!d!";
        var stringToCheck = "";

        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.That(stringToCheck, Is.EqualTo(expectedString));
    }

    [Test]
    public void ForAllTest_DifferentType()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        const string expectedString = "!1!!2!!3!!4!!5!";
        var stringToCheck = "";

        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.That(stringToCheck, Is.EqualTo(expectedString));
    }

    [Test]
    public void ForAllTest_EnumerationNull_NoException()
    {
        const IEnumerable<object> list = null!;
        Assert.DoesNotThrow(() => list.ForAll(entry => string.Empty.ToArray()));
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
        const string expectedString = "0,1,2,3,4,5,6,7,8,9,";
        const int ten = 10;

        ten.Times(t => resultString += $"{t},");

        Assert.That(resultString, Is.EqualTo(expectedString));
    }

    [Test]
    public void TimesTest_ActionNull_DoesNotThrowException()
    {
        const int multiple = 10;
        Assert.DoesNotThrow(() => multiple.Times(null!));
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
        const string expected = @"{""Amount"":108,""Message"":""Hella""} not equal to {""Amount"":108,""Message"":""Hello""}";

        var exception = Assert.Throws<Exception>(() => a.EqualJsonCheck(b));
        Assert.That(exception.Message, Contains.Substring(expected));
        Assert.That(_stringWriter.ToString(), Is.EqualTo($"{Serialize(a)} not equal to {Serialize(b)}\r\n"));
    }

    [Test]
    public void EqualJsonCheckTest_ExceptionThrown_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MinValue;
        const string expected = @"""9999-12-31T23:59:59.9999999"" not equal to ""0001-01-01T00:00:00""";

        var exception = Assert.Throws<Exception>(() => a.EqualJsonCheck(b));

        Assert.That(exception.Message, Contains.Substring(expected));
    }

    [Test]
    public void IsEqualJsonTest_ReturnsTrue()
    {
        var a = new { Amount = 108, Message = "Hello" };
        var b = new { Amount = 108, Message = "Hello" };

        Assert.That(a.IsEqualJson(b), Is.True, "Objects are not equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsTrue_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MaxValue;

        Assert.That(a.IsEqualJson(b), Is.True, "Objects are not equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsFalse()
    {
        var a = new { Amount = 108, Message = "Hella" };
        var b = new { Amount = 108, Message = "Hello" };

        Assert.That(a.IsEqualJson(b), Is.False, "Objects are equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsFalse_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MinValue;

        Assert.That(a.IsEqualJson(b), Is.False, "Objects are equal");
    }

    [TestCase("Os")]
    [TestCase("Username")]
    [TestCase("Temp")]
    public void ExpandEnvTest(string name) =>
        Assert.That(Environment.ExpandEnvironmentVariables(name), Is.EqualTo(name.ExpandEnv()));

    [TestCaseSource(nameof(_emptyValues))]
    public void IsEmptyTest_ReturnsTrue(IEnumerable values) =>
        Assert.That(values.IsEmpty(), Is.True, "'' was not empty");

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsEmptyTest_ReturnsFalse(IEnumerable values) =>
        Assert.That(values.IsEmpty(), Is.False, $"'{values}' was not empty");

    [TestCaseSource(nameof(_nullOrEmptyValues))]
    public void IsNotEmptyTest_ReturnsFalse(IEnumerable values) =>
        Assert.That("".IsNotEmpty(), Is.False, "'' was not empty");

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsNotEmptyTest_ReturnsTrue(IEnumerable values) =>
        Assert.That(values.IsNotEmpty(), Is.True, $"'{values}' was not empty");

    [TestCaseSource(nameof(_nullOrEmptyValues))]
    public void IsNullOrEmptyTest_ReturnsTrue(IEnumerable values) =>
        Assert.That(values.IsNullOrEmpty(), Is.True, $"'{values}' was not null or empty");

    [TestCaseSource(nameof(_notNullOrEmptyValues))]

    public void IsNullOrEmptyTest_ReturnsFalse(IEnumerable values) =>
        Assert.That(values.IsNullOrEmpty(), Is.False, $"'{values}' was null or empty");

    [TestCaseSource(nameof(_nullOrEmptyValues))]

    public void IsNotNullOrEmptyTest_ReturnsTrue(IEnumerable values) =>
        Assert.That(values.IsNotNullOrEmpty(), Is.False, $"'{values}' was null or empty");

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsNotNullOrEmptyTest_ReturnsFalse(IEnumerable values) =>
        Assert.That(values.IsNotNullOrEmpty(), Is.True, $"'{values}' was not null or empty");

    [Test]
    public void UseFormatTest_NotingToReplace()
    {
        var testString = Helper.GenerateRandomString();
        var result = testString.UseFormat("a", "b", "c");

        Assert.That(result, Is.EqualTo(testString));
    }

    [Test]
    public void UseFormatTest_Replaces3Values()
    {
        const string testString = "{0}{1}{2}";
        const string expected = "abc";

        var result = testString.UseFormat("a", "b", "c");

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void UseFormatTest_ReplacesSingleValue3Time()
    {
        const string testString = "{0}{0}{0}";
        const string expected = "aaa";

        var result = testString.UseFormat("a", "b", "c");

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void UseFormatTest_ThrowsExceptionIfParamsAreNotPassed()
    {
        const string testString = "{0}{1}{2}";

        Assert.Throws<FormatException>(() => testString.UseFormat("a", "b"));
    }

    [Test]
    public void PopTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "a", "b", "c", "d" };
        list.Count.Times(t =>
        {
            var result = list.Pop();
            Assert.That(result, Is.EqualTo(expected[expected.Count - t - 1]));
            Assert.That(list.Count, Is.EqualTo(expected.Count - (t + 1)));
        });
    }

    [Test]
    public void PopTest_ListWithDifferentType()
    {
        var expected = new List<int> { 1, 2, 3, 4 };
        var list = new List<int> { 1, 2, 3, 4 };
        list.Count.Times(t =>
        {
            var result = list.Pop();
            Assert.That(result, Is.EqualTo(expected[expected.Count - t - 1]));
            Assert.That(list.Count, Is.EqualTo(expected.Count - (t + 1)));
        });
    }

    [Test]
    public void PopTest_EmptyList() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new List<string>().Pop());

    [Test]
    public void ShiftTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "a", "b", "c", "d" };
        list.Count.Times(t =>
        {
            var result = list.Shift();
            Assert.That(result, Is.EqualTo(expected[t]));
            Assert.That(list.Count, Is.EqualTo(expected.Count - (t + 1)));
        });
    }

    [Test]
    public void ShiftTest_ListWithDifferentType()
    {
        var expected = new List<int> { 1, 2, 3, 4 };
        var list = new List<int> { 1, 2, 3, 4 };
        list.Count.Times(t =>
        {
            var result = list.Shift();
            Assert.That(result, Is.EqualTo(expected[t]));
            Assert.That(list.Count, Is.EqualTo(expected.Count - (t + 1)));
        });
    }

    [Test]
    public void ShiftTest_EmptyList() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new List<string>().Shift());

    [Test]
    public void UnshiftTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "b", "c", "d" };

        list.Unshift("a");
        Assert.That(list, Is.EqualTo(expected));
    }

    [Test]
    public void UnshiftTest_ListWithDifferentType()
    {
        var expected = new List<int> { 1, 2, 3, 4 };
        var list = new List<int> { 2, 3, 4 };

        list.Unshift(1);

        Assert.That(list, Is.EqualTo(expected));
    }

    [Test]
    public void UnshiftTest_EmptyList()
    {
        var expected = new List<string> { "A" };
        var actual = new List<string>();

        actual.Unshift("A");

        Assert.That(actual, Is.EqualTo(expected));
    }
}
