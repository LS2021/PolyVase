# PolyVase

A short command line program to procedurally create low poly vases for 3D-printing.


### Requirements
- .NET 7 runtime
- for building from source: c# languge.
## Build with dotnet cli
0. navigate to the root directory of the project.
1. Restore external references with the```dotnet restore``` command.
2. Build in Release config with ```dotnet publish --sc -c Release -r <runtime> -o <output directory>```
    - ```<runtime>``` needs to be replaced with the correct runtime-identifier (see: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog)
        - win-x64 for 64 bit windows
        - win-x86 for 32 bit (x86)
        - linux-x64 for 64 bit linux
    - ```<output directory>``` needs to be replaced with the chosen path for the chosen output folder 