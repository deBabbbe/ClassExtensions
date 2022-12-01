namespace TestHelperTest;

public static class TestExtensions
{
    public static bool IsInBetween(this int value, int min, int max)
    {
        if (min > value)
        {
            Console.WriteLine($"{min} is bigger than {value}");
            return false;
        }
        if (max < value)
        {
            Console.WriteLine($"{max} is smaller than {value}");
            return false;
        }
        return true;
    }
}