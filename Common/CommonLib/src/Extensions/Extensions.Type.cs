namespace DotNetExtras.Common.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// Determine whether the specified type is simple 
    /// (i.e. enum, string, number, GUID, date, time, offset, etc.) 
    /// or complex (i.e. custom class with public properties and methods, list, array, etc.).
    /// </summary>
    /// <param name="type">
    /// Data type.
    /// </param>
    /// <returns>
    /// True if the type is simple; otherwise, false.
    /// </returns>
    /// <remarks>
    /// Adapted from 
    /// https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs
    /// for object cloning, but also see
    /// <see href="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
    /// and <see href="https://gist.github.com/jonathanconway/3330614"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// Console.WriteLine(typeof(int).IsSimple()); // True
    /// Console.WriteLine(typeof(string).IsSimple()); // True
    /// Console.WriteLine(typeof(DateTime).IsSimple()); // True
    /// Console.WriteLine(typeof(Guid).IsSimple()); // True
    /// Console.WriteLine(typeof(Employee).IsSimple()); // False
    /// </code>
    /// </example>
	public static bool IsSimple
    (
		this Type type
    )
	{
		return
			type.IsValueType ||
			type.IsPrimitive ||
			new Type[] { 
				typeof(string),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(Guid)
			}.Contains(type) ||
			Convert.GetTypeCode(type) != TypeCode.Object;
    }

    /// <summary>
    /// Determine whether the specified type is a primitive type.
    /// </summary>
    /// <param name="type">
    /// Data type.
    /// </param>
    /// <returns>
    /// True if the specified type is a primitive type; otherwise, false.
    /// </returns>
    /// <remarks>
    /// Adapted from 
    /// <see href="https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs"/>
    /// for object cloning.
    /// </remarks>
    public static bool IsPrimitive
    (
        this Type type
    )
    {
        return type == typeof(string) || (type.IsValueType && type.IsPrimitive);
    }
}

