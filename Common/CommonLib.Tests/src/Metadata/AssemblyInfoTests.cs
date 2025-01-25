using DotNetExtras.Metadata;
using System.Reflection;

namespace CommonLibTests.Metadata;

public class AssemblyInfoTests
{
    [Fact]
    public void Company()
    {
        string? company = AssemblyInfo.Company;
        Assert.Equal("Microsoft Corporation", company);
    }

    [Fact]
    public void Copyright()
    {
        string? copyright = AssemblyInfo.Copyright;
        Assert.Equal("© Microsoft Corporation. All rights reserved.", copyright);
    }

    [Fact]
    public void Description()
    {
        string? description = AssemblyInfo.Description;
        Assert.Null(description);
    }

    [Fact]
    public void Product()
    {
        string? product = AssemblyInfo.Product;
        Assert.Equal("testhost", product);
    }

    [Fact]
    public void Title()
    {
        string? title = AssemblyInfo.Title;
        Assert.Equal("testhost", title);
    }

    [Fact]
    public void Version_ShouldReturnAssemblyFileVersion()
    {
        string? version = AssemblyInfo.Version;
        Assert.NotNull(version);
        Assert.NotEmpty(version);
    }

    [Fact]
    public void GetAssembly_ShouldReturnCurrentAssembly()
    {
        var assembly = AssemblyInfo.GetAssembly();

        Assert.NotNull(assembly);
        Assert.Equal(
            (( Assembly.GetEntryAssembly() 
            ?? Assembly.GetCallingAssembly()) 
            ?? Assembly.GetExecutingAssembly()) 
            ?? Assembly.GetAssembly(typeof(AssemblyInfo)), assembly);
    }
}
