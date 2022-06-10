# C->Cpp->C# Integration with Pointer to Array
This is a simple example showing how to call C function in C# by wrapping C function into a C++ .dll.
C# -> c++.dll -> C.lib

## Step 1. Build C functions and construct lib
### Step 1.1
New a C project (named with "Engine")
### Step 1.2
Add engine.c and engine.h to project.
### Step 1.3
Add following code to all .h files so that the files can be compiled with C compiler instead of C++ compiler:
```
#ifdef __cplusplus
extern "C" {
#endif

    // your code

#ifdef __cplusplus
}
#endif
```
### Step 1.4
Declare and define functions in .c and .h files.
### Step 1.5
Modify:
Project "Property" -> Set "Configuration Type" to .lib
Project "Property" -> "Advanced" -> Set target extension to .lib
### Step 1.6
Ctrl+B to build. Don' t use F5 to run because project type is not .exe



## Step 2. Build C++ Wrapper for C functions and construct .dll
### Step 2.1
New a C++ project (named with "Engine_cpp")
### Step 2.2
Add engine_cpp.cpp and engine_cpp.h to project.
### Step 2.3
Add following code to engine_cpp.h files:
```
#ifdef ENGINE_EXPORTS
#define ENGINE_API __declspec(dllexport)
    
    // functions
    // __declspec(dllexport) tells the linker that you want this object
    // to be made available for other DLL's to import. It is used when
    // creating a DLL that others can link to.

#else
#define ENGINE_EXPORTS __declspec(dllimport)
    
    // functions
    //__declspec(dllimport) imports the implementation from a DLL so your application can use it.
    
#endif

```
### Step 2.4
Modify:
Configuration Properties > C/C++ -> Preprocessor property -> Add "ENGINE_EXPORTS" into Preprocessor Definitions.
By this, functions in ```ENGINE_API __declspec(dllexport)``` will be processed.
### Step 2.5
Declare cpp wrapper functions for corresponding functions in C project in .h file
Add ```extern "C" ENGINE_API``` before each wrapper functions:
```
extern "C" ENGINE_API your_function_declaration
```
### Step 2.6
Define wrapper functions in .cpp file. In your wrapper function, you can call functions in C project.
Note that you should: Add ```#include "../Engine/engine.h"``` to directly include src code of C project, or modify project properties so that you can use .lib file: 
1. Properties -> Configuration Properties -> Linker.
Linker -> Input. Add the actual library files under Additional Dependencies.
2. For the Header Files you'll also want to include their directories under C/C++ -> Additional Include Directories.
3. If there is a dll have a copy of it in your main project folder, and done.

You can use macro like ``` $(SolutionDir)$(Platform)\$(Configuration) ```

### Step 2.7
Modify:
Project "Property" -> Set "Configuration Type" to .dll
Project "Property" -> "Advanced" -> Set target extension to .dll
### Step 2.8
Ctrl+B to build. Don' t use F5 to run because project type is not .exe

### Note:
You can use struct to wrap all function arguments passed or return, but be careful that you should define the same struct  in C# file before using it.


## Step 3. Use C++ .dll in C# Projects
### Step 3.1
The DLL must be present at all times - as the name indicates..
You should copy .dll file to C#.exe directory (e.g. C#project/bin/x64/Debug)
Or "Specify build events" (https://docs.microsoft.com/en-us/visualstudio/ide/how-to-specify-build-events-csharp?view=vs-2022) 

You can use macro like 
```
copy /Y "$(SolutionDir)$(Platform)\$(ConfigurationName)\Engine_CPP.dll" "$(ProjectDir)\bin\$(Platform)\$(ConfigurationName)\Engine_CPP.dll"
```
### Step 3.2
Use “unsafe” modifier to declare a class or any member of it to make entire class or member considered as unsafe:
```
 [DllImport("Engine_CPP.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, ExactSpelling = true)]
      unsafe public extern static double* AddTwoArrayWIthLenth3_CPP(double* a, double* b, double** sum);
```
(You would be asked to assign /unsafe by VS IDE)

### Step 3.3
Enjoy