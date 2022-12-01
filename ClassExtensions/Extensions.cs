namespace ClassExtensions;

public static class Extensions
{
    private static Random _random = new();

    public static string ToRandomCase(this string text)
    {
        string RandomCase(char charText) => _random.Next(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();

        return string.Join(",", text.Select(RandomCase));
    }

    public static bool IsNull(this object @object) =>
        @object == null;

    public static bool IsNotNull(this object @object) =>
        !@object.IsNull();

    public static void ForAll<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        if (enumerable.IsNull() || action.IsNull()) return;

        foreach (var entry in enumerable)
        {
            action(entry);
        }
    }

    public static void Times(this int count, Action<int> action)
    {
        Enumerable.Range(0, count).ForAll(action);
    }
}