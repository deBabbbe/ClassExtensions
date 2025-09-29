# 🚀 ClassExtensions for C#

[![.NET Core Desktop](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/dotnet-desktop.yml)
[![CodeQL](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codeql.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codeql.yml)
[![Codacy Security Scan](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codacy.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/codacy.yml)
[![SecurityCodeScan](https://github.com/deBabbbe/ClassExtensions/actions/workflows/securitycodescan.yml/badge.svg)](https://github.com/deBabbbe/ClassExtensions/actions/workflows/securitycodescan.yml)

A set of **handy C# extension methods** for objects, collections, strings, and streams to make your code **cleaner and more expressive**.

---

## 🟢 Null Checks

- `bool IsNull(this object obj)`  
  ✅ Returns `true` if the object is `null`.

- `bool IsNotNull(this object obj)`  
  ✅ Returns `true` if the object is **not null**.

---

## 📦 Collection Utilities

- `void ForAll(this IEnumerable<T> source, Action<T> action)`  
  Executes `action` for each element. Safe if `null`.

- `void Times(this int count, Action<int> action)`  
  Executes `action` `count` times. Safe if `null`.

- `bool IsEmpty(this IEnumerable value)` – Checks if empty  
- `bool IsNotEmpty(this IEnumerable value)` – Checks if not empty  
- `bool IsNullOrEmpty(this IEnumerable value)` – Checks null or empty  
- `bool IsNotNullOrEmpty(this IEnumerable value)` – Checks not null and not empty

- `T Pop<T>(this IList<T> source)` – Returns **last element** and removes it  
- `T Shift<T>(this IList<T> source)` – Returns **first element** and removes it  
- `void Unshift<T>(this IList<T> source, T toAdd)` – Adds element at **first position**  

- `T AnyOne<T>(this IEnumerable<T> source)` – Returns **any element** (throws if null/empty)  
- `bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)` – Checks if **no elements match** the predicate  

---

## 🔍 Object Comparison

- `void EqualJsonCheck(this object obj, object toCompare)`  
  Compares objects via **JSON serialization** and throws if unequal.

- `bool IsEqualJson(this object obj, object toCompare)`  
  Returns `true` if objects are equal via JSON.

---

## ✏️ String Utilities

- `string ExpEnv(this string text)` – Shortcut for `Environment.ExpandEnvironmentVariables`  
- `string UseFormat(this string text, params string[] @params)` – Uses `string.Format` for placeholders

---

## 📄 Stream & Byte Array

- `byte[] ToByteArray(this Stream stream)` – Converts a **Stream** to a **byte array**  
- `Stream ToStream(this byte[] byteArray)` – Converts a **byte array** to a **Stream**