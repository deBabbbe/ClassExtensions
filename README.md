[![.NET Core Desktop](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml)

# Unit test helper for C#

### int GenerateRandomInt()

Creates a random integer between 1 and 9

### int GenerateRandomInt(int min, int max)

Creates a random integer between min and max

### string GenerateRandomString()

Creates a random string with chars from A-z with 10 chars

### string GenerateRandomString(int numberOfCharacters)

Creates a random string with chars from A-z with passed numberOfCharacters

### List\<T\> GenerateRandomList\<T\>(Func\<T\> Generator, int numberOfElements)

Creates a random list of given type with elements from the generator

### bool GenerateRandomBool()

Creates a random bool

### DateTime GetGenerateRandomDateTime()

Creates a random DateTime between 1870 and 2300

### string GenerateRandomStringGuidWithPrefix(string prefix)

Returns a GUID as string with the given prefix
