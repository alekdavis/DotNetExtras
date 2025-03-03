# Extensions Class


Implements extension methods.



## Definition
**Namespace:** <a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions</a>  
**Assembly:** DotNetExtras.Common (in DotNetExtras.Common.dll) Version: 0.0.0+ad9e09e88ea1e6582dceda1560895fc6af63de46

**C#**
``` C#
public static class Extensions
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  Extensions</td></tr>
</table>



## Methods
<table>
<tr>
<td><a href="b670c279-23ad-0b63-12e2-996cadcfd71f.md">Clone(Object)</a></td>
<td>Creates a deep copy of an object.</td></tr>
<tr>
<td><a href="7a3633f3-a182-72a7-b0b3-4566d30d804d.md">Clone(T)(T)</a></td>
<td>Creates a deep copy of an object.</td></tr>
<tr>
<td><a href="a9188e45-bce9-8c71-9c09-27606a2b52c4.md">Count</a></td>
<td>Returns number of items in any collection type.</td></tr>
<tr>
<td><a href="4811b0d8-583e-a109-2721-1ebbaac3cb07.md">Escape</a></td>
<td>Escapes specific characters in a string.</td></tr>
<tr>
<td><a href="8dfe9854-06f5-3465-1e54-d432225bdb1e.md">FromJson(T)</a></td>
<td>Converts a JSON string to an object.</td></tr>
<tr>
<td><a href="a98f97ba-c45d-e9a3-1ac9-a9e7845de3ed.md">GetAttribute(T)</a></td>
<td>Gets the value of an attribute applied to an enum data type.</td></tr>
<tr>
<td><a href="599e277e-2deb-fbd0-8859-23e8e4cf4c87.md">GetMessages(Exception, Boolean)</a></td>
<td>Gets messages from the immediate and all inner exceptions.</td></tr>
<tr>
<td><a href="58468440-92e7-60d8-7b03-70cbe892cc74.md">GetMessages(T)(Exception, Boolean)</a></td>
<td>Returns messages from the immediate and inner exceptions derived from the specified type.</td></tr>
<tr>
<td><a href="df82af8f-4bd6-cfbf-3c15-5ff6c82ca1e5.md">GetPropertyValue</a></td>
<td>Returns the value of the immediate or nested object property.</td></tr>
<tr>
<td><a href="ecc8f2bd-2191-f5a9-25fd-ad511790cb69.md">GetSafeMessages</a></td>
<td>Gets messages from the immediate and all inner exceptions.</td></tr>
<tr>
<td><a href="ec3ea06d-87dd-491e-f97b-cd513ba606d6.md">IsEmpty</a></td>
<td>Determines whether the specified object has no properties or fields holding non-null values or non-empty collections.</td></tr>
<tr>
<td><a href="30c10c95-ab06-425c-c215-0588373c6ce5.md">IsEquivalentTo</a></td>
<td>Checks if the source object is identical to the target (comparing all instance properties and fields).</td></tr>
<tr>
<td><a href="28c67f1e-4163-8282-995b-fa445f391473.md">IsPrimitive</a></td>
<td>Determine whether the specified type is a primitive type.</td></tr>
<tr>
<td><a href="1a482ca3-185d-be16-85a9-bdf5fcf1f286.md">IsSimple</a></td>
<td>Determine whether the specified type is simple (i.e. enum, string, number, GUID, date, time, offset, etc.) or complex (i.e. custom class with public properties and methods, list, array, etc.).</td></tr>
<tr>
<td><a href="f55887d7-00a2-0d06-c7d5-07155bd46b59.md">RemoveMatching</a></td>
<td>Removes all items in the list that match the values of the specified properties in the provided item.</td></tr>
<tr>
<td><a href="e2617e0b-3764-f767-1ccc-fc47d7b49e71.md">SetPropertyValue</a></td>
<td>Sets the new value of an immediate or nested object property (creating parent properties if needed).</td></tr>
<tr>
<td><a href="d32d4613-6034-4527-605e-7e2e9422b89b.md">ToAbbreviation</a></td>
<td>Returns the value of the <a href="d9421df2-3b9f-ea28-4ec3-d94a59b92905.md">AbbreviationAttribute</a> applied to an enumerated field.</td></tr>
<tr>
<td><a href="4a0a6194-7653-c55c-4c63-eb65975e56e8.md">ToArray(T)</a></td>
<td>Converts string to array.</td></tr>
<tr>
<td><a href="3bfdc925-3507-2984-e948-3c44efa3a2d0.md">ToCsv(T)</a></td>
<td>Converts a collection of generic elements to a comma-separated string value.</td></tr>
<tr>
<td><a href="71f33ed9-375d-58de-b501-92e7447e14d8.md">ToDateTime</a></td>
<td>Converts a string to a <a href="https://learn.microsoft.com/dotnet/api/system.datetime" target="_blank" rel="noopener noreferrer">DateTime</a> value.</td></tr>
<tr>
<td><a href="f45b9f94-269a-b13c-2ec2-a119cf1068c2.md">ToDateTimeOffset</a></td>
<td>Converts a string to a <a href="https://learn.microsoft.com/dotnet/api/system.datetimeoffset" target="_blank" rel="noopener noreferrer">DateTimeOffset</a> value.</td></tr>
<tr>
<td><a href="ffead708-14b9-1adf-dfe3-ce1a20705c5b.md">ToDescription</a></td>
<td>Returns the value of the <a href="https://learn.microsoft.com/dotnet/api/system.componentmodel.descriptionattribute" target="_blank" rel="noopener noreferrer">DescriptionAttribute</a> applied to an enumerated field.</td></tr>
<tr>
<td><a href="37685bf7-9c08-6709-a6fe-373b972684ef.md">ToDictionary(TKey, TValue)</a></td>
<td>Converts string to a dictionary.</td></tr>
<tr>
<td><a href="4e82274c-2d4a-970e-2bce-1d724d8dfcca.md">ToDynamic(Dictionary(String, Object))</a></td>
<td>Converts a string dictionary object to a dynamic object.</td></tr>
<tr>
<td><a href="f3fec2ba-756b-c65a-d513-1c2579556a57.md">ToDynamic(T)(T, Dictionary(String, Object), Boolean)</a></td>
<td>Converts any object to a dynamic object.</td></tr>
<tr>
<td><a href="67e9d56b-0488-fe41-7992-0bb428247c26.md">ToHashSet(T)</a></td>
<td>Converts string to a hash set.</td></tr>
<tr>
<td><a href="0278d89f-1de4-6904-8560-4a3ee7660a12.md">ToHResult</a></td>
<td>Converts negative integer value to properly formatted HResult value.</td></tr>
<tr>
<td><a href="21d017d8-9be3-e598-aaad-ea36c9a014a4.md">ToJson</a></td>
<td>Converts an object to a JSON string.</td></tr>
<tr>
<td><a href="9c89f18e-afb0-67e1-9082-d797c5a81e4f.md">ToList(T)</a></td>
<td>Converts string to a list.</td></tr>
<tr>
<td><a href="29ed6db8-5577-cf0b-7dbe-a6b0369f9def.md">ToSentence</a></td>
<td>Appends a period at the end of the string, unless it already ends with one of the punctuation characters: ,.!?;:</td></tr>
<tr>
<td><a href="9a3b8ec9-ef6b-1e2b-ffc7-996eaf8319b7.md">ToShortName</a></td>
<td>Returns the value of the <a href="65f3ee8b-df08-b710-3243-c74f2f588652.md">ShortNameAttribute</a> applied to an enumerated field.</td></tr>
<tr>
<td><a href="1fa71cf5-928b-dc8c-aeaf-e6bc4ebe7041.md">ToType(T)</a></td>
<td>Converts a string value to the specified type.</td></tr>
</table>

## See Also


#### Reference
<a href="9184e3b0-90b9-a3bc-0ea0-71d3642c662f.md">DotNetExtras.Common.Extensions Namespace</a>  
