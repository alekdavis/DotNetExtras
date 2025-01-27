using System.Reflection;

namespace DotNetExtras.Common;
/// <summary>
/// Provides information about the primary application assembly.
/// </summary>
/// <remarks>
/// Adapted some code from 
/// "Access AssemblyInfo File and Get Product Informations"
/// https://www.c-sharpcorner.com/UploadFile/ravesoft/access-assemblyinfo-file-and-get-product-informations/
/// </remarks>
public static class AssemblyInfo
{
    #region Public properties
    /// <summary>
    /// Returns the company name.
    /// </summary>
    public static string? Company
    {
        get
        {
            AssemblyCompanyAttribute? company = GetCustomAttribute<AssemblyCompanyAttribute>();
            return company?.Company;
        }
    }

    /// <summary>
    /// Returns the copyright message.
    /// </summary>
    public static string? Copyright
    {
        get
        {
            AssemblyCopyrightAttribute? copyright = GetCustomAttribute<AssemblyCopyrightAttribute>();
            return copyright?.Copyright;
        }
    }

    /// <summary>
    /// Returns the assembly description.
    /// </summary>
    public static string? Description
    {
        get
        {
            AssemblyDescriptionAttribute? description = GetCustomAttribute<AssemblyDescriptionAttribute>();
            return description?.Description;
        }
    }

    /// <summary>
    /// Returns the product name.
    /// </summary>
    public static string? Product
    {
        get
        {
            AssemblyProductAttribute? product = GetCustomAttribute<AssemblyProductAttribute>();
            return product?.Product;
        }
    }

    /// <summary>
    /// Returns the assembly title.
    /// </summary>
    public static string? Title
    {
        get
        {
            AssemblyTitleAttribute? title = GetCustomAttribute<AssemblyTitleAttribute>();
            return title?.Title;
        }
    }

    /// <summary>
    /// Returns the version of the assembly file.
    /// </summary>
    /// <remarks>
    /// See "What are differences between AssemblyVersion, AssemblyFileVersion and AssemblyInformationalVersion?":
    /// https://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin#answer-65062
    /// </remarks>
    public static string? Version
    {
        get
        {
            AssemblyFileVersionAttribute? version = GetCustomAttribute<AssemblyFileVersionAttribute>();
            return version?.Version;
        }
    }
    #endregion

    #region Private methods    
    /// <summary>
    /// Returns the main application assembly.
    /// </summary>
    /// <returns>
    /// Current assembly.
    /// </returns>
    public static Assembly? GetAssembly()
    {
        Assembly? assembly = ((Assembly.GetEntryAssembly() 
            ?? Assembly.GetCallingAssembly()) 
            ?? Assembly.GetExecutingAssembly()) 
            ?? Assembly.GetAssembly(typeof(AssemblyInfo));

        return assembly;
    }

    /// <summary>
    /// Returns the custom assembly attribute.
    /// </summary>
    /// <typeparam name="T">
    /// Attribute data type.
    /// </typeparam>
    /// <returns>
    /// Attribute for the matching type.
    /// </returns>
    private static T? GetCustomAttribute<T>()
    where T : Attribute
    {
        Assembly? assembly = GetAssembly();

        if (assembly == null)
        {
            return null;
        }

        object[] attributes = assembly.GetCustomAttributes(typeof(T), false);

        return (attributes == null) || (attributes.Length == 0)
            ? null 
            : (T)attributes[0];
    }
    #endregion
}

