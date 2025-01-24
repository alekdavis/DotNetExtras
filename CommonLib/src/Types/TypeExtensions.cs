/// <summary>
/// Implements extension methods applicable to types.
/// </summary>
namespace DotNetExtras.Types;
public static class TypeExtensions
{
    /// <summary>
    /// Determine whether the specified type is simple 
    /// (i.e. enum, string, number, GUID, date, time, offset, etc.) 
    /// or complex (i.e. custom class with public properties and methods, list, array, etc.).
    /// </summary>
    /// <remarks>
    /// See <see href="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
    /// and <see href="https://gist.github.com/jonathanconway/3330614"/>.
    /// </remarks>
	public static bool IsSimpleType
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
}
