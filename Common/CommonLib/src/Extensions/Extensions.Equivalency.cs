using System.Collections;
using System.Globalization;
using System.Reflection;

using System.Text;

namespace DotNetExtras.Common.Extensions;
public static partial class Extensions
{
    /// <summary>
    /// Checks if the source object is identical to the target (comparing all instance properties and fields). 
    /// </summary>
    /// <param name="source">
    /// Object we are comparing.
    /// </param>
    /// <param name="target">
    /// Object we're comparing to.
    /// </param>
    /// <param name="ignoreNullSource">
    /// If true, the null value of the source indicates that the property is unchanged.
    /// </param>
    /// <param name="publicOnly">
    /// If true, only public properties and fields will be checked.
    /// </param>
    /// <returns>
    /// True if objects are equivalent, otherwise, false.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Adapted from
    /// <see href="https://stackoverflow.com/questions/10454519/best-way-to-compare-two-complex-objects"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// User user = new()
    /// {
    ///     Id = 1,
    ///     Email = "joe@mail.com",
    ///     Name = new()
    ///     {
    ///         GivenName = "Joe",
    ///     },
    /// };
    /// 
    /// User clone = user.Clone();
    /// 
    /// Assert.True(user.IsEquivalentTo(clone));
    /// Assert.True(clone.IsEquivalentTo(user));
    /// 
    /// user.Email = null;
    /// 
    /// // The source object has a null value, so the properties are not equivalent,
    /// // unless we ignore null properties in the source object.
    /// Assert.True(user.IsEquivalentTo(clone, true));
    /// Assert.False(clone.IsEquivalentTo(user, true));
    /// </code>
    /// </example>
    public static bool IsEquivalentTo
    (
        this object? source,
        object? target,
        bool ignoreNullSource = false,
        bool publicOnly = false
    )
    {
        if (source == null && target == null)
        {
            return true;
        }

        if (source == null && ignoreNullSource)
        {
            return true;
        }

        if (source == null || target == null)
        {
            return false;
        }

        if (ReferenceEquals(source, target))
        {
            return true;
        }

        Type sourceType = source.GetType();
        Type targetType = target.GetType();

        if (source is bool && target is bool)
        {
            return source.Equals(target);
        }
        else if (source is bool && target is string tBool)
        {
            tBool = tBool.ToLower();

            bool? b = tBool == "true" ? true : tBool == "false" ? false : null;

            return b.HasValue ? source.Equals(b.Value) : source.Equals(target);
        }
        else if (target is bool && source is string sBool)
        {
            sBool = sBool.ToLower();

            bool? b = sBool == "true" ? true : sBool == "false" ? false : null;

            return b.HasValue ? target.Equals(b.Value) : target.Equals(source);
        }
        else if (source is bool && (target is short || target is int || target is long || target is ushort || target is uint || target is ulong))
        {
            long tLong = Convert.ToInt64(target);

            bool? b = tLong == 1 ? true : tLong == 0 ? false : null;

            return b.HasValue ? source.Equals(b.Value) : source.Equals(target);
        }
        else if (target is bool && (source is short || source is int || source is long || source is ushort || source is uint || source is ulong))
        {
            long sLong = Convert.ToInt64(source);

            bool? b = sLong == 1 ? true : sLong == 0 ? false : null;

            return b.HasValue ? target.Equals(b.Value) : target.Equals(source);
        }
        else if (source is string && targetType == typeof(string))
        {
            return source.Equals(target);
        }
        else if (source is StringBuilder && target is StringBuilder)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is StringBuilder && target is string)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is string && target is StringBuilder)
        {
            return source.ToString() == target.ToString();
        }
        else if (source is DateTime && target is DateTime)
        {
            return source.Equals(target);
        }
        else if (source is DateTime && target is string tStringDt)
        {
            DateTime dt;
            if (tStringDt.Contains('+') || tStringDt.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(tStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTimeOffset dto))
                {
                    return false;
                }

                dt = dto.UtcDateTime;
            }
            else
            {
                dt = DateTime.Parse(tStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
            
            return source.Equals(dt);
        }
        else if (target is DateTime && source is string sStringDt)
        {
            DateTime dt;
            if (sStringDt.Contains('+') || sStringDt.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(sStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTimeOffset dto))
                {
                    return false;
                }

                dt = dto.UtcDateTime;
            }
            else
            {
                dt = DateTime.Parse(sStringDt, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
            
            return target.Equals(dt);
        }
        else if (source is DateTime sDt && target is DateTimeOffset tDto)
        {
            DateTimeOffset dto = new(sDt);

            return tDto.Equals(dto);
        }
        else if (source is DateTimeOffset && target is DateTimeOffset)
        {
            return source.Equals(target);
        }
        else if (source is DateTimeOffset && target is string tString)
        {
            DateTimeOffset dto;
            if (tString.Contains('+') || tString.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(tString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dto))
                {
                    return false;
                }
            }
            else
            {          
                if (!DateTime.TryParse(tString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime dt))
                {
                    return false;
                }

                dto = new(dt);
            }
            
            return source.Equals(dto);
        }
        else if (target is DateTimeOffset && source is string sString)
        {
            DateTimeOffset dto;
            if (sString.Contains('+') || sString.Contains('-'))
            {
                if (!DateTimeOffset.TryParse(sString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dto))
                {
                    return false;
                }
            }
            else
            {          
                if (!DateTime.TryParse(sString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime dt))
                {
                    return false;
                }

                dto = new(dt);
            }
            
            return target.Equals(dto);
        }
        else if (source is DateTimeOffset sDto && target is DateTime tDt)
        {
            DateTimeOffset dto = new(tDt);

            return sDto.Equals(dto);
        }
        else if (sourceType.IsEnum && targetType.IsEnum)
        {
            return source.Equals(target);
        }
        else if (sourceType.IsEnum && (target is short || target is int || target is long || target is ushort || target is uint || target is ulong))
        {
            return Convert.ToInt64(source) == Convert.ToInt64(target);
        }
        else if (sourceType.IsEnum && target is string)
        {
            return (source?.ToString() ?? "").ToUpper().Equals(target.ToString()?.ToUpper());
        }
        else if (targetType.IsEnum && (source is short || source is int || source is long || source is ushort || source is uint || source is ulong))
        {
            return Convert.ToInt64(target) == Convert.ToInt64(source);
        }
        else if (targetType.IsEnum && source is string)
        {
            return (target?.ToString() ?? "").ToUpper().Equals(source.ToString()?.ToUpper());
        }
        else if (sourceType.IsSimple() && targetType.IsSimple())
        {
            return source.ToString() == target.ToString();
        }
        else if (sourceType.IsArray && targetType.IsArray)
        {
            Array sourceArray = (Array)source;
            Array targetArray = (Array)target;

            if ((ignoreNullSource && sourceArray.Length > targetArray.Length) ||
                (!ignoreNullSource && sourceArray.Length != targetArray.Length))
            {
                return false;
            }

            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (!IsEquivalentTo(sourceArray.GetValue(i), targetArray.GetValue(i), ignoreNullSource))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IList).IsAssignableFrom(sourceType) && typeof(IList).IsAssignableFrom(targetType))
        {
            IList sourceList = (IList)source;
            IList targetList = (IList)target;

            if ((ignoreNullSource && sourceList.Count > targetList.Count) || 
               (!ignoreNullSource && sourceList.Count != targetList.Count))
            {
                return false;
            }

            for (int i = 0; i < sourceList.Count; i++)
            {
                if (!IsEquivalentTo(sourceList[i], targetList[i], ignoreNullSource))
                {
                    return false;
                }
            }

            return true;
        }
        else if (typeof(IDictionary).IsAssignableFrom(sourceType) && typeof(IDictionary).IsAssignableFrom(targetType))
        {
            IDictionary sourceDictionary = (IDictionary)source;
            IDictionary targetDictionary = (IDictionary)target;

            if ((ignoreNullSource && sourceDictionary.Count > targetDictionary.Count) || 
               (!ignoreNullSource && sourceDictionary.Count != targetDictionary.Count))
            {
                return false;
            }            
            
            foreach (object? key in sourceDictionary.Keys)
            {
                if (!targetDictionary.Contains(key) || !IsEquivalentTo(sourceDictionary[key], targetDictionary[key], ignoreNullSource))
                {
                    return false;
                }
            }

            return true;
        }
        else if (sourceType.IsClass && targetType.IsClass)
        {
            HashSet<string> skip = [];

            foreach (PropertyInfo property in sourceType.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public | 
                (publicOnly ? 0 : BindingFlags.NonPublic)))
            {
                if (property.CanRead)
                {
                    skip.Add(property.Name);
                    object? sourceValue = property.GetValue(source);
                    object? targetValue = property.GetValue(target);

                    if (!IsEquivalentTo(sourceValue, targetValue, ignoreNullSource))
                    {
                        return false;
                    }
                }
            }

            if (!ignoreNullSource)
            {
                foreach (PropertyInfo property in targetType.GetProperties(
                    BindingFlags.Instance | 
                    BindingFlags.Public |
                    (publicOnly ? 0 : BindingFlags.NonPublic)))
                {
                    if (property.CanRead)
                    {
                        if (skip.Contains(property.Name))
                        {
                            continue;
                        }

                        object? targetValue = property.GetValue(target);

                        if (!IsEquivalentTo(null, targetValue, ignoreNullSource))
                        {
                            return false;
                        }
                    }
                }
            }

            skip.Clear();

            foreach (FieldInfo field in sourceType.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public | 
                (publicOnly ? 0 : BindingFlags.NonPublic)))
            {
                skip.Add(field.Name);
                object? sourceValue = field.GetValue(source);
                object? targetValue = field.GetValue(target);

                if (!IsEquivalentTo(sourceValue, targetValue, ignoreNullSource))
                {
                    return false;
                }
            }

            if (!ignoreNullSource)
            {
                foreach (FieldInfo field in targetType.GetFields( 
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    (publicOnly ? 0 : BindingFlags.NonPublic)))
                {
                    if (skip.Contains(field.Name))
                    {
                        continue;
                    }

                    object? targetValue = field.GetValue(target);

                    if (!IsEquivalentTo(null, targetValue, ignoreNullSource))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        else
        {
            return source.Equals(target);
        }
    }
}
