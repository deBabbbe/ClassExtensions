using System.Text.Json;
using System.Collections;

namespace ClassExtensions;

public static class Extensions
{
    public static bool IsNull(this object @object) => @object == null;

    public static bool IsNotNull(this object @object) => !@object.IsNull();

    public static void ForAll<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        if (enumerable.IsNull() || action.IsNull()) return;

        foreach(var entry in enumerable)
        {
            action(entry);
        }
    }

    public static void Times(this int count, Action<int> action) =>
        Enumerable.Range(0, count).ForAll(action);

    public static void EqualJsonCheck<T>(this T self, T toCompare)
    {
        var (a, b) = ConvertToJson(self, toCompare);

        if (a != b)
        {
            var message = $"{a} not equal to {b}";
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public static bool IsEqualJson<T>(this T self, T toCompare)
    {
        var (a, b) = ConvertToJson(self, toCompare);
        return a == b;
    }

    public static string ExpandEnv(this string name) =>
        Environment.ExpandEnvironmentVariables(name);

    public static bool IsEmpty(this IEnumerable value) =>
        value.Cast<object>().Count() == 0;

    public static bool IsNotEmpty(this IEnumerable value) => !value.IsEmpty();

    public static bool IsNullOrEmpty(this IEnumerable value) =>
        value.IsNull() || value.IsEmpty();

    public static bool IsNotNullOrEmpty(this IEnumerable value) =>
        !value.IsNullOrEmpty();

    public static string UseFormat(this string text, params string[] @params) =>
        string.Format(text, @params);

    private static (string selfAsString, string toCompareAsString) ConvertToJson<T>(T self, T toCompare) =>
        (JsonSerializer.Serialize(self), JsonSerializer.Serialize(toCompare));
}
