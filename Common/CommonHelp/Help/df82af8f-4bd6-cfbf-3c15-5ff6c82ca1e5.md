# GetPropertyValue Method


Returns the value of the immediate or nested object property.



## Definition
**Namespace:** <a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions</a>  
**Assembly:** DotNetExtras.Common (in DotNetExtras.Common.dll) Version: 0.0.0+ad9e09e88ea1e6582dceda1560895fc6af63de46

**C#**
``` C#
public static Object? GetPropertyValue(
	this Object? source,
	string name
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a></dt><dd>Object that owns the property.</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a></dt><dd>Name of the property (case-insensitive; can be compound with names separated by periods).</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  
Property value (or `null`, if property does not exists).

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">

Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">

Extension Methods (C# Programming Guide)</a>.

## Remarks

The code assumes that the property exists; if it does not, the code will return `null`.

The property can be nested.

The code handles both class properties and fields.


## Example


**C#**  
``` C#
string? givenName = user.GetPropertyValue("Name.GivenName");
int? age = user.GetPropertyValue("Age");
```


## See Also


#### Reference
<a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions Class</a>  
<a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions Namespace</a>  
