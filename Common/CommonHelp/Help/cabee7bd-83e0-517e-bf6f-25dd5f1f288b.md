# RegexString Class


Implements common regular expressions.



## Definition
**Namespace:** <a href="10b5cf0d-dc51-2132-aa59-84b581d400eb.md">DotNetExtras.Common.Text</a>  
**Assembly:** DotNetExtras.Common (in DotNetExtras.Common.dll) Version: 0.0.0+ad9e09e88ea1e6582dceda1560895fc6af63de46

**C#**
``` C#
public class RegexString
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  RegexString</td></tr>
</table>



## Remarks
This class is not declared as static because a non-static class can be extended, so you can access both these regular expressions and your custom regular expressions from a derived class.

## Constructors
<table>
<tr>
<td><a href="4ecbdc3a-d314-3ed9-cddd-ac613a286f84.md">RegexString</a></td>
<td>Initializes a new instance of the RegexString class</td></tr>
</table>

## Methods
<table>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.equals#system-object-equals(system-object)" target="_blank" rel="noopener noreferrer">Equals</a></td>
<td>Determines whether the specified object is equal to the current object.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.finalize" target="_blank" rel="noopener noreferrer">Finalize</a></td>
<td>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.gethashcode" target="_blank" rel="noopener noreferrer">GetHashCode</a></td>
<td>Serves as the default hash function.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.gettype" target="_blank" rel="noopener noreferrer">GetType</a></td>
<td>Gets the <a href="https://learn.microsoft.com/dotnet/api/system.type" target="_blank" rel="noopener noreferrer">Type</a> of the current instance.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone" target="_blank" rel="noopener noreferrer">MemberwiseClone</a></td>
<td>Creates a shallow copy of the current <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
<tr>
<td><a href="https://learn.microsoft.com/dotnet/api/system.object.tostring" target="_blank" rel="noopener noreferrer">ToString</a></td>
<td>Returns a string that represents the current object.<br />(Inherited from <a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>)</td></tr>
</table>

## Fields
<table>
<tr>
<td><a href="b97d7d87-6f35-7fed-4906-c1b71395a320.md">EmailAddress</a></td>
<td>A simplified regular expression for validating Azure-compliant email addresses: (?=^.{5,64}$)^[a-z0-9!#$%&amp;'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$</td></tr>
<tr>
<td><a href="9b344b57-633f-850b-1a9e-cd12c94635ca.md">Guid</a></td>
<td>A regular expression for validating GUID/UUID values: ^[({]?[a-fA-F0-9]{8}[-]?([a-fA-F0-9]{4}[-]?){3}[a-fA-F0-9]{12}[})]?$</td></tr>
</table>

## Extension Methods
<table>
<tr>
<td><a href="b670c279-23ad-0b63-12e2-996cadcfd71f.md">Clone</a></td>
<td>Creates a deep copy of an object.<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
<tr>
<td><a href="7f956b7b-024d-5d38-c09f-7207e9d91ca3.md">Full</a></td>
<td>Returns full name of the object, class, type, or property.<br />(Defined by <a href="4d9b8d97-0e02-2be7-3992-328efcc7d771.md">NameOf</a>)</td></tr>
<tr>
<td><a href="df82af8f-4bd6-cfbf-3c15-5ff6c82ca1e5.md">GetPropertyValue</a></td>
<td>Returns the value of the immediate or nested object property.<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
<tr>
<td><a href="ec3ea06d-87dd-491e-f97b-cd513ba606d6.md">IsEmpty</a></td>
<td>Determines whether the specified object has no properties or fields holding non-null values or non-empty collections.<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
<tr>
<td><a href="30c10c95-ab06-425c-c215-0588373c6ce5.md">IsEquivalentTo</a></td>
<td>Checks if the source object is identical to the target (comparing all instance properties and fields).<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
<tr>
<td><a href="83cd6be1-35a7-9e12-9d87-5a9d342c8efa.md">Keep</a></td>
<td>Returns a shortened name of the object, class, type, or property keeping the specified number of compound prefixes or suffixes.<br />(Defined by <a href="4d9b8d97-0e02-2be7-3992-328efcc7d771.md">NameOf</a>)</td></tr>
<tr>
<td><a href="68206f70-4fba-59f9-c83b-d3a46bcebf70.md">Long</a></td>
<td>Returns the partial name of the object, class, type, or property omitting the entry before the dot (counting from the left).<br />(Defined by <a href="4d9b8d97-0e02-2be7-3992-328efcc7d771.md">NameOf</a>)</td></tr>
<tr>
<td><a href="e2617e0b-3764-f767-1ccc-fc47d7b49e71.md">SetPropertyValue</a></td>
<td>Sets the new value of an immediate or nested object property (creating parent properties if needed).<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
<tr>
<td><a href="78e73933-ba5e-cf81-0743-df14426d6bd7.md">Short</a></td>
<td>Returns the short name of the immediate object property (same as nameof()).<br />(Defined by <a href="4d9b8d97-0e02-2be7-3992-328efcc7d771.md">NameOf</a>)</td></tr>
<tr>
<td><a href="75b5bb8f-7259-1d16-23f9-899b9022fb3c.md">Skip</a></td>
<td>Returns a shortened name of the object, class, type, or property after removing the specified number of compound prefixes or suffixes.<br />(Defined by <a href="4d9b8d97-0e02-2be7-3992-328efcc7d771.md">NameOf</a>)</td></tr>
<tr>
<td><a href="21d017d8-9be3-e598-aaad-ea36c9a014a4.md">ToJson</a></td>
<td>Converts an object to a JSON string.<br />(Defined by <a href="cd9aff4b-4a32-a8a4-5f57-e5fc9dbf4b67.md">Extensions</a>)</td></tr>
</table>

## See Also


#### Reference
<a href="10b5cf0d-dc51-2132-aa59-84b581d400eb.md">DotNetExtras.Common.Text Namespace</a>  
