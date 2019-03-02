# CArray

C array like type implemented in C#.
It was implemented to check new features in C# 7 and was never ran in production.

# How to use

```
using CArray;

...

var array = new CArray<int>(10);
array[3] = new Random.Next();

Console.WriteLine(array[3]);
Console.WriteLine(array.Length);

array.Dispose();

...
/* Output
3
10
*/
```
Could be used with pointers

```
var array = new CArray<int>(10);
int* ptr = aray.GetPointer(3);
*ptr = 10

Console.WriteLine(array[3]);

array.Dispose();

...

/* Output
10
*/
```

It's possible to initialize and Convert to a managed array

```
var managedArray = new[] { 1, 2, 3 };

//Initialize from C# array
var cArray = CArray<int>.FromArray(managedArray);

//Back to C# array
var newManagedArray = cArray.ToArray();
```

# Warnings

* It's necessary to call the ```Dispose()``` method after use the array to free memory
* CArray it is implemented as ref struct and cannot be be saved on the heap
* CArray it's not thread safety
* CArray not implements interfaces, since it is a ref struct

# TODO

* Separate Write and Read benchmarks, how to setup ref structs?
