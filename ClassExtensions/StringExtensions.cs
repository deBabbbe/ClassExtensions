namespace ClassExtensions;

public static class StringExtensions
{
    private static Random _random = new();

    public static string ToRandomCase(this string text)
    {
        string RandomCase(char charText) => _random.Next(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();

        return string.Join(",", text.Select(RandomCase));
    }
}