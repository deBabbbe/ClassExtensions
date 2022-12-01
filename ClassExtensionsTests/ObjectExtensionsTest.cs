using ClassExtensions;

namespace ClassExtensionsTest;

public class ObjectExtensionsTest
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
}