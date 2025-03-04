using DotNetExtras.Common.Metadata;
using System.Reflection;

namespace CommonLibTests;

public class AssemblyInfoTests
{
    [Fact]
    public void AssemblyInfo_Company()
    {
        string? company = AssemblyInfo.Company;
        Assert.Equal("Microsoft Corporation", company);
    }

    [Fact]
    public void AssemblyInfo_Copyright()
    {
        string? copyright = AssemblyInfo.Copyright;
        Assert.Equal("© Microsoft Corporation. All rights reserved.", copyright);
    }

    [Fact]
    public void AssemblyInfo_Description()
    {
        string? description = AssemblyInfo.Description;
        Assert.Null(description);
    }

    [Fact]
    public void AssemblyInfo_Product()
    {
        string? product = AssemblyInfo.Product;
        Assert.Equal("testhost", product);
    }

    [Fact]
    public void AssemblyInfo_Title()
    {
        string? title = AssemblyInfo.Title;
        Assert.Equal("testhost", title);
    }

    [Fact]
    public void AssemblyInfo_Version()
    {
        string? version = AssemblyInfo.Version;
        Assert.NotNull(version);
        Assert.NotEmpty(version);
    }

    [Fact]
    public void AssemblyInfo_GetAssembly()
    {
        Assembly? assembly = AssemblyInfo.GetAssembly();

        Assert.NotNull(assembly);
        Assert.Equal(
            (( Assembly.GetEntryAssembly() 
            ?? Assembly.GetCallingAssembly()) 
            ?? Assembly.GetExecutingAssembly()) 
            ?? Assembly.GetAssembly(typeof(AssemblyInfo)), assembly);
    }
}
