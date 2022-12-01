namespace ClassExtensions;

public static class ObjectExtensions
{
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
}