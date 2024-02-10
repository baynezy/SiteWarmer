namespace Console.UnitTests;

public abstract class BaseTest
{
    protected readonly string TestPath = SetTestPath();

    private static string SetTestPath()
    {
        const string fallback = @"..\..\..\";

        return Environment.GetEnvironmentVariable("Test.Path") ?? fallback;
    }
}