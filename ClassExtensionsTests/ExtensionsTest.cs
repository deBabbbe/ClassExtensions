using System.Collections;
using ClassExtensions;
using TestHelper;

namespace ClassExtensionsTest;

public class ExtensionsTest
{
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
    public void IsNullTest_ReturnsTrue(object @object)
    {
        Assert.IsTrue(@object.IsNull(), $"{@object} was not null");
    }

    [TestCaseSource(nameof(_notNullCases))]
    public void IsNullTest_ReturnsFalse(object @object)
    {
        Assert.IsFalse(@object.IsNull(), $"{@object} was null");
    }

    [TestCaseSource(nameof(_notNullCases))]
    public void IsNotNullTest_ReturnsTrue(object @object)
    {
        Assert.IsTrue(@object.IsNotNull(), $"{@object} was null");
    }

    [TestCaseSource(nameof(_nullCases))]
    public void IsNotNullTest_ReturnsFalse(object @object)
    {
        Assert.IsFalse(@object.IsNotNull(), $"{@object} was not null");
    }

    [Test]
    public void ForAllTest()
    {
        var list = new List<string> { "A", "b", "C", "d" };
        const string expectedString = "!A!!b!!C!!d!";
        var stringToCheck = "";

        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.AreEqual(expectedString, stringToCheck);
    }

    [Test]
    public void ForAllTest_DifferentType()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        const string expectedString = "!1!!2!!3!!4!!5!";
        var stringToCheck = "";

        list.ForAll(entry => stringToCheck += $"!{entry}!");

        Assert.AreEqual(expectedString, stringToCheck);
    }

    [Test]
    public void ForAllTest_EnumerationNull_NoException()
    {
        const IEnumerable<object> list = null!;
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
        const string expectedString = "0,1,2,3,4,5,6,7,8,9,";
        const int ten = 10;

        ten.Times(t => resultString += $"{t},");

        Assert.AreEqual(expectedString, resultString);
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

        StringAssert.Contains(expected, exception!.Message);
    }

    [Test]
    public void EqualJsonCheckTest_ExceptionThrown_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MinValue;
        const string expected = @"""9999-12-31T23:59:59.9999999"" not equal to ""0001-01-01T00:00:00""";

        var exception = Assert.Throws<Exception>(() => a.EqualJsonCheck(b));

        StringAssert.Contains(expected, exception!.Message);
    }

    [Test]
    public void IsEqualJsonTest_ReturnsTrue()
    {
        var a = new { Amount = 108, Message = "Hello" };
        var b = new { Amount = 108, Message = "Hello" };

        Assert.IsTrue(a.IsEqualJson(b), "Objects are not equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsTrue_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MaxValue;

        Assert.IsTrue(a.IsEqualJson(b), "Objects are not equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsFalse()
    {
        var a = new { Amount = 108, Message = "Hella" };
        var b = new { Amount = 108, Message = "Hello" };

        Assert.IsFalse(a.IsEqualJson(b), "Objects are equal");
    }

    [Test]
    public void IsEqualJsonTest_ReturnsFalse_DifferentType()
    {
        var a = DateTime.MaxValue;
        var b = DateTime.MinValue;

        Assert.IsFalse(a.IsEqualJson(b), "Objects are equal");
    }

    [TestCase("Os")]
    [TestCase("Username")]
    [TestCase("Temp")]
    public void ExpandEnvTest(string name)
    {
        Assert.AreEqual(name.ExpandEnv(), Environment.ExpandEnvironmentVariables(name));
    }

    [TestCaseSource(nameof(_emptyValues))]
    public void IsEmptyTest_ReturnsTrue(IEnumerable values)
    {
        Assert.IsTrue(values.IsEmpty(), "'' was not empty");
    }

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsEmptyTest_ReturnsFalse(IEnumerable values)
    {
        Assert.IsFalse(values.IsEmpty(), $"'{values}' was not empty");
    }

    [TestCaseSource(nameof(_nullOrEmptyValues))]
    public void IsNotEmptyTest_ReturnsFalse(IEnumerable values)
    {
        Assert.IsFalse("".IsNotEmpty(), "'' was not empty");
    }

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsNotEmptyTest_ReturnsTrue(IEnumerable values)
    {
        Assert.IsTrue(values.IsNotEmpty(), $"'{values}' was not empty");
    }

    [TestCaseSource(nameof(_nullOrEmptyValues))]
    public void IsNullOrEmptyTest_ReturnsTrue(IEnumerable values)
    {
        Assert.IsTrue(values.IsNullOrEmpty(), $"'{values}' was not null or empty");
    }

    [TestCaseSource(nameof(_notNullOrEmptyValues))]

    public void IsNullOrEmptyTest_ReturnsFalse(IEnumerable values)
    {
        Assert.IsFalse(values.IsNullOrEmpty(), $"'{values}' was null or empty");
    }

    [TestCaseSource(nameof(_nullOrEmptyValues))]

    public void IsNotNullOrEmptyTest_ReturnsTrue(IEnumerable values)
    {
        Assert.IsFalse(values.IsNotNullOrEmpty(), $"'{values}' was null or empty");
    }

    [TestCaseSource(nameof(_notNullOrEmptyValues))]
    public void IsNotNullOrEmptyTest_ReturnsFalse(IEnumerable values)
    {
        Assert.IsTrue(values.IsNotNullOrEmpty(), $"'{values}' was not null or empty");
    }

    [Test]
    public void UseFormatTest_NotingToReplace()
    {
        var testString = Helper.GenerateRandomString();
        var result = testString.UseFormat("a", "b", "c");

        Assert.AreEqual(testString, result);
    }

    [Test]
    public void UseFormatTest_Replaces3Values()
    {
        const string testString = "{0}{1}{2}";
        const string expected = "abc";

        var result = testString.UseFormat("a", "b", "c");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void UseFormatTest_ReplacesSingleValue3Time()
    {
        const string testString = "{0}{0}{0}";
        const string expected = "aaa";

        var result = testString.UseFormat("a", "b", "c");

        Assert.AreEqual(expected, result);
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
            Assert.AreEqual(expected[expected.Count - t - 1], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
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
            Assert.AreEqual(expected[expected.Count - t - 1], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
        });
    }

    [Test]
    public void PopTest_EmptyList()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new List<string>().Pop());
    }

    [Test]
    public void ShiftTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "a", "b", "c", "d" };
        list.Count.Times(t =>
        {
            var result = list.Shift();
            Assert.AreEqual(expected[t], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
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
            Assert.AreEqual(expected[t], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
        });
    }

    [Test]
    public void ShiftTest_EmptyList()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new List<string>().Shift());
    }

    [Test]
    public void UnshiftTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "b", "c", "d" };

        list.Unshift("a");

        CollectionAssert.AreEqual(expected, list.ToList());
    }

    [Test]
    public void UnshiftTest_ListWithDifferentType()
    {
        var expected = new List<int> { 1, 2, 3, 4 };
        var list = new List<int> { 2, 3, 4 };

        list.Unshift(1);

        CollectionAssert.AreEqual(expected, list.ToList());
    }

    [Test]
    public void UnshiftTest_EmptyList()
    {
        var expected = new List<string> { "A" };
        var actual = new List<string>();

        actual.Unshift("A");

        CollectionAssert.AreEqual(expected, actual);
    }
}
