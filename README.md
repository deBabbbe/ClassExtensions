[![.NET Core Desktop](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml)

[![CodeQL](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codeql.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codeql.yml)

# Extensions for C# classes

### bool IsNull(this object)

Returns true, if the passed object is null, otherwise false

### bool IsNotNull(this object)

Returns true, if the passed object is not null, otherwise false

### void ForAll(this IEnumerable<T> src, Action<T> action)

Executes action for each element. No exception, if enumeration or action is null

### void Times(this int count, Action<int> action)

Executes action n times. No exception, if action is null

### void EqualJsonCheck(this object, object toCompare)

Compares two objects by serializing them to json string and throws exception if not

### bool IsEqualJson(this object, object toCompare)

Compares two objects by serializing them to json string and returns bool

### string ExpEnv(this string)

Shortcut for Environment.ExpandEnvironmentVariables

### bool IsEmpty(this IEnumerable value)

Checks if a IEnumerable is empty

### bool IsNotEmpty(this IEnumerable value)

Checks if a IEnumerable is not empty

### bool IsNullOrEmpty(this IEnumerable value)

Checks if a IEnumerable is null or empty

### bool IsNotNullOrEmpty(this IEnumerable value)

Checks if a IEnumerable is not null or empty

### string UseFormat(this string text, params string[] @params)

Uses string.Format to replace placeholder values
