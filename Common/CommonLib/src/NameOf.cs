using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DotNetExtras.Common;

/// <summary>
/// Utility methods for formatting type, object, class, and property names.
/// </summary>
public static class NameOf
{
    #region Public methods
    /// <summary>
    /// Returns full name of the object, class, type, or property.
    /// </summary>
    /// <param name="caller">
    /// Object, class, type, or property.
    /// </param>
    /// <param name="camelCase">
    /// Indicates whether to return the name in camelCase.
    /// </param>
    /// <param name="name">
    /// Must be omitted.
    /// </param>
    /// <returns>
    /// Full name including all compound parts.
    /// </returns>
    /// <example>
    /// <code>
    /// // Class.Parent?.ChildProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent?.ChildProp)));
    /// 
    /// // class.parent?.childProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent?.ChildProp), true));
    /// 
    /// // object.Parent.ChildProp
    /// Console.WriteLine(NameOf.Long(object.Parent?.ChildProp));
    /// 
    /// // object.parent.childProp
    /// Console.WriteLine(NameOf.Long(object.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Full
    (
        this object? caller,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = Normalize(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = name.Replace("?", "").Split('.');

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = parts.Select(JsonNamingPolicy.CamelCase.ConvertName);
            result = string.Join('.', camelCaseParts);
        }
        else
        {
            result = string.Join('.', parts);
        }

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns the partial name of the object, class, type, or property
    /// omitting the entry before the dot (counting from the left).
    /// </summary>
    /// <returns>
    /// Name without the first (left) compound prefix.
    /// </returns>
    /// <remarks>
    /// If the name does not include any compound parts, it will be returned as-is.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Parent?.ChildProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent?.ChildProp)));
    /// 
    /// // parent?.childProp
    /// Console.WriteLine(NameOf.Long(nameof(Class.Parent?.ChildProp), true));
    /// 
    /// // Parent.ChildProp
    /// Console.WriteLine(NameOf.Long(object.Parent?.ChildProp));
    /// 
    /// // parent.childProp
    /// Console.WriteLine(NameOf.Long(object.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Long
    (
        this object? caller,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        string result = Skip(caller, 1, camelCase, name);

        return string.IsNullOrEmpty(result)
            ? camelCase
                ? JsonNamingPolicy.CamelCase.ConvertName(Normalize(name))
                : Normalize(name)
            : result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns the short name of the immediate object property (same as nameof()).
    /// </summary>
    /// <returns>
    /// Short (immediate) name (without compound prefix).
    /// </returns>
    /// <example>
    /// <code>
    /// // ChildProp
    /// Console.WriteLine(NameOf.Short(nameof(Class.Parent?.ChildProp)));
    /// 
    /// // childProp
    /// Console.WriteLine(NameOf.Short(nameof(Class.Parent?.ChildProp), true));
    /// 
    /// // ChildProp
    /// Console.WriteLine(NameOf.Short(object.Parent?.ChildProp));
    /// 
    /// // childProp
    /// Console.WriteLine(NameOf.Short(object.Parent?.ChildProp, true));
    /// </code>
    /// </example>
    public static string Short
    (
#pragma warning disable IDE0060 // Remove unused parameter
        this object? caller,
#pragma warning restore IDE0060 // Remove unused parameter
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = Normalize(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = name.Replace("?", "").Split('.');
        string part = parts[^1];

        result = camelCase ? JsonNamingPolicy.CamelCase.ConvertName(part) : part;

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns a shortened name of the object, class, type, or property
    /// after removing the specified number of compound prefixes or suffixes.
    /// </summary>
    /// <param name="count">
    /// Number of parts to be excluded from the result.
    /// A positive number indicates skipping from the left,
    /// a negative number indicates skipping from the right.
    /// If the skipped number is bigger than the number of parts,
    /// an empty string will be returned.
    /// </param>
    /// <returns>
    /// Shortened name.
    /// </returns>
    public static string Skip
    (
        this object? caller,
        [Range(1, int.MaxValue)]
        int count = 1,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = Normalize(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = name.Replace("?", "").Split('.');

        count = Math.Min(count, parts.Length);

        string[] resultParts = Enumerable.Skip(parts, count).ToArray();

        if (resultParts.Length == 0)
        {
            return "";
        }

        string[] results;

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = resultParts.Select(JsonNamingPolicy.CamelCase.ConvertName);

            results = camelCaseParts.ToArray();
        }
        else
        {
            results = resultParts;
        }

        result = string.Join('.', results);

        return result;
    }

    /// <inheritdoc cref="Full(object?, bool, string?)" path="param|remarks"/>
    /// <summary>
    /// Returns a shortened name of the object, class, type, or property
    /// keeping the specified number of compound prefixes or suffixes.
    /// </summary>
    /// <param name="count">
    /// Number of parts to be included in the result.
    /// A positive number indicates including from the left,
    /// a negative number indicates including from the right.
    /// If the kept number is bigger than the number of parts,
    /// an empty string will be returned.
    /// </param>
    /// <returns>
    /// Shortened name.
    /// </returns>
    public static string Keep
    (
        this object? caller,
        [Range(1, int.MaxValue)]
        int count = 1,
        bool camelCase = false,
        [CallerArgumentExpression(nameof(caller))] string? name = null
    )
    {
        name = Normalize(name);

        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        string result;
        string[] parts = name.Replace("?", "").Split('.');

        int skip = Math.Max(0, parts.Length - count);

        string[] resultParts = Enumerable.Skip(parts, skip).ToArray();

        if (resultParts.Length == 0)
        {
            return "";
        }

        string[] results;

        if (camelCase)
        {
            IEnumerable<string> camelCaseParts = resultParts.Select(JsonNamingPolicy.CamelCase.ConvertName);

            results = camelCaseParts.ToArray();
        }
        else
        {
            results = resultParts;
        }

        result = string.Join('.', results);

        return result;
    } 
    #endregion

    #region Private methods
    /// <summary>
    /// Removes 'nameof' from the name.
    /// </summary>
    /// <param name="name">
    /// Name that may the leading 'nameof' prefix.
    /// </param>
    /// <returns>
    /// Normalized name.
    /// </returns>
    private static string Normalize
    (
        string? name
    )
    {
        if (string.IsNullOrEmpty(name))
        {
            return "";
        }

        if (name.StartsWith("nameof("))
        {
            name = name[7..^1];
        }

        return name;
    } 
    #endregion
}

