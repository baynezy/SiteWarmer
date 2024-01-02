namespace SiteWarmer.Core.UnitTests.Logging;

public class FileHelperTest
{
    [Fact]
    public void FileHelper_ImplementsIHelper()
    {
        // arrange
        var fileHelper = new FileHelper();

        // assert
        fileHelper.Should().BeAssignableTo<IFileHelper>();
    }
}