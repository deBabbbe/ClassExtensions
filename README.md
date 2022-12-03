[![.NET Core Desktop](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml)

# Extensions for C# classes

### string ToRandomCase(this string text)

Returns the passed string in random case

### IsNull(this object)

Returns true, if the passed object is null, otherwise false

### IsNotNull(this object)

Returns true, if the passed object is not null, otherwise false

### ForAll(this IEnumerable<T> src, Action<T> action)

Executes action for each element. No exception, if enumeration or action is null

### Times(this int count, Action<int> action)

Executes action n times. No exception, if action is null

### EqualJsonCheck(this object, object toCompare)

Compares two objects by serializing them to json string

### ExpEnv(this string)

Shortcut for Environment.ExpandEnvironmentVariables

### IsEmpty(this string)

Checks if a string is empty

### IsNotEmpty(this string)

Checks if a string is not empty

### IsNullOrEmpty(this string)

Checks if a string is null or empty

### IsNotNullOrEmpty(this string)

Checks if a string is not null or empty
