# PolyVase
## A short command line program to procedurally create low poly vases for 3D-printing.

## Requirements
- .NET 7 runtime (earlier versions *might* work)
- for building from source: c# languge.

## "Installation"
The project is built as a self-contained executable wich can be run directly.
Optinally it is possible to set up the directory of the executable file with the ```PATH``` environment variable. This allows the Application to be run from anywhere, without having to specify the whole path.

1. Download a pre built executable, from the releases. The following versions are currently provided.
    - 64 Bit Windows
    - 32 Bit Windows
    - 64 Bit Linux
3. Build directly from source.

## Usage
1. Run the Executable with ```PolyVase``` (full or relative path necassary if ```PATH``` is not set up).
2. The Model Parameters are can be specified directly via command line Paramters, otherwise the user will be promted to enter them.

    | Parameter | Command Line Argument | Valid Inputs |
    | --- | --- | --- |
    | seed | ```-seed``` | any string |
    | height | ```-height``` | any decimal number |
    | maximum diameter | ```-max-diameter```  | any decimal number |
    | minimum diameter | ```-min-diameter``` | any decimal number |
    | number of cuts | ```-cuts```  | any integer greater or equal to 0 |
    | number of facets | ```-facets``` | any integer greater or equal to 3 |
    | angle range | ```-angle-range``` | decimal numbers between 0 and 1 |
    | file save location | ```-out``` | valid file parths |

3. The save location for the output *.stl -file

## Build with dotnet cli
0. navigate to the root directory of the project.
1. Restore external references with the```dotnet restore``` command.
2. Build in Release config with ```dotnet publish --sc -c Release -r <runtime> -o <output directory>```
    - ```<runtime>``` needs to be replaced with the correct runtime-identifier (see: https://learn.microsoft.com/en-us/dotnet/core/rid-catalog)
        - win-x64 for 64 bit windows
        - win-x86 for 32 bit (x86)
        - linux-x64 for 64 bit linux
    - ```<output directory>``` needs to be replaced with the chosen path for the chosen output folder

