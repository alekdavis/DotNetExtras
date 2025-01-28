using System.Collections;
using System.Dynamic;
using System.Reflection;

namespace DotNetExtras.Common.Extensions;
/// <summary>
/// General-purpose extension methods for objects.
/// </summary>
public static partial class Extensions
{
    #region Public methods
    /// <summary>
    /// Determines whether the specified object 
    /// has no properties or fields holding non-null values or non-empty collections.
    /// </summary>
    /// <param name="source">
    /// The object to check.
    /// </param>
    /// <param name="publicOnly">
    /// If <c>true</c>, only public properties and fields will be checked.
    /// </param>
    /// <returns>
    /// <c>true</c> if the object is empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEmpty
    (
        this object? source,
        bool publicOnly = false
    )
    {
        if (source == null)
        {
            return true;
        }

        Type type = source.GetType();

        // Check all public and internal properties
        foreach (PropertyInfo propertyInfo in type.GetProperties(
            BindingFlags.Instance |
            BindingFlags.Public |
            (publicOnly ? 0 : BindingFlags.NonPublic)))
        {
            if (propertyInfo.GetIndexParameters().Length == 0) // Ignore indexers
            {
                object? value = propertyInfo.GetValue(source);

                if (value != null)
                {
                    if (!IsEmptyValue(value))
                    {
                        return false;
                    }
                }
            }
        }

        // Check all public and internal fields
        foreach (FieldInfo field in type.GetFields(
            BindingFlags.Instance |
            BindingFlags.Public  |
            (publicOnly ? 0 : BindingFlags.Public)))
        {
            object? value = field.GetValue(source);

            if (value != null)
            {
                if (!IsEmptyValue(value))
                {
                    return false;
                }
            }
        }

        return true;
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Determines whether the specified value is empty.
    /// A value is considered empty if it is null, an empty string, an empty collection, or an empty enumerable.
    /// </summary>
    /// <param name="value">
    /// The value to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if the value is empty; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsEmptyValue
    (
        object? value
    )
    {
        return value == null || (value is not string && (value is ICollection collection
            ? collection.Count == 0
            : value is IEnumerable enumerable
                ? !enumerable.Cast<object>().Any()
                : !value.GetType().IsValueType && value.IsEmpty()));
    }

    /// <summary>
    /// Converts any object to a dynamic object.
    /// </summary>
    /// <typeparam name="T">
    /// Data type of the source object.
    /// </typeparam>
    /// <param name="source">
    /// The original object.
    /// </param>
    /// <param name="extras">
    /// Additional propertyNames to be added to the expando object.
    /// </param>
    /// <param name="publicOnly">
    /// If true, only public properties will be included.
    /// </param>
    /// <returns>
    /// Expando object.
    /// </returns>
    /// <remarks>
    /// Adapted from:
    /// https://stackoverflow.com/questions/42836936/convert-class-to-dynamic-and-add-propertyNames#answer-42837044
    /// </remarks>
    public static dynamic? ToDynamic<T>
    (
        this T source,
        Dictionary<string, object>? extras = null,
        bool publicOnly = false
    )
    {
        IDictionary<string, object?> expando = new ExpandoObject();

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(
            BindingFlags.Instance | 
            BindingFlags.Public | 
            (publicOnly ? 0 : BindingFlags.NonPublic)))
        {
            object? currentValue = propertyInfo.GetValue(source);

            if (currentValue != null)
            {
                expando.Add(propertyInfo.Name, currentValue);
            }
        }

        if (extras != null)
        {
            foreach (string key in extras.Keys)
            {
                if (extras[key] != null)
                {
                    expando.Add(key, extras[key]);
                }
            }
        }

        return expando as ExpandoObject;
    }
    #endregion
}
