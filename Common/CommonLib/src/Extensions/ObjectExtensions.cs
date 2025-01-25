using System.Collections;
using System.Reflection;

namespace DotNetExtras.Extensions;
/// <summary>
/// General-purpose extension methods for objects.
/// </summary>
public static class ObjectExtensions
{
    #region Public methods
    /// <summary>
    /// Determines whether the specified object 
    /// has no properties or fields holding non-null values or non-empty collections.
    /// </summary>
    /// <param name="source">
    /// The object to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if the object is empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsEmpty
    (
        this object? source
    )
    {
        if (source == null)
        {
            return true;
        }

        Type type = source.GetType();

        // Check all public and internal properties
        foreach (PropertyInfo propertyInfo in type.GetProperties(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance))
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
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance))
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
        if (value == null)
        {
            return true;
        }
        
        if (value is string)
        {
            return false;
        }
        
        if (value is ICollection collection)
        {
            if (collection.Count == 0)
            { 
                return true;
            }

            return false;
        }
        
        if (value is IEnumerable enumerable)
        {
            if (enumerable.Cast<object>().Any())
            {
                return false;
            }

            return true;
        }
        
        if (value.GetType().IsValueType)
        {
            return false;
        }

        return value.IsEmpty();
    } 
    #endregion
}
