# ToDescription Method


Returns the value of the <a href="https://learn.microsoft.com/dotnet/api/system.componentmodel.descriptionattribute" target="_blank" rel="noopener noreferrer">DescriptionAttribute</a> applied to an enumerated field.



## Definition
**Namespace:** <a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions</a>  
**Assembly:** DotNetExtras.Common (in DotNetExtras.Common.dll) Version: 0.0.0+ad9e09e88ea1e6582dceda1560895fc6af63de46

**C#**
``` C#
public static string? ToDescription(
	this Enum value
)
```



#### Parameters
<dl><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.enum" target="_blank" rel="noopener noreferrer">Enum</a></dt><dd>Enumerated field value.</dd></dl>

#### Return Value
<a href="https://learn.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>  
<a href="https://learn.microsoft.com/dotnet/api/system.componentmodel.descriptionattribute" target="_blank" rel="noopener noreferrer">DescriptionAttribute</a> value (or null, if the attribute is not applied).

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="https://learn.microsoft.com/dotnet/api/system.enum" target="_blank" rel="noopener noreferrer">Enum</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">

Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">

Extension Methods (C# Programming Guide)</a>.

## Example


**C#**  
``` C#
private enum TestEnum
{
    [Description("Description of value 1.")]
    Value1,

    [Description("Description of value 2.")]
    Value2,
} 

public void Enum_ToAbbreviation()
{
    TestEnum value = TestEnum.Value1;

    string? description = value.ToDescription();

    Assert.Equal("Description of value 1.", description);
}
```


## See Also


#### Reference
<a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions Class</a>  
<a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions Namespace</a>  
<a href="https://learn.microsoft.com/dotnet/api/system.componentmodel.descriptionattribute" target="_blank" rel="noopener noreferrer">DescriptionAttribute</a>  
