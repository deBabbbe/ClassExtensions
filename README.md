[![.NET Core Desktop](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml)

# Extensions for C# classes

### string ToRandomCase(string text)

Returns the passed string in random case

### IsNull()

Returns true, if the passed object is null, otherwise false

### IsNotNull()

Returns true, if the passed object is not null, otherwise false

### ForAll(Action<T> action)

Executes action for each element. No exception, if enumeration or action is null

### Times(int count, Action<int> action)

Executes action n times. No exception, if action is null

### EqualJsonCheck(object toCompare)

Compares two objects by serializing them to json string

## ExpEnv(string name)

Shortcut for Environment.ExpandEnvironmentVariables
