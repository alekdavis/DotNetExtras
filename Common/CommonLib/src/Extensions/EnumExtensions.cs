using System.ComponentModel;
using System.Reflection;

namespace DotNetExtras.Common;

/// <summary>
/// Implements extension methods for enumerated objects.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the value of an attribute applied to an enum data type.
    /// </summary>
    /// <typeparam name="T">
    /// Data type.
    /// </typeparam>
    /// <param name="value">
    /// Enumerated value.
    /// </param>
    /// <returns>
    /// Attribute value.
    /// </returns>
    public static T? GetAttribute<T>
    (
        this Enum value
    ) 
    where T : Attribute
    {
        Type            type        = value.GetType();
        MemberInfo[]    memberInfo  = type.GetMember(value.ToString());
        object[]        attributes  = memberInfo[0].GetCustomAttributes(typeof(T), false);

        return attributes == null
            ? null
            : attributes.Length > 0
                ? attributes[0] as T
                : null;
    }

    /// <summary>
    /// Gets the value of the description attribute applied to an enum data type.
    /// </summary>
    /// <param name="value">
    /// Enum value.
    /// </param>
    /// <returns>
    /// Attribute value or null.
    /// </returns>
    /// <seealso cref="DescriptionAttribute"/>
    public static string? ToDescription
    (
        this Enum value
    )
    {
        DescriptionAttribute? attribute = value.GetAttribute<DescriptionAttribute>();
        return attribute?.Description;
    }

    /// <summary>
    /// Gets the value of the abbreviation attribute applied to an enum data type.
    /// </summary>
    /// <param name="value">
    /// Enum value.
    /// </param>
    /// <returns>
    /// Attribute value or null.
    /// </returns>
    public static string? ToAbbreviation
    (
        this Enum value
    )
    {
        AbbreviationAttribute? attribute = value.GetAttribute<AbbreviationAttribute>();
        return attribute?.Abbreviation;
    }

    /// <summary>
    /// Gets the value of the short name attribute applied to an enum data type.
    /// </summary>
    /// <param name="value">
    /// Enum value.
    /// </param>
    /// <returns>
    /// Attribute value or null.
    /// </returns>
    /// <seealso cref="ShortNameAttribute"/>
    public static string? ToShortName
    (
        this Enum value
    )
    {
        ShortNameAttribute? attribute = value.GetAttribute<ShortNameAttribute>();
        return attribute?.ShortName;
    }
}
