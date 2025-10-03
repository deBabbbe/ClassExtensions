using System.Collections;
using System.Security.Cryptography;
using static System.Text.Json.JsonSerializer;

namespace ClassExtensions;

public static class Extensions
{
    public static bool IsNull(this object? @object) => @object == null;

    public static bool IsNotNull(this object @object) => !@object.IsNull();

    public static void ForAll<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        if (enumerable.IsNull() || action.IsNull()) return;

        foreach (var entry in enumerable)
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
        !value.Cast<object>().Any();

    public static bool IsNotEmpty(this IEnumerable value) => !value.IsEmpty();

    public static bool IsNullOrEmpty(this IEnumerable value) =>
        value.IsNull() || value.IsEmpty();

    public static bool IsNotNullOrEmpty(this IEnumerable value) =>
        !value.IsNullOrEmpty();

    public static string UseFormat(this string text, params object?[] @params) =>
        string.Format(text, @params);

    public static T Pop<T>(this IList<T> source)
    {
        var idx = source.Count() - 1;
        var result = source.ElementAt(idx);
        source.RemoveAt(idx);
        return result;
    }

    public static T Shift<T>(this IList<T> source)
    {
        var result = source.ElementAt(0);
        source.RemoveAt(0);
        return result;
    }

    public static void Unshift<T>(this IList<T> source, T toAdd) => source.Insert(0, toAdd);

    public static byte[] ToByteArray(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public static Stream ToStream(this byte[] byteArray) =>
        new MemoryStream(byteArray);

    public static bool None<T>(this IEnumerable<T> source, Func<T, bool>? predicate = null)
    {
        if (source.IsNull()) return true;
        return predicate == null
            ? !source.Any()
            : !source.Any(predicate);
    }

    public static T AnyOne<T>(this IEnumerable<T> source)
    {
        if (source.IsNullOrEmpty()) throw new ArgumentNullException(nameof(source));

        int index = RandomNumberGenerator.GetInt32(source.Count());
        return source.ElementAt(index);
    }

    private static (string selfAsString, string toCompareAsString) ConvertToJson<T>(T self, T toCompare) =>
        (Serialize(self), Serialize(toCompare));
}
